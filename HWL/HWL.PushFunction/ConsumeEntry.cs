using HWL.PushStandard;
using HWL.RabbitMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.PushFunction
{
    public class ConsumeEntry
    {
        public static void Process()
        {
            foreach (var item in PushPositionQueue.QUEUE_SYMBOLS)
            {
                MQConsumer.ReceiveMessage(item.Value, m =>
                {
                    string jsonString = Encoding.UTF8.GetString(m);
                    PushModel model = JsonConvert.DeserializeObject<PushModel>(jsonString);


                });
            }
        }
    }
}
