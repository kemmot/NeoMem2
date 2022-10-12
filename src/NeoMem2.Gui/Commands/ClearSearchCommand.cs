using System;

namespace NeoMem2.Gui.Commands
{
    public class ClearSearchCommand : CommandBase
    {
        public ClearSearchCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.CboSearchText.Text = string.Empty;
        }
    }
}
