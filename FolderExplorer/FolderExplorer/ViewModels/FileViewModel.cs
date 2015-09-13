using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.IO;
using FolderExplorer.Models.DataManager;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class FileViewModel : ISupportParameter {
        public string File { get; set; }
        public object Parameter {
            get { throw new NotImplementedException(); }
            set { File = GetFile((string)value); }
        }

        private string GetFile(string path) {
            return DataManager.CurrentProvider.GetFilePath(path);
        }
    }
}
