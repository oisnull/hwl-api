using HWL.PushService;
using HWL.PushStandard;
using HWL.RabbitMQ;
using HWL.Tools;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace HWL.PushClient
{
    class Program
    {
        static void Test1()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "liytest",
                Password = "ya.li.4321",
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static void Test2()
        {
            string testQueue = "test-1";
            //string msg = "Hello world";

            MQConsumer.ReceiveMessage(testQueue, m =>
            {
                Console.WriteLine(m);
            });

            while (true)
            {
                string inputText = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputText) && !string.IsNullOrWhiteSpace(inputText))
                {
                    MQPublisher.PushMessage(testQueue, Encoding.UTF8.GetBytes(inputText));
                }
            }
        }

        static void Test3()
        {
            foreach (var item in PushPositionQueue.QUEUE_SYMBOLS)
            {
                Console.WriteLine($"{item.Value} receive start...");
                MQConsumer.ReceiveMessage(item.Value, m =>
                {
                    string msg = Encoding.UTF8.GetString(m);
                    Console.WriteLine($"{item.Value}:{msg}");
                });
            }

            Func<string, PushModel> CreateModel = (txt) =>
            {
                return new PushModel()
                {
                    //PositionType = PushPositionType.User,
                    PositionType = PushPositionType.Near,
                    PositionModel = new PushPositionModel()
                    {
                        UserId = 1,
                    },
                    SourceType = SourceType.TestCreate,
                    PushMessageType = 0,
                    MessageModel = new PushMessageModel()
                    {
                        Content = $"content-{txt}-test",
                    },
                };
            };

            while (true)
            {
                string inputText = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputText) && !string.IsNullOrWhiteSpace(inputText))
                {
                    PushModel model = CreateModel(inputText);
                    PushEntry.Process(model);
                }
            }
        }

        static void Test4()
        {
            foreach (var item in PushPositionQueue.QUEUE_SYMBOLS)
            {
                Console.WriteLine($"{item.Value} receive start...");
                MQConsumer.ReceiveMessage(item.Value, m =>
                {
                    string msg = Encoding.UTF8.GetString(m);
                    Console.WriteLine($"{item.Value}:{msg}");

                    try
                    {
                        PushModel model = JsonConvert.DeserializeObject<PushModel>(msg);
                        IConsumeHandler consumeHandler = ConsumeFactory.Create(model.PositionType);
                        consumeHandler.Process(model);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Process failure:{CommonCs.GetExceptionMessages(ex)}");
                        Console.ResetColor();
                    }
                });
            }
        }

        //put model into queue
        static void Main(string[] args)
        {
            //Test1();

            //Test2();

            //Test3();

            Test4();
        }
    }
}
