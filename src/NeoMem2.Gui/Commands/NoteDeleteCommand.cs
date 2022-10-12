using System;
using System.Windows.Forms;

namespace NeoMem2.Gui.Commands
{
    public class NoteDeleteCommand : CommandBase
    {
        public NoteDeleteCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            if (Form.CurrentNoteForm.CurrentNote != null)
            {
                string message = string.Format("Delete note: {0}?", Form.CurrentNoteForm.CurrentNote.Name);
                var result = MessageBox.Show(message, "Delete Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    Form.Model.Delete(Form.CurrentNoteForm.CurrentNote);
                    Form.CurrentNoteForm.CurrentNote = null;
                }
            }
        }
    }
}
