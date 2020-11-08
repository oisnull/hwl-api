using StackExchange.Redis;

namespace HWL.Redis.Client
{
    public interface IRedisConnectionListener
    {
        void MuxerConfigurationChanged(object sender, EndPointEventArgs e);

        void MuxerErrorMessage(object sender, RedisErrorEventArgs e);

        void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e);

        void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e);

        void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e);

        void MuxerInternalError(object sender, InternalErrorEventArgs e);
    }
}
