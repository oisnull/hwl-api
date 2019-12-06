using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.RabbitMQ
{
    public class MQPublisher
    {
        public const string DEFAULT_EXCHANGE_NAME = "hwl.direct";
        private static IModel channel;

        protected static IModel PublishChannel
        {
            get
            {
                if (channel == null || channel.IsClosed)
                {
                    channel = MQConnection.PublishConnection.CreateModel();
                    //DIRECT("direct"), FANOUT("fanout"), TOPIC("topic"), HEADERS("headers")
                    //channel.ExchangeDeclare(DEFAULT_EXCHANGE_NAME, "direct", true, false, null);
                }
                return channel;
            }
        }

        public static void PushMessage(string routingKey, byte[] messageBytes)
        {
            PublishChannel.BasicPublish(exchange: DEFAULT_EXCHANGE_NAME,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: messageBytes);
        }
    }
}
