using Microsoft.Extensions.Configuration;
using System;

namespace HWL.ShareConfig
{
    public class RedisConfigManager : ShareConfiguration
    {
        /// <summary>
        /// 用户动态配置
        /// </summary>
        public static string UserDynamicRedisHosts
        {
            get
            {
                return RedisSettings["UserDynamicRedisHosts"];
            }
        }

        /// <summary>
        /// 群组好友总数量
        /// </summary>
        public static int GroupUserTotalCount
        {
            get
            {
                return Convert.ToInt32(RedisSettings["GroupUserTotalCount"]);
            }
        }

        ///// <summary>
        ///// 用户基本信息地址配置
        ///// </summary>
        //public static string UserBaseInfoRedisHosts
        //{
        //    get
        //    {
        //        return ConfigurationManager.AppSettings["UserBaseInfoRedisHosts"].ToString();
        //    }
        //}

        /// <summary>
        /// 消息处理地址配置
        /// </summary>
        public static string IMMessageRedisHosts
        {
            get
            {
                return RedisSettings["IMMessageRedisHosts"];
            }
        }

        public static string HWLManageSessionRedisHosts
        {
            get
            {
                return RedisSettings["HWLManageSessionRedisHosts"];
            }
        }

        public static string HWLManageSessionRedisInstance
        {
            get
            {
                return RedisSettings["HWLManageSessionRedisInstance"];
            }
        }

        public static int HWLManageSessionTimeOut
        {
            get
            {
                return Convert.ToInt32(RedisSettings["HWLManageSessionTimeOut"]);
            }
        }

        public static string CollectionRedisHosts
        {
            get
            {
                return RedisSettings["CollectionRedisHosts"];
            }
        }
    }
}
