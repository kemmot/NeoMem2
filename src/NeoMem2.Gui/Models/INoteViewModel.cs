using System;

using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public interface INoteViewModel
    {
        event EventHandler<ItemEventArgs<Note>> CurrentNoteChanged;
        event EventHandler<ItemEventArgs<int>> DisplayedNoteCountChanged;
        event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;

        void Refresh(INoteView notes);

        void RemoveNote(Note note);

        void SelectFirstNote();
    }
}
