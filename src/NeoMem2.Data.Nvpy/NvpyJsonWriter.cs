using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

using NeoMem2.Core;
using NeoMem2.Core.Stores;

namespace NeoMem2.Data.Nvpy
{
    public class NvpyJsonWriter : ExporterBase
    {
        private const double DefaultDate = 1374741003.198;
        

        public NvpyJsonWriter()
        {
            
        }

        public NvpyJsonWriter(string connectionString)
        {
            ConnectionString = connectionString;
        }

        
        public override void Export(NeoMemFile file)
        {
            foreach (Note note in file.AllNotes.GetNotes())
            {
                CreateNote(note);
            }
        }

        public void CreateNote(Note note)
        {
            CreateNote(note, GenerateNewNotePath(note));
        }

        public string GenerateNewNotePath(Note note)
        {
            string filename = GenerateNewNoteFilename(note);
            string folder = ConnectionString;
            return Path.Combine(folder, filename);
        }

        public string GenerateNewNoteFilename(Note note)
        {
            string filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".json";
            return Path.Combine(ConnectionString, filename);
        }

        public void CreateNote(Note note, string path)
        {
            NvpyNoteMarkup n;

            NvpyNote internalNote = note as NvpyNote;
            if (internalNote != null)
            {
                n = internalNote.InternalNote;
            }
            else
            {
                StringBuilder noteText = new StringBuilder();
                //noteText.AppendLine(note.Name);
                //noteText.AppendLine("Class: {0}", note.Class);
                //noteText.AppendLine("Description: {0}", note.Description);
                //foreach (var property in note.Properties)
                //{
                //    noteText.AppendLine("[{0}]{1}: {2}", property.DataType, property.Name, property.Value);
                //}
                noteText.Append(note.Text);

                n = new NvpyNoteMarkup
                    {
                        content = noteText.ToString(),
                        createdate = DefaultDate,
                        modifydate = DefaultDate,
                        savedate = DefaultDate,
                        syncdate = DefaultDate,
                        tags = new[] { note.Class.ToLower() }
                    };
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NvpyNoteMarkup));
            using (FileStream s = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ser.WriteObject(s, n);
            }
        }
    }
}
