using System;

namespace NeoMem2.Core
{
    public class FileNoteWrapper
    {
        public static FileNoteWrapper ConvertToFileNote(Note note, string path)
        {
            var wrapper = new FileNoteWrapper(note);
            wrapper.IsFileNote = true;
            wrapper.Path = path;
            return wrapper;
        }

        public FileNoteWrapper(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            Note = note;
        }

        public bool IsFileNote
        {
            get { return Note.Namespace == NoteNamespace.File; }
            set { Note.Namespace = NoteNamespace.File; }
        }

        public Note Note { get; }

        public string Path
        {
            get { return Note.Text; }
            set
            {
                Note.Text = value;
                Note.Name = System.IO.Path.GetFileName(value);
                Note.TextFormat = TextFormatInspector.GetTextFormat(value);
            }
        }
    }
}
