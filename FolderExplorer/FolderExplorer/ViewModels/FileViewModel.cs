using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class FileViewModel : ISupportParameter {
        public string Path { get; set; }
        public object Parameter {
            get { throw new NotImplementedException(); }
            set { Path = (string)value; }
        }
    }
}
