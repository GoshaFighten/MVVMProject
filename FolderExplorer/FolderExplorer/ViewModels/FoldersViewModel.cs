using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using FolderExplorer.Models;
using System.Text.RegularExpressions;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class FoldersViewModel {
        public FoldersViewModel() {
            ParentFolder = Root.Instance;
        }

        public virtual Directory ParentFolder { get; set; }
        protected void OnParentFolderChanged() {
            LoadFiles(ParentFolder);
        }

        public virtual File CurrentFile { get; set; }
        public List<File> Files { get; private set; }
        private void LoadFiles(Directory directory) {
            directory.LoadFiles();
            Files = directory.NestedFiles;
            this.RaisePropertyChanged(vm => vm.Files);
        }

        public void Open(File file) {
            ParentFolder = (Directory)file;
        }

        public bool CanOpen(File file) {
            return file is Directory;
        }

        public void Return(Directory directory) {
            ParentFolder = directory.Parent;
        }

        public bool CanReturn(Directory directory)
        {
            return !(directory is Root);
        }
    }
}
