using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HWL.Redis.Client
{
    public class DefaultRedisConnectionListener : IRedisConnectionListener
    {
        public void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            ShareConfig.LogHelper.Info($"MuxerConfigurationChanged:sender={JsonConvert.SerializeObject(sender)},e={JsonConvert.SerializeObject(e)}", typeof(DefaultRedisConnectionListener));
        }

        public void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            ShareConfig.LogHelper.Error($"MuxerConnectionFailed:sender={JsonConvert.SerializeObject(sender)},e={JsonConvert.SerializeObject(e)}", typeof(DefaultRedisConnectionListener));
        }

        public void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            ShareConfig.LogHelper.Info($"MuxerConnectionRestored:sender={JsonConvert.SerializeObject(sender)},e={JsonConvert.SerializeObject(e)}", typeof(DefaultRedisConnectionListener));
        }

        public void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            ShareConfig.LogHelper.Error($"MuxerErrorMessage:sender={JsonConvert.SerializeObject(sender)},e={JsonConvert.SerializeObject(e)}", typeof(DefaultRedisConnectionListener));
        }

        public void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            ShareConfig.LogHelper.Info($"MuxerHashSlotMoved:sender={JsonConvert.SerializeObject(sender)},e={JsonConvert.SerializeObject(e)}", typeof(DefaultRedisConnectionListener));
        }

        public void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            ShareConfig.LogHelper.Error($"MuxerInternalError:sender={JsonConvert.SerializeObject(sender)},e={JsonConvert.SerializeObject(e)}", typeof(DefaultRedisConnectionListener));
        }
    }
}
