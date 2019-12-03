using HWL.CollectCore.Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore
{
    public class DefaultCollectListener : ICollectListener
    {
        public void OnStart(string desc, string url, int level)
        {
        }

        public void OnEnd(string desc, string url, int level)
        {
        }

        public void OnFailed(string processUrl, int processLevel, Exception ex)
        {
        }

        public List<string> OnNext(ExtractResult result)
        {
            return result?.Hrefs;
        }

        public CollectContinueModel OnContinue(string desc, string url)
        {
            return null;
        }
    }
}
