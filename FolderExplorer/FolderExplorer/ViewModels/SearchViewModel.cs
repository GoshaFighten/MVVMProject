using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using FolderExplorer.Models;
using System.Threading.Tasks;
using DevExpress.Mvvm.POCO;
using System.Threading;
using FolderExplorer.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class SearchViewModel : ISupportParameter {
        public SearchViewModel() {
            Files = new BindingList<File>();
        }

        public virtual Directory Root { get; set; }
        private FilterItem Filter;
        private object[] parameters;
        public object Parameter {
            get { throw new NotSupportedException(); }
            set {
                parameters = (object[])value;
                Root = (Directory)(parameters[0]);
                Filter = (FilterItem)(parameters[1]);
            }
        }

        public virtual string SearchText { get; set; }
        public Task Search(Directory directory) {
            Action<object> action = null;
            action = (f) => {
                Directory folder = (Directory)f;
                var asyncCommand = this.GetAsyncCommand(x => x.Search(null));
                foreach (File item in folder.LoadFiles(Filter.Filter, false)) {
                    if (asyncCommand.IsCancellationRequested) {
                        break;
                    }
                    if (item is Directory) {
                        Task.Factory.StartNew(action, item, TaskCreationOptions.AttachedToParent);
                    }
                    else {
                        if (item.Name.ToLower().Contains(SearchText.ToLower())) {
                            DispatcherService.BeginInvoke(() => { Files.Add(item);});
                        }
                    }
                }
            };
            return Task.Factory.StartNew(action, directory);
        }

        public BindingList<File> Files { get; private set; }
        public virtual File CurrentFile { get; set; }
        protected void OnCurrentFileChanged() {
            parameters[0] = CurrentFile;
        }

        protected virtual IDispatcherService DispatcherService {
            get { return null; }
        }
    }
}
