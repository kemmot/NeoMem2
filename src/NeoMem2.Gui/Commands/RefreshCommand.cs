using System;

namespace NeoMem2.Gui.Commands
{
    public class RefreshCommand : CommandBase
    {
        public RefreshCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.Model.Refresh();
        }
    }
}
