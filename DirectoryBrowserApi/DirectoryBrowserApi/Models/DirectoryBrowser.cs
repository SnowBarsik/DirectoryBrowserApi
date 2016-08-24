using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryBrowserApi.Models
{
    public class DirectoryBrowser
    {
        public static readonly List<Directory> Directories = new List<Directory>();
        public static readonly Stack<Directory> DirPath = new Stack<Directory>();
        private readonly List<Directory> _currentDirectories = new List<Directory>();
        private readonly List<string> _currentFiles = new List<string>();
        private readonly CurrentDirectory _currentDirectory = new CurrentDirectory();

        public CurrentDirectory GetDir()
        {
            try
            {
                GetHardDrives();
            }

            catch (Exception)
            {
                throw new DirectoryNotFoundException();
            }

            UpdateCurrentDir();

            return _currentDirectory;
        }

        public CurrentDirectory GetDir(int id)
        {
            Directory dir = Directories.SingleOrDefault(d => d.Id == id);

            if (dir == null)
            {
                throw new DirectoryNotFoundException();
            }

            
            BrowseDir(dir.FullName);

            UpdateCurrentDir(dir);
            UpdatePath(id);
            _currentDirectory.Id = id;

            return _currentDirectory;
        }

        private void GetHardDrives()
        {
            DriveInfo[] di = DriveInfo.GetDrives();

            _currentDirectories.Clear();
            _currentFiles.Clear();

            foreach (var drive in di)
            {
                if (!drive.IsReady || drive.DriveType != DriveType.Fixed)
                {
                    continue;
                }

                if (Directories.Any(d => d.FullName == drive.Name))
                {
                    ReplaceDir(drive.Name);
                }
                else
                {
                    AddDir(drive.Name);
                }
            }
        }

        private void BrowseDir(string fullName)
        {
            string[] subDirs = System.IO.Directory.GetDirectories(fullName);
            string[] files = System.IO.Directory.GetFiles(fullName);

            _currentDirectories.Clear();
            _currentFiles.Clear();

            foreach (var subDir in subDirs)
            {
                if (Directories.Any(d => d.FullName == subDir))
                {
                    ReplaceDir(subDir);
                }
                else
                {
                    AddDir(subDir);
                }
            }

            AddFiles(files);
        }

        private void AddDir(string fullName)
        {
            try
            {
                Directory dir = Directory.DirFactory(fullName);
                Directories.Add(dir);
                _currentDirectories.Add(dir);
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }

        }

        private void ReplaceDir(string subDir)
        {
            try
            {
                Directory oldDir = Directories.SingleOrDefault(d => d.FullName == subDir);
                Directory dir = Directory.DirFactory(subDir, oldDir.Id);
                Directories[Directories.IndexOf(oldDir)] = dir;
                _currentDirectories.Add(dir);
            }

            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }

        private void UpdateCurrentDir(Directory dir)
        {
            var dirInfo = new DirectoryInfo(dir.FullName);
            var parentDirInfo = System.IO.Directory.GetParent(dirInfo.FullName);
            Directory dirParent = null;
            if (parentDirInfo != null)
            {
                dirParent = Directories.SingleOrDefault(d => d.FullName == parentDirInfo.FullName);
            }

            _currentDirectory.FullName = dirInfo.FullName;
            _currentDirectory.CurrentDirs = _currentDirectories;
            _currentDirectory.CurrentFiles = _currentFiles;
            _currentDirectory.ParentId = dirParent?.Id;
        }

        private void UpdateCurrentDir()
        {
            _currentDirectory.CurrentDirs = _currentDirectories;
            _currentDirectory.CurrentFiles = _currentFiles;
        }

        private void AddFiles(string[] files)
        {
            foreach (var file in files)
            {
                _currentFiles.Add(new FileInfo(file).Name);
            }
        }

        private void UpdatePath(int id)
        {
            if (DirPath.Any(d => d.Id == id))
            {
                while (DirPath.Peek().Id != id)
                {
                    DirPath.Pop();
                }
                _currentDirectory.Path.AddRange(DirPath);
                _currentDirectory.Path.Reverse();
                return;
            }

            DirPath.Push(Directories.SingleOrDefault(d => d.Id == id));
            _currentDirectory.Path.AddRange(DirPath);
            _currentDirectory.Path.Reverse();
        }
    }
}
