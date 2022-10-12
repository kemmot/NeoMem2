using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui.Models
{
    public abstract class TreeModelBase
    {
        protected readonly static Color AlternatingNodeColour = NoteCategoryTreeNode.AlternatingNodeColour;

        private readonly TreeView m_View;

        protected TreeModelBase(TreeView view)
        {
            Filter = string.Empty;

            m_View = view;
            m_View.AllowDrop = true;

            Register();
        }


        protected string Filter { get; set; }
        protected TreeView View { get { return m_View; } }


        public abstract void Refresh(INoteView view);

        private void Register()
        {
            m_View.AfterCheck += SafeAfterCheck;
            m_View.AfterExpand += SafeAfterExpand;
            m_View.AfterSelect += SafeAfterSelect;
            m_View.DoubleClick += SafeDoubleClick;
            m_View.DragDrop += SafeDragDrop;
            m_View.DragEnter += SafeDragEnter;
            m_View.DragOver += SafeDragOver;
            m_View.ItemDrag += SafeItemDrag;
            m_View.MouseDown += SafeMouseDown;
        }

        public virtual void Unregister()
        {
            m_View.AfterCheck -= SafeAfterCheck;
            m_View.AfterExpand -= SafeAfterExpand;
            m_View.AfterSelect -= SafeAfterSelect;
            m_View.DoubleClick -= SafeDoubleClick;
            m_View.DragDrop -= SafeDragDrop;
            m_View.DragEnter -= SafeDragEnter;
            m_View.DragOver -= SafeDragOver;
            m_View.ItemDrag -= SafeItemDrag;
            m_View.MouseDown -= SafeMouseDown;
        }

        private void SafeAfterCheck(object sender, TreeViewEventArgs e)
        {
            ExceptionSafeBlock(() => { AfterCheck(sender, e); });
        }

        protected virtual void AfterCheck(object sender, TreeViewEventArgs e)
        {
        }

        private void SafeAfterExpand(object sender, TreeViewEventArgs e)
        {
            ExceptionSafeBlock(() => { AfterExpand(sender, e); });
        }

        protected virtual void AfterExpand(object sender, TreeViewEventArgs e)
        {
        }

        private void SafeAfterSelect(object sender, TreeViewEventArgs e)
        {
            ExceptionSafeBlock(() => { AfterSelect(sender, e); });
        }

        protected virtual void AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void SafeDoubleClick(object sender, EventArgs e)
        {
            ExceptionSafeBlock(() => { DoubleClick(sender, e); });
        }

        protected virtual void DoubleClick(object sender, EventArgs e)
        {
        }
        
        protected void AddNodes(IEnumerable<TreeNode> nodes)
        {
            View.BeginUpdate();
            try
            {
                foreach (var node in View.Nodes)
                {
                    var noteNode = node as NoteTreeNode;
                    if (noteNode != null) noteNode.Dispose();
                }

                View.Nodes.Clear();
                int nodeIndex = 0;

                string upperFilter = Filter.ToUpper();
                foreach (var node in nodes)
                {
                    if (string.IsNullOrEmpty(Filter) || node.Text.ToUpper().Contains(upperFilter))
                    {
                        if (nodeIndex % 2 == 1)
                        {
                            node.BackColor = AlternatingNodeColour;
                        }
                        View.Nodes.Add(node);
                        nodeIndex++;
                    }
                }
            }
            finally
            {
                View.EndUpdate();
            }
        }

        protected void FillNotes(TreeNodeCollection nodes, IEnumerable<Note> notes)
        {
            View.BeginUpdate();
            try
            {
                Clear(nodes);
                AddNotes(nodes, notes);
            }
            finally
            {
                View.EndUpdate();
            }
        }

        protected void Clear(TreeNodeCollection nodes)
        {
            foreach (var node in nodes)
            {
                var noteNode = node as NoteTreeNode;
                if (noteNode != null) noteNode.Dispose();
            }

            nodes.Clear();
        }

        protected void AddNotes(TreeNodeCollection nodes, IEnumerable<Note> notes)
        {
            int nodeIndex = 0;
            foreach (Note rootNote in notes)
            {
                var node = new NoteTreeNode(rootNote, false);
                if (nodeIndex % 2 == 1)
                {
                    node.BackColor = AlternatingNodeColour;
                }
                nodes.Add(node);
                nodeIndex++;
            }
        }

        private void SafeDragDrop(object sender, DragEventArgs e)
        {
            ExceptionSafeBlock(() => { DragDrop(sender, e); });
        }

        protected virtual void DragDrop(object sender, DragEventArgs e)
        {
        }

        private void SafeDragOver(object sender, DragEventArgs e)
        {
            ExceptionSafeBlock(() => { DragOver(sender, e); });
        }

        protected virtual void DragOver(object sender, DragEventArgs e)
        {
            SetDragEffect(sender, e);
        }

        private void SafeDragEnter(object sender, DragEventArgs e)
        {
            ExceptionSafeBlock(() => { DragEnter(sender, e); });
        }

        protected virtual void DragEnter(object sender, DragEventArgs e)
        {
            SetDragEffect(sender, e);
        }

        private void SafeItemDrag(object sender, ItemDragEventArgs e)
        {
            ExceptionSafeBlock(() => { ItemDrag(sender, e); });
        }

        protected virtual void ItemDrag(object sender, ItemDragEventArgs e)
        {
            View.DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Link | DragDropEffects.Move);
        }

        private void SafeMouseDown(object sender, MouseEventArgs e)
        {
            ExceptionSafeBlock(() => { MouseDown(sender, e); });
        }

        protected virtual void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = m_View.GetNodeAt(e.Location);
                if (node != null)
                {
                    //m_View.SelectedNode = node;
                }
            }
        }

        protected virtual void SetDragEffect(object sender, DragEventArgs e)
        {
        }

        private void ExceptionSafeBlock(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                using (var form = new ExceptionForm())
                {
                    form.SetException(ex);
                    form.ShowDialog();
                }
            }
        }
    }
}
