using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Filter
{
    public class FilterModel
    {
        public List<string> DefaultValues { get; set; }
        public FilterRemoveChar RemoveChar { get; set; }
        public List<FilterReplaceChar> ReplaceChars { get; set; }
    }
}
