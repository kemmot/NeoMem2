namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NeoMem2.Utils;

    public abstract class XmlWriter : EncodingWriterBase
    {
        public XmlWriter(TextWriter writer)
            : base(writer)
        {
        }

        public void WriteTag(EncodingTagType tagType)
        {
            Writer.WriteLine("<{0} />", GetTagType(tagType));
        }

        public void WriteTag(string tagName)
        {
            if (tagName == null) throw new ArgumentNullException("tagName");
            if (tagName.Length == 0) throw new ArgumentException("Argument cannot be zero length", "tagName");

            Writer.WriteLine("<{0} />", tagName);
        }

        public override IDisposable WriteOpenTag(string tagName, Dictionary<string, string> attributes = null)
        {
            if (tagName == null) throw new ArgumentNullException(nameof(tagName));
            if (tagName.Length == 0) throw new ArgumentException("Argument cannot be zero length", nameof(tagName));

            Writer.Write("<{0}", tagName);
            if (attributes != null)
            {
                foreach (string attributeName in attributes.Keys)
                {
                    Writer.Write(" {0}=\"{1}\"", attributeName, attributes[attributeName]);
                }
            }
            Writer.WriteLine(">");

            return new TagScope(this, tagName);
        }

        public override void WriteCloseTag(string name)
        {
            Writer.WriteLine("</{0}>", name);
        }

        public override string GetFileExtension()
        {
            return "xml";
        }

        class TagScope : DisposableBase
        {
            private readonly string m_TagName;
            private readonly XmlWriter m_Writer;

            public TagScope(XmlWriter writer, string tagName)
            {
                if (writer == null) throw new ArgumentNullException("writer");

                m_Writer = writer;
                m_TagName = tagName;
            }

            protected override void DisposeManagedResources()
            {
                m_Writer.WriteCloseTag(m_TagName);
                base.DisposeManagedResources();
            }
        }
    }
}
