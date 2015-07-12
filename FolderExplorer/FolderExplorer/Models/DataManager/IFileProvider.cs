using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models.DataManager {
    public interface IFileProvider {
        string RootPath { get; }
        List<File> LoadFiles(Directory directory, Func<string, bool> filter);
    }
}
