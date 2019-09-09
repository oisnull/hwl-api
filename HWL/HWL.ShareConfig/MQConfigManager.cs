using System;
using System.Collections.Generic;
using System.Configuration;

namespace HWL.ShareConfig
{
    public class MQConfigManager : ShareConfiguration
    {
        public static int MQPort
        {
            get
            {
                return Convert.ToInt32(RabbitMQSettings["Port"]);
            }
        }

        public static string MQHost
        {
            get
            {
                return RabbitMQSettings["Host"];
            }
        }

        public static string UserName
        {
            get
            {
                return RabbitMQSettings["UserName"];
            }
        }

        public static string Password
        {
            get
            {
                return RabbitMQSettings["Password"];
            }
        }
    }
}
