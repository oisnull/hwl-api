using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.RabbitMQ
{
    public class MQConsumer
    {
        public const string DEFAULT_EXCHANGE_NAME = "hwl.direct";
        private static IModel channel;

        public static IModel ConsumeChannel
        {
            get
            {
                if (channel == null || channel.IsClosed)
                {
                    channel = MQConnection.ConsumeConnection.CreateModel();
                    //DIRECT("direct"), FANOUT("fanout"), TOPIC("topic"), HEADERS("headers")
                    channel.ExchangeDeclare(DEFAULT_EXCHANGE_NAME, "direct", true, false, null);
                }
                return channel;
            }
        }

        public static void BindQueue(string queueName)
        {
            QueueDeclareOk queueInfo = ConsumeChannel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            ConsumeChannel.QueueBind(queueInfo.QueueName, DEFAULT_EXCHANGE_NAME, queueInfo.QueueName, null);
        }

        public static void ReceiveMessage(string queueName, Action<string> callback)
        {
            BindQueue(queueName);
            EventingBasicConsumer consumer = new EventingBasicConsumer(ConsumeChannel);
            ConsumeChannel.BasicConsume(queueName, false, consumer);
            consumer.Received += (sender, e) =>
            {
                ConsumeChannel.BasicAck(e.DeliveryTag, false);
                if (e == null || e.Body == null || e.Body.Length <= 0) return;

                //handle logic for this
                callback(Encoding.UTF8.GetString(e.Body));
            };
        }
    }
}
