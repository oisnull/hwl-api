using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HWL.ShareConfig
{
    public class LogConfigManager : ShareConfiguration
    {

        /// <summary>
        /// 值可以设置为：directory或c:\\directory
        /// 如果为directory，会自动转换为当前项目所在磁盘的根地址，比如项目地址在d:\\***\\***，则转换为d:\\directory
        /// 如果为c:\\directory，则不做任何操作
        /// </summary>
        public static string LogDirectory
        {
            get
            {
                string dir = LogSettings["LogDirectory"]?.Trim();
                if (string.IsNullOrEmpty(dir))
                {
                    return Path.GetPathRoot(AppContext.BaseDirectory);
                }

                if (Path.IsPathRooted(dir))
                {
                    return dir;
                }

                return Path.Combine(Path.GetPathRoot(AppContext.BaseDirectory), dir);
            }
        }
        public static bool EnableError { get; } = bool.Parse(LogSettings["EnableError"]);
        public static bool EnableDebug { get; } = bool.Parse(LogSettings["EnableDebug"]);
        public static bool EnableInfo { get; } = bool.Parse(LogSettings["EnableInfo"]);
        public static bool EnableWarn { get; } = bool.Parse(LogSettings["EnableWarn"]);
        public static bool EnableFatal { get; } = bool.Parse(LogSettings["EnableFatal"]);
    }
}
