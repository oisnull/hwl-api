using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.PushStandard
{
    public class PushPositionQueue
    {
        public static readonly Dictionary<PushPositionType, string> QUEUE_SYMBOLS = new Dictionary<PushPositionType, string>()
        {
            { PushPositionType.User,"hwl.messages.users" },
            { PushPositionType.Near,"hwl.messages.near" },
            { PushPositionType.Area,"hwl.messages.area" },
        };

        public static string GetQueueName(PushPositionType positionType)
        {
            return QUEUE_SYMBOLS[positionType];
        }
    }
}
