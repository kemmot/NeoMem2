using System;
using System.Collections.Generic;
using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public abstract class NotesCommand : INotesCommand
    {
        public event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;

        public bool IsSupported(List<Note> notes)
        {
            bool result;
            try
            {
                result = DoIsSupported(notes);
            }
            catch (Exception ex)
            {
                result = false;
                OnExceptionOccurred(new ItemEventArgs<Exception>(ex));
            }

            return result;
        }

        protected abstract bool DoIsSupported(List<Note> notes);

        public void Execute(List<Note> notes)
        {
            try
            {
                DoExecute(notes);
            }
            catch (Exception ex)
            {
                OnExceptionOccurred(new ItemEventArgs<Exception>(ex));
            }
        }

        protected abstract void DoExecute(List<Note> notes);

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            ExceptionOccurred?.Invoke(this, e);
        }
    }
}