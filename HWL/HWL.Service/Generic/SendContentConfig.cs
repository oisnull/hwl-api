using System;

namespace HWL.Service.Generic
{
    public class SendContentConfig
    {

        /// <summary>
        /// title,content
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, string> EmailRegisterDesc(string code)
        {
            string title = $"{ShareConfig.AppConfigManager.DefaultAppName}1.0注册";
            string content = "您当前的注册验证码是：" + code;
            return new Tuple<string, string>(title, content);
        }

        /// <summary>
        /// title,content
        /// </summary>
        /// <returns></returns>
        public static string SMSRegisterDesc(string code)
        {
            string content = $"您当前的{ShareConfig.AppConfigManager.DefaultAppName}1.0注册验证码是：{code}";
            return content;
        }

    }
}
