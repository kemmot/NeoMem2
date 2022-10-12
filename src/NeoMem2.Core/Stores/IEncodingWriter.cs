namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IEncodingWriter
    {
       string GetLinkTag(string link, string caption);

        void Write(DataTable table);

        void WriteTag(EncodingTagType tagType);

        void WriteTag(EncodingTagType tagType, string value);

        //void WriteTag(string tagName);

        void WriteTag(string tagName, string value);

        IDisposable WriteOpenTag(EncodingTagType tagType, Dictionary<string, string> attributes = null);

        IDisposable WriteOpenTag(string tagName, Dictionary<string, string> attributes = null);
    }
}
