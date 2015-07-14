using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Messages {
    public class FileSelectedMessage {
        public FileSelectedMessage(FolderExplorer.Models.File file) {
            File = file;
        }

        public FolderExplorer.Models.File File { get; private set; }
    }
}
