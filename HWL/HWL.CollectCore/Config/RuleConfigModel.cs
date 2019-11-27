using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Config
{
    public class RuleConfigModel
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public string Charset { get; set; }
        /// <summary>
        /// -1:unlimited(default), 0:entrance page, 1...
        /// </summary>
        public int Level { get; set; } = -1;
        public List<RuleExtractConfigModel> Rules { get; set; }
        public bool Enable { get; set; } = true;

        public Encoding GetEncoding()
        {
            if (string.IsNullOrEmpty(this.Charset))
                return Encoding.Default;

            return Encoding.GetEncoding(this.Charset);
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Url)
                && this.Rules != null
                && this.Rules.Count > 0;
        }
    }
}
