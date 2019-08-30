using StackExchange.Redis;
using System;
using System.Linq;
using System.Collections.Generic;
using HWL.ShareConfig;

namespace HWL.Redis
{
    /// <summary>
    /// 群组操作
    /// </summary>
    public static class GroupStore
    {
        /// <summary>
        /// 搜索附近的范围初始值
        /// </summary>
        public const int NEAR_RANGE = 100;

        /// <summary>
        /// 检测周围100M内的组数据,并返回对应的组标识列表
        /// </summary>
        public static List<string> GetGroupGuids(double lon, double lat)
        {
            if (lon <= 0 || lat <= 0) return null;

            List<string> keys = null;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_GEO_DB, db =>
             {
                 GeoRadiusResult[] results = db.GeoRadius(RedisConfigManager.GROUP_GEO_KEY, lon, lat, NEAR_RANGE, GeoUnit.Miles, 1);
                 if (results != null && results.Length > 0)
                 {
                     keys = results.Select(s => s.Member.ToString()).ToList();
                 }
             });

            return keys;
        }

        /// <summary>
        /// 检测周围100M内的组数据,并返回对应的组标识
        /// </summary>
        public static string GetNearGroupGuid(double lon, double lat)
        {
            List<string> groupGuids = GetGroupGuids(lon, lat);
            if (groupGuids == null || groupGuids.Count <= 0) return null;

            foreach (var guid in groupGuids)
            {
                if (GetGroupUserCount(guid) < RedisConfigManager.GroupUserTotalCount)
                {
                    return guid;
                }
            }

            return null;
        }

        public static void SaveGroupUser(string groupGuid, params int[] userIds)
        {
            if (string.IsNullOrEmpty(groupGuid)) return;
            if (userIds == null || userIds.Length <= 0) return;

            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
             {
                 RedisValue[] array = new RedisValue[userIds.Length];
                 for (int i = 0; i < userIds.Length; i++)
                 {
                     array[i] = userIds[i];
                 }
                 db.SetAdd(groupGuid, array);
             });
        }

        public static bool ExistsInGroup(string groupGuid, int userId)
        {
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
             {
                 succ = db.SetContains(groupGuid, userId);
             });
            return succ;
        }

        public static bool DeleteGroupUser(string groupGuid, int userId)
        {
            if (string.IsNullOrEmpty(groupGuid) || userId <= 0) return false;

            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
             {
                 RedisValue[] array = new RedisValue[1] { userId };
                 if (db.SetRemove(groupGuid, array) > 0)
                 {
                     succ = true;
                 }
             });
            return succ;
        }

        public static bool DeleteGroup(string groupGuid)
        {
            if (string.IsNullOrEmpty(groupGuid)) return false;

            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
             {
                 succ = db.KeyDelete(groupGuid);
             });
            return succ;
        }

        public static bool DeleteGroup(string groupGuid, List<int> userIds)
        {
            if (string.IsNullOrEmpty(groupGuid)) return false;

            bool succ = false;
            //base.DbNum = GROUP_DB;
            //base.Exec(db =>
            //{
            //    succ = db.KeyDelete(groupGuid);
            //});

            //if (!succ) return false;

            if (userIds != null && userIds.Count > 0)
            {
                RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
                 {
                     RedisValue[] array = new RedisValue[userIds.Count];
                     for (int i = 0; i < userIds.Count; i++)
                     {
                         array[i] = userIds[i];
                     }
                     if (db.SetRemove(groupGuid, array) > 0)
                     {
                         succ = true;
                     }
                 });
            }
            return succ;
        }

        public static int GetGroupUserCount(string groupGuid)
        {
            if (string.IsNullOrEmpty(groupGuid)) return 0;
            int count = 0;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
             {
                 count = Convert.ToInt32(db.SetLength(groupGuid));
             });
            return count;
        }

        //public static List<int> GetNearGroupUserIds(string groupGuid)
        //{
        //    if (string.IsNullOrEmpty(groupGuid)) return null;

        //    List<int> userIds = new List<int>();
        //    base.DbNum = GROUP_USER_SET_DB;
        //    base.Exec(db =>
        //    {
        //        RedisValue[] users = db.SetMembers(groupGuid);
        //        if (users != null && users.Length > 0)
        //        {
        //            userIds.AddRange(users.Select(u =>
        //            {
        //                int uid;
        //                u.TryParse(out uid);
        //                return uid;
        //            }).ToArray());
        //        }
        //    });

        //    return userIds;
        //}

        public static List<int> GetGroupUserIds(string groupGuid)
        {
            if (string.IsNullOrEmpty(groupGuid)) return null;

            List<int> userIds = new List<int>();
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
             {
                 RedisValue[] users = db.SetMembers(groupGuid);
                 if (users != null && users.Length > 0)
                 {
                     userIds.AddRange(users.Select(u =>
                     {
                         int uid;
                         u.TryParse(out uid);
                         return uid;
                     }).ToArray());
                 }
             });

            return userIds;
        }

        ///// <summary>
        ///// 保存组用户
        ///// </summary>
        //public static void SaveGroupUser(string groupGuid,params int[] userIds)
        //{
        //    if (string.IsNullOrEmpty(groupGuid)) return;
        //    if (userIds == null || userIds.Length <= 0) return;

        //    base.DbNum = GROUP_USER_SET_DB;
        //    base.Exec(db =>
        //    {
        //        RedisValue[] array = new RedisValue[userIds.Length];
        //        for (int i = 0; i < userIds.Length; i++)
        //        {
        //            array[i] = userIds[i];
        //        }
        //        db.SetAdd(groupGuid, array);
        //    });
        //}

        //public static int GetGroupUserCount(string groupGuid)
        //{
        //    if (string.IsNullOrEmpty(groupGuid)) return 0;
        //    base.DbNum = GROUP_USER_SET_DB;
        //    int count = 0;
        //    base.Exec(db =>
        //    {
        //        count = Convert.ToInt32(db.SetLength(groupGuid));
        //    });
        //    return count;
        //}

        //public static List<string> GetGroupUserIds(string groupGuid)
        //{
        //    if (string.IsNullOrEmpty(groupGuid)) return null;

        //    List<string> userIds = new List<string>();
        //    base.DbNum = GROUP_USER_SET_DB;
        //    base.Exec(db =>
        //    {
        //        RedisValue[] users = db.SetMembers(groupGuid);
        //        if (users != null && users.Length > 0)
        //        {
        //            userIds.AddRange(users.ToStringArray());
        //        }
        //    });

        //    return userIds;
        //}

        /// <summary>
        /// 创建组位置数据,返回创建成功后的组标识
        /// </summary>
        public static string CreateNearGroupPos(double lon, double lat)
        {
            string guid = Guid.NewGuid().ToString();
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_GEO_DB, db =>
             {
                 db.GeoAdd(RedisConfigManager.GROUP_GEO_KEY, lon, lat, guid);
             });
            return guid;
        }

        //#region 组的创建人操作

        //public static bool SaveGroupCreateUser(string groupGuid,int userId)
        //{
        //    if (userId <= 0) return false;
        //    if (string.IsNullOrEmpty(groupGuid)) return false;

        //    bool succ = false;
        //    base.DbNum = GROUP_CREATE_USER_DB;
        //    base.Exec(db =>
        //    {
        //        succ = db.StringSet(groupGuid, userId.ToString());
        //    });
        //    return succ;
        //}

        //#endregion
    }
}
