using System;
using System.Windows.Forms;

using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public class CommandManager
    {
        public event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred = delegate { };

        public void Connect(Button item, ICommand command)
        {
            command.ExceptionOccurred += CommandExceptionOccurred;
            item.Click += (sender, e) => command.Execute(e);
        }

        public void Connect(ToolStripMenuItem item, ICommand command)
        {
            command.ExceptionOccurred += CommandExceptionOccurred;
            item.Click += (sender, e) => command.Execute(e);
        }

        public void Execute(ICommand command, EventArgs args)
        {
            command.ExceptionOccurred += CommandExceptionOccurred;
            command.Execute(args);
        }

        private void CommandExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            OnExceptionOccurred(e);
        }

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            ExceptionOccurred(this, e);
        }
    }
}