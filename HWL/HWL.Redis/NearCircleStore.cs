using HWL.ShareConfig;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Redis
{
    public class NearCircleStore
    {
        const string NEAR_CIRCLE_GEO_KEY = "near:circle:pos";

        /// <summary>
        /// 创建附近圈子信息位置数据,返回创建成功后的组标识
        /// </summary>
        public static bool CreateNearCirclePos(int circleId, double lon, double lat)
        {
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(AppConfigManager.NEAR_CIRCLE_GEO_DB, db =>
             {
                 succ = db.GeoAdd(NEAR_CIRCLE_GEO_KEY, lon, lat, circleId);
             });
            return succ;
        }

        public static List<int> GetNearCircleIds(double lon, double lat)
        {
            if (lon < 0 && lat < 0) return null;

            List<int> ids = null;
            RedisUtils.DefaultInstance.Exec(AppConfigManager.NEAR_CIRCLE_GEO_DB, db =>
             {
                 GeoRadiusResult[] results = db.GeoRadius(NEAR_CIRCLE_GEO_KEY, lon, lat, AppConfigManager.SEARCH_NEAR_CIRCLE_RANGE, GeoUnit.Miles, -1);
                 if (results != null && results.Length > 0)
                 {
                     ids = results.Select(s => (int)s.Member).OrderByDescending(s => s).ToList();
                 }
             });

            return ids;
        }

        public static bool DeleteNearCircleId(int nearCircleId)
        {
            if (nearCircleId <= 0) return false;
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(AppConfigManager.NEAR_CIRCLE_GEO_DB, db =>
             {
                 succ = db.GeoRemove(NEAR_CIRCLE_GEO_KEY, nearCircleId);
             });
            return succ;
        }
    }
}
