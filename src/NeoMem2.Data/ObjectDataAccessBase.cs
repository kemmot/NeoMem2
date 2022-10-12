// <copyright file="ObjectDataAccessBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;

    using NLog;

    /// <summary>
    /// A base class implementation of <see cref="IObjectDataAccess{T}"/> providing functionality common to all implementations.
    /// </summary>
    /// <typeparam name="T">The type of object to deal with.</typeparam>
    public abstract class ObjectDataAccessBase<T> : IObjectDataAccess<T>
    {
        /// <summary>
        /// The backing field for the <see cref="AdoDataAccess"/> property.
        /// </summary>
        private readonly IAdoDataAccess adoDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDataAccessBase{T}" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        protected ObjectDataAccessBase(IAdoDataAccess adoDataAccess)
        {
            if (adoDataAccess == null)
            {
                throw new ArgumentNullException("adoDataAccess");
            }

            this.adoDataAccess = adoDataAccess;
        }

        /// <summary>
        /// Gets the data access object to use.
        /// </summary>
        protected IAdoDataAccess AdoDataAccess
        {
            get { return this.adoDataAccess; }
        }

        /// <summary>
        /// Saves any changes to the specified item's state, performing whatever actions are required.
        /// </summary>
        /// <param name="item">The item to save.</param>
        public void Save(T item)
        {
            using (AdoContext context = this.AdoDataAccess.CreateContext())
            {
                this.Save(item, context);
            }
        }

        /// <summary>
        /// Saves any changes to the specified item's state, performing whatever actions are required.
        /// </summary>
        /// <param name="item">The item to save.</param>
        /// <param name="context">The database context.</param>
        public void Save(T item, AdoContext context)
        {
            using (NestedDiagnosticsContext.Push(string.Format("Saving {0}: {1}", item.GetType().Name, item)))
            {
                this.PreSave(item);

                if (this.IsNew(item))
                {
                    if (!this.IsDeleted(item))
                    {
                        this.Insert(item);
                    }
                }
                else if (this.IsDeleted(item))
                {
                    this.Delete(item);
                }
                else if (this.IsDirty(item))
                {
                    this.Update(item);
                }

                this.PostSave(item);
            }
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public virtual void Delete(T item)
        {
            using (AdoContext context = this.AdoDataAccess.CreateContext())
            {
                this.Delete(item, context);
            }
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <param name="context">The database context.</param>
        public abstract void Delete(T item, AdoContext context);


        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        protected virtual void Insert(T item)
        {
            using (AdoContext context = this.AdoDataAccess.CreateContext())
            {
                this.Insert(item, context);
            }
        }

        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        /// <param name="context">The database context.</param>
        protected abstract void Insert(T item, AdoContext context);

        /// <summary>
        /// Gets a value indicating whether the item has been deleted.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True if the item has been deleted; false otherwise.</returns>
        protected abstract bool IsDeleted(T item);

        /// <summary>
        /// Gets a value indicating whether the item's state has changed since it was last saved.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True if the item's state has changed; false otherwise.</returns>
        protected abstract bool IsDirty(T item);

        /// <summary>
        /// Gets a value indicating whether the item is new and has never been saved.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True if the item is new; false otherwise.</returns>
        protected abstract bool IsNew(T item);

        /// <summary>
        /// Performs any pre-save work.
        /// </summary>
        /// <param name="item">The item about to be saved.</param>
        protected virtual void PreSave(T item)
        {
        }

        /// <summary>
        /// Performs any post-save work.
        /// </summary>
        /// <param name="item">The item that was saved.</param>
        protected virtual void PostSave(T item)
        {
        }

        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="item">The item to update.</param>
        protected virtual void Update(T item)
        {
            using (AdoContext context = this.AdoDataAccess.CreateContext())
            {
                this.Update(item, context);
            }
        }

        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <param name="context">The database context.</param>
        protected abstract void Update(T item, AdoContext context);
    }
}