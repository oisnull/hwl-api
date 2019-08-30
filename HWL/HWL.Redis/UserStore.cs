﻿using HWL.ShareConfig;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace HWL.Redis
{
    public class UserStore
    {
        /// <summary>
        /// 搜索附近用户的范围
        /// </summary>
        const int USER_SEARCH_RANGE = 1000;

        #region 用户sessioin操作
        //存储用户会话状态(key为用户id)
        public static bool SaveSession(int userId, string sessionId)
        {
            if (userId <= 0) return false;
            if (string.IsNullOrEmpty(sessionId)) return false;
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_SESSION_DB, db =>
             {
                 succ = db.StringSet(userId.ToString(), sessionId);
             });
            return succ;
        }

        public static bool DeleteSession(int userId)
        {
            if (userId <= 0) return false;

            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_SESSION_DB, db =>
             {
                 succ = db.KeyDelete(userId.ToString());
             });
            return succ;
        }

        /// <summary>
        /// 获取用户会话id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserSessionId(int userId)
        {
            if (userId <= 0) return null;

            string sessionId = null;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_SESSION_DB, db =>
             {
                 sessionId = db.StringGet(userId.ToString());
             });
            return sessionId;
        }

        /// <summary>
        /// 获取用户会话列表（如果用户的会话不存在,则列表里面会存在null数据）
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static List<string> GetUserSessionIds(List<string> userIds)
        {
            if (userIds == null || userIds.Count <= 0) return null;

            List<string> sessions = new List<string>();
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_SESSION_DB, db =>
             {
                 RedisValue[] values = db.StringGet(userIds.ConvertAll(u => (RedisKey)u).ToArray());
                 if (values != null && values.Length > 0)
                 {
                     sessions.AddRange(values.ToStringArray());
                 }
             });
            return sessions;
        }

        #endregion

        #region 用户位置操作 (redis地理位置的坐标是以WGS84为标准，WGS84，全称World Geodetic System 1984，是为GPS全球定位系统使用而建立的坐标系统)

        //存储用户当前在线的位置信息
        public static bool SavePos(int userId, double lon, double lat)
        {
            if (userId <= 0) return false;
            if (lon <= 0) return false;
            if (lat <= 0) return false;

            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_GEO_DB, db =>
             {
                 succ = db.GeoAdd(RedisConfigManager.USER_GEO_KEY, lon, lat, userId.ToString());
             });
            return succ;
        }

        //获取附近的用户列表
        public static int[] GetNearUserList(double lon, double lat)
        {
            int[] userIdArray = null;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_GEO_DB, db =>
             {
                 GeoRadiusResult[] results = db.GeoRadius(RedisConfigManager.USER_GEO_KEY, lon, lat, USER_SEARCH_RANGE, GeoUnit.Miles);
                 if (results != null && results.Length > 0)
                 {
                     userIdArray = new int[results.Length];
                     for (int i = 0; i < results.Length; i++)
                     {
                         userIdArray[i] = Convert.ToInt32(results[i].Member);
                     }
                 }
             });
            return userIdArray;
        }

        #endregion

        #region 用户token操作

        //public static string createUserToken(int userId)
        //{
        //    if (userId <= 0) return null;

        //    string token = Guid.NewGuid().ToString();
        //    bool succ = false;
        //    base.DbNum = USER_TOKEN_DB;
        //    base.RedisUtils.DefaultInstance.Exec(RedisConfigManager.NEAR_CIRCLE_GEO_DB,db=>
        //    {
        //        //直接存token,不管里面有没有
        //        if (db.StringSet(userId.ToString(), token))
        //        {
        //            succ = this.SaveTokenUser(userId, token);
        //        }
        //    });

        //    if (succ)
        //    {
        //        return token;
        //    }
        //    return null;
        //}

        /// <summary>
        /// 保存用户登录票据,保存成功后返回token(key为用户id)
        /// userid:token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool SaveUserToken(int userId, string token)
        {
            if (userId <= 0) return false;
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token)) return false;
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_TOKEN_DB, db =>
             {
                 //直接存token,不管里面有没有
                 if (db.StringSet(userId.ToString(), token))
                 {
                     succ = SaveTokenUser(userId, token);
                 }
             });
            return succ;
        }

        //token:userid
        private static bool SaveTokenUser(int userId, string token, string oldToken = null)
        {
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.TOKEN_USER_DB, db =>
             {
                 if (!string.IsNullOrEmpty(oldToken))
                 {
                     db.KeyDelete(oldToken);
                 }
                 succ = db.StringSet(token, userId.ToString());
             });
            return succ;
        }

        public static int GetTokenUser(string token)
        {
            if (string.IsNullOrEmpty(token)) return 0;

            int userId = 0;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.TOKEN_USER_DB, db =>
             {
                 string id = db.StringGet(token);
                 if (!string.IsNullOrEmpty(id))
                 {
                     userId = Convert.ToInt32(id);
                 }
             });
            return userId;
        }

        public static string GetUserToken(int userId)
        {
            if (userId <= 0) return null;

            string token = null;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_TOKEN_DB, db =>
             {
                 token = db.StringGet(userId.ToString());
             });
            return token;
        }

        //后期需要应用事务
        public static bool RemoveUserToken(int userId)
        {
            if (userId <= 0) return false;
            string token = GetUserToken(userId);
            if (string.IsNullOrEmpty(token)) return true;

            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_TOKEN_DB, db =>
             {
                 succ = db.KeyDelete(userId.ToString());
             });

            RedisUtils.DefaultInstance.Exec(RedisConfigManager.TOKEN_USER_DB, db =>
             {
                 succ = db.KeyDelete(token);
             });
            return succ;
        }

        #endregion

        #region 用户group操作
        ///// <summary>
        ///// 根据用户id获取组标识
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public static string GetGroupByUserId(int userId)
        //{
        //    if (userId <= 0) return null;
        //    base.DbNum = USER_CREAT_GROUP_DB;
        //    string groupGuid = null;
        //    RedisUtils.DefaultInstance.Exec(RedisConfigManager.NEAR_CIRCLE_GEO_DB,db=>
        //    {
        //        groupGuid = db.StringGet(userId.ToString());
        //    });
        //    return groupGuid;
        //}

        //public static Dictionary<int, string> GetGroupGuids(List<int> userIds)
        //{
        //    if (userIds == null || userIds.Count <= 0) return null;

        //    Dictionary<int, string> groupGuids = new Dictionary<int, string>();
        //    base.DbNum = USER_CREAT_GROUP_DB;
        //    base.RedisUtils.DefaultInstance.Exec(RedisConfigManager.NEAR_CIRCLE_GEO_DB,db=>
        //    {
        //        RedisValue[] values = db.StringGet(userIds.ConvertAll(u => (RedisKey)u.ToString()).ToArray());
        //        if (values != null && values.Length > 0)
        //        {
        //            //groupGuids.AddRange(values.ToStringArray());
        //            for (int i = 0; i < userIds.Count; i++)
        //            {
        //                groupGuids.Add(userIds[i], values[i]);
        //            }
        //        }
        //    });
        //    return groupGuids;
        //}

        ///// <summary>
        ///// 存储这个组是哪个用户创建的
        ///// </summary>
        ///// <returns></returns>
        //public static bool CreateUserGroup(int userId, string groupGuid)
        //{
        //    if (userId <= 0) return false;
        //    if (string.IsNullOrEmpty(groupGuid)) return false;
        //    base.DbNum = USER_CREAT_GROUP_DB;
        //    bool succ = false;
        //    RedisUtils.DefaultInstance.Exec(RedisConfigManager.NEAR_CIRCLE_GEO_DB,db=>
        //    {
        //        succ = db.StringSet(userId.ToString(), groupGuid);
        //    });

        //    return succ;
        //}

        #endregion

        #region 用户基本信息操作

        ///// <summary>
        ///// 获取用户基本信息(infos:[name,headimage])
        ///// </summary>
        //public static List<string> GetUserInfo(int userId)
        //{
        //    if (userId <= 0) return null;

        //    List<string> infos = null;

        //    base.DbNum = USER_BASEINFO_DB;
        //    base.RedisUtils.DefaultInstance.Exec(RedisConfigManager.NEAR_CIRCLE_GEO_DB,db=>
        //    {
        //        string infoStr = db.StringGet(userId.ToString());
        //        if (!string.IsNullOrEmpty(infoStr))
        //        {
        //            infos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(infoStr);
        //        }
        //    });

        //    return infos;
        //}

        ///// <summary>
        ///// 添加用户基本信息(infos:[name,headimage])
        ///// </summary>
        //public static bool SaveUserInfo(int userId, List<string> infos)
        //{
        //    if (userId <= 0) return false;
        //    if (infos == null || infos.Count <= 0) return false;

        //    base.DbNum = USER_BASEINFO_DB;
        //    bool succ = false;
        //    base.RedisUtils.DefaultInstance.Exec(RedisConfigManager.NEAR_CIRCLE_GEO_DB,db=>
        //    {
        //        succ = db.StringSet(userId.ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(infos), new TimeSpan(0, USER_BASERINFO_ERPIRE_TIME, 0));
        //    });
        //    return succ;
        //}

        /// <summary>
        /// 设置备注过期
        /// </summary>
        public static bool SetUserInfoExpire(int userId)
        {
            if (userId <= 0) return false;
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_BASEINFO_DB, db =>
             {
                 succ = db.StringSet(userId.ToString(), "", new TimeSpan(0, 0, 0, 0, 1));
             });
            return succ;
        }

        #endregion

        #region 用户好友信息操作

        /// <summary>
        /// 获取好友的备注信息,(我给用户的备注,用户给我的备注)
        /// </summary>
        public static string[] GetFriendRemark(int userId, int fuserId)
        {
            if (userId <= 0 || fuserId <= 0) return null;

            string[] remarks = null;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_FRIEND_DB, db =>
             {
                 RedisKey[] keys = new RedisKey[2];
                 keys[0] = string.Format("{0}:{1}", userId, fuserId);
                 keys[1] = string.Format("{0}:{1}", fuserId, userId);
                 RedisValue[] values = db.StringGet(keys);
                 if (values != null && values.Length > 0)
                 {
                     remarks = values.ToStringArray();
                 }
             });
            return remarks;
        }

        /// <summary>
        /// 保存好友备注信息
        /// </summary>
        public static bool SaveFriendRemark(int userId, int fuserId, string fremark)
        {
            if (userId <= 0 || fuserId <= 0 || string.IsNullOrEmpty(fremark)) return false;

            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_FRIEND_DB, db =>
             {
                 string key = string.Format("{0}:{1}", userId, fuserId);
                 succ = db.StringSet(key, fremark, new TimeSpan(0, RedisConfigManager.USER_FRIEND_ERPIRE_TIME, 0));
             });
            return succ;
        }

        /// <summary>
        /// 设置备注过期
        /// </summary>
        public static bool SetFriendRemarkExpire(int userId, int fuserId)
        {
            if (userId <= 0 || fuserId <= 0) return false;
            bool succ = false;
            RedisUtils.DefaultInstance.Exec(RedisConfigManager.USER_FRIEND_DB, db =>
             {
                 string key = string.Format("{0}:{1}", userId, fuserId);
                 succ = db.StringSet(key, "", new TimeSpan(0, 0, 0, 0, 1));
             });
            return succ;
        }

        #endregion
    }
}
