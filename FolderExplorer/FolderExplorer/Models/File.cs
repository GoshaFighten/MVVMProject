using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models {
    public class File {
        public static List<File> LoadFile(Directory directory) {
            if (string.IsNullOrEmpty(directory.Name)) {
                string[] drivers = System.IO.Directory.GetLogicalDrives();
                return drivers.Select(d => new Directory() { Name = d, Parent = directory }).ToList<File>();
            }
            List<File> list = System.IO.Directory.GetDirectories(directory.Name).Select(d => new Directory() { Name = d, Parent = directory }).ToList<File>();
            list.AddRange(System.IO.Directory.GetFiles(directory.Name).Select(f => new File() { Name = f, Parent = directory }).ToList());
            return list;
        }

        public Directory Parent { get; private set; }
        public virtual string Name { get; set; }
    }
}
