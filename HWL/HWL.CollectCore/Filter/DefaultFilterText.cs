using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.CollectCore.Filter
{
    /// <summary>
    /// Priority:XpathExtractModel > DefaultValue > RemoveChar > ReplaceChars
    /// </summary>
    public class DefaultFilterText : IFilterText
    {
        private bool IsDefault(FilterModel filter)
        {
            return filter != null && filter.DefaultValues != null && filter.DefaultValues.Count > 0;
        }

        private bool IsRemove(FilterModel filter)
        {
            return filter != null && filter.RemoveChar != null && filter.RemoveChar.Chars != null && filter.RemoveChar.Chars.Count > 0;
        }

        private bool IsReplace(FilterModel filter)
        {
            return filter != null && filter.ReplaceChars != null && filter.ReplaceChars.Count > 0;
        }

        public string GetFirst(string content, FilterModel filter)
        {
            if (IsDefault(filter) && string.IsNullOrEmpty(content))
            {
                content = filter.DefaultValues?.FirstOrDefault();
            }

            if (IsRemove(filter))
            {
                content = RuleFilterUtils.Remove(content, filter.RemoveChar);
            }

            if (IsReplace(filter))
            {
                content = RuleFilterUtils.Replace(content, filter.ReplaceChars);
            }

            return content;
        }

        public List<string> GetList(List<string> contents, FilterModel filter)
        {
            List<string> results = new List<string>();
            if (IsDefault(filter) && (contents == null || contents.Count <= 0))
            {
                results.AddRange(filter.DefaultValues);
            }
            else
            {
                if (contents != null && contents.Count > 0)
                    results.AddRange(contents);
            }

            if (IsRemove(filter))
            {
                results = RuleFilterUtils.Remove(results, filter.RemoveChar);
            }

            if (IsReplace(filter))
            {
                results = RuleFilterUtils.Replace(results, filter.ReplaceChars);
            }

            return results;
        }
    }
}
