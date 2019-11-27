using HWL.CollectCore.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Config
{
    public class RuleExtractConfigModel
    {
        public string Key { get; set; }
        public bool IsList { get; set; }
        public XpathExtractModel Xpath { get; set; }
        public FilterModel Filter { get; set; }
    }
}
