using GMSF;
using HWL.Entity;
using HWL.Entity.Models;
using HWL.Service.Generic.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Generic.Service
{
    public class CollectLog : ServiceHandler<CollectLogRequestBody>
    {
        private readonly HWLEntities db;
        public CollectLog(HWLEntities db, CollectLogRequestBody request) : base(request)
        {
            this.db = db;
        }

        public override void ExecuteCore()
        {
            t_app_log log = new t_app_log()
            {
                app_type = request.AppType,
                app_version = request.AppVersion,
                crash_details = request.CrashDetails,
                crash_info = request.CrashInfo,
                create_time = DateTime.Now,
            };
            db.t_app_log.Add(log);
            db.SaveChanges();
        }
    }
}
