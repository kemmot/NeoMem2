using System;
using System.Collections.Generic;

using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public interface ICategoryModel
    {
        event EventHandler<ItemEventArgs<List<Note>>> NotesSelected;

        bool SortCategories { get; }

        IEnumerable<NoteCategory> GetCategories(IEnumerable<Note> notes);
        void Refresh(INoteView notes);
        void RemoveNote(Note note);
        void SetFilter(string filter);
        void Unregister();
    }
}
