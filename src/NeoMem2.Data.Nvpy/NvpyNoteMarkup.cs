using System.Runtime.Serialization;

namespace NeoMem2.Data.Nvpy
{
    // ReSharper disable InconsistentNaming
    [DataContract]
    internal class NvpyNoteMarkup
    {
        [DataMember]
        internal double modifydate;

        [DataMember]
        internal string[] tags = new string[0];

        [DataMember]
        internal double createdate;

        [DataMember]
        internal double syncdate;

        [DataMember]
        internal string[] systemtags = new string[0];

        [DataMember]
        internal string content = string.Empty;

        [DataMember]
        internal double savedate;
    }
    // ReSharper restore InconsistentNaming
}
