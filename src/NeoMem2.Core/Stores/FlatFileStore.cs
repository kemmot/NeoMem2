// <copyright file="FlatFileStore.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;

using NeoMem2.Automation.Updates;
using NeoMem2.Utils;

using NLog;

namespace NeoMem2.Core.Stores
{
    public class FlatFileStore : NeoMemStoreBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FlatFileStore(string rootFolder)
        {
            RootFolder = rootFolder;

            if (!Directory.Exists(RootFolder))
            {
                Directory.CreateDirectory(RootFolder);
            }

            Logger.Info("Store configured, root folder: {0}", RootFolder);
        }

        public string RootFolder { get; private set; }


        public override void Backup(string backupFile)
        {
            throw new NotSupportedException("Flat file store does not support in app backups");
        }

        public override Note CreateNewNote()
        {
            throw new NotSupportedException();
        }

        public override void CreateNewStore(bool recreate = false)
        {
            throw new NotSupportedException();
        }

        public override INoteView GetNotes()
        {
            return new NoteView(GetNotes(RootFolder));
        }

        public override void PopulateChildren(Note note)
        {
            note.Children.Clear();
            foreach (Note childNote in GetNotes(note))
            {
                note.Children.Add(childNote);
                childNote.Parent = note;
            }
        }

        private IEnumerable<Note> GetNotes(Note note)
        {
            string noteFolder = GetNoteChildrenFolder(note);
            return GetNotes(noteFolder);
        }

        public override List<Tuple<ComponentInfo, UpdateContext>> GetUpdates()
        {
            return new List<Tuple<ComponentInfo, UpdateContext>>();
        }

        public void Move(Note note, Note newParent)
        {
            var noteFiles = GetNoteFiles(note);
            string newNoteFolder = GetNoteChildrenFolder(newParent);
            foreach (string noteFile in noteFiles)
            {
                string newNoteFile = Path.Combine(newNoteFolder, Path.GetFileName(noteFile));
                File.Move(noteFile, newNoteFile);
            }
        }

        private IEnumerable<string> GetNoteFiles(Note note)
        {
            string noteFolder = GetNoteFolder(note);
            return Directory.GetFiles(noteFolder, note.Name + ".*");
        }

        public override void Save(Note note)
        {
            using (NLogExtensions.Push("Note: {0}", note))
            {
                string folder = GetNoteFolder(note);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string noteFileNameStub = CleanseToFileName(note.Name);

                if (!string.IsNullOrEmpty(note.FormattedText))
                {
                    string rtfFileName = Path.Combine(folder, noteFileNameStub + ".rtf");
                    File.WriteAllText(rtfFileName, note.FormattedText);
                    Logger.Debug("Saved note RTF to file: {0}", rtfFileName);
                }

                if (!string.IsNullOrEmpty(note.Text))
                {
                    string txtFileName = Path.Combine(folder, noteFileNameStub + ".txt");
                    File.WriteAllText(txtFileName, note.Text);
                    Logger.Debug("Saved note text to file: {0}", txtFileName);
                }

                string metaDataFileName = Path.Combine(folder, noteFileNameStub + ".ini");
                using (StreamWriter writer = new StreamWriter(metaDataFileName))
                {
                    writer.WriteLine("class={0}", note.Class);
                    foreach (Property property in note.Properties)
                    {
                        writer.WriteLine("{0}={1}", property.Name, property.Value);
                    }
                }
                Logger.Debug("Saved note metadata to file: {0}", metaDataFileName);

                Logger.Info("Note saved");
            }
        }

        private static string CleanseToFileName(string value)
        {
            string result = value;
            foreach (char invalidCharacter in Path.GetInvalidFileNameChars())
            {
                result = result.Replace(invalidCharacter, '_');
            }
            return result;
        }

        private string GetNoteChildrenFolder(Note note)
        {
            string folderName = GetNoteFolder(note);
            folderName = Path.Combine(folderName, CleanseToFileName(note.Name));
            return folderName;
        }

        private string GetNoteFolder(Note note)
        {
            string folderName;
            if (note.Parent == null)
            {
                folderName = RootFolder;
            }
            else
            {
                folderName = GetNoteFolder(note.Parent);
                folderName = Path.Combine(folderName, CleanseToFileName(note.Parent.Name));
            }

            return folderName;
        }

        private List<Note> GetNotes(string folder)
        {
            Dictionary<string, Note> fileNotes = new Dictionary<string, Note>();
            if (Directory.Exists(folder))
            {
                foreach (string file in Directory.GetFiles(folder))
                {
                    string noteName = Path.GetFileNameWithoutExtension(file);

                    Note note;
                    if (!fileNotes.TryGetValue(noteName, out note))
                    {
                        note = new Note
                        {
                            Name = noteName
                        };

                        fileNotes[noteName] = note;
                    }

                    string extension = Path.GetExtension(file);
                    switch (extension)
                    {
                        case ".rtf":
                            note.FormattedText = File.ReadAllText(file);
                            break;
                        case ".txt":
                            note.Text = File.ReadAllText(file);
                            break;
                        case ".ini":
                            using (StreamReader reader = new StreamReader(file))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    string[] lineParts = line.Split('=');
                                    if (lineParts.Length >= 2)
                                    {
                                        note.AddProperty(new Property(lineParts[0].Trim(), lineParts[1].Trim()));
                                    }
                                }
                            }
                            break;
                    }
                }

                foreach (string subFolder in Directory.GetDirectories(folder))
                {
                    string noteName = Path.GetFileName(subFolder);

                    Note note;
                    if (fileNotes.TryGetValue(noteName, out note))
                    {
                        note.HasChildren = true;
                    }
                }
            }

            List<Note> notes = new List<Note>(fileNotes.Values);
            notes.Sort((left, right) => left.Name.CompareTo(right.Name));
            return notes;
        }

        public List<Note> SearchNotes(string searchQuery)
        {
            List<Note> notes = new List<Note>();

            string[] searchTexts = searchQuery.ToUpper().Split();

            string[] files = Directory.GetFiles(RootFolder, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string searchFileName = Path.GetFileName(file).ToUpper();
                foreach (string searchText in searchTexts)
                {                    
                    if (searchFileName.Contains(searchText))
                    {
                        string noteName = Path.GetFileNameWithoutExtension(file);
                        Note note = new Note();
                        note.Name = noteName;
                        notes.Add(note);
                        break;
                    }
                }
            }

            return notes;
        }

        public override void Delete(Note note)
        {
            throw new NotSupportedException();
        }
    }
}
