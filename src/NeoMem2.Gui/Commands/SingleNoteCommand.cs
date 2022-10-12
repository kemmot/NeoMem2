using System;
using System.Collections.Generic;
using NeoMem2.Core;

namespace NeoMem2.Gui.Commands
{
    public abstract class SingleNoteCommand : NotesCommand
    {
        protected override bool DoIsSupported(List<Note> notes)
        {
            if (notes == null || notes.Count == 0) return false;
            return DoIsSupported(notes[0]);
        }

        protected abstract bool DoIsSupported(Note note);

        protected override void DoExecute(List<Note> notes)
        {
            DoExecute(notes[0]);
        }

        protected abstract void DoExecute(Note note);
    }
}