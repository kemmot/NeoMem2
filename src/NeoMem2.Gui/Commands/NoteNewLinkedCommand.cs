using System;

using NeoMem2.Core;

namespace NeoMem2.Gui.Commands
{
    public class NoteNewLinkedCommand : NoteNewCommand
    {
        public NoteNewLinkedCommand(MainForm form)
            : base(form)
        {
        }

        protected override void DoExecute(EventArgs e)
        {
            Note parentNote = Form.Model.CurrentNote;
            if (parentNote != null)
            {
                base.DoExecute(e);
                Note childNote = Form.CurrentNoteForm.CurrentNote;
                parentNote.AddLinkedNote(childNote);
            }
        }
    }
}
