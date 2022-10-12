using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

using NeoMem2.Automation.Updates;
using NeoMem2.Core;
using NeoMem2.Core.Stores;

namespace NeoMem2.Data.Nvpy
{
    public class NvpyStore : NeoMemStoreBase
    {
        private const string PropertyNameFilename = "NvpyStore.Filename";

        public static string GetDefaultConnectionString()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nvpy");
        }


        private readonly string m_Path;
        private readonly NvpyJsonWriter m_Writer;


        public NvpyStore(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (path.Length == 0) throw new ArgumentException("Argument cannot be zero length", "path");

            m_Path = path;
            m_Writer = new NvpyJsonWriter(m_Path);
        }


        public override void Backup(string backupFile)
        {
            throw new NotSupportedException("NVPY store does not support in app backups");
        }

        public override Note CreateNewNote()
        {
            return new NvpyNote(new NvpyNoteMarkup());
        }

        public override void CreateNewStore(bool recreate = false)
        {
            throw new NotSupportedException();
        }

        public override void Delete(Note note)
        {
            string filename = GetNoteFilename(note, false);
            File.Delete(filename);
        }

        public override INoteView GetNotes()
        {
            string[] filenames = Directory.GetFiles(m_Path, "*.json");
            return new NoteView(filenames.Select(ReadFile));
        }

        public override List<Tuple<ComponentInfo, UpdateContext>> GetUpdates()
        {
            return new List<Tuple<ComponentInfo, UpdateContext>>();
        }

        private static Note ReadFile(string filename)
        {
            NvpyNoteMarkup nvpyNoteMarkup;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NvpyNoteMarkup));
            using (FileStream s = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                nvpyNoteMarkup = (NvpyNoteMarkup)ser.ReadObject(s);
            }

            StringReader reader = new StringReader(nvpyNoteMarkup.content);

            NvpyNote note = new NvpyNote(nvpyNoteMarkup)
            {
                Class = string.Empty,
                HasChildren = false
            };

            reader.ReadLine();
            string nextLine = reader.ReadLine();
            if (nextLine != null)
            {
                note.Class = nextLine.StartsWith("Class: ") 
                    ? nextLine.Substring(7) 
                    : string.Empty;
            }

            note.Text = nvpyNoteMarkup.content;
            note.LastModifiedDate = File.GetLastWriteTime(filename);
            note.CreatedDate = File.GetCreationTime(filename);

            if (nvpyNoteMarkup.tags != null)
            {
                string tags = string.Empty;
                foreach (string tag in nvpyNoteMarkup.tags)
                {
                    if (!string.IsNullOrEmpty(tag))
                    {
                        //note.Properties.Add(new Property(tag, true));
                        if (tags.Length > 0)
                        {
                            tags += ";";
                        }
                        tags += tag;
                    }
                }
                //note.Tags = tags;
            }
            note.AddProperty(new Property(PropertyNameFilename, filename, true));

            if (nvpyNoteMarkup.systemtags != null)
            {
                foreach (string systemTag in nvpyNoteMarkup.systemtags)
                {
                    if (systemTag == NvpyNote.SystemTagPinned)
                    {
                        note.IsPinned = true;
                    }
                }
            }

            return note;
        }

        public override void PopulateChildren(Note note)
        {
        }

        public override void Save(Note note)
        {
            string filename = GetNoteFilename(note, true);
            m_Writer.CreateNote(note, filename);

            note.LastModifiedDate = DateTime.Now;
        }

        private string GetNoteFilename(Note note, bool create)
        {
            string filename;
            if (!TryGetNoteFilename(note, out filename))
            {
                if (!create)
                {
                    throw new Exception(string.Format("Filename not found for note: {0}", note.Name));
                }

                filename = m_Writer.GenerateNewNoteFilename(note);
                note.SetProperty(PropertyNameFilename, filename);
            }

            return filename;
        }

        private static bool TryGetNoteFilename(Note note, out string filename)
        {
            Property property;
            bool result = note.TryGetPropertyByName(PropertyNameFilename, out property);
            filename = result ? property.Value : string.Empty;
            return result;
        }
    }
}
