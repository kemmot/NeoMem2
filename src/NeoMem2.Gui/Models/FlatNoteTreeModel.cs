using System;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NeoMem2.Gui.Models
{
    public class FlatNoteTreeModel : NoteTreeModelBase
    {
        private readonly NoteComparer m_Comparer;
        private INoteView m_NoteView;
        private string m_SortProperty;


        public FlatNoteTreeModel(TreeView view, string sortProperty)
            : base(view)
        {
            if (sortProperty == null) throw new ArgumentNullException("sortProperty");
            if (sortProperty.Length == 0) throw new ArgumentException("Argument cannot be zero length", "sortProperty");

            m_SortProperty = sortProperty;
            bool reverseSort = m_SortProperty == Note.PropertyNameScore;
            m_Comparer = new NoteComparer(m_SortProperty, reverseSort);
        }


        protected INoteView NoteView
        {
            get { return m_NoteView; }
            private set
            {
                if (m_NoteView != null)
                {
                    m_NoteView.NotesCollectionChanged -= NoteViewNotesCollectionChanged;
                }
                m_NoteView = value;
                if (m_NoteView != null)
                {
                    m_NoteView.NotesCollectionChanged += NoteViewNotesCollectionChanged;
                }
            }
        }

        
        public override void Refresh(INoteView view)
        {
            NoteView = view;

            List<Note> notes = NoteView.GetNotes().ToList();
            notes.Sort(m_Comparer);
            FillNotes(View.Nodes, notes);

            UpdateNoteCount();
        }

        public override void RemoveNote(Note note)
        {
            void RemoveNoteFunction()
            {
                foreach (TreeNode node in View.Nodes)
                {
                    var noteNode = node as NoteTreeNode;
                    if (noteNode != null && noteNode.Note == note)
                    {
                        noteNode.Remove();
                        UpdateNoteCount();
                        break;
                    }
                }
            }

            if (View.InvokeRequired) View.Invoke((ThreadStart) RemoveNoteFunction);
            else RemoveNoteFunction();
        }

        private void UpdateNoteCount()
        {
            OnDisplayedNoteCountChanged(new ItemEventArgs<int>(View.Nodes.Count));
        }

        private void NoteViewNotesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Note newNote in e.NewItems)
                {
                    View.Nodes.Add(new NoteTreeNode(newNote, false));
                }
            }

            if (e.OldItems != null)
            {
                foreach (Note oldNote in e.OldItems)
                {
                    RemoveNote(oldNote);
                }
            }
        }

        public override void SelectFirstNote()
        {
            List<Note> notes = NoteView.GetNotes().ToList();
            if (Properties.Settings.Default.SelectTopSearchMatch && notes.Count > 0)
            {
                OnCurrentNoteChanged(new ItemCompleteEventArgs<Note>(notes[0]));
            }
        }
    }
}
