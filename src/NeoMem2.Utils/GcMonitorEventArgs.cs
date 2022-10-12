// <copyright file="GcMonitorEventArgs.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;
    using System.Collections.Generic;

    using NeoMem2.Utils.IO;

    /// <summary>
    /// An event arguments class containing garbage collection details.
    /// </summary>
    public class GcMonitorEventArgs : EventArgs
    {
        /// <summary>
        /// The backing field for the <see cref="CustomProperties"/> property.
        /// </summary>
        private readonly Dictionary<string, float> customProperties = new Dictionary<string, float>();

        /// <summary>
        /// The backing field for the <see cref="Generations"/> property.
        /// </summary>
        private readonly Dictionary<int, GcMonitorGenerationDetails> generations = new Dictionary<int, GcMonitorGenerationDetails>();

        /// <summary>
        /// Gets the custom properties.
        /// </summary>
        public Dictionary<string, float> CustomProperties
        {
            get { return this.customProperties; }
        }

        /// <summary>
        /// Gets the generation details.
        /// </summary>
        public Dictionary<int, GcMonitorGenerationDetails> Generations
        {
            get { return this.generations; }
        }

        /// <summary>
        /// Gets or sets the time that the details were generated.
        /// </summary>
        public DateTime TimeGenerated { get; set; }

        /// <summary>
        /// Gets or sets the total memory at the time that the details were generated.
        /// </summary>
        public DataSize TotalMemory { get; set; }
    }
}