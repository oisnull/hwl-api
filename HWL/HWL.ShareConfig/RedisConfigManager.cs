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


        /*
         * 功能描述：
         * 1,存储用户的token,格式：db=0 set userid token
         * 1.1,存储token对应的用户,格式：db=1 set token userid
         * 
         * 2,存储用户的sessionid,格式：db=2 set userid sessionid
         * 3,存储组所属用户的标识,格式：db=3 set userid groupGuid
         * 
         * 4,存储用户的位置pos,格式：db=4 geo user:pos lat lon userid
         * 
         * 5,存储用户的基本信息,格式:db=7 set userid [name,headimage] 过期时间为 5分钟
         * 6,存储用户好友信息,格式：db=8 set userid:fuserid remark 过期时间为 5分钟
         */

        public const int USER_TOKEN_DB = 00;
        public const int TOKEN_USER_DB = 01;
        public const int USER_SESSION_DB = 02;
        //const int USER_CREAT_GROUP_DB = 03;
        public const int USER_GEO_DB = 04;
        public const int USER_BASEINFO_DB = 07;
        public const int USER_FRIEND_DB = 08;
        public const int USER_OFFLINE_MESSAGE_DB = 09;

        public const int USER_BASERINFO_ERPIRE_TIME = 30;//用户基本信息过期时间配置，单位：分钟
        public const int USER_FRIEND_ERPIRE_TIME = 30;//用户对应的好友信息过期时间配置，单位：分钟

        /// <summary>
        /// 附近圈子信息所在的数据库
        /// </summary>
        public const int NEAR_CIRCLE_GEO_DB = 20;

        /// <summary>
        /// 组所在的数据库
        /// </summary>
        public const int GROUP_GEO_DB = 10;
        /// <summary>
        /// 组的用户set集合所在的数据库
        /// </summary>
        public const int GROUP_USER_SET_DB = 11;
        /// <summary>
        /// 个人组所在的数据库
        /// </summary>
        //public const int GROUP_DB = 12;

        public const int COLLECTION_HREFS_DB = 21;
    }
}
