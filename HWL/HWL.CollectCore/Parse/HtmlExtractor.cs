using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HWL.CollectCore.Config;

namespace HWL.CollectCore.Parse
{
    public class HtmlExtractor
    {
        private string url;
        private Encoding encoding;
        private HtmlNode htmlNode;

        public HtmlExtractor(string url = null, Encoding encoding = null)
        {
            this.url = url;
            this.encoding = encoding;

            if (encoding == null)
                encoding = Encoding.Default;
        }

        public string LoadHtml()
        {
            if (string.IsNullOrEmpty(this.url))
                throw new ArgumentNullException("Url");

            string htmlContent = CollectTools.GetHtmlContent(url, encoding);
            if (string.IsNullOrEmpty(htmlContent))
                throw new Exception($"Load html content by url = {url} and encoding = {encoding.ToString()} failure.");

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            this.htmlNode = document.DocumentNode;
            return htmlContent;
        }

        public string SetHtml(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent)) return null;

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            this.htmlNode = document.DocumentNode;
            return htmlContent;
        }

        public string GetWholeHtml()
        {
            return this.htmlNode?.InnerHtml;
        }

        public string GetWholeText()
        {
            return this.htmlNode?.InnerText;
        }

        public List<string> ParseList(XpathExtractModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.XpathRule)) return null;

            HtmlNodeCollection nodes = this.htmlNode?.SelectNodes(model.XpathRule);
            if (nodes == null || nodes.Count <= 0) return null;

            if (model.XpathEndAttributes != null && model.XpathEndAttributes.Count > 0)
            {
                return nodes.Select(n => n.Attributes.Where(a => model.XpathEndAttributes.Contains(a.Name)).Select(a => a.Value).FirstOrDefault()).
                    Where(n => !string.IsNullOrEmpty(n))
                    .ToList();
            }

            switch (model.ExtractType)
            {
                case ExtractType.Text:
                    return nodes.Select(n => n.InnerText.Trim()).Where(n => !string.IsNullOrEmpty(n)).ToList();
                case ExtractType.Html:
                default:
                    return nodes.Select(n => n.InnerHtml.Trim()).Where(n => !string.IsNullOrEmpty(n)).ToList();
            }
        }

        public string ParseFirst(XpathExtractModel model)
        {
            List<string> results = this.ParseList(model);
            if (results == null || results.Count <= 0) return null;

            return results.FirstOrDefault();
        }
    }
}
