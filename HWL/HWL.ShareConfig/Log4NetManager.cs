using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HWL.ShareConfig
{
    public class Log4NetManager
    {
        private static ILoggerRepository repository { get; set; }
        private static bool _disable;
        private static ILog _log;
        private static ILog log
        {
            get
            {
                if (_log == null)
                {
                    InitConfigure();
                }
                return _log;
            }
        }

        public static void Enable()
        {
            _disable = false;
        }

        public static void Disable()
        {
            _disable = true;
        }

        public static void InitConfigure(string repositoryName = "HWLRepository")
        {
            string configFilePath = Path.Combine(AppContext.BaseDirectory, "log4net.config");
            repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(repository, new FileInfo(configFilePath));
            _log = LogManager.GetLogger(repositoryName, "");
            _disable = false;
        }

        public static void Info(string msg)
        {
            if (!_disable)
                log.Info(msg);
        }

        public static void Warn(string msg)
        {
            if (!_disable)
                log.Warn(msg);
        }

        public static void Error(string msg)
        {
            if (!_disable)
                log.Error(msg);
        }
    }
}
