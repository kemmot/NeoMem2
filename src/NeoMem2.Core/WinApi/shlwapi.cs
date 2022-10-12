using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NeoMem2.Core.WinApi
{
    public class shlwapi
    {
        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shlwapi.dll", SetLastError = true)]
        private static extern int PathRelativePathTo(StringBuilder pszPath, string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);

        public static string GetRelativePathFromFolderToFile(string fromPath, string toPath)
        {
            return GetRelativePathInternal(fromPath, FILE_ATTRIBUTE_DIRECTORY, toPath, FILE_ATTRIBUTE_NORMAL);
        }

        public static string GetRelativePath(string fromPath, string toPath)
        {
            int fromAttr = GetPathAttribute(fromPath);
            int toAttr = GetPathAttribute(toPath);
            return GetRelativePathInternal(fromPath, fromAttr, toPath, toAttr);
        }

        private static string GetRelativePathInternal(string fromPath, int fromAttr, string toPath, int toAttr)
        {

            StringBuilder path = new StringBuilder(260); // MAX_PATH
            if (PathRelativePathTo(
                path,
                fromPath,
                fromAttr,
                toPath,
                toAttr) == 0)
            {
                throw new ArgumentException(string.Format("Paths must have a common prefix [{0}] - [{1}]", fromPath, toPath));
            }

            return path.ToString();
        }

        private static int GetPathAttribute(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                return FILE_ATTRIBUTE_DIRECTORY;
            }

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                return FILE_ATTRIBUTE_NORMAL;
            }

            throw new FileNotFoundException("File not found: " + path, path);
        }
    }
}
