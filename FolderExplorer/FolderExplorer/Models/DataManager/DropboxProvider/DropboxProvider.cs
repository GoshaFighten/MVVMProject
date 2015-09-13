using Nemiro.OAuth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer.Models.DataManager.DropboxProvider {
    public class DropboxProvider : IFileProvider {
        public string RootPath {
            get { return "/"; }
        }

        public List<File> LoadFiles(Directory directory, Func<string, bool> filter, bool getDirectoryIcons = true) {
            RequestResult result = OAuthUtility.Get("https://api.dropbox.com/1/metadata/auto/", new HttpParameterCollection { { "path", directory.Path.Replace("\\", "/") }, { "access_token", AccessToken } });
            if (IsSuccess(result)) {
                return null;
            }
            return result["contents"].Where(i => Convert.ToBoolean(i["is_dir"]) || filter(i["path"].ToString())).OrderBy(i => System.IO.Path.GetFileName(i["path"].ToString())).OrderByDescending(i => Convert.ToBoolean(i["is_dir"])).Select(i => Convert.ToBoolean(i["is_dir"]) ? new Directory(directory, i["path"].ToString(), System.IO.Path.GetFileName(i["path"].ToString()), GetIcon(i)) : new File(directory, i["path"].ToString(), System.IO.Path.GetFileName(i["path"].ToString()), GetIcon(i))).ToList();
        }

        public string AccessToken {
            get { return "z4WcHGY1AkUAAAAAAAAA-iRRs6tqa9ce8u74gbkNIU-ehgdBdgeACyEdOPIvX_hA"; }
        }

        private bool IsSuccess(RequestResult result) {
            return result.StatusCode != 200;
        }

        private Image GetIcon(UniValue file) {
            System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(string.Format("FolderExplorer.Resources.DropboxIcons.{0}.gif", file["icon"]));
            return Image.FromStream(stream);
        }
    }
}
