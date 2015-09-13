using DevExpress.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models.DataManager.FileSystemProvider {
    public class FileSystemProvider : IFileProvider {
        public string RootPath {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer); }
        }

        private readonly Size imgSize = new Size(16, 16);
        public string GetFilePath(string path) {
            return path;
        }

        public List<File> LoadFiles(Directory directory, Func<string, bool> filter, bool getDirectoryIcons = true) {
            if (string.IsNullOrEmpty(directory.Path)) {
                return FileSystemHelper.GetFixedDrives().Select(d => new Directory(directory, d.Name, d.Name, FileSystemHelper.GetImage(d.Name, IconSizeType.Small, imgSize))).ToList<File>();
            }
            FileSystemEntryCollection collection = GetFileSystemEntries(directory.Path, IconSizeType.Small, imgSize, getDirectoryIcons);
            collection.ShowExtensions = true;
            return collection.Where(entry => entry is DirectoryEntry || filter(entry.Path)).Select(entry => { return entry is DirectoryEntry ? new Directory(directory, entry.Path, entry.Name, entry.Image) : new File(directory, entry.Path, entry.Name, entry.Image);}).ToList();
        }

        private static FileSystemEntryCollection GetFileSystemEntries(string path, IconSizeType sizeType, Size itemSize, bool getIcons) {
            FileSystemEntryCollection col = new FileSystemEntryCollection();
            FileSystemHelper.InitDirectories(path, col, sizeType, itemSize, getIcons);
            FileSystemHelper.InitFiles(path, col, sizeType, itemSize);
            return col;
        }
    }
}
