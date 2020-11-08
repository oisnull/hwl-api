using HWL.Entity.Extends;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWL.Manage.Models
{
    public class AdminSessionManager
    {
        const string ADMIN_SESSION_IDENTITY = "SystemAdmin";

        private static bool CheckSessionAvailable()
        {
            if (AppHttpContext.Current.Session.IsAvailable) return true;

            ShareConfig.LogHelper.Error("HWL后台管理系统下的Session服务不可用，Session是存储在redis里面，请检测redis是否已经开户或者redis的相关配置是否正确", typeof(AdminSessionManager));

            return false;
        }

        public static Admin GetAdmin()
        {
            if (!CheckSessionAvailable()) return null;

            return AppHttpContext.Current.Session.GetObject<Admin>(ADMIN_SESSION_IDENTITY);
        }

        public static void SetAdmin(Admin admin)
        {
            if (admin == null) return;

            if (!CheckSessionAvailable()) return;

            AppHttpContext.Current.Session.SetObject(ADMIN_SESSION_IDENTITY, admin);
        }

        public static void ClearAdmin()
        {
            if (!CheckSessionAvailable()) return;

            AppHttpContext.Current.Session.Remove(ADMIN_SESSION_IDENTITY);
        }
    }
}
