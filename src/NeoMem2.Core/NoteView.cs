// <copyright file="NoteView.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NeoMem2.Core
{
    public class NotePropertyChangedEventArgs : PropertyChangedEventArgs
    {
        private readonly Note m_Note;

        public NotePropertyChangedEventArgs(Note note, string propertyName)
            : base(propertyName)
        {
            m_Note = note;
        }

        public Note Note { get { return m_Note; } }
    }

    public interface INoteView
    {
        event EventHandler<NotePropertyChangedEventArgs> NotePropertyChanged;
        event EventHandler<NotifyCollectionChangedEventArgs> NotesCollectionChanged;

        int Count { get; }

        IEnumerable<Note> GetNotes();
        INoteView GetView(Func<Note, bool> filter);
    }

    public abstract class NoteViewBase : INoteView
    {
        public event EventHandler<NotePropertyChangedEventArgs> NotePropertyChanged = delegate { }; 
        public event EventHandler<NotifyCollectionChangedEventArgs> NotesCollectionChanged = delegate { };

        private readonly ObservableCollection<Note> m_Notes = new ObservableCollection<Note>();


        protected NoteViewBase()
        {
            Notes.CollectionChanged += NotesCollectionChangedHandler;
        }

        
        public abstract int Count { get; }
        protected ObservableCollection<Note> Notes { get { return m_Notes; } }

        
        public abstract IEnumerable<Note> GetNotes();

        public INoteView GetView(Func<Note, bool> filter)
        {
            return new FilteredNoteView(this, filter);
        }

        private void NotesCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Note newNote in e.NewItems)
                {
                    newNote.PropertyChanged += NotePropertyChangedHandler;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Note oldNote in e.OldItems)
                {
                    oldNote.PropertyChanged -= NotePropertyChangedHandler;
                }
            }

            OnNotesCollectionChanged(e);
        }

        private void NotePropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            OnNotePropertyChanged(new NotePropertyChangedEventArgs((Note)sender, e.PropertyName));
        }

        protected virtual void OnNotePropertyChanged(NotePropertyChangedEventArgs e)
        {
            NotePropertyChanged(this, e);
        }

        protected virtual void OnNotesCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            NotesCollectionChanged(this, e);
        }
    }

    public class NoteView : NoteViewBase
    {
        public NoteView()
        {
        }
        
        public NoteView(IEnumerable<Note> notes)
        {
            foreach (Note note in notes)
            {
                Notes.Add(note);
            }
        }


        public override int Count { get { return Notes.Count; } }


        public void AddRange(IEnumerable<Note> notes)
        {
            foreach (Note note in notes)
            {
                Add(note);
            }
        }

        public void Add(Note note)
        {
            Notes.Add(note);
        }

        public void Clear()
        {
            Notes.Clear();
        }

        public override IEnumerable<Note> GetNotes()
        {
            return Notes;
        }

        public void Remove(Note note)
        {
            Notes.Remove(note);
        }
    }

    public class FilteredNoteView : NoteViewBase
    {
        private readonly Func<Note, bool> m_Filter;


        public FilteredNoteView(INoteView parent, Func<Note, bool> filter)
        {
            parent.NotePropertyChanged += NotePropertyChangedHandler;
            parent.NotesCollectionChanged += ParentNotesCollectionChanged;

            m_Filter = filter;

            foreach (Note note in parent.GetNotes())
            {
                CheckAndAdd(note);
            }
        }


        public override int Count
        {
            get { return Notes.Count; }
        }


        public override IEnumerable<Note> GetNotes()
        {
            return Notes;
        }

        private void ParentNotesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (Note note in e.NewItems)
                        {
                            CheckAndAdd(note);
                        }
                    }
                    break;
                    //case NotifyCollectionChangedAction.Move:
                    //    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (Note note in e.OldItems)
                        {
                            Remove(note);
                        }
                    }
                    break;
                    //case NotifyCollectionChangedAction.Replace:
                    //    break;
                case NotifyCollectionChangedAction.Reset:
                    base.OnNotesCollectionChanged(e);
                    break;
                default:
                    throw new NotSupportedException("Action not supported: " + e.Action);
            }
        }

        private void NotePropertyChangedHandler(object sender, NotePropertyChangedEventArgs e)
        {
            OnNotePropertyChanged(e);
        }

        protected override void OnNotePropertyChanged(NotePropertyChangedEventArgs e)
        {
            base.OnNotePropertyChanged(e);

            if (m_Filter(e.Note))
            {
                Add(e.Note);
            }
            else
            {
                Remove(e.Note);
            }
        }

        private void CheckAndAdd(Note note)
        {
            if (m_Filter(note) && !Notes.Contains(note))
            {
                Notes.Add(note);
            }
        }

        private void Add(Note note)
        {
            if (!Notes.Contains(note))
            {
                Notes.Add(note);
            }
        }

        private void Remove(Note note)
        {
            if (Notes.Contains(note))
            {
                Notes.Remove(note);
            }
        }
    }
}
