namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class EncodingWriterBase
    {
        protected TextWriter Writer { get; }

        public EncodingWriterBase(TextWriter writer)
        {
            if (writer == null) throw new ArgumentNullException("writer");

            Writer = writer;
        }

        public void WriteTag(EncodingTagType tagType, string content)
        {
            Writer.Write(GetTagTypeOpen(tagType));
            Writer.Write(content);
            Writer.Write(GetTagTypeClose(tagType));
            Writer.WriteLine();
        }

        public void WriteTag(string tagName, string content)
        {
            using (WriteOpenTag(tagName))
            {
                Writer.WriteLine(content);
            }
        }

        public abstract string GetLinkTag(string link, string caption);

        public IDisposable WriteOpenTag(EncodingTagType tagType, Dictionary<string, string> attributes = null)
        {
            return WriteOpenTag(GetTagType(tagType), attributes);
        }

        public abstract IDisposable WriteOpenTag(string tagName, Dictionary<string, string> attributes = null);

        public void WriteCloseTag(EncodingTagType tagType)
        {
            WriteCloseTag(GetTagTypeClose(tagType));
        }

        public abstract string GetFileExtension();

        public abstract void WriteCloseTag(string name);

        protected abstract string GetTagTypeOpen(EncodingTagType tagType);

        protected abstract string GetTagTypeClose(EncodingTagType tagType);

        protected abstract string GetTagType(EncodingTagType tagType);
    }
}
