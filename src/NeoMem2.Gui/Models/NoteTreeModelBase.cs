using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Gui.Commands;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public abstract class NoteTreeModelBase : TreeModelBase, INoteViewModel
    {
        public event EventHandler<ItemEventArgs<Note>> CurrentNoteChanged;
        public event EventHandler<ItemEventArgs<int>> DisplayedNoteCountChanged;
        public event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;
        public event EventHandler<ItemEventArgs<List<Note>>> NotesSelected;

        private readonly MultiCommandHandler m_DoubleClickHandler = new MultiCommandHandler();

        protected NoteTreeModelBase(TreeView view)
            : base(view)
        {
            m_DoubleClickHandler.ExceptionOccurred += DoubleClickHandlerOnExceptionOccurred;
            m_DoubleClickHandler.RegisterCommand(new OpenFileNoteCommand());
        }

        private void DoubleClickHandlerOnExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            OnExceptionOccurred(e);
        }

        public void SetFilter(string filter)
        {
            Filter = filter ?? string.Empty;
            //AddNodes(m_CategoryNodes);
        }

        protected override void AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as NoteTreeNode;
            if (node != null)
            {
                OnCurrentNoteChanged(new ItemEventArgs<Note>(node.Note));
            }
        }

        protected override void DoubleClick(object sender, EventArgs e)
        {
            if (View.SelectedNode is NoteTreeNode node)
            {
                m_DoubleClickHandler.Execute(new List<Note>() { node.Note });
            }
        }

        protected override void DragDrop(object sender, DragEventArgs e)
        {
            base.DragDrop(sender, e);

            Note targetNote;
            Note droppedNote;
            if (CanNoteDrop(sender, e, out targetNote, out droppedNote))
            {
                targetNote.AddLinkedNote(droppedNote);
            }

            string[] files;
            if (CanFileDrop(sender, e, out targetNote, out files))
            {
                foreach (string file in files)
                {
                    bool canAdd = targetNote.Attachments.All(attachment => attachment.Filename != file);
                    if (canAdd)
                    {
                        targetNote.Attachments.Add(new Attachment { Filename = file, Note = targetNote });
                    }
                }
            }
        }

        protected virtual void OnCurrentNoteChanged(ItemEventArgs<Note> e)
        {
            if (CurrentNoteChanged != null) CurrentNoteChanged(this, e);
        }

        protected virtual void OnDisplayedNoteCountChanged(ItemEventArgs<int> e)
        {
            if (DisplayedNoteCountChanged != null) DisplayedNoteCountChanged(this, e);
        }

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            if (ExceptionOccurred != null) ExceptionOccurred(this, e);
        }

        protected virtual void OnNotesSelected(ItemEventArgs<List<Note>> e)
        {
            if (NotesSelected != null) NotesSelected(this, e);
        }

        public abstract void RemoveNote(Note note);

        protected override void SetDragEffect(object sender, DragEventArgs e)
        {
            DragDropEffects effect = DragDropEffects.None;
            if ((CanNoteDrop(sender, e) || CanFileDrop(sender, e)) && e.IsEffectAllowed(DragDropEffects.Link))
            {
                if (e.IsEffectAllowed(DragDropEffects.Link))
                {
                    effect = DragDropEffects.Link;
                }
            }

            e.Effect = effect;
        }

        private bool CanFileDrop(object sender, DragEventArgs e)
        {
            Note targetNote;
            string[] files;
            return CanFileDrop(sender, e, out targetNote, out files);
        }

        private bool CanFileDrop(object sender, DragEventArgs e, out Note targetNote, out string[] files)
        {
            targetNote = GetTargetNote(sender, e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
            else
            {
                files = null;
            }

            return files != null;
        }

        private bool CanNoteDrop(object sender, DragEventArgs e)
        {
            Note targetNote;
            Note droppedNote;
            return CanNoteDrop(sender, e, out targetNote, out droppedNote);
        }

        private bool CanNoteDrop(object sender, DragEventArgs e, out Note targetNote, out Note droppedNote)
        {
            GetNoteDropDetails(sender, e, out targetNote, out droppedNote);

            //string reason;
            bool result;
            if (droppedNote == null)
            {
                result = false;
                //reason = "dropped note is null";
            }
            else if (targetNote == null)
            {
                result = false;
                //reason = "target note is null";
            }
            else if (droppedNote == targetNote)
            {
                result = false;
                //reason = "target note is dropped note";
            }
            else
            {
                result = true;
                //reason = "Drop allowed";
            }

            //StringBuilder output = new StringBuilder();
            //output.AppendFormat("Drop allowed: {0}", result);
            //output.AppendFormat(", control: {0}", ((Control)sender).Name);
            //output.AppendFormat(", dropped note: {0}", droppedNote == null ? "[null]" : droppedNote.Name);
            //output.AppendFormat(", target note: {0}", targetNote == null ? "[null]" : targetNote.Name);
            //output.AppendFormat(", reason: {0}", reason);
            //Debug.WriteLine(output.ToString());

            return result;
        }

        private void GetNoteDropDetails(object sender, DragEventArgs e, out Note targetNote, out Note droppedNote)
        {
            targetNote = GetTargetNote(sender, e);

            if (e.Data.GetDataPresent(typeof(NoteTreeNode)))
            {
                NoteTreeNode droppedNode = (NoteTreeNode)e.Data.GetData(typeof(NoteTreeNode));
                droppedNote = droppedNode.Note;
            }
            else if (e.Data.GetDataPresent(typeof(Note)))
            {
                droppedNote = (Note)e.Data.GetData(typeof(Note));
            }
            else
            {
                droppedNote = null;
            }
        }

        private Note GetTargetNote(object sender, DragEventArgs e)
        {
            TreeView selectedTreeView = (TreeView)sender;
            Point point = selectedTreeView.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = selectedTreeView.GetNodeAt(point);
            NoteTreeNode targetNoteNode = targetNode as NoteTreeNode;
            return targetNoteNode != null ? targetNoteNode.Note : null;
        }

        public abstract void SelectFirstNote();
    }
}
