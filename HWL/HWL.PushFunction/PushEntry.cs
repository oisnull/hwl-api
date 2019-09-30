using HWL.PushStandard;
using Newtonsoft.Json;
using System;
using System.Text;

namespace HWL.PushFunction
{
    public class PushEntry
    {
        public static void Process(PushModel model)
        {
            if (model == null)
                throw new ArgumentNullException("PushModel");

            if (model.PositionModel == null)
                throw new ArgumentNullException("PositionModel");

            string queueName = PushPositionQueue.GetQueueName(model.PositionType);
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            RabbitMQ.MQPublisher.PushMessage(queueName, bytes);
        }


    }
}
