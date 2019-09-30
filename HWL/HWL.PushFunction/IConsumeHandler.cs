using HWL.PushStandard;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.PushFunction
{
    public interface IConsumeHandler
    {
        void Process(PushModel model);
    }
}
