using System;

namespace NeoMem2.Gui.Commands
{
    public class ShowBatchChangeCommand : CommandBase
    {
        public ShowBatchChangeCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            using (var dialog = new BatchChangeForm())
            {
                dialog.Notes = Form.Model.CurrentNotes;
                dialog.ShowDialog(Form);
            }
        }
    }
}
