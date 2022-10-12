// <copyright file="UpdateManager.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System;
    using System.Collections.Generic;
    using System.Transactions;

    using NeoMem2.Utils;

    using NLog;

    /// <summary>
    /// Manages the execution of updates.
    /// </summary>
    public abstract class UpdateManager
    {
        /// <summary>
        /// The logger to use in this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The backing field for the <see cref="Context"/> property.
        /// </summary>
        private readonly UpdateContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateManager" /> class.
        /// </summary>
        /// <param name="context">The update context.</param>
        protected UpdateManager(UpdateContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
        }

        /// <summary>
        /// Fired when the component update is complete.
        /// </summary>
        public event EventHandler<ItemCompleteEventArgs<ComponentInfo>> ComponentComplete;

        /// <summary>
        /// Fired when the component update starts.
        /// </summary>
        public event EventHandler<ItemEventArgs<ComponentInfo>> ComponentStarted;

        /// <summary>
        /// Fired when a step update is complete.
        /// </summary>
        public event EventHandler<ItemCompleteEventArgs<StepInfo>> StepComplete;

        /// <summary>
        /// Fired when a step update starts.
        /// </summary>
        public event EventHandler<ItemEventArgs<StepInfo>> StepStarted;

        /// <summary>
        /// Fired when a version update is complete.
        /// </summary>
        public event EventHandler<ItemCompleteEventArgs<VersionInfo>> VersionComplete;

        /// <summary>
        /// Fired when a version update starts.
        /// </summary>
        public event EventHandler<ItemEventArgs<VersionInfo>> VersionStarted;

        /// <summary>
        /// Gets the update context.
        /// </summary>
        protected UpdateContext Context
        {
            get { return this.context; }
        }

        /// <summary>
        /// Updates a component.
        /// </summary>
        /// <param name="component">The component to update.</param>
        /// <returns>Statistics regarding the update.</returns>
        public UpdateStatistics Update(ComponentInfo component)
        {
            UpdateStatistics updateStatistics = new UpdateStatistics();

            this.OnComponentStarted(new ItemEventArgs<ComponentInfo>(component));
            try
            {
                double currentVersion = this.GetCurrentVersion();

                var sortedVersions = new List<VersionInfo>(component.Versions);
                sortedVersions.Sort((left, right) => left.Version.CompareTo(right.Version));

                foreach (var version in sortedVersions)
                {
                    if (version.Version > currentVersion)
                    {
                        this.Update(version, updateStatistics);
                    }
                    else
                    {
                        Logger.Info("Skipped update, current version: {0}, new version: {1}", currentVersion, version);
                    }
                }

                this.OnComponentComplete(new ItemCompleteEventArgs<ComponentInfo>(component));
            }
            catch (Exception ex)
            {
                this.OnComponentComplete(new ItemCompleteEventArgs<ComponentInfo>(component, ex));

                string message = string.Format(
                    "Failed to update: {0}",
                    component);
                throw new Exception(message, ex);
            }

            return updateStatistics;
        }

        /// <summary>
        /// Gets the current component version.
        /// </summary>
        /// <returns>The current component version.</returns>
        protected abstract double GetCurrentVersion();

        /// <summary>
        /// Fires the <see cref="ComponentComplete"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnComponentComplete(ItemCompleteEventArgs<ComponentInfo> e)
        {
            if (this.ComponentComplete != null)
            {
                this.ComponentComplete(this, e);
            }
        }

        /// <summary>
        /// Fires the <see cref="ComponentStarted"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnComponentStarted(ItemEventArgs<ComponentInfo> e)
        {
            if (this.ComponentStarted != null)
            {
                this.ComponentStarted(this, e);
            }
        }

        /// <summary>
        /// Fires the <see cref="StepComplete"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnStepComplete(ItemCompleteEventArgs<StepInfo> e)
        {
            if (this.StepComplete != null)
            {
                this.StepComplete(this, e);
            }
        }

        /// <summary>
        /// Fires the <see cref="StepStarted"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnStepStarted(ItemEventArgs<StepInfo> e)
        {
            if (this.StepStarted != null)
            {
                this.StepStarted(this, e);
            }
        }

        /// <summary>
        /// Fires the <see cref="VersionComplete"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnVersionComplete(ItemCompleteEventArgs<VersionInfo> e)
        {
            if (this.VersionComplete != null)
            {
                this.VersionComplete(this, e);
            }
        }

        /// <summary>
        /// Fires the <see cref="VersionStarted"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnVersionStarted(ItemEventArgs<VersionInfo> e)
        {
            if (this.VersionStarted != null)
            {
                this.VersionStarted(this, e);
            }
        }

        /// <summary>
        /// Updates to the specified version.
        /// </summary>
        /// <param name="version">The version to update to.</param>
        /// <param name="updateStatistics">Statistics regarding the update.</param>
        private void Update(VersionInfo version, UpdateStatistics updateStatistics)
        {
            this.OnVersionStarted(new ItemEventArgs<VersionInfo>(version));
            try
            {
                using (var scope = new TransactionScope())
                {
                    var sortedSteps = new List<StepInfo>(version.Steps);
                    sortedSteps.Sort((left, right) => left.StepIndex.CompareTo(right.StepIndex));

                    foreach (var step in sortedSteps)
                    {
                        this.Update(step, updateStatistics);
                    }

                    scope.Complete();
                }

                updateStatistics.VersionsUpdated++;
                Logger.Info("Update complete: {0}", version);
                this.OnVersionComplete(new ItemCompleteEventArgs<VersionInfo>(version));
            }
            catch (Exception ex)
            {
                this.OnVersionComplete(new ItemCompleteEventArgs<VersionInfo>(version, ex));

                string message = string.Format(
                    "Failed to update: {0}",
                    version);
                throw new Exception(message, ex);
            }
        }

        /// <summary>
        /// Updates to the specified step.
        /// </summary>
        /// <param name="step">The step to update to.</param>
        /// <param name="updateStatistics">Statistics regarding the update.</param>
        private void Update(StepInfo step, UpdateStatistics updateStatistics)
        {
            this.OnStepStarted(new ItemEventArgs<StepInfo>(step));
            try
            {
                step.Step.Context = this.Context;
                step.Step.Execute();
                updateStatistics.StepsUpdated++;
                this.OnStepComplete(new ItemCompleteEventArgs<StepInfo>(step));
            }
            catch (Exception ex)
            {
                this.OnStepComplete(new ItemCompleteEventArgs<StepInfo>(step, ex));

                string message = string.Format(
                    "Failed to update: {0}",
                    step);
                throw new Exception(message, ex);
            }
        }
    }
}
