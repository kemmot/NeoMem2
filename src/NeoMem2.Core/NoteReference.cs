using System;

namespace NeoMem2.Core
{
    /// <summary>
    /// A reference to a note object.
    /// </summary>
    /// <remarks>
    /// Note that this class is serializable so that it can be used with the windows clipboard.
    /// </remarks>
    [Serializable]
    public class NoteReference
    {
        public long NoteId;
    }
}
