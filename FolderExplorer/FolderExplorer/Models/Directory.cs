using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models {
    public class Directory : File {
        public List<File> NestedFiles { get; private set; }
        public void LoadFiles() {
            NestedFiles = File.LoadFile(this);
        }
    }
}
