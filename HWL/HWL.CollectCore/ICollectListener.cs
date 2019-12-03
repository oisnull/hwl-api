using HWL.CollectCore.Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore
{
    public interface ICollectListener : IProcessListener
    {
        void OnStart(string desc, string url, int level);
        CollectContinueModel OnContinue(string desc, string url);
        void OnEnd(string desc, string url, int level);
    }
}
