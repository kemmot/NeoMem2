// <copyright file="IExporter.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace NeoMem2.Core.Stores
{
    /// <summary>
    /// The interface that must be implemented to provide note exporting functionality.
    /// </summary>
    public interface IExporter
    {
        event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Gets or sets the connection string to export to.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Exports the specified notes.
        /// </summary>
        /// <param name="file">The notes to export.</param>
        void Export(NeoMemFile file);
    }
}
