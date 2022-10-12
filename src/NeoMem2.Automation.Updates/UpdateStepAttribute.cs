// <copyright file="UpdateStepAttribute.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System;

    /// <summary>
    /// An attribute for marking a class as being an update step.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class UpdateStepAttribute : Attribute
    {
        /// <summary>
        /// The backing field for the <see cref="Component"/> property.
        /// </summary>
        private readonly string component;

        /// <summary>
        /// The backing field for the <see cref="Description"/> property.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// The backing field for the <see cref="Step"/> property.
        /// </summary>
        private readonly int step;

        /// <summary>
        /// The backing field for the <see cref="Version"/> property.
        /// </summary>
        private readonly double version;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStepAttribute" /> class.
        /// </summary>
        /// <param name="component">The name of the component that this step is for.</param>
        /// <param name="version">The component version that this step is for.</param>
        /// <param name="step">The step index.</param>
        /// <param name="description">The step description.</param>
        public UpdateStepAttribute(string component, double version, int step, string description)
        {
            this.component = component;
            this.version = version;
            this.step = step;
            this.description = description;
        }

        /// <summary>
        /// Gets the name of the component that this step is for.
        /// </summary>
        public string Component
        {
            get { return this.component; }
        }

        /// <summary>
        /// Gets the step description.
        /// </summary>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// Gets the step index.
        /// </summary>
        public int Step
        {
            get { return this.step; }
        }

        /// <summary>
        /// Gets the component version that this step is for.
        /// </summary>
        public double Version
        {
            get { return this.version; }
        }
    }
}
