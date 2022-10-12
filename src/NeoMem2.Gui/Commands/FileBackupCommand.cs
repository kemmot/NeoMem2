using System;
using System.Windows.Forms;

namespace NeoMem2.Gui.Commands
{
    public class FileBackupCommand : CommandBase
    {
        public FileBackupCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                if (dialog.ShowDialog(Form) == DialogResult.OK)
                {
                    Form.Model.Backup(dialog.FileName);
                }
            }
        }
    }
}
