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
            string env = Environment.GetEnvironmentVariable("Environment")?.ToLower();
            if (string.IsNullOrEmpty(env) || string.IsNullOrWhiteSpace(env))
            {
                throw new ArgumentNullException("Environment");
            }

            string settingFilePath = Path.Combine(AppContext.BaseDirectory, $"sharesettings.{env}.json");

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

        protected static IConfigurationSection AppSettings
        {
            get
            {
                return Configuration.GetSection("AppSettings");
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

        protected static IConfigurationSection RabbitMQSettings
        {
            get
            {
                return Configuration.GetSection("RabbitMQSettings");
            }
        }
    }
}
