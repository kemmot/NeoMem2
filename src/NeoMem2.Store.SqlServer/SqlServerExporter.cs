// <copyright file="SqlServerExporter.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer
{
    using NeoMem2.Core;
    using NeoMem2.Core.Stores;

    /// <summary>
    /// An implementation of <see cref="IExporter"/> that exports to the <see cref="SqlServerStore"/>.
    /// </summary>
    public class SqlServerExporter : ExporterBase
    {
        /// <summary>
        /// Exports the specified notes.
        /// </summary>
        /// <param name="file">The notes to export.</param>
        public override void Export(NeoMemFile file)
        {
            var store = new SqlServerStore(this.ConnectionString);

            foreach (var note in file.AllNotes.GetNotes())
            {
                note.Id = 0;
                store.Save(note);
            }
        }
    }
}
