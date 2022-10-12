using System;
using System.Windows.Forms;

namespace NeoMem2.Gui.Commands
{
    public class WindowsCascadeCommand : CommandBase
    {
        public WindowsCascadeCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.LayoutMdi(MdiLayout.Cascade);
        }
    }
}
