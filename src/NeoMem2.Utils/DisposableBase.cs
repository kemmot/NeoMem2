// <copyright file="DisposableBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;

    /// <summary>
    /// Provides a standard implementation of the <see cref="IDisposable"/> pattern.
    /// </summary>
    public class DisposableBase : IDisposable
    {
        /// <summary>
        /// A synchronization object for ensuring this object can only be disposed by one thread at a time.
        /// </summary>
        private readonly object disposeSyncRoot = new object();

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableBase"/> class.
        /// </summary>
        ~DisposableBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether this object has been disposed.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Throws an exception if this object has been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown if this object has been disposed.</exception>
        protected void CheckDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("The object has been disposed.");
            }
        }

        /// <summary>
        /// Cleans up the object.
        /// </summary>
        /// <param name="disposing">If true then called by Dispose function.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this.disposeSyncRoot)
            {
                if (!this.IsDisposed)
                {
                    if (disposing)
                    {
                        // Free other state (managed objects).
                        this.DisposeManagedResources();
                    }

                    // Free your own state (unmanaged objects).
                    // Set large fields to null.
                    this.DisposeUnmanagedResources();

                    this.IsDisposed = true;
                }
            }
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        {
        }
    }
}
