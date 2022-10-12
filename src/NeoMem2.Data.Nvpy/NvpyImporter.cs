using NeoMem2.Core;
using NeoMem2.Core.Stores;

namespace NeoMem2.Data.Nvpy
{
    public class NvpyImporter : IImporter
    {
        public string ConnectionString { get; set; }

        public NeoMemFile Read()
        {
            var store = new NvpyStore(ConnectionString);

            var file = new NeoMemFile();
            file.AllNotes.AddRange(store.GetNotes().GetNotes());
            return file;
        }
    }
}
