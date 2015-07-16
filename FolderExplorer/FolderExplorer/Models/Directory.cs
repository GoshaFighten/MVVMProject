using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models {
    public class Directory : File {
        public Directory(Directory parent, string path, string name, Image image)
            : base(parent, path, name, image) {
        }

        public List<File> LoadFiles(Func<string, bool> filter) {
            return DataManager.DataManager.CurrentProvider.LoadFiles(this, filter);
        }

        public override string GetInfo() {
            return string.Format("Directory: {0}", Name);
        }
    }
}
