using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.CollectCore
{
    public class ExtractResult
    {
        public string OriginUrl { get; set; }
        public int Level { get; set; }
        public List<string> Hrefs { get; set; }
        public Dictionary<string, ContentResult> ContentResults { get; set; }

        public Dictionary<string, string> ConvertContentToDictionary(string separatorForList = ",")
        {
            return this.ContentResults?.Select(s => new
            {
                Key = s.Key,
                Value = s.Value.IsList ? (s.Value.Contents != null && s.Value.Contents.Count > 0) ? string.Join(separatorForList, s.Value.Contents) : null : s.Value.Content
            }).ToDictionary(k => k.Key, v => v.Value);
        }

        public string ConvertContentToJsonString(string separatorForList = ",")
        {
            return JsonConvert.SerializeObject(this.ConvertContentToDictionary(separatorForList));
        }
    }

    public class ContentResult
    {
        //public string Key { get; set; }
        public bool IsList { get; set; }
        public string Content { get; set; }
        public List<string> Contents { get; set; }
    }
}
