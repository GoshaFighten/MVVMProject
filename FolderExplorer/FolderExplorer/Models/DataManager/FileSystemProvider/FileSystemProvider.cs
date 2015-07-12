using DevExpress.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models.DataManager.FileSystemProvider {
    public class FileSystemProvider : IFileProvider {
        public string RootPath {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer); }
        }

        public List<File> LoadFiles(Directory directory, Func<string, bool> filter) {
            if (string.IsNullOrEmpty(directory.Path)) {
                return FileSystemHelper.GetFixedDrives().Select(d => new Directory(directory, d.Name, d.Name, null)).ToList<File>();
            }
            FileSystemEntryCollection collection = FileSystemHelper.GetFileSystemEntries(directory.Path, IconSizeType.Small, new System.Drawing.Size(16, 16));
            collection.ShowExtensions = true;
            return collection.Where(entry => entry is DirectoryEntry || filter(entry.Path)).Select(entry => { return entry is DirectoryEntry ? new Directory(directory, entry.Path, entry.Name, entry.Image) : new File(directory, entry.Path, entry.Name, entry.Image);}).ToList();
        }
    }
}
