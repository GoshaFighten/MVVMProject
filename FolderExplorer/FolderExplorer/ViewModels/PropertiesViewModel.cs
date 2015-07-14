using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Text;
using System.Threading.Tasks;
using FolderExplorer.Models;
using FolderExplorer.Messages;

namespace FolderExplorer.ViewModels {
    [POCOViewModel]
    public class PropertiesViewModel {
        public PropertiesViewModel()
        {
            Messenger.Default.Register<FileSelectedMessage>(this, OnFileSelectedMessage);
        }

        private void OnFileSelectedMessage(FileSelectedMessage msg)
        {
            Info = msg.File.GetInfo();
        }
        public virtual string Info { get; set; }
    }
}
