using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models {
    public class File {
        public File(Directory parent, string path, string name, Image image) {
            Name = name;
            Image = image;
            Parent = parent;
            Path = path;
        }

        public Directory Parent { get; private set; }
        public string Path { get; private set; }
        public string Name { get; private set; }
        [DisplayName(" ")]
        public Image Image { get; private set; }
        public virtual string GetInfo() {
            return string.Format("File: {0}", Name);
        }
    }
}
