using Markdig;

namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    using NeoMem2.Core.WinApi;

    public class EncodingExporterBase : ExporterBase
    {
        private const int MaxPath = 259;
        private const string IndexesSubFolder = "indexes";
        private const string NotesSubFolder = "notes";

        private string m_IndexesFolder;
        private string m_NotesFolder;
        private string m_RootFolder;
        private readonly Dictionary<Note, bool> m_NoteExportAllowed = new Dictionary<Note, bool>();
        private readonly Dictionary<Note, string> m_NoteFilenames = new Dictionary<Note, string>();

        public List<string> ClassesToExclude { get; } = new List<string>();

        private IEncodingWriterFactory writerFactory;

        protected EncodingExporterBase(IEncodingWriterFactory writerFactory)
        {
            this.writerFactory = writerFactory ?? throw new ArgumentNullException(nameof(writerFactory));
        }

        /// <summary>
        /// Exports the specified notes.
        /// </summary>
        /// <param name="file">The notes to export.</param>
        public override void Export(NeoMemFile file)
        {
            m_RootFolder = ConnectionString;
            Directory.CreateDirectory(m_RootFolder);

            m_NotesFolder = Path.Combine(m_RootFolder, NotesSubFolder);
            Directory.CreateDirectory(m_NotesFolder);

            m_IndexesFolder = Path.Combine(m_RootFolder, IndexesSubFolder);
            Directory.CreateDirectory(m_IndexesFolder);

            var notes = file.AllNotes.GetNotes().ToList();
            ExportNotes(notes);

            var indexFilenames = new List<Tuple<string, int, string>>();

            int indexNoteCount;
            string filename = string.Format("AllNotesByName.{0}", this.writerFactory.GetFileExtension());
            filename = WriteIndex("All Notes By Name", notes.OrderBy(n => n.Name), Path.Combine(m_IndexesFolder, filename), out indexNoteCount);
            indexFilenames.Add(new Tuple<string, int, string>("All By Name", indexNoteCount, filename));

            filename = string.Format("AllNotesById.{0}", this.writerFactory.GetFileExtension());
            filename = WriteIndex("All Notes By ID", notes.OrderBy(n => n.Id), Path.Combine(m_IndexesFolder, filename), out indexNoteCount);
            indexFilenames.Add(new Tuple<string, int, string>("All By ID", indexNoteCount, filename));

            filename = WriteClassIndex(notes);
            indexFilenames.Add(new Tuple<string, int, string>("Classes", notes.Count, filename));

            filename = WriteTagIndex(notes);
            indexFilenames.Add(new Tuple<string, int, string>("Tags", notes.Count, filename));

            filename = WritePinnedIndex(notes);
            indexFilenames.Add(new Tuple<string, int, string>("Pinned", notes.Count, filename));

            WriteIndexOfIndexes(indexFilenames);
        }

        private void WriteIndexOfIndexes(IEnumerable<Tuple<string, int, string>> indexFiles)
        {
            string filename = Path.Combine(m_RootFolder, string.Format("NeoMem2.{0}", this.writerFactory.GetFileExtension()));

            using (var writer = new StreamWriter(filename))
            {
                var encodingWriter = this.writerFactory.GetWriter(writer);
                encodingWriter.WriteTag(EncodingTagType.Heading1, "NeoMem2");
                WriteLinkList(encodingWriter, indexFiles, m_RootFolder);
            }
        }

        private string WritePinnedIndex(IEnumerable<Note> notes)
        {
            string filename = Path.Combine(m_IndexesFolder, "Pinned." + this.writerFactory.GetFileExtension());

            using (var writer = new StreamWriter(filename))
            {
                var encodingWriter = this.writerFactory.GetWriter(writer);
                encodingWriter.WriteTag(EncodingTagType.Heading1, "Pinned");
                WriteNoteTable(encodingWriter, notes.Where(n => n.IsPinned).OrderBy(n => n.Name), m_IndexesFolder);
            }

            return filename;
        }

        private string WriteClassIndex(IEnumerable<Note> notes)
        {
            string filename = Path.Combine(m_IndexesFolder, "Classes." + this.writerFactory.GetFileExtension());

            using (var writer = new StreamWriter(filename))
            {
                var encodingWriter = this.writerFactory.GetWriter(writer);
                encodingWriter.WriteTag(EncodingTagType.Heading1, "Classes");
                var indexDetails = WriteClassIndexes(notes);
                WriteLinkList(encodingWriter, indexDetails.OrderBy(n => n.Item1), m_IndexesFolder);
            }

            return filename;
        }

        private IEnumerable<Tuple<string, int, string>> WriteClassIndexes(IEnumerable<Note> notes)
        {
            var notesByClass = new Dictionary<string, List<Note>>();
            foreach (var note in notes)
            {
                List<Note> classNotes;
                if (!notesByClass.TryGetValue(note.Class, out classNotes))
                {
                    classNotes = new List<Note>();
                    notesByClass[note.Class] = classNotes;
                }

                classNotes.Add(note);
            }

            foreach (string className in notesByClass.Keys)
            {
                var classNotes = notesByClass[className];
                classNotes.Sort((left, right) => String.Compare(left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase));
                int indexNoteCount;
                string filename = WriteIndex("Class: " + className, classNotes, GetClassIndexFilename(className), out indexNoteCount);
                yield return new Tuple<string, int, string>(className, indexNoteCount, filename);
            }
        }

        private string GetClassIndexFilename(string className)
        {
            return GetIndexFilename("Class", className);
        }

        private string GetTagIndexFilename(string className)
        {
            return GetIndexFilename("Tag", className);
        }

        private string GetIndexFilename(string indexType, string className)
        {
            string filename = string.Format("{0}_{1}.{2}", indexType, className, writerFactory.GetFileExtension());
            filename = CleanseFilename(filename);
            return Path.Combine(m_IndexesFolder, filename);
        }

        private string WriteTagIndex(IEnumerable<Note> notes)
        {
            string filename = Path.Combine(m_IndexesFolder, string.Format("Tags.{0}", writerFactory.GetFileExtension()));

            using (var writer = new StreamWriter(filename))
            {
                var encodingWriter = this.writerFactory.GetWriter(writer);
                encodingWriter.WriteTag(EncodingTagType.Heading1, "Tags");
                var indexDetails = WriteTagIndexes(notes);
                WriteLinkList(encodingWriter, indexDetails.OrderBy(n => n.Item1), m_IndexesFolder);
            }

            return filename;
        }

        private void WriteLinkList(IEncodingWriter encodingWriter, IEnumerable<Tuple<string, int, string>> details, string relativeTo)
        {
            var table = new DataTable("Properties");
            table.Columns.Add("Name");
            table.Columns.Add("Count");
            table.Columns.Add("Link");

            foreach (Tuple<string, int, string> detail in details)
            {
                string name = detail.Item1;
                int noteCount = detail.Item2;
                string path = shlwapi.GetRelativePathFromFolderToFile(relativeTo, detail.Item3);
                if (noteCount > 0)
                {
                    table.Rows.Add(name, noteCount, encodingWriter.GetLinkTag(path, path));
                }
            }

            encodingWriter.Write(table);
        }

        private IEnumerable<Tuple<string, int, string>> WriteTagIndexes(IEnumerable<Note> notes)
        {
            var notesByTag = new Dictionary<string, List<Note>>();
            foreach (var note in notes)
            {
                foreach (var tag in note.Tags)
                {
                    string tagName = tag.Tag.Name;
                    List<Note> tagNotes;
                    if (!notesByTag.TryGetValue(tagName, out tagNotes))
                    {
                        tagNotes = new List<Note>();
                        notesByTag[tagName] = tagNotes;
                    }

                    tagNotes.Add(note);
                }
            }

            foreach (string tagName in notesByTag.Keys)
            {
                var tagNotes = notesByTag[tagName];
                tagNotes.Sort((left, right) => String.Compare(left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase));
                int indexNoteCount;
                string filename = WriteIndex("Tag: " + tagName, tagNotes, GetTagIndexFilename(tagName), out indexNoteCount);
                yield return new Tuple<string, int, string>(tagName, indexNoteCount, filename);
            }
        }

        private string WriteIndex(string title, IEnumerable<Note> notes, string filename, out int indexNoteCount)
        {
            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    var encodingWriter = this.writerFactory.GetWriter(writer);
                    encodingWriter.WriteTag(EncodingTagType.Heading1, title);
                    indexNoteCount = WriteNoteTable(encodingWriter, notes, m_IndexesFolder);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to export note to file: {filename}", ex);
            }

            return filename;
        }

        private void ExportNotes(List<Note> notes)
        {
            int notesComplete = 0;
            foreach (var note in notes)
            {
                try
                {
                    ExportNote(note);
                    notesComplete++;
                    OnProgressChanged(notesComplete, notes.Count, note.Name);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to export note {note.Id}", ex);
                }
            }
        }

        private void ExportNote(Note note)
        {
            if (IsExportAllowed(note))
            {
                string fullname = GetNotePath(note);
                try
                {
                    using (var writer = new StreamWriter(fullname))
                    {
                        ExportNote(writer, note);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to export note to file: {fullname}", ex);
                }
            }
        }

        private void ExportNote(TextWriter writer, Note note)
        {
            var encodingWriter = this.writerFactory.GetWriter(writer);
            using (encodingWriter.WriteOpenTag(EncodingTagType.Body))
            {
                encodingWriter.WriteTag(EncodingTagType.Heading1, note.Name);
                WriteKnownProperties(encodingWriter, note);
                WriteCustomProperties(encodingWriter, note);
                WriteLinkedNotes(encodingWriter, note);
                WriteAttachments(encodingWriter, note);
                WriteNoteBody(encodingWriter, note);
            }
        }

        private void WriteKnownProperties(IEncodingWriter encodingWriter, Note note)
        {
            encodingWriter.WriteTag(EncodingTagType.Heading2, "Properties");

            var table = new DataTable("Properties");
            table.Columns.Add("PropertyName");
            table.Columns.Add("Value");
            table.Rows.Add("Class", encodingWriter.GetLinkTag(shlwapi.GetRelativePathFromFolderToFile(m_NotesFolder, GetClassIndexFilename(note.Class)), note.Class));
            table.Rows.Add("CreatedDate", note.CreatedDate);
            table.Rows.Add("DeletedDate", note.DeletedDate);
            table.Rows.Add("Description", note.Description);
            table.Rows.Add("Id", note.Id);
            table.Rows.Add("IsDeleted", note.IsDeleted);
            table.Rows.Add("IsPinned", note.IsPinned);
            table.Rows.Add("FormattedText", note.FormattedText);
            table.Rows.Add("LastAccessedDate", note.LastAccessedDate);
            table.Rows.Add("LastModifiedDate", note.LastModifiedDate);
            table.Rows.Add("Namespace", note.Namespace);
            table.Rows.Add("Tags", note.TagText);
            encodingWriter.Write(table);

            encodingWriter.WriteTag(EncodingTagType.Line);
        }

        private void WriteCustomProperties(IEncodingWriter encodingWriter, Note note)
        {
            var properties = note.AllProperties.ToList();
            if (properties.Count > 0)
            {
                encodingWriter.WriteTag(EncodingTagType.Heading2, "Custom Properties");

                var table = new DataTable("Properties");
                table.Columns.Add("PropertyName");
                table.Columns.Add("Value");
                foreach (var property in properties)
                {
                    table.Rows.Add(property.Name, property.ValueString);
                }
                encodingWriter.Write(table);

                encodingWriter.WriteTag(EncodingTagType.Line);
            }
        }

        private void WriteLinkedNotes(IEncodingWriter encodingWriter, Note note)
        {
            var linkedNotes = new List<Note>();
            foreach (var linkedNote in note.LinkedNotes)
            {
                var otherNote = linkedNote.Note1 == note ? linkedNote.Note2 : linkedNote.Note1;
                linkedNotes.Add(otherNote);
            }

            if (linkedNotes.Count > 0)
            {
                encodingWriter.WriteTag(EncodingTagType.Heading2, "Linked Notes");
                WriteNoteTable(encodingWriter, linkedNotes.OrderBy(n => n.Class), m_NotesFolder);
                encodingWriter.WriteTag(EncodingTagType.Line);
            }
        }

        private int WriteNoteTable(IEncodingWriter encodingWriter, IEnumerable<Note> notes, string fromFolder)
        {
            var exportableNotes = notes.Where(IsExportAllowed).ToList();
            if (exportableNotes.Count > 0)
            {
                var table = new DataTable();
                table.Columns.Add("ID");
                table.Columns.Add("Name");
                table.Columns.Add("Link");
                foreach (var linkedNote in exportableNotes)
                {
                    string linkedNotePath = GetNotePath(linkedNote);
                    string relativePath = shlwapi.GetRelativePathFromFolderToFile(fromFolder, linkedNotePath);
                    string filename = Path.GetFileName(linkedNotePath);
                    //WriteRow(writer, linkedNote.Id, linkedNote.Name, HtmlWriter.GetLinkTag(relativePath, filename));
                    table.Rows.Add(linkedNote.Id, linkedNote.Name, encodingWriter.GetLinkTag(relativePath, filename));
                }
                encodingWriter.Write(table);
            }

            return exportableNotes.Count;
        }

        private void WriteAttachments(IEncodingWriter encodingWriter, Note note)
        {
            if (note.Attachments.Count > 0)
            {
                encodingWriter.WriteTag(EncodingTagType.Heading2, "Attachments");
                foreach (var attachment in note.Attachments)
                {
                    using (encodingWriter.WriteOpenTag(EncodingTagType.UnorderedList))
                    {
                        encodingWriter.WriteTag(EncodingTagType.ListItem, encodingWriter.GetLinkTag(attachment.Filename, Path.GetFileName(attachment.Filename)));
                    }
                }
                encodingWriter.WriteTag(EncodingTagType.Line);
            }
        }

        protected virtual void WriteNoteBody(IEncodingWriter encodingWriter, Note note)
        {
            if (!string.IsNullOrEmpty(note.Text))
            {
                encodingWriter.WriteTag(EncodingTagType.Heading2, "Note Text");

                var reader = new StringReader(note.Text);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    encodingWriter.WriteTag(EncodingTagType.Paragraph, line);
                    encodingWriter.WriteTag(EncodingTagType.Line);
                }
            }
        }

        private bool IsExportAllowed(Note note)
        {
            bool isAllowed = false;
            if (!note.IsDeleted && !m_NoteExportAllowed.TryGetValue(note, out isAllowed))
            {
                isAllowed = !ClassesToExclude.Contains(note.Class);
                m_NoteExportAllowed[note] = isAllowed;
            }

            return isAllowed;
        }

        private string GetNotePath(Note note)
        {
            string path;
            if (!m_NoteFilenames.TryGetValue(note, out path))
            {
                string prefix = string.Empty;
                string suffix = string.Format("-{0}.{1}", note.Id, this.writerFactory.GetFileExtension());

                int clashDigitLength = 1;
                int folderSeperatorLength = 1;
                int maxNameLength = MaxPath - m_NotesFolder.Length - prefix.Length - suffix.Length - clashDigitLength - folderSeperatorLength;

                string name = note.Name;
                name = CleanseFilename(name);
                if (name.Length > maxNameLength)
                {
                    name = name.Substring(0, maxNameLength);
                }

                path = Path.Combine(m_NotesFolder, prefix + name + suffix);
                int clashDigit = 1;
                while (File.Exists(path))
                {
                    if (clashDigit >= 10)
                    {
                        throw new Exception(string.Format("Cannot find unique filename for note, ID: {0}, name: {1}", note.Id, note.Name));
                    }

                    path = Path.Combine(m_NotesFolder, prefix + name + "-" + clashDigit + suffix);
                    clashDigit++;
                }

                m_NoteFilenames[note] = path;
            }

            return path;
        }

        private string CleanseFilename(string filename)
        {
            filename = filename.Replace(' ', '_');

            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(invalidChar, '_');
            }

            return filename;
        }
    }
}
