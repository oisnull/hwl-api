using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HWL.ShareConfig
{
    public class ShareConfiguration
    {
        protected static IConfiguration Configuration;

        static ShareConfiguration()
        {
            string settingFilePath = Path.Combine(AppContext.BaseDirectory, "sharesettings.json");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile(settingFilePath, false)
                .Build();
        }

        protected static IConfigurationSection RedisSettings
        {
            get
            {
                return Configuration.GetSection("RedisSettings");
            }
        }

        protected static IConfigurationSection ApiSettings
        {
            get
            {
                return Configuration.GetSection("ApiSettings");
            }
        }

        public static string DBConnectionString
        {
            get
            {
                return Configuration.GetConnectionString("HWLDBConnectionString");
            }
        }

        protected static IConfigurationSection IMSettings
        {
            get
            {
                return Configuration.GetSection("IMSettings");
            }
        }
    }
}
