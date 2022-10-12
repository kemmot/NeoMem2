using System.IO;

namespace NeoMem2.Core
{
    public static class TextFormatInspector
    {
        public static TextFormat GetTextFormat(string path)
        {
            TextFormat format;

            string extension = Path.GetExtension(path);
            switch (extension.ToUpper())
            {
                case ".CS":
                    format = TextFormat.SourceCodeCSharp;
                    break;
                case ".CPP":
                    format = TextFormat.SourceCodeCPP;
                    break;
                case ".CSS":
                    format = TextFormat.SourceCodeCSS;
                    break;
                case ".BAT":
                case ".CMD":
                    format = TextFormat.SourceCodeDOSBatch;
                    break;
                case ".HTML":
                    format = TextFormat.SourceCodeHTML;
                    break;
                case ".JAVA":
                    format = TextFormat.SourceCodeJava;
                    break;
                case ".JS":
                    format = TextFormat.SourceCodeJavaScript;
                    break;
                case ".MD":
                case ".MARKDOWN":
                    format = TextFormat.Markdown;
                    break;
                case ".PHP":
                    format = TextFormat.SourceCodePHP;
                    break;
                case ".PL":
                    format = TextFormat.SourceCodePerl;
                    break;
                case ".PY":
                    format = TextFormat.SourceCodePython;
                    break;
                case ".RTF":
                    format = TextFormat.Rtf;
                    break;
                case ".SQL":
                    format = TextFormat.SourceCodeTSQL;
                    break;
                case ".CSPROJ":
                case ".PROJ":
                case ".SETTINGS":
                case ".TARGETS":
                case ".XML":
                    format = TextFormat.SourceCodeXML;
                    break;
                case ".LOG":
                case ".TXT":
                default:
                    format = TextFormat.Txt;
                    break;
            }

            return format;
        }
    }
}
