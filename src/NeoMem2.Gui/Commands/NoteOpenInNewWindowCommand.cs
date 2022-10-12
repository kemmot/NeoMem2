using System;

using NeoMem2.Core;

namespace NeoMem2.Gui.Commands
{
    public class NoteOpenInNewWindowCommand : CommandBase
    {
        private readonly Note m_Note;

        public NoteOpenInNewWindowCommand(MainForm form, Note note)
            : base(form)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            m_Note = note;
        }

        protected override void DoExecute(EventArgs e)
        {
            Form.CreateNoteWindow();
            Form.ViewNote(m_Note);
        }
    }
}
