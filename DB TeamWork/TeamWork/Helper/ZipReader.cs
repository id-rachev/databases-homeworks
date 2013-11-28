using System;
using Ionic.Zip;
using System.Collections.Generic;
using System.IO;

namespace Helper
{
    public static class ZipReader
    {
        public static List<string> ExtractZip(string filename, string destinationDirectory)
        {
            List<string> filePaths = new List<string>();

            if (Directory.Exists(destinationDirectory))
            {
                Directory.Delete(destinationDirectory, true);
            }

            using (ZipFile file = ZipFile.Read(filename))
            {
                file.ExtractAll(destinationDirectory);

                string fileExtension = ".xls";

                foreach (var zipEntity in file)
                {
                    int index = zipEntity.FileName.Length - 4;

                    if (zipEntity.FileName.Substring(index) == fileExtension)
                    {
                        filePaths.Add(zipEntity.FileName);
                    }
                }
            }

            return filePaths;
        }

        public static List<string> GetDates(string path)
        {
            List<string> dates = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] folders = dir.GetDirectories();

            foreach (var folder in folders)
            {
                dates.Add(folder.Name);
            }

            return dates;
        }
    }
}
