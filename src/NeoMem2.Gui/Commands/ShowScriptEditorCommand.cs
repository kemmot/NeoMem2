using System;

namespace NeoMem2.Gui.Commands
{
    public class ShowScriptEditorCommand : CommandBase
    {
        public ShowScriptEditorCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            using (var dialog = new ScriptEditorForm { Model = Form.Model })
            {
                dialog.ShowDialog(Form);
            }
        }
    }
}
