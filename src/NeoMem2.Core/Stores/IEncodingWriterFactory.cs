namespace NeoMem2.Core.Stores
{
    using System.IO;

    public interface IEncodingWriterFactory
    {
        string GetFileExtension();

        IEncodingWriter GetWriter(TextWriter writer);
    }
}
