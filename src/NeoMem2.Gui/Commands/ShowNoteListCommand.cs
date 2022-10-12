using System;

namespace NeoMem2.Gui.Commands
{
    public class ShowNoteListCommand : CommandBase
    {
        public ShowNoteListCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.LvNotes.Visible = Form.MiShowNoteList.Checked;
        }
    }
}
