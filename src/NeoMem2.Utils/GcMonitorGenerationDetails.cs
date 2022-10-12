// <copyright file="GcMonitorGenerationDetails.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The garbage collector details for a specific generation.
    /// </summary>
    public class GcMonitorGenerationDetails
    {
        /// <summary>
        /// The backing field for the <see cref="CustomProperties"/> property.
        /// </summary>
        private readonly Dictionary<string, float> customProperties = new Dictionary<string, float>();

        /// <summary>
        /// Gets the custom properties.
        /// </summary>
        public Dictionary<string, float> CustomProperties
        {
            get { return this.customProperties; }
        }

        /// <summary>
        /// Gets or sets the ID of the generation.
        /// </summary>
        public int GenerationId { get; set; }

        /// <summary>
        /// Gets or sets the collection count.
        /// </summary>
        public int CollectionCount { get; set; }

        /// <summary>
        /// Gets or sets the time that the last GC collection occurred.
        /// </summary>
        public DateTime LastCollectionTime { get; set; }
    }
}