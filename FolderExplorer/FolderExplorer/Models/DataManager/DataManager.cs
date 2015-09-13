using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models.DataManager {
    public static class DataManager {
        static DataManager() {
            CurrentProvider = new DropboxProvider.DropboxProvider();
            //CurrentProvider = new FileSystemProvider.FileSystemProvider();
        }

        public static IFileProvider CurrentProvider { get; private set; }
    }
}
