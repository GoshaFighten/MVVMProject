using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models {
    public class Root : Directory {
        public static Root Instance { get; private set; }
        static Root() {
            Instance = new Root();
        }

        public override string Name {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer); }
            set { throw new NotImplementedException(); }
        }
    }
}
