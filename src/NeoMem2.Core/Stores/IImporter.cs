// <copyright file="IImporter.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Stores
{
    /// <summary>
    /// The interface that must be implemented to provide note importing functionality.
    /// </summary>
    public interface IImporter
    {
        /// <summary>
        /// Gets or sets the connection string to import to.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Returns the imported notes.
        /// </summary>
        /// <returns>The imported notes.</returns>
        NeoMemFile Read();
    }
}
