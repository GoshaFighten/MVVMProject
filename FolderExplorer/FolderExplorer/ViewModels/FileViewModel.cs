using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.IO;
using FolderExplorer.Models.DataManager;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class FileViewModel : ISupportParameter {
        public Stream File { get; set; }
        public object Parameter {
            get { throw new NotImplementedException(); }
            set { File = GetFile((string)value); }
        }

        private Stream GetFile(string path) {
            return DataManager.CurrentProvider.LoadFile(path);
        }
    }
}
