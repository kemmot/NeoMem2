using System;

using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public interface ICommand
    {
        event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;

        void Execute(EventArgs e);
    }
}