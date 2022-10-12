using System;
using System.Collections.Generic;
using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public class MultiCommandHandler : NotesCommand
    {
        private readonly List<INotesCommand> m_Commands = new List<INotesCommand>();

        public void RegisterCommand(INotesCommand command)
        {
            m_Commands.Add(command);
            command.ExceptionOccurred += CommandOnExceptionOccurred;
        }

        private void CommandOnExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            OnExceptionOccurred(e);
        }

        protected override bool DoIsSupported(List<Note> notes)
        {
            bool result = false;
            foreach (var command in m_Commands)
            {
                result = command.IsSupported(notes);
                if (result) break;
            }

            return result;
        }

        protected override void DoExecute(List<Note> notes)
        {
            foreach (var command in m_Commands)
            {
                if (command.IsSupported(notes))
                {
                    command.Execute(notes);
                    break;
                }
            }
        }
    }
}