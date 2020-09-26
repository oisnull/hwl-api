using StackExchange.Redis;
using System;
using System.Linq;
using System.Collections.Generic;
using HWL.ShareConfig;

namespace HWL.Redis
{
    public static class GroupStore
    {
        const string GROUP_GEO_KEY = "group:pos";

        //public static string CreateNearGroupByPos(double lon, double lat)
        //{
        //    string guid = Guid.NewGuid().ToString();
        //    RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_GEO_DB, db =>
        //    {
        //        db.GeoAdd(GROUP_GEO_KEY, lon, lat, guid);
        //    });
        //    return guid;
        //}

        public static List<string> SearchNearGroupGuids(double lon, double lat)
        {
            if (lon <= 0 || lat <= 0) return null;

            return RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_GEO_DB, db =>
            {
                return db.GeoRadius(GROUP_GEO_KEY, lon, lat, AppConfigManager.SEARCH_NEAR_GROUP_RANGE, GeoUnit.Meters, 1)?
                .Select(s => s.Member.ToString()).ToList();
            });
        }

        public static string GetAvailableNearGroupGuid(double lon, double lat)
        {
            return RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_GEO_DB, db =>
            {
                List<string> groupGuids = db.GeoRadius(GROUP_GEO_KEY, lon, lat, AppConfigManager.SEARCH_NEAR_GROUP_RANGE, GeoUnit.Meters, 1)?
                .Select(s => s.Member.ToString()).ToList();

                string availableGroupGuid = null;
                if (groupGuids != null && groupGuids.Count > 0)
                {
                    foreach (var guid in groupGuids)
                    {
                        if (GetGroupUserCount(guid) < RedisConfigManager.GroupUserTotalCount)
                        {
                            availableGroupGuid = guid;
                            break;
                        }
                    }
                }

                if (availableGroupGuid == null)
                {
                    availableGroupGuid = Guid.NewGuid().ToString();
                    db.GeoAdd(GROUP_GEO_KEY, lon, lat, availableGroupGuid);
                }

                return availableGroupGuid;
            });
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
            if (string.IsNullOrEmpty(groupGuid) || userId <= 0) return false;

            return RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db => db.SetContains(groupGuid, userId));
        }

        public static bool DeleteGroupUsers(string groupGuid, params int[] userIds)
        {
            if (string.IsNullOrEmpty(groupGuid) || userIds == null || userIds.Length <= 0) return false;

            return RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
            {
                RedisValue[] array = userIds.Select(u => RedisValue.Unbox(u)).ToArray();
                return db.SetRemove(groupGuid, array) > 0;
            });
        }

        public static bool DeleteGroup(string groupGuid)
        {
            if (string.IsNullOrEmpty(groupGuid)) return false;

            return RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db => db.KeyDelete(groupGuid));
        }

        public static bool DeleteGroup(string groupGuid, List<int> userIds)
        {
            if (string.IsNullOrEmpty(groupGuid)) return false;

            bool succ = false;

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

            return RedisUtils.DefaultInstance.Exec(RedisConfigManager.GROUP_USER_SET_DB, db =>
            {
                return db.SetMembers(groupGuid)?.Select(u => int.Parse(u)).ToList();
            });
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
