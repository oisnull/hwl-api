using HWL.CollectCore.Parse;
using HWL.CollectCore.Config;
using System;
using System.Collections.Generic;
using Xunit;

namespace HWL.CollectXUnitTest
{
    public class HtmlExtractorUnitTest
    {
        [Fact]
        public void HtmlContentParseTest()
        {
            string url = "https://ai.baidu.com";
            string htmlContent = $"<div class=\"ai-nav-menu-item-list\"><a href=\"{url}\" target=\"_blank\" class=\"ai-nav-menu-item-list-item\">test001</a></div>";
            HtmlExtractor htmlExtract = new HtmlExtractor();
            htmlExtract.SetHtml(htmlContent);

            List<string> urls = htmlExtract.ParseList(new XpathExtractModel()
            {
                ExtractType = ExtractType.Text,
                XpathRule = "//a[@href]",
                XpathEndAttributes = new List<string>() { "href" }
            });

            Assert.True(urls.Exists(e => e == url));
        }

        [Fact]
        public void ContentParseTest()
        {
            string content = "China and India are making the planet greener, NASA says";
            HtmlExtractor htmlExtract = new HtmlExtractor();
            htmlExtract.SetHtml($"<div><h1 class=\"pg-headline\">{content}</h1></div>");

            string extContent = htmlExtract.ParseFirst(new XpathExtractModel()
            {
                ExtractType = ExtractType.Text,
                XpathRule = "//h1[@class='pg-headline']",
                XpathEndAttributes = null
            });

            Assert.True(extContent == content);
        }
    }
}
