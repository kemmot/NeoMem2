namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;

    public class HtmlWriter : XmlWriter, IEncodingWriter
    {
        public HtmlWriter(TextWriter writer)
            : base(writer)
        {
        }

        public void Write(DataTable table)
        {
            using (WriteOpenTag("TABLE", new System.Collections.Generic.Dictionary<string, string> { { "border", "1" } }))
            {
                var columnNames = new List<string>();
                foreach (DataColumn column in table.Columns)
                {
                    columnNames.Add(column.ColumnName);
                }

                WriteRow(columnNames.ToArray());

                foreach (DataRow row in table.Rows)
                {
                    WriteRow(row);
                }
            }
        }

        private void WriteRow(DataRow row)
        {
            WriteRow(row.ItemArray);
        }

        private void WriteRow(object[] cellValues)
        {
            using (WriteOpenTag("TR"))
            {
                foreach (object cellValue in cellValues)
                {
                    string cellValueString = cellValue == null ? string.Empty : cellValue?.ToString();
                    WriteTag("TD", cellValueString);
                }
            }
        }

        public override string GetLinkTag(string link, string caption)
        {
            return string.Format("<a href=\"{0}\">{1}</a>", link, caption);
        }

        protected override string GetTagTypeOpen(EncodingTagType tagType)
        {
            return string.Format("<{0}>", GetTagType(tagType));
        }

        protected override string GetTagTypeClose(EncodingTagType tagType)
        {
            return string.Format("</{0}>", GetTagType(tagType));
        }

        protected override string GetTagType(EncodingTagType tagType)
        {
            string tag;
            switch (tagType)
            {
                case EncodingTagType.Body:
                    tag = "BODY";
                    break;
                case EncodingTagType.Heading1:
                    tag = "H1";
                    break;
                case EncodingTagType.Heading2:
                    tag = "H2";
                    break;
                case EncodingTagType.Line:
                    tag = "BR";
                    break;
                case EncodingTagType.Link:
                    tag = "BR";
                    break;
                case EncodingTagType.ListItem:
                    tag = "LI";
                    break;
                case EncodingTagType.Paragraph:
                    tag = "PARA";
                    break;
                case EncodingTagType.Section:
                    tag = "DIV";
                    break;
                case EncodingTagType.UnorderedList:
                    tag = "UL";
                    break;
                default:
                    throw new NotSupportedException("Tag type not supported: " + tagType);
            }
            return tag;
        }
    }
}
