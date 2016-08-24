namespace DirectoryBrowserApi.Models
{
    public class FileCounter
    {
        public FileCounter()
        {
            Smalls = 0;
            Mediums = 0;
            Bigs = 0;
        }

        public int Smalls { get; set; }
        public int Mediums { get; set; }
        public int Bigs { get; set; }

        public void ClearCounters()
        {
            Smalls = 0;
            Mediums = 0;
            Bigs = 0;
        }

        public void AddCount(long size)
        {
            int mb = (int)(size / 1048576);

            if (mb < 10)
            {
                Smalls += 1;
            }
            else if (mb > 10 && mb <= 50)
            {
                Mediums += 1;
            }
            else if (mb > 100)
            {
                Bigs += 1;
            }
        }
    }
}
