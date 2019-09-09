using HWL.ShareConfig;
using RabbitMQ.Client;
using System;

namespace HWL.RabbitMQ
{
    public class MQConnection
    {
        private static IConnection _publishConnection = null;
        private static IConnection _consumeConnection = null;

        public static IConnection PublishConnection
        {
            get
            {
                if (_publishConnection == null || !_publishConnection.IsOpen)
                {
                    _publishConnection = CreateConnection();
                }
                return _publishConnection;
            }
        }

        public static IConnection ConsumeConnection
        {
            get
            {
                if (_consumeConnection == null || !_consumeConnection.IsOpen)
                {
                    _consumeConnection = CreateConnection();
                }
                return _consumeConnection;
            }
        }

        public static IConnection CreateConnection()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = MQConfigManager.MQHost,
                Port = MQConfigManager.MQPort,
                UserName = MQConfigManager.UserName,
                Password = MQConfigManager.Password,
            };
            return factory.CreateConnection();
        }

    }
}
