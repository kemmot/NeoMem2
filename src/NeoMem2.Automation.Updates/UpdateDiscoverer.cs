// <copyright file="UpdateDiscoverer.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Discovers any component version updates that need to be applied to work with a version of software.
    /// </summary>
    public class UpdateDiscoverer
    {
        /// <summary>
        /// Discovers updates for the specified component and version.
        /// </summary>
        /// <param name="componentName">The component to consider; leave empty for all components.</param>
        /// <param name="currentVersion">The target version.</param>
        /// <returns>The component updates.</returns>
        public List<ComponentInfo> Discover(string componentName = "", double currentVersion = 0)
        {
            return this.Discover(componentName, currentVersion, AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Discovers updates for the specified component and version.
        /// </summary>
        /// <param name="componentName">The component to consider; leave empty for all components.</param>
        /// <param name="currentVersion">The target version.</param>
        /// <param name="assemblies">The assemblies to inspect.</param>
        /// <returns>The component updates.</returns>
        public List<ComponentInfo> Discover(string componentName = "", double currentVersion = 0, params Assembly[] assemblies)
        {
            IEnumerable<ComponentInfo> allComponents = this.GetAllComponents(assemblies);

            List<ComponentInfo> filteredComponents = new List<ComponentInfo>();
            foreach (var component in allComponents)
            {
                if (string.IsNullOrEmpty(componentName) || component.Name == componentName)
                {
                    for (int versionIndex = component.Versions.Count - 1; versionIndex >= 0; versionIndex--)
                    {
                        if (component.Versions[versionIndex].Version <= currentVersion)
                        {
                            component.Versions.RemoveAt(versionIndex);
                        }
                    }

                    if (component.Versions.Count > 0)
                    {
                        component.Versions.Sort((left, right) => left.Version.CompareTo(right.Version));
                        foreach (var version in component.Versions)
                        {
                            version.Steps.Sort((left, right) => left.StepIndex.CompareTo(right.StepIndex));
                        }

                        filteredComponents.Add(component);
                    }
                }
            }

            return filteredComponents;
        }

        /// <summary>
        /// Gets all components that have updates in the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to inspect.</param>
        /// <returns>The available components.</returns>
        private IEnumerable<ComponentInfo> GetAllComponents(IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Step> steps = this.GetSteps(assemblies);

            var allComponents = new Dictionary<string, ComponentInfo>();
            foreach (var step in steps)
            {
                ComponentInfo component;
                if (!allComponents.TryGetValue(step.Component, out component))
                {
                    component = new ComponentInfo(step.Component);
                    allComponents.Add(step.Component, component);
                }

                VersionInfo foundVersion = component.Versions.FirstOrDefault(version => version.Version == step.Version);
                if (foundVersion == null)
                {
                    foundVersion = new VersionInfo(step.Version);
                    component.Versions.Add(foundVersion);
                }

                foundVersion.Steps.Add(new StepInfo(
                    step.StepIndex, 
                    step.Description, 
                    (IUpdateStep)Activator.CreateInstance(step.StepType)));
            }

            return new List<ComponentInfo>(allComponents.Values);
        }

        /// <summary>
        /// Gets all update steps from the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to inspect.</param>
        /// <returns>The available steps.</returns>
        private IEnumerable<Step> GetSteps(IEnumerable<Assembly> assemblies)
        {
            List<Step> steps = new List<Step>();
            foreach (var assembly in assemblies)
            {
                steps.AddRange(this.GetSteps(assembly));
            }

            return steps;
        }

        /// <summary>
        /// Gets all update steps from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>The available steps.</returns>
        private IEnumerable<Step> GetSteps(Assembly assembly)
        {
            List<Step> steps = new List<Step>();
            foreach (var type in assembly.GetTypes())
            {
                bool canAdd = type.IsClass;
                if (canAdd)
                {
                    canAdd = !type.IsAbstract;
                }

                if (canAdd)
                {
                    canAdd = !type.ContainsGenericParameters;
                }

                if (canAdd)
                {
                    canAdd = type.GetInterface(typeof(IUpdateStep).FullName) != null;
                }

                if (canAdd)
                {
                    canAdd = type.GetConstructor(Type.EmptyTypes) != null;
                }

                if (canAdd)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(UpdateStepAttribute), false);
                    if (attributes.Length > 0)
                    {
                        var attribute = (UpdateStepAttribute)attributes[0];
                        steps.Add(new Step
                            {
                                Component = attribute.Component,
                                Version = attribute.Version,
                                StepIndex = attribute.Step,
                                Description = attribute.Description,
                                StepType = type
                            });
                    }
                }
            }

            return steps;
        }
        
        /// <summary>
        /// A temporary class used in discovery.
        /// </summary>
        private class Step
        {
            /// <summary>
            /// Gets or sets the name of the component that the step is for.
            /// </summary>
            public string Component { get; set; }

            /// <summary>
            /// Gets or sets the version that the step is for.
            /// </summary>
            public double Version { get; set; }

            /// <summary>
            /// Gets or sets the step type.
            /// </summary>
            public Type StepType { get; set; }

            /// <summary>
            /// Gets or sets the step index.
            /// </summary>
            public int StepIndex { get; set; }

            /// <summary>
            /// Gets or sets the step description.
            /// </summary>
            public string Description { get; set; }
        }
    }
}
