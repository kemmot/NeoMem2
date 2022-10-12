// <copyright file="IObjectDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    /// <summary>
    /// The interface that must be implemented to provide object specific data access.
    /// </summary>
    /// <typeparam name="T">The type of object handled by this class.</typeparam>
    public interface IObjectDataAccess<in T>
    {
        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        void Delete(T item);

        /// <summary>
        /// Saves any changes to the specified item's state, performing whatever actions are required.
        /// </summary>
        /// <param name="item">The item to save.</param>
        void Save(T item);

        /// <summary>
        /// Saves any changes to the specified item's state, performing whatever actions are required.
        /// </summary>
        /// <param name="item">The item to save.</param>
        /// <param name="context">The database context.</param>
        void Save(T item, AdoContext context);
    }
}