using HWL.PushStandard;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.PushService
{
    public interface IConsumeHandler
    {
        void Process(PushModel model);
    }
}
