using System;

namespace NeoMem2.Gui.Commands
{
    public class ShowAboutCommand : CommandBase
    {
        public ShowAboutCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            using (var form = new AboutForm())
            {
                form.GcMonitor = Form.GcMonitor;
                form.ShowDialog(Form);
            }
        }
    }
}
