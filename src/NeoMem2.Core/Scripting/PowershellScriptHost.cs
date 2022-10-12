// <copyright file="PowershellScriptHost.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// An implementation of <see cref="IScriptHost"/> that supports PowerShell commands.
    /// </summary>
    public class PowerShellScriptHost : IScriptHost
    {
        /// <summary>
        /// The name of the script for the <see cref="ScriptType.MainFormLoad"/> event.
        /// </summary>
        private const string MainFormLoadScript = "MainFormLoad.ps1";

        /// <summary>
        /// The name of the script for the <see cref="ScriptType.NoteFormLoad"/> event.
        /// </summary>
        private const string NoteFormLoadScript = "NoteFormLoad.ps1";

        /// <summary>
        /// The name of the script for the <see cref="ScriptType.NewNoteCreated"/> event.
        /// </summary>
        private const string NewNoteCreatedScript = "NewNoteCreated.ps1";

        /// <summary>
        /// The name of the scripts folder.
        /// </summary>
        private const string ScriptFolder = "Scripts";

        /// <summary>
        /// The scripts to use for supported script events.
        /// </summary>
        private readonly Dictionary<ScriptType, Script> m_WellKnownScripts = new Dictionary<ScriptType, Script> 
            { 
                {ScriptType.MainFormLoad, new Script(ScriptType.MainFormLoad, GetScriptFile(MainFormLoadScript))},
                {ScriptType.NoteFormLoad, new Script(ScriptType.NoteFormLoad, GetScriptFile(NoteFormLoadScript))},
                {ScriptType.NewNoteCreated, new Script(ScriptType.NewNoteCreated, GetScriptFile(NewNoteCreatedScript))}
            };

        /// <summary>
        /// The PowerShell runspace to use.
        /// </summary>
        private readonly Runspace m_Runspace;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellScriptHost" /> class.
        /// </summary>
        /// <param name="variables">The variables to inject into the script.</param>
        public PowerShellScriptHost(IDictionary<string, object> variables = null)
        {
            m_Runspace = RunspaceFactory.CreateRunspace();
            m_Runspace.ThreadOptions = PSThreadOptions.UseCurrentThread;
            m_Runspace.Open();

            SetVariables(variables);
        }
        
        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="scriptType">The type of script to run.</param>
        /// <param name="target">The target of the script event.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(ScriptType scriptType, object target)
        {
            Dictionary<string, object> variables = new Dictionary<string, object>();
            switch (scriptType)
            {
                case ScriptType.MainFormLoad:
                    variables[ScriptVariableNames.MainForm] = target;
                    break;
                case ScriptType.NewNoteCreated:
                    variables[ScriptVariableNames.Note] = target;
                    break;
                case ScriptType.NoteFormLoad:
                    variables[ScriptVariableNames.CurrentNoteForm] = target;
                    break;
            }

            return ExecuteScript(scriptType, variables);
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="scriptType">The type of script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(ScriptType scriptType, IDictionary<string, object> variables = null)
        {
            return ExecuteScript(m_WellKnownScripts[scriptType], variables);
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="script">The script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(Script script, IDictionary<string, object> variables = null)
        {
            return ExecuteScript(script.Filename, variables);
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="filename">The file name of the script to run.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult ExecuteScript(string filename, IDictionary<string, object> variables = null)
        {
            string filenameToUse = filename;

            if (!File.Exists(filenameToUse))
            {
                filenameToUse = GetScriptFile(filenameToUse);
            }

            if (!File.Exists(filenameToUse))
            {
                string message = string.Format(
                    "Script file not found: {0}",
                    filename);
                throw new FileNotFoundException(message, filename);
            }

            string scriptText = File.ReadAllText(filenameToUse);
            return Execute(new ScriptArguments { ScriptText = scriptText }, variables);
        }

        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="arguments">The script arguments to use.</param>
        /// <param name="variables">The variables to inject into the script.</param>
        /// <returns>The result of the script execution.</returns>
        public ScriptResult Execute(ScriptArguments arguments, IDictionary<string, object> variables = null)
        {
            SetVariables(variables);

            Pipeline pipeline = m_Runspace.CreatePipeline();
            pipeline.Commands.AddScript(arguments.ScriptText);

            // add an extra command to transform the script
            // output objects into nicely formatted strings
            // remove this line to get the actual objects
            // that the script returns. For example, the script
            // "Get-Process" returns a collection
            // of System.Diagnostics.Process instances.
            pipeline.Commands.Add("Out-String");

            // execute the script
            var result = new ScriptResult();
            try
            {
                Collection<PSObject> results = pipeline.Invoke();

                // convert the script result into a single string
                StringBuilder output = new StringBuilder();
                foreach (PSObject obj in results)
                {
                    output.AppendLine(obj.ToString());
                }

                result.Output = output.ToString();
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    "Failed running powershell command: {0}",
                    arguments.ScriptText);
                throw new Exception(message, ex);
                //result.Exception = ex;
            }

            return result;
        }

        /// <summary>
        /// Sets the value of the specified variables in the scripting environment.
        /// </summary>
        /// <param name="variables">The variables to set.</param>
        private void SetVariables(IDictionary<string, object> variables)
        {
            if (variables != null)
            {
                foreach (var variableName in variables.Keys)
                {
                    SetVariable(variableName, variables[variableName]);
                }
            }
        }

        /// <summary>
        /// Sets the value of a variable in the scripting environment.
        /// </summary>
        /// <param name="name">The name of the variable to set.</param>
        /// <param name="value">The value to set to the variable.</param>
        private void SetVariable(string name, object value)
        {
            m_Runspace.SessionStateProxy.SetVariable(name, value);
        }

        /// <summary>
        /// Gets the fully qualified path to a script file from the script folder.
        /// </summary>
        /// <param name="filename">The name of the file to get.</param>
        /// <returns>The fully qualified file path.</returns>
        private static string GetScriptFile(string filename)
        {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), 
                ScriptFolder, 
                filename);
        }

        /// <summary>
        /// Gets the available scripts.
        /// </summary>
        /// <returns>The available scripts.</returns>
        public IEnumerable<Script> GetAvailableScripts()
        {
            var availableScripts = new Dictionary<string, Script>();
            foreach (var script in m_WellKnownScripts.Values)
            {
                availableScripts[script.Filename] = script;
            }

            string scriptsFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                ScriptFolder);
            foreach (var scriptFilename in Directory.GetFiles(scriptsFolder, "*.ps1"))
            {
                if (!availableScripts.ContainsKey(scriptFilename))
                {
                    availableScripts[scriptFilename] = new Script(ScriptType.Custom, scriptFilename);
                }
            }

            return availableScripts.Values;
        }
    }
}
