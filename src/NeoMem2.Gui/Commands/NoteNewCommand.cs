using System;
using System.Collections.Generic;

using NeoMem2.Core.Scripting;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public class NoteNewCommand : CommandBase
    {
        public NoteNewCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            var newNote = Form.Model.CreateNewNote();
            newNote.Name = Form.CboSearchText.Text;

            Form.CurrentNoteForm.CurrentNote = newNote;
            Form.CurrentNoteForm.TxtName.Focus();

            try
            {
                var variables = new Dictionary<string, object>();
                variables[ScriptVariableNames.CurrentNoteForm] = Form.CurrentNoteForm;
                variables[ScriptVariableNames.MainForm] = Form;
                variables[ScriptVariableNames.Note] = newNote;
                Form.Model.ScriptHost.ExecuteScript(ScriptType.NewNoteCreated, variables);
            }
            catch (Exception ex)
            {
                OnExceptionOccurred(new ItemEventArgs<Exception>(ex));
            }
        }
    }
}
