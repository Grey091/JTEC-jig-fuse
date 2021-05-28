using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.Models
{
    class FileInputUtil
    {
        public static FileInfo GetFileInfo(string directory, string file)
        {
            var rootDir = GetRootDirectory().FullName;
            return new FileInfo(Path.Combine(rootDir, directory, file));
        }

        public static DirectoryInfo GetRootDirectory()
        {
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            while (!currentDir.EndsWith("bin"))
            {
                currentDir = Directory.GetParent(currentDir).FullName.TrimEnd('\\');
            }
            return new DirectoryInfo(currentDir).Parent;
        }

        public static DirectoryInfo GetSubDirectory(string directory, string subDirectory)
        {
            var currentDir = GetRootDirectory().FullName;
            return new DirectoryInfo(Path.Combine(currentDir, directory, subDirectory));
        }
    }
}
