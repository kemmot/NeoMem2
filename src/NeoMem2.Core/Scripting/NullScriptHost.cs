using System.Collections.Generic;

namespace NeoMem2.Core.Scripting
{
    /// <summary>
    /// A script host that performs no actions.
    /// </summary>
    public class NullScriptHost : IScriptHost
    {
        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="scriptType">The type of script to run.</param>
        /// <param name="target">The target of the script event.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(ScriptType scriptType, object target)
        {
            return new ScriptResult();
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="scriptType">The type of script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(ScriptType scriptType, IDictionary<string, object> variables = null)
        {
            return new ScriptResult();
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="script">The script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(Script script, IDictionary<string, object> variables = null)
        {
            return new ScriptResult();
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="arguments">The script arguments to use.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult Execute(ScriptArguments arguments, IDictionary<string, object> variables = null)
        {
            return new ScriptResult();
        }

        /// <summary>
        /// Gets the available scripts.
        /// </summary>
        /// <returns>The available scripts.</returns>
        public IEnumerable<Script> GetAvailableScripts()
        {
            yield break;
        }
    }
}
