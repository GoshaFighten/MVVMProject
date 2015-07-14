using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using FolderExplorer.Models;
using System.Text.RegularExpressions;
using FolderExplorer.Core;
using FolderExplorer.Messages;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class FoldersViewModel {
        public FoldersViewModel() {
            LockReload();
            ExtensionFilter = FilterItem.DefaultFilter;
            ParentFolder = Root.Instance;
            UnlockReload();
        }

        private int reloadLocker = 0;
        private void LockReload() {
            reloadLocker++;
        }

        private void UnlockReload() {
            reloadLocker--;
            Reload();
        }

        private bool IsReloadLocked() {
            return reloadLocker != 0;
        }

        public virtual Directory ParentFolder { get; set; }
        protected void OnParentFolderChanged() {
            Reload();
            this.RaiseCanExecuteChanged(x => x.Return(null));
        }

        private void Reload() {
            if (IsReloadLocked()) {
                return;
            }
            LoadFiles(ParentFolder);
        }

        public virtual File CurrentFile { get; set; }
        protected void OnCurrentFileChanged() {
            Messenger.Default.Send(new FileSelectedMessage(CurrentFile));
        }

        public List<File> Files { get; private set; }
        private void LoadFiles(Directory directory) {
            directory.LoadFiles(ExtensionFilter.Filter);
            Files = directory.NestedFiles;
            if (!(directory is Root)) {
                Files.Insert(0, Back.Instance);
            }
            this.RaisePropertyChanged(vm => vm.Files);
        }

        public void Open(File file) {
            if (file is Back) {
                Return(ParentFolder);
                return;
            }
            ParentFolder = (Directory)file;
        }

        public bool CanOpen(File file) {
            return file is Directory || file is Back;
        }

        public void Return(Directory directory) {
            ParentFolder = directory.Parent;
        }

        public bool CanReturn(Directory directory) {
            return !(directory is Root);
        }

        public virtual FilterItem ExtensionFilter { get; set; }
        protected void OnExtensionFilterChanged() {
            Reload();
        }
    }
}
