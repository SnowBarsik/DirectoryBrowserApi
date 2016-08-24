﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryBroserApi.Models
{
    public class DirectoryTravereser
    {
        private readonly FileCounter _fileCounter = new FileCounter();

        public FileCounter GetFilesCount(int id, CancellationToken cancellationToken)
        {

            var dir = DirectoryBrowser.Directories.SingleOrDefault(d => d.Id == id);
            CountAllFiles(dir.FullName, cancellationToken);

            return _fileCounter;
        }

        private void CountAllFiles(string dir, CancellationToken cancellationToken)
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException();
            }
            dirs.Push(dir);

            _fileCounter.ClearCounters();
            while (dirs.Count > 0)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _fileCounter.ClearCounters();
                    return;
                }


                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }

                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException)
                {
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

                foreach (string file in files)
                {
                    try
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        _fileCounter.AddCount(fi.Length);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        continue;
                    }
                }

                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }

    }
}
