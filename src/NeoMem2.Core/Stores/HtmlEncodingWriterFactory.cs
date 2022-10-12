namespace NeoMem2.Core.Stores
{
    using System.IO;

    public class HtmlEncodingWriterFactory : IEncodingWriterFactory
    {
        public string GetFileExtension()
        {
            return "html";
        }

        public IEncodingWriter GetWriter(TextWriter writer)
        {
            return new HtmlWriter(writer);
        }
    }
}
