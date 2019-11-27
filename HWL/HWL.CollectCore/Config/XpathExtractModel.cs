using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Config
{
    public class XpathExtractModel
    {
        public string XpathRule { get; set; }
        public List<string> XpathEndAttributes { get; set; }
        public ExtractType ExtractType { get; set; } = ExtractType.Html;
    }
}
