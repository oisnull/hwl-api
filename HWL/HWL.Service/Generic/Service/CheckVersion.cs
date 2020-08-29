using GMSF;
using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Service.Generic.Body;
using System;
using System.Linq;

namespace HWL.Service.Generic.Service
{
    public class CheckVersion : ServiceHandler<CheckVersionRequestBody, CheckVersionResponseBody>
    {
        private readonly HWLEntities db;
        public CheckVersion(HWLEntities db, CheckVersionRequestBody request) : base(request)
        {
            this.db = db;
        }

        protected override void ValidateRequestParams()
        {
            if (this.request.UserId <= 0)
            {
                throw new Exception("用户信息错误");
            }
        }

        public override CheckVersionResponseBody ExecuteCore()
        {
            CheckVersionResponseBody res = new CheckVersionResponseBody();
            var version = db.t_app_version.OrderByDescending(v => v.publish_time).Select(v => new AppVersionInfo
            {
                AppName = v.app_name,
                DownloadUrl = v.download_url,
                AppSize = v.app_size ?? 0,
                UpgradeLog = v.upgrade_log,
                AppVersion = v.app_version,
            }).FirstOrDefault();
            if (version == null)
            {
                res.IsNewVersion = false;
                return res;
            }
            if (string.IsNullOrEmpty(request.OldVersion) || GenericUtility.CompareVersion(this.request.OldVersion, version.AppVersion) == -1)
            {
                res.IsNewVersion = true;
                res.AppVersionInfo = version;
            }
            else
            {
                res.IsNewVersion = false;
            }
            return res;
        }
    }
}
