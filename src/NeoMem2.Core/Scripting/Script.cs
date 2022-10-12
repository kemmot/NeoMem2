namespace NeoMem2.Core.Scripting
{
    /// <summary>
    /// The details of a script to run.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Script" /> class.
        /// </summary>
        public Script()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Script" /> class.
        /// </summary>
        /// <param name="scriptType">The script event type that the script is intended to handle.</param>
        /// <param name="filename">The file name containing the script.</param>
        public Script(ScriptType scriptType, string filename)
        {
            this.ScriptType = scriptType;
            this.Filename = filename;
        }

        /// <summary>
        /// Gets or sets the file name containing the script.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the script event type that the script is intended to handle.
        /// </summary>
        public ScriptType ScriptType { get; set; }
    }
}