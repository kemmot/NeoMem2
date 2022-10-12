using System;

namespace NeoMem2.Gui.Commands
{
    public class NewNoteWindowCommand : CommandBase
    {
        public NewNoteWindowCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.CreateNoteWindow();
        }
    }
}
