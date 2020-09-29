using System;
using System.IO;

namespace HWL.ShareConfig
{
    public class AppConfigManager : ShareConfiguration
    {
        public static string DefaultAppName { get; } = "ZiChat";
        public static string DefaultEAppName { get; } = "ZL";
        public static string CheckCodeForDebug { get; } = "888888";


        /// <summary>
        /// 用户登录过期时间设置,按天算,指在当前时间往后加的天数
        /// </summary>
        public static int UserLoginExpireDay
        {
            get
            {
                return Convert.ToInt32(AppSettings["UserLoginExpireDay"]);
            }
        }

        /// <summary>
        /// 用户验证时,发送的验证码过期时间配置,单位：秒
        /// </summary>
        public static int UserCodeExpireSecond
        {
            get
            {
                return Convert.ToInt32(AppSettings["UserCodeExpireSecond"]);
            }
        }

        /// <summary>
        /// 文件上传目录配置,值为当前计算机的绝对地址
        /// </summary>
        public static string UploadDirectory
        {
            get
            {
                return AppSettings["UploadDirectory"];
            }
        }

        /// <summary>
        /// 文件对外访问的地址配置,值以http://或者https://开头
        /// </summary>
        public static string FileAccessUrl
        {
            get
            {
                return AppSettings["FileAccessUrl"];
            }
        }

        public static string UserDefaultHeadImage
        {
            get
            {
                return Path.Combine(FileAccessUrl, "user/default.png");
            }
        }

        public static string UserCircleBackImage
        {
            get
            {
                return Path.Combine(FileAccessUrl, "user/circleback.png");
            }
        }

        /// <summary>
        /// 添加好友总数量上限
        /// </summary>
        public static int UserAddFriendTotalCount
        {
            get
            {
                return Convert.ToInt32(AppSettings["UserAddFriendTotalCount"]);
            }
        }

        /// <summary>
        /// 每天添加好友数量上限
        /// </summary>
        public static int UserAddFriendDayCount
        {
            get
            {
                return Convert.ToInt32(AppSettings["UserAddFriendDayCount"]);
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

        public const int GROUP_USER_IMAGE_COUNT = 9;
        /// <summary>
        /// 搜索附近组的范围初始值
        /// </summary>
        public const int SEARCH_NEAR_GROUP_RANGE = 500;//unit:m
        /// <summary>
        /// 搜索附近信息的范围初始值
        /// </summary>
        public const int SEARCH_NEAR_CIRCLE_RANGE = 500;//unit:m
    }
}
