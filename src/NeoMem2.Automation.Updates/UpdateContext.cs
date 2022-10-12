// <copyright file="UpdateContext.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System;
    using System.Collections.Generic;

    using NeoMem2.Utils;

    /// <summary>
    /// The context for an update.
    /// </summary>
    public class UpdateContext : DisposableBase
    {
        /// <summary>
        /// The backing field for the <see cref="Variables"/> property.
        /// </summary>
        private readonly Dictionary<string, object> variables = new Dictionary<string, object>();

        /// <summary>
        /// Gets the variables stored in this context.
        /// </summary>
        public Dictionary<string, object> Variables
        {
            get { return this.variables; }
        }

        /// <summary>
        /// Gets a string variable value by name.
        /// </summary>
        /// <param name="name">The name of the variable to retrieve.</param>
        /// <returns>The variable value.</returns>
        public string GetVariableString(string name)
        {
            object value = this.GetVariable(name);
            return (string)value;
        }

        /// <summary>
        /// Gets a variable value by name.
        /// </summary>
        /// <param name="name">The name of the variable to retrieve.</param>
        /// <returns>The variable value.</returns>
        public object GetVariable(string name)
        {
            object value;
            if (!this.Variables.TryGetValue(name, out value))
            {
                string message = string.Format(
                    "Variable not found: {0}",
                    name);
                throw new Exception(message);
            }

            return value;
        }

        /// <summary>
        /// Disposes managed resources.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            foreach (object value in this.variables.Values)
            {
                var disposable = value as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            base.DisposeManagedResources();
        }
    }
}
