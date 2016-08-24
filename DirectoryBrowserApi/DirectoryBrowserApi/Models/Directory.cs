using System.Collections.Generic;
using System.IO;

namespace DirectoryBrowserApi.Models
{
    public class Directory
    {
        private static int _counter = 0;
        private Directory()
        {
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }

        public static Directory DirFactory(string fullName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fullName);
            Directory newDir = new Directory
            {
                FullName = dirInfo.FullName,
                Name = dirInfo.Name,
                Id = _counter
            };
            _counter++;

            return newDir;
        }

        public static Directory DirFactory(DirectoryInfo dirInfo)
        {
            Directory newDir = new Directory
            {
                FullName = dirInfo.FullName,
                Name = dirInfo.Name,
                Id = _counter
            };
            _counter++;

            return newDir;
        }

        public static Directory DirFactory(string fullName, int id)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fullName);
            Directory newDir = new Directory
            {
                FullName = dirInfo.FullName,
                Name = dirInfo.Name,
                Id = id
            };
            return newDir;
        }

        public static List<Directory> DirListFactory(string fullName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fullName);
            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();

            List<Directory> dirs = new List<Directory>();

            foreach (var dir in dirInfos)
            {
                dirs.Add(DirFactory(dir));
            }

            return dirs;
        }
    }
}
