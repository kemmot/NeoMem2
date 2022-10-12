using Markdig;
using System.IO;

namespace NeoMem2.Core.Stores
{
    public class HtmlExporter : EncodingExporterBase
    {
        public HtmlExporter()
            : base(new HtmlEncodingWriterFactory())
        {
        }

        protected override void WriteNoteBody(IEncodingWriter encodingWriter, Note note)
        {
            if (!string.IsNullOrEmpty(note.Text))
            {
                encodingWriter.WriteTag(EncodingTagType.Heading2, "Note Text");

                switch (note.TextFormat)
                {
                    case TextFormat.Markdown:
                        var builder = new MarkdownPipelineBuilder();
                        var extensions = builder.UseAdvancedExtensions();
                        var pipeline = extensions.Build();
                        string markdownhtml = Markdown.ToHtml(note.Text, pipeline);
                        encodingWriter.WriteTag(EncodingTagType.Section, markdownhtml);
                        break;
                    default:
                        StringReader reader = new StringReader(note.Text);
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            encodingWriter.WriteTag(EncodingTagType.Paragraph, line);
                            encodingWriter.WriteTag(EncodingTagType.Line);
                        }
                        break;
                }
            }
        }
    }
}
