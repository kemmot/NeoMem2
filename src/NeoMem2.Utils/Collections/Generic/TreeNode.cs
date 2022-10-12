// <copyright file="TreeNode.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// A basic implementation of a tree node.
    /// </summary>
    /// <typeparam name="T">The type of data to hold in the tree.</typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// The backing field for the <see cref="Children"/> property.
        /// </summary>
        private readonly List<T> children = new List<T>();

        /// <summary>
        /// Gets the child nodes of this node.
        /// </summary>
        public List<T> Children
        {
            get { return this.children; }
        }

        /// <summary>
        /// Gets or sets this nodes parent node.
        /// </summary>
        public T Parent { get; set; }
    }
}
