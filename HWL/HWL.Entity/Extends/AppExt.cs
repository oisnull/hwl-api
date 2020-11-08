using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Entity.Extends
{
    public class AppExt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public AppVersionType VersionType { get; set; }
        public string VersionTypeString { get; set; }
        public string UpgradeLog { get; set; }
        public long Size { get; set; }
        public string DownloadUrl { get; set; }
        public DateTime PublishTime { get; set; }
    }

    public class AppVersionInfo
    {
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public AppVersionType AppVersionType { get; set; }
        public string UpgradeLog { get; set; }
        public long AppSize { get; set; }
        public string DownloadUrl { get; set; }
    }
}
