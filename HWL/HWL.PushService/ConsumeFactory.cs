using HWL.PushStandard;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.PushService
{
    public class ConsumeFactory
    {
        public static IConsumeHandler Create(PushPositionType positionType)
        {
            IConsumeHandler handler = null;
            switch (positionType)
            {
                case PushPositionType.User:
                    //handler = new UserConsumeHandler();
                    throw new NotImplementedException("UserConsumeHandler");
                case PushPositionType.Near:
                    handler = new NearCircleConsumeHandler();
                    break;
                case PushPositionType.Area:
                    throw new NotImplementedException("AreaConsumeHandler");
                default:
                    throw new NotImplementedException("ConsumeHandler");
            }
            return handler;
        }
    }
}
