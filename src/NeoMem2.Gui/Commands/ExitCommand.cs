using System;

namespace NeoMem2.Gui.Commands
{
    public class ExitCommand : CommandBase
    {
        public ExitCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.Close();
        }
    }
}
