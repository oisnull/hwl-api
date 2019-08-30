using StackExchange.Redis;
using System;

namespace HWL.Redis.Client
{
    public class RedisConnection
    {
        private ConnectionMultiplexer connection;
        private RedisConnectionListener connectionListener;

        public RedisConnection(string connectionString, RedisConnectionListener connectionListener = null)
        {
            this.connectionListener = connectionListener;
            this.Init(connectionString);
        }

        private void Init(string connectionString)
        {
            connection = ConnectionMultiplexer.Connect(connectionString);

            if (this.connectionListener != null)
            {
                connection.ConnectionFailed += this.connectionListener.MuxerConnectionFailed;
                connection.ConnectionRestored += this.connectionListener.MuxerConnectionRestored;
                connection.ErrorMessage += this.connectionListener.MuxerErrorMessage;
                connection.ConfigurationChanged += this.connectionListener.MuxerConfigurationChanged;
                connection.HashSlotMoved += this.connectionListener.MuxerHashSlotMoved;
                connection.InternalError += this.connectionListener.MuxerInternalError;
            }
        }

        public bool IsConnected
        {
            get { return connection != null && connection.IsConnected; }
        }

        public T Exec<T>(Func<IDatabase, T> func)
        {
            return Exec<T>(0, func);
        }

        public T Exec<T>(int dbNum, Func<IDatabase, T> func)
        {
            if (IsConnected)
            {
                var db = connection.GetDatabase(dbNum);
                return func(db);
            }

            return default(T);
        }

        public void Exec(Action<IDatabase> act)
        {
            Exec(0, act);
        }

        public void Exec(int dbNum, Action<IDatabase> act)
        {
            if (IsConnected)
            {
                var db = connection.GetDatabase(dbNum);
                act(db);
            }
        }
    }
}
