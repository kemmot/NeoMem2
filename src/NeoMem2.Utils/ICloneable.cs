// <copyright file="ICloneable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    /// <summary>
    /// A generically typed version of the <see cref="System.ICloneable"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of object being cloned.</typeparam>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Creates a clone of the current object.
        /// </summary>
        /// <param name="deepCopy">Whether to perform a deep copy of any referenced objects.</param>
        /// <returns>The cloned object.</returns>
        T Clone(bool deepCopy);
    }
}
