namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    public class MarkdownWriter : EncodingWriterBase, IEncodingWriter
    {
        public MarkdownWriter(TextWriter writer)
            : base(writer)
        {
        }

        public void Write(DataTable table)
        {
            var columnNames = new List<string>();
            var lineItems = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                columnNames.Add(column.ColumnName);
                lineItems.Add("-");
            }
            
            Writer.WriteLine();
            Writer.WriteLine("{border=\"1\"}");
            WriteRow(columnNames.ToArray());
            WriteRow(lineItems.ToArray());

            foreach (DataRow row in table.Rows)
            {
                WriteRow(row);
            }
            
            Writer.WriteLine();
        }

        private void WriteRow(DataRow row)
        {
            WriteRow(row.ItemArray);
        }

        private void WriteRow(object[] cellValues)
        {
            var output = new StringBuilder();
            if (cellValues.Length > 0)
            {
                output.Append("|");
                foreach (object cellValue in cellValues)
                {
                    string cellValueString = cellValue == null ? string.Empty : cellValue?.ToString();
                    output.AppendFormat("{0}|", cellValueString);
                }
            }

            Writer.WriteLine(output);
        }

        public override string GetLinkTag(string link, string caption)
        {
            return string.Format("[{0}]({1})", caption, link);
        }

        public override void WriteCloseTag(string name)
        {
            // nothing to do
        }

        public override IDisposable WriteOpenTag(string tagName, Dictionary<string, string> attributes = null)
        {
            WriteTag(tagName);
            return new NullScope();
        }

        public override string GetFileExtension()
        {
            return "md";
        }

        protected override string GetTagTypeOpen(EncodingTagType tagType)
        {
            return GetTagType(tagType);
        }

        protected override string GetTagTypeClose(EncodingTagType tagType)
        {
            return string.Empty;
        }

        protected override string GetTagType(EncodingTagType tagType)
        {
            string tag;
            switch (tagType)
            {
                case EncodingTagType.Body:
                    tag = Environment.NewLine;
                    break;
                case EncodingTagType.Heading1:
                    tag = "# ";
                    break;
                case EncodingTagType.Heading2:
                    tag = "## ";
                    break;
                case EncodingTagType.Line:
                    tag = string.Empty;
                    break;
                case EncodingTagType.ListItem:
                    tag = "* ";
                    break;
                case EncodingTagType.Paragraph:
                    tag = Environment.NewLine;
                    break;
                case EncodingTagType.Section:
                    tag = Environment.NewLine;
                    break;
                case EncodingTagType.UnorderedList:
                    tag = "";
                    break;
                default:
                    throw new NotSupportedException("Tag type not supported: " + tagType);
            }

            return tag;
        }

        public void WriteTag(EncodingTagType tagType)
        {
            WriteTag(GetTagType(tagType));
        }

        public void WriteTag(string tagName)
        {
            this.Writer.WriteLine(tagName);
        }

        class NullScope : IDisposable
        {

            #region IDisposable Functions

            private readonly object m_DisposeSyncRoot = new object();
            private bool m_IsDisposed;

            /// <summary>
            /// Finaliser function.
            /// </summary>
            ~NullScope()
            {
                Dispose(false);
            }

            /// <summary>
            /// Gets a value indicating whether this object has been disposed.
            /// </summary>
            protected bool IsDisposed
            {
                get { return m_IsDisposed; }
                private set { m_IsDisposed = value; }
            }

            /// <summary>
            /// Throws an exception if this object has been disposed.
            /// </summary>
            /// <exception cref="ObjectDisposedException">Thrown if this object has been disposed.</exception>
            protected void CheckDisposed()
            {
                if (IsDisposed) throw new ObjectDisposedException("The object has been disposed.");
            }

            /// <summary>
            /// Disposes the object.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Cleans up the object.
            /// </summary>
            /// <param name="disposing">If true then called by Dispose function.</param>
            protected virtual void Dispose(bool disposing)
            {
                lock (m_DisposeSyncRoot)
                {
                    if (!IsDisposed)
                    {
                        if (disposing)
                        {
                            // Free other state (managed objects).
                            DisposeManagedResources();
                        }

                        // Free your own state (unmanaged objects).
                        // Set large fields to null.
                        DisposeUnmanagedResources();

                        IsDisposed = true;
                    }
                }
            }

            /// <summary>
            /// Dispose managed resources.
            /// </summary>
            protected virtual void DisposeManagedResources()
            {
            }

            /// <summary>
            /// Disposes the unmanaged resources.
            /// </summary>
            protected virtual void DisposeUnmanagedResources()
            {
            }

            #endregion

        }
    }
}
