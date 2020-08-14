using HWL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Generic.Body
{
    public class SendSMSRequestBody
    {
        public string Mobile { get; set; }
    }

    public class SendSMSResponseBody
    {
        public string CurrentMobile { get; set; }
        public ResultStatus Status { get; set; }
    }
}
