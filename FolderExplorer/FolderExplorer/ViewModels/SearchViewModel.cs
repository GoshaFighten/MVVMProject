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
    public class SearchViewModel : ISupportParameter, IDisposable {
        public SearchViewModel() {
            Files = new BindingList<File>();
        }

        public virtual Directory Root { get; set; }
        private CancellationTokenSource fTokenSource;
        private FilterItem Filter;
        public object Parameter {
            get { return CurrentFile; }
            set {
                object[] values = (object[])value;
                Root = (Directory)(values[0]);
                Filter = (FilterItem)(values[1]);
            }
        }

        public virtual string SearchText { get; set; }
        public Task Search(Directory directory) {
            fTokenSource = new CancellationTokenSource();
            var token = fTokenSource.Token;
            Task task = null;
            task = Task.Factory.StartNew(() => {
                directory.LoadFiles(Filter.Filter);
                foreach (File item in directory.NestedFiles) {
                    if (token.IsCancellationRequested) {
                        token.ThrowIfCancellationRequested();
                    }
                    if (item is Directory) {
                        task.ContinueWith(t => Search((Directory)item), token, TaskContinuationOptions.AttachedToParent, TaskScheduler.Current);
                    }
                    else {
                        if (item.Name.Contains(SearchText)) {
                            DispatcherService.BeginInvoke(() => { Files.Add(item);});
                        }
                    }
                }
            }, token);
            return task;
        }

        public void Cancel() {
            fTokenSource.Cancel();
        }

        public BindingList<File> Files { get; private set; }
        public virtual File CurrentFile { get; set; }
        protected virtual IDispatcherService DispatcherService {
            get { return null; }
        }

        public void Dispose() {
            if (fTokenSource != null) {
                fTokenSource.Dispose();
                fTokenSource = null;
            }
        }
    }
}
