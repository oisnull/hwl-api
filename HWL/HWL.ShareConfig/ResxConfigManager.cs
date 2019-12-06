using System.Collections.Generic;
using System.Configuration;

namespace HWL.ShareConfig
{
    public class ResxConfigManager : ShareConfiguration
    {
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

        /// <summary>
        /// 暂时只能一个一个上传,后面再优化
        /// </summary>
        public static readonly int RESX_MAX_COUNT = 1;//限制上传资源的数量
        public static readonly long PREVIEW_IMAGE_SIZE = 30 * 1024;//当图片达到指定值时,需要压缩

        /// <summary>
        /// 定义图片允许上传的文件扩展名,默认支持(.gif,.jpg,.jpeg,.png)
        /// </summary>
        public readonly static List<string> IMAGE_FILE_TYPES = new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
        /// <summary>
        /// 默认2M
        /// </summary>
        public readonly static long IMAGE_MAX_SIZE = 2 * 1024 * 1024;

        /// <summary>
        /// 定义语音允许上传的文件扩展名,默认支持(.amr)
        /// </summary>
        public readonly static List<string> SOUND_FILE_TYPES = new List<string>() { ".amr" };
        /// <summary>
        /// 默认5M
        /// </summary>
        public readonly static int SOUND_MAX_SIZE = 5 * 1024 * 1024;
        /// <summary>
        /// 默认60s
        /// </summary>
        public readonly static long SOUND_MAX_TIME = 60;

        /// <summary>
        /// 定义视频允许上传的文件扩展名,默认支持(.mp4,.tmp)
        /// </summary>
        public readonly static List<string> VIDEO_FILE_TYPES = new List<string>() { ".mp4", ".tmp" };
        /// <summary>
        /// 默认20M
        /// </summary>
        public readonly static long VIDEO_MAX_SIZE = 20 * 1024 * 1024;
        //public readonly static int VIDEO_MAX_SIZE = 700 * 1024;//默认600K
    }
}
