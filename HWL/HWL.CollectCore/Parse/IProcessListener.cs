using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Parse
{
    public interface IProcessListener
    {
        List<string> OnNext(ExtractResult result);
        void OnFailed(string processUrl, int processLevel, Exception ex);
    }
}
