using FolderExplorer.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models {
    public class Root : Directory {
        public static Root Instance { get; private set; }
        static Root() {
            Instance = new Root(null, DataManager.DataManager.CurrentProvider.RootPath, string.Empty, null);
        }

        protected Root(Directory parent, string path, string name, Image image)
            : base(parent, path, name, image) {
        }
    }
}
