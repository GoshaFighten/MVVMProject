using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FolderExplorer.Models {
    public class Back : File {
        static Back() {
            Instance = new Back(null, string.Empty, "..", DevExpress.Images.ImageResourceCache.Default.GetImage("images/navigation/up_16x16.png"));
        }

        public static Back Instance { get; private set; }
        protected Back(Directory parent, string path, string name, Image image)
            : base(parent, path, name, image) {
        }

        public override string GetInfo() {
            return "Back";
        }
    }
}
