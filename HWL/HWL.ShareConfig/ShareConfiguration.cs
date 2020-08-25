using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HWL.ShareConfig
{
    public class ShareConfiguration
    {
        private static IConfiguration _configuration;

        protected static IConfiguration Configuration
        {
            get
            {
                if (_configuration != null)
                {
                    return _configuration;
                }

                InitConfiguration();

                return _configuration;
            }
        }

        public static void InitConfiguration()
        {
            if (string.IsNullOrEmpty(CurrentEnvironment) || string.IsNullOrWhiteSpace(CurrentEnvironment))
            {
                throw new ArgumentNullException("Environment");
            }

            string settingFilePath = Path.Combine(AppContext.BaseDirectory, $"sharesettings.{CurrentEnvironment}.json");

            _configuration = new ConfigurationBuilder()
                .AddJsonFile(settingFilePath, false)
                .Build();
        }

        //protected static IConfiguration Configuration;
        //static ShareConfiguration()
        //{
        //    if (string.IsNullOrEmpty(CurrentEnvironment) || string.IsNullOrWhiteSpace(CurrentEnvironment))
        //    {
        //        throw new ArgumentNullException("Environment");
        //    }

        //    string settingFilePath = Path.Combine(AppContext.BaseDirectory, $"sharesettings.{CurrentEnvironment}.json");

        //    Configuration = new ConfigurationBuilder()
        //        .AddJsonFile(settingFilePath, false)
        //        .Build();
        //}

        public static string CurrentEnvironment { get; } = Environment.GetEnvironmentVariable("Environment")?.ToLower();

        protected static IConfigurationSection RedisSettings { get; } = Configuration.GetSection("RedisSettings");

        protected static IConfigurationSection AppSettings { get; } = Configuration.GetSection("AppSettings");

        public static string DBConnectionString { get; } = Configuration.GetConnectionString("HWLDBConnectionString");

        protected static IConfigurationSection IMSettings { get; } = Configuration.GetSection("IMSettings");

        protected static IConfigurationSection RabbitMQSettings { get; } = Configuration.GetSection("RabbitMQSettings");

        protected static IConfigurationSection LogSettings { get; } = Configuration.GetSection("LogSettings");
    }
}
