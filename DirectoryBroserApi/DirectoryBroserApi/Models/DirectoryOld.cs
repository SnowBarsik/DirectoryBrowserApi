using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryBroserApi.Models
{
    public class DirectoryOld
    {
        private static readonly Dictionary<int, string> DirNames = new Dictionary<int, string>();
        private static int _idCounter = 0;
        public CurrentDirectory CurrentDirectory { get; set; }
        public DirectoryOld()
        {
            CurrentDirectory = new CurrentDirectory();
        }
        //public CurrentDirectory GetHardDrives()
        //{
        //    System.IO.DriveInfo[] di = System.IO.DriveInfo.GetDrives();

        //    CurrentDirectory.ClearCurrentDir();
        //    foreach (var drive in di)
        //    {
        //        if (!drive.IsReady && drive.DriveType != System.IO.DriveType.Fixed)
        //        {
        //            continue;
        //        }

        //        DirNames.Add(_idCounter, drive.Name);
        //        CurrentDirectory.CurrentDirs.Add(new Dir(drive.Name, drive.Name, _idCounter));
        //        _idCounter += 1;
        //    }

        //    CurrentDirectory.CurrentPath = "Root";

        //    return CurrentDirectory;
        //}

        //public CurrentDirectory GetDirectory(int id)
        //{
        //    var dir = DirNames.Where(d => d.Key == id).Select(d => d.Value).SingleOrDefault();
        //    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(dir);
        //    System.IO.DirectoryInfo[] subDirInfos = dirInfo.GetDirectories();
        //    System.IO.FileInfo[] fileInfos = dirInfo.GetFiles();

        //    CurrentDirectory.ClearCurrentDir();

        //    foreach (var subDir in subDirInfos)
        //    {
        //        CurrentDirectory.CurrentDirs.Add(new Dir(subDir.Name, subDir.FullName, _idCounter));
        //        DirNames.Add(_idCounter, subDir.FullName);
        //        _idCounter += 1;
        //    }

        //    foreach (var fileInfo in fileInfos)
        //    {
        //        CurrentDirectory.CurrentFiles.Add(new File(fileInfo.Name, fileInfo.FullName));
        //    }

        //    CurrentDirectory.CurrentPath = dirInfo.FullName;
        //    if (dirInfo.Parent != null)
        //    {
        //        CurrentDirectory.ParentDir = dirInfo.Parent.ToString();
        //    }

        //    CountAllFiles(dir);

        //    return CurrentDirectory;
        //}

        private void CountAllFiles(string dir)
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(dir))
            {
                throw new ArgumentException();
            }
            dirs.Push(dir);

            CurrentDirectory.FileCounter.ClearCounters();
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The 
                // choice of which exceptions to catch depends entirely on the specific task 
                // you are intending to perform and also on how much you know with certainty 
                // about the systems on which this code will run.
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Perform the required action on each file here.
                // Modify this block to perform your required task.

                foreach (string file in files)
                {
                    try
                    {
                        // Perform whatever action is required in your scenario.
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        CurrentDirectory.FileCounter.AddCount(fi.Length);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }
    }
}
