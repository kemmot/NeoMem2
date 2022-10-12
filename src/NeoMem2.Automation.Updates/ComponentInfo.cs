// <copyright file="ComponentInfo.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System.Collections.Generic;

    /// <summary>
    /// The information related to an update component.
    /// </summary>
    public class ComponentInfo
    {
        /// <summary>
        /// The backing field for the <see cref="Versions"/> property.
        /// </summary>
        private readonly List<VersionInfo> versions = new List<VersionInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentInfo" /> class.
        /// </summary>
        public ComponentInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentInfo" /> class.
        /// </summary>
        /// <param name="name">The component name.</param>
        /// <param name="versions">The available component versions.</param>
        public ComponentInfo(string name, params VersionInfo[] versions)
        {
            this.Name = name;
            this.Versions.AddRange(versions);
        }

        /// <summary>
        /// Gets or sets the component name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the available component versions.
        /// </summary>
        public List<VersionInfo> Versions
        {
            get { return this.versions; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Component: {0}", this.Name);
        }
    }
}
