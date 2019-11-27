using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HWL.CollectCore.Filter
{
    public class RuleFilterUtils
    {
        public static string Remove(string content, FilterRemoveChar removeChar)
        {
            if (removeChar == null || removeChar.Chars == null || removeChar.Chars.Count <= 0) return content;
            if (string.IsNullOrEmpty(content)) return null;

            removeChar.Chars.ForEach(f =>
            {
                if (removeChar.IsRegex)
                {
                    content = Regex.Replace(content, f, "");
                }
                else
                {
                    content = content.Replace(f, "");
                }
            });

            return content;
        }

        public static List<string> Remove(List<string> contents, FilterRemoveChar removeChar)
        {
            if (removeChar == null || removeChar.Chars == null || removeChar.Chars.Count <= 0) return contents;
            if (contents == null || contents.Count <= 0) return contents;

            List<string> results = new List<string>(contents.Count);
            string res = string.Empty;
            foreach (string con in contents)
            {
                removeChar.Chars.ForEach(f =>
                {
                    if (removeChar.IsRegex)
                    {
                        res = Regex.Replace(con, f, "");
                    }
                    else
                    {
                        res = con.Replace(f, "");
                    }
                });
                results.Add(res);
            }

            return results;
        }

        public static string Replace(string content, List<FilterReplaceChar> replaceChars)
        {
            if (replaceChars == null || replaceChars.Count <= 0) return content;
            if (string.IsNullOrEmpty(content)) return null;

            replaceChars.ForEach(f =>
            {
                if (f.IsRegex)
                {
                    content = Regex.Replace(content, f.OldChar, f.NewChar);
                }
                else
                {
                    content = content.Replace(f.OldChar, f.NewChar);
                }
            });

            return content;
        }

        public static List<string> Replace(List<string> contents, List<FilterReplaceChar> replaceChars)
        {
            if (replaceChars == null || replaceChars.Count <= 0) return contents;
            if (contents == null || contents.Count <= 0) return null;

            List<string> results = new List<string>(contents.Count);
            string res = string.Empty;
            foreach (string con in contents)
            {
                replaceChars.ForEach(f =>
                {
                    if (f.IsRegex)
                    {
                        res = Regex.Replace(con, f.OldChar, f.NewChar);
                    }
                    else
                    {
                        res = con.Replace(f.OldChar, f.NewChar);
                    }
                });
                results.Add(res);
            }

            return results;
        }
    }
}
