using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Utility
{
    public class ZipHelper
    {
        public static void CompressFolder(string fileName,ZipOutputStream zipOutputStream)
        {
            var fileInfo = new FileInfo(fileName);

            var entry = new ZipEntry(fileInfo.Name);

            entry.DateTime = fileInfo.LastWriteTime;

            entry.Size = fileInfo.Length;

            zipOutputStream.PutNextEntry(entry);

            byte[] buffer = new byte[4096];

            using (FileStream fs = File.OpenRead(fileName))
            {
                StreamUtils.Copy(fs, zipOutputStream, buffer);
            }

            zipOutputStream.CloseEntry();
        }
    }
}
