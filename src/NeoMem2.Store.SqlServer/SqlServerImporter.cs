using NeoMem2.Core;
using NeoMem2.Core.Stores;

namespace NeoMem2.Data.SqlServer
{
    public class SqlServerImporter : IImporter
    {
        /// <summary>
        /// Gets or sets the connection string to import to.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Returns the imported notes.
        /// </summary>
        /// <returns>The imported notes.</returns>
        public NeoMemFile Read()
        {
            var store = new SqlServerStore(ConnectionString);

            var file = new NeoMemFile();
            foreach (var note in store.GetNotes().GetNotes())
            {
                file.AllNotes.Add(note);
            }

            return file;
        }
    }
}
