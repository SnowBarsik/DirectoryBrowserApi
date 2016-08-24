using System.Collections.Generic;

namespace DirectoryBroserApi.Models
{
    public class CurrentDirectory
    {
        public CurrentDirectory()
        {
            CurrentDirs = new List<Directory>();
            CurrentFiles = new List<string>();
            FileCounter = new FileCounter();
            Path = new List<Directory>();
        }

        public int? Id { get; set; }
        public List<Directory> CurrentDirs { get; set; }
        public List<string> CurrentFiles { get; set; }
        public List<Directory> Path { get; set; }
        public string FullName { get; set; }
        public int? ParentId { get; set; }
        public FileCounter FileCounter { get; set; }



    }
}
