using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Core {
    public class FilterItem {
        protected FilterItem() {
        }

        public string Name { get; set; }
        public Func<string, bool> Filter { get; set; }
        public static List<FilterItem> GetAllFilters() {
            List<FilterItem> list = new List<FilterItem>();
            list.Add(DefaultFilter);
            list.Add(new FilterItem() { Name = "Only CS", Filter = (path) => { return path.ToLower().EndsWith(".cs");} });
            list.Add(new FilterItem() { Name = "Only VB", Filter = (path) => { return path.ToLower().EndsWith(".vb");} });
            return list;
        }

        public override string ToString() {
            return Name;
        }

        private static FilterItem fDefaultFilter;
        public static FilterItem DefaultFilter {
            get {
                if (fDefaultFilter == null) {
                    fDefaultFilter = new FilterItem() { Name = "CS & VB", Filter = (path) => { return path.ToLower().EndsWith(".cs") || path.ToLower().EndsWith(".vb");} };
                }
                return fDefaultFilter;
            }
        }
    }
}
