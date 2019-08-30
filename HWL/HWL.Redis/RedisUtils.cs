using HWL.Redis.Client;
using HWL.ShareConfig;
using System;

namespace HWL.Redis
{
    public class RedisUtils
    {
        private static object _locker = new Object();
        private static RedisConnection _instance1 = null;
        private static RedisConnection _instance2 = null;

        public static RedisConnection DefaultInstance
        {
            get
            {
                if (_instance1 == null)
                {
                    lock (_locker)
                    {
                        if (_instance1 == null || !_instance1.IsConnected)
                        {
                            _instance1 = new RedisConnection(RedisConfigManager.UserDynamicRedisHosts);
                        }
                    }
                }
                return _instance1;
            }
        }

        public static RedisConnection MessageInstance
        {
            get
            {
                if (_instance2 == null)
                {
                    lock (_locker)
                    {
                        if (_instance2 == null || !_instance2.IsConnected)
                        {
                            _instance2 = new RedisConnection(RedisConfigManager.MsgHandlerRedisHosts);
                        }
                    }
                }
                return _instance2;
            }
        }
    }
}
