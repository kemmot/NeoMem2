// <copyright file="StepInfo.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    /// <summary>
    /// The information related to an update step.
    /// </summary>
    public class StepInfo
    {
        /// <summary>
        /// The backing field for the <see cref="Description"/> property.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// The backing field for the <see cref="Step"/> property.
        /// </summary>
        private readonly IUpdateStep step;

        /// <summary>
        /// The backing field for the <see cref="StepIndex"/> property.
        /// </summary>
        private readonly int stepIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepInfo" /> class.
        /// </summary>
        public StepInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StepInfo" /> class.
        /// </summary>
        /// <param name="stepIndex">The step index.</param>
        /// <param name="description">The step description.</param>
        /// <param name="step">The step object that will perform the work.</param>
        public StepInfo(int stepIndex, string description, IUpdateStep step)
        {
            this.stepIndex = stepIndex;
            this.description = description;
            this.step = step;
        }

        /// <summary>
        /// Gets the step description.
        /// </summary>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// Gets the step object that will perform the work.
        /// </summary>
        public IUpdateStep Step
        {
            get { return this.step; }
        }

        /// <summary>
        /// Gets the step index.
        /// </summary>
        public int StepIndex
        {
            get { return this.stepIndex; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Step {0}: {1}", this.StepIndex, this.Description);
        }
    }
}
