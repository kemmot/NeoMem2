namespace NeoMem2.Core.Stores
{
    using System.IO;

    public class MarkdownEncodingWriterFactory : IEncodingWriterFactory
    {
        public string GetFileExtension()
        {
            return "md";
        }

        public IEncodingWriter GetWriter(TextWriter writer)
        {
            return new MarkdownWriter(writer);
        }
    }
}
