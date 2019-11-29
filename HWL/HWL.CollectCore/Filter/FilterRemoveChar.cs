using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Filter
{
    public class FilterRemoveChar
    {
        public bool IsRegex { get; set; }
        public List<string> Chars { get; set; }
    }
}
