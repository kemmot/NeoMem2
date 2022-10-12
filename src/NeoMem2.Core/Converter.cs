// <copyright file="Converter.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using NeoMem2.Core.Stores;

    /// <summary>
    /// Converts a note database from one format to another.
    /// </summary>
    public class Converter
    {
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Gets or sets the connection string to use for imports.
        /// </summary>
        public string ImportConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the connection string to use for exports.
        /// </summary>
        public string ExportConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the importer.
        /// </summary>
        public IImporter Importer { get; set; }

        /// <summary>
        /// Gets or sets the exporter.
        /// </summary>
        public IExporter Exporter { get; set; }

        /// <summary>
        /// Converts the database.
        /// </summary>
        /// <returns>Information regarding the conversion.</returns>
        public ConvertStats Convert()
        {
            this.Importer.ConnectionString = this.ImportConnectionString;
            NeoMemFile file = this.Importer.Read();

            this.Exporter.ProgressChanged += ExporterOnProgressChanged;
            this.Exporter.ConnectionString = this.ExportConnectionString;
            this.Exporter.Export(file);
            this.Exporter.ProgressChanged -= ExporterOnProgressChanged;

            return new ConvertStats { NoteCount = file.AllNotes.GetNotes().Count() };
        }

        private void ExporterOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var exportProgress = new ProgressChangedEventArgs(
                50 + (e.ProgressPercentage / 2),
                e.UserState);
            OnProgressChanged(exportProgress);
        }

        protected void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}
