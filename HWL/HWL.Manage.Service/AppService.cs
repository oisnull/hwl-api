using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.IMClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HWL.Manage.Service
{
    public class AppService : BaseService
    {
        public AppService(HWLEntities db) : base(db)
        {
        }

        public List<AppExt> GetAppVersionList(int topCount = 0)
        {
            IQueryable<AppExt> query = db.t_app_version.Select(v => new AppExt
            {
                Id = v.id,
                DownloadUrl = v.download_url,
                Name = v.app_name,
                Size = v.app_size ?? 0,
                PublishTime = v.publish_time,
                Version = v.app_version,
                VersionType = v.app_version_type,
            });
            if (topCount > 0)
            {
                query = query.Take(topCount).OrderByDescending(o => o.Id);
            }

            List<AppExt> list = query.ToList();
            list.ForEach(f => f.VersionTypeString = CustomerEnumDesc.GetAppVersionTypeDesc(f.VersionType));
            return list;
        }

        public AppExt GetAppVersionInfo(int id)
        {
            if (id <= 0) return null;

            return db.t_app_version.Where(v => v.id == id).Select(v => new AppExt
            {
                Id = v.id,
                DownloadUrl = v.download_url,
                Name = v.app_name,
                Size = v.app_size ?? 0,
                UpgradeLog = v.upgrade_log,
                PublishTime = v.publish_time,
                Version = v.app_version,
                VersionType = v.app_version_type,
            }).FirstOrDefault();
        }

        public string GetAppLastVersionUrl()
        {
            return db.t_app_version.OrderByDescending(v => v.id).Select(v => v.download_url).FirstOrDefault();
        }

        public int DeleteAppVersion(int id, out string error)
        {
            error = "";
            if (id <= 0)
            {
                error = "参数错误";
                return 0;
            }

            var model = db.t_app_version.Where(a => a.id == id).FirstOrDefault();
            if (model == null)
            {
                error = "数据不存在";
                return 0;
            }

            try
            {
                db.t_app_version.Remove(model);
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return 0;
            }
        }

        public int AppVersionAction(AppExt model, out string error)
        {
            error = "";
            if (model == null)
            {
                error = "数据不能为空";
                return 0;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                error = "app 名称不能为空";
                return 0;
            }
            if (string.IsNullOrEmpty(model.Version))
            {
                error = "app 版本号不能为空";
                return 0;
            }
            if (string.IsNullOrEmpty(model.DownloadUrl))
            {
                error = "app 下载地址不能为空";
                return 0;
            }

            t_app_version entity = null;
            if (model.Id > 0)
            {
                entity = db.t_app_version.Where(v => v.id == model.Id).FirstOrDefault();
                if (entity == null)
                {
                    error = "数据不存在";
                    return 0;
                }
            }
            else
            {
                entity = new t_app_version();
                entity.publish_time = DateTime.Now;
                db.t_app_version.Add(entity);
            }
            entity.app_name = model.Name;
            entity.app_version = model.Version;
            entity.app_version_type = model.VersionType;
            entity.app_size = model.Size;
            entity.upgrade_log = model.UpgradeLog;
            entity.download_url = model.DownloadUrl;
            entity.update_time = DateTime.Now;

            try
            {
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return 0;
            }
        }

        public int AddAppVersionPush(int appId, string userIds, out string error)
        {
            error = "";
            if (appId <= 0)
            {
                error = "App id can't be empty.";
                return 0;
            }

            t_app_version app = db.t_app_version.Where(a => a.id == appId).FirstOrDefault();
            if (app == null)
            {
                error = $"Not found app version data by appid = {appId}.";
                return 0;
            }

            ulong[] userIdArray = userIds?.Trim().Split(new string[] { ",", ";", " " }, StringSplitOptions.RemoveEmptyEntries)
                .Where(u => !string.IsNullOrEmpty(u))
                .Select(u => ulong.Parse(u))
                .Distinct()
                .ToArray();
            if (userIdArray == null || userIdArray.Length <= 0)
            {
                error = "User Ids can't be empty.";
                return 0;
            }

            t_app_version_push model = new t_app_version_push()
            {
                app_version_id = appId,
                pushed_users = userIds,
                push_date = DateTime.Now
            };
            db.t_app_version_push.Add(model);
            db.SaveChanges();

            if (model.id > 0)
            {
                IMClientV.INSTANCE.SendAppVersionMessage(userIdArray, new IMCore.Protocol.ImAppVersionContent()
                {
                    AppName = app.app_name,
                    AppSize = (ulong)(app.app_size ?? 0),
                    AppVersion = app.app_version,
                    DownloadUrl = app.download_url,
                    UpgradeLog = app.upgrade_log,
                });
            }
            return 1;
        }
    }
}
