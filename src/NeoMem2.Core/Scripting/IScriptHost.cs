// <copyright file="IScriptHost.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Scripting
{
    using System.Collections.Generic;

    /// <summary>
    /// The interface that must be implemented to provide script hosting.
    /// </summary>
    public interface IScriptHost
    {
        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="scriptType">The type of script to run.</param>
        /// <param name="target">The target of the script event.</param>
        /// <returns>The result of the script execution.</returns>
        ScriptResult ExecuteScript(ScriptType scriptType, object target);

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="scriptType">The type of script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        ScriptResult ExecuteScript(ScriptType scriptType, IDictionary<string, object> variables = null);

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="script">The script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        ScriptResult ExecuteScript(Script script, IDictionary<string, object> variables = null);

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="arguments">The script arguments to use.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        ScriptResult Execute(ScriptArguments arguments, IDictionary<string, object> variables = null);

        /// <summary>
        /// Gets the available scripts.
        /// </summary>
        /// <returns>The available scripts.</returns>
        IEnumerable<Script> GetAvailableScripts();
    }
}
