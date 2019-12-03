using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Filter
{
    public class FilterReplaceChar
    {
        public bool IsRegex { get; set; }
        public string OldChar { get; set; }
        public string NewChar { get; set; }
    }
}
