using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Generic.Body
{
    public class CollectLogRequestBody
    {
        public string AppType { get; set; }
        public string AppVersion { get; set; }
        public string CrashInfo { get; set; }
        public string CrashDetails { get; set; }
    }
}
