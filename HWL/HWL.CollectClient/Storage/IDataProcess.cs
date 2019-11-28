using HWL.CollectCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectClient.Storage
{
    public interface IDataProcess
    {
        string Save(string rootUrl, ExtractResult result);
    }
}
