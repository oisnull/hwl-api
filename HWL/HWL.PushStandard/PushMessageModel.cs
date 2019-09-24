using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.PushStandard
{
    [Serializable]
    public class PushMessageModel
    {
        public string OriginUrl { get; set; }
        public string Title { get; set; }
        public string Keys { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }
        public List<string> ImageUrls { get; set; }
        public string Content { get; set; }
        public string OriginDate { get; set; }
    }
}
