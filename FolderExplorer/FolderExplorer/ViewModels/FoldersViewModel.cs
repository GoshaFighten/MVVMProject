using System;
using System.Linq;
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
            this.RaiseCanExecuteChanged(x => x.ViewFile(null));
        }

        public List<File> Files { get; private set; }
        private void LoadFiles(Directory directory) {
            Files = directory.LoadFiles(ExtensionFilter.Filter);
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

        public void Search() {
            object[] dialogParams = { ParentFolder, ExtensionFilter };
            MessageResult showDialog = DialogService.ShowDialog(MessageButton.OKCancel, "Search Dialog", "Search", dialogParams, this);
            if (showDialog == MessageResult.OK) {
                File target = (File)dialogParams[0];
                Open(target.Parent);
                CurrentFile = Files.First(f => f.Path == target.Path);
            }
        }

        protected virtual IDialogService DialogService {
            get { return null; }
        }

        public void ViewFile(File file) {
            var document = DocumentManagerService.CreateDocument("FileView", file.Path, this);
            if (document != null) {
                document.Title = file.Path;
                document.Show();
            }
        }

        public bool CanViewFile(File file) {
            return file.GetType() == typeof(File);
        }

        public virtual IDocumentManagerService DocumentManagerService
        {
            get { return null; }
        }
    }
}
