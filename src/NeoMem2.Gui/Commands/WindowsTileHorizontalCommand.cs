using System;
using System.Windows.Forms;

namespace NeoMem2.Gui.Commands
{
    public class WindowsTileHorizontalCommand : CommandBase
    {
        public WindowsTileHorizontalCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            Form.LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}
