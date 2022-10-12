using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui
{
    public class NoteTreeNode : TreeNode, IDisposable
    {
        public NoteTreeNode(Note note, bool hasChildren)
        {
            if (note == null) throw new ArgumentNullException("note");

            Note = note;
            Text = GetName();

            if (hasChildren)
            {
                Nodes.Add("Loading...");
            }

            Note.PropertyChanged += NotePropertyChanged;
        }

        private void NotePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Note.PropertyNameName)
            {
                var treeView = TreeView;
                if (treeView != null)
                {
                    ThreadStart del = delegate { Text = GetName(); };
                    if (treeView.InvokeRequired) treeView.Invoke(del);
                    else del();
                }
            }
        }

        public Note Note { get; private set; }

        private string GetName()
        {
            var note = Note;
            if (note == null) throw new NullReferenceException("note");

            string name = string.Empty;
            name += Note.Name;
            if (Note.Score > 0)
            {
                name += string.Format(" ({0}) ", Note.Score);
            }

            return name;
        }


        #region IDisposable Functions

        private readonly object m_DisposeSyncRoot = new object();
        private bool m_IsDisposed;

        /// <summary>
        /// Finaliser function.
        /// </summary>
        ~NoteTreeNode()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether this object has been disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get { return m_IsDisposed; }
            private set { m_IsDisposed = value; }
        }

        /// <summary>
        /// Throws an exception if this object has been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown if this object has been disposed.</exception>
        protected void CheckDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException("The object has been disposed.");
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up the object.
        /// </summary>
        /// <param name="disposing">If true then called by Dispose function.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (m_DisposeSyncRoot)
            {
                if (!IsDisposed)
                {
                    if (disposing)
                    {
                        // Free other state (managed objects).
                        DisposeManagedResources();
                    }

                    // Free your own state (unmanaged objects).
                    // Set large fields to null.
                    DisposeUnmanagedResources();

                    IsDisposed = true;
                }
            }
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
            Note.PropertyChanged -= NotePropertyChanged;
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        {
        }

        #endregion
    }
}
