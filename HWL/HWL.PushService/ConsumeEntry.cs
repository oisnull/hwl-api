using HWL.PushStandard;
using HWL.RabbitMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.PushService
{
    //public class ConsumeEntry
    //{
    //    public static void Start()
    //    {
    //        foreach (var item in PushPositionQueue.QUEUE_SYMBOLS)
    //        {
    //            MQConsumer.ReceiveMessage(item.Value, m =>
    //            {
    //                string jsonString = Encoding.UTF8.GetString(m);
    //                PushModel model = JsonConvert.DeserializeObject<PushModel>(jsonString);

    //                IConsumeHandler consumeHandler = ConsumeFactory.Create(model.PositionType);
    //                consumeHandler.Process(model);
    //            });
    //        }
    //    }
    //}
}
