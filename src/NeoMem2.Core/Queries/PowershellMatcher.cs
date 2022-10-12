// <copyright file="PowershellMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    using System;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;

    /// <summary>
    /// A matcher that uses a PowerShell query.
    /// </summary>
    public class PowerShellMatcher : INoteMatcher
    {
        /// <summary>
        /// The backing field for the <see cref="Command"/> property.
        /// </summary>
        private readonly string command;

        /// <summary>
        /// The run space to use.
        /// </summary>
        private readonly Runspace runspace = RunspaceFactory.CreateRunspace();

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellMatcher" /> class.
        /// </summary>
        /// <param name="command">The PowerShell command to use.</param>
        public PowerShellMatcher(string command)
        {
            this.command = command;

            this.runspace.Open();
        }

        /// <summary>
        /// Gets the PowerShell command to use.
        /// </summary>
        public string Command
        {
            get { return this.command; }
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        public bool IsMatch(Note note)
        {
            PSObject noteObject = new PSObject(note);
            foreach (var noteProperty in note.Properties)
            {
                noteObject.Properties.Add(new PSNoteProperty(noteProperty.Name, noteProperty.Value));
            }

            this.runspace.SessionStateProxy.SetVariable("note", note);

            Pipeline pipeline = this.runspace.CreatePipeline();
            pipeline.Commands.AddScript(this.command);

            bool isMatch = false;
            try
            {
                Collection<PSObject> results = pipeline.Invoke();
                foreach (var result in results)
                {
                    if (result.BaseObject is bool)
                    {
                        isMatch = (bool)result.BaseObject;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    "Failed running powershell command: {0}",
                    this.command);
                throw new Exception(message, ex);
            }

            return isMatch;
        }
    }
}
