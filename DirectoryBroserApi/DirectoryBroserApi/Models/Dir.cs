namespace DirectoryBroserApi.Models
{
    public class Dir
    {
        public Dir(string name, string fullName, int id)
        {
            Name = name;
            FullName = fullName;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
