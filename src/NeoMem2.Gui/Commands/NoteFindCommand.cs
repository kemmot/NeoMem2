using System;

namespace NeoMem2.Gui.Commands
{
    public class NoteFindCommand : CommandBase
    {
        public NoteFindCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.CboSearchText.Focus();
        }
    }
}
