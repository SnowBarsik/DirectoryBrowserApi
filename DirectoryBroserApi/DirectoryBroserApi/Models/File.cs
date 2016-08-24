namespace DirectoryBroserApi.Models
{
    public class File
    {
        public File(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
        }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
