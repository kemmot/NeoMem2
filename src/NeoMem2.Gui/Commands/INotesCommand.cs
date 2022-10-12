using System;
using System.Collections.Generic;
using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public interface INotesCommand
    {
        event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;

        bool IsSupported(List<Note> notes);

        void Execute(List<Note> notes);
    }
}
