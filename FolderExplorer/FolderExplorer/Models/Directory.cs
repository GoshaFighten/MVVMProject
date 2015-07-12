﻿using System;
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

        public List<File> NestedFiles { get; private set; }
        public void LoadFiles() {
            NestedFiles = DataManager.DataManager.CurrentProvider.LoadFiles(this);
        }
    }
}
