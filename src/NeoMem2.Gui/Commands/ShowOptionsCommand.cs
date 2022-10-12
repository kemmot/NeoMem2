using System;

namespace NeoMem2.Gui.Commands
{
    public class ShowOptionsCommand : CommandBase
    {
        public ShowOptionsCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            using (OptionsForm form = new OptionsForm())
            {
                form.AddTarget(Form.DefaultNoteQueryOptions, "Default Note Query Options");
                form.AddTarget(Form, "Main Form");
                form.AddTarget(Form.CurrentNoteForm, "Current Note Form");
                form.AddTarget(Form.CurrentNoteForm.Helper.GetEditor(), "Current Note Editor");
                form.AddTarget(Properties.Settings.Default, "Settings");
                form.ShowDialog(Form);

                Form.NoteListModel.SetColumns(Properties.Settings.Default.NoteColumns.Split(new[] { ',', ';' }));
                Form.CurrentNoteForm.NoteListModel.SetColumns(Properties.Settings.Default.NoteColumns.Split(new[] { ',', ';' }));
            }
        }
    }
}
