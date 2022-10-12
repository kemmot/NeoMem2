namespace NeoMem2.Core.Scripting
{
    /// <summary>
    /// The supported script events.
    /// </summary>
    public enum ScriptType
    {
        /// <summary>
        /// A custom script that can be run at any time.
        /// </summary>
        Custom,

        /// <summary>
        /// A script to be run when the main application form loads.
        /// </summary>
        MainFormLoad,

        /// <summary>
        /// A script to be run when a note form loads.
        /// </summary>
        NoteFormLoad,

        /// <summary>
        /// A script to be run when a new note is created.
        /// </summary>
        NewNoteCreated
    }
}