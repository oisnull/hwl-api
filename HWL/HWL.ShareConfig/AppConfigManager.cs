﻿using System;
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

        public const int GROUP_USER_IMAGE_COUNT = 9;
        public const int SEARCH_NEAR_GROUP_RANGE = 100;//unit:m
    }
}
