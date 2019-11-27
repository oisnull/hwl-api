using System;
using System.Collections.Generic;
using System.Text;
using HWL.CollectCore.Config;
using HWL.CollectCore.Filter;
using HWL.CollectCore.Parse;
using HWL.CollectCore;
using Newtonsoft.Json;
using System.Linq;

namespace HWL.CollectClient
{
    class Program
    {
        static void UrlContentTest()
        {
            string htmlContent = CollectTools.GetHtmlContent("https://news.163.com/19/1127/09/EUVS3V370001899O.html", Encoding.GetEncoding("GB2312"));
            Console.WriteLine(htmlContent);
            Console.ReadLine();
        }

        static void HtmlContentParseTest()
        {
            string htmlContent = "<div class=\"ai-nav-menu-item-list\"><a href=\"https://ai.baidu.com/tech/speech/asr\" target=\"_blank\" class=\"ai-nav-menu-item-list-item\">test001</a></div>";
            HtmlExtractor htmlExtract = new HtmlExtractor();
            htmlExtract.SetHtml(htmlContent);

            List<string> urls = htmlExtract.ParseList(new XpathExtractModel()
            {
                ExtractType = ExtractType.Text,
                XpathRule = "//a[@href]",
                XpathEndAttributes = new List<string>() { "href" }
            });

            urls?.ForEach(f => Console.WriteLine(f));
            Console.ReadLine();
        }

        static void UrlParseTest()
        {
            HtmlExtractor htmlExtract = new HtmlExtractor("https://edition.cnn.com/2019/02/13/world/china-india-greener-planet-scli-intl/index.html");
            htmlExtract.LoadHtml();

            List<string> urls = htmlExtract.ParseList(new XpathExtractModel()
            {
                ExtractType = ExtractType.Text,
                XpathRule = "//a[@href]",
                XpathEndAttributes = new List<string>() { "href" }
            });

            urls?.ForEach(f => Console.WriteLine(f));
            Console.ReadLine();
        }

        static void ContentParseTest()
        {
            HtmlExtractor htmlExtract = new HtmlExtractor("https://edition.cnn.com/2019/02/13/world/china-india-greener-planet-scli-intl/index.html");
            htmlExtract.LoadHtml();

            string content = htmlExtract.ParseFirst(new XpathExtractModel()
            {
                ExtractType = ExtractType.Text,
                XpathRule = "//h1[@class='pg-headline']",
                XpathEndAttributes = null
            });

            Console.WriteLine(content);
            Console.ReadLine();
        }

        static void LevelParseTest()
        {
            List<RuleExtractConfigModel> rules = new List<RuleExtractConfigModel>()
            {
                new RuleExtractConfigModel()
                {
                    Key="title",
                    IsList=false,
                    Xpath=new XpathExtractModel()
                    {
                        ExtractType= ExtractType.Text,
                        XpathRule="//div[@class='article-title']"
                    }
                },
                new RuleExtractConfigModel()
                {
                    Key="date",
                    IsList=false,
                    Xpath=new XpathExtractModel()
                    {
                        ExtractType=ExtractType.Text,
                        XpathRule="//div[@class='article-source article-source-bjh']",
                    },
                    Filter=new FilterModel()
                    {
                        RemoveChar=new FilterRemoveChar()
                        {
                            IsRegex=false,
                            Chars=new List<string>(){ "时间：" }
                        }
                    }
                },
                new RuleExtractConfigModel()
                {
                    Key="images",
                    IsList=true,
                    Xpath=new XpathExtractModel()
                    {
                        ExtractType= ExtractType.Text,
                        XpathRule="//div[@class='article-content']//img[@src]",
                        XpathEndAttributes = new List<string>() { "src" }
                    }
                }
            };
            RuleParser ruleParser = new RuleParser("http://baijiahao.baidu.com/s?id=1650146502027667116", 3, rules);
            ruleParser.LevelProcess(new CollectProcessListener());
        }

        static void RuleConfigTest()
        {
            List<RuleExtractConfigModel> rules = new List<RuleExtractConfigModel>()
            {
                new RuleExtractConfigModel()
                {
                    Key="title",
                    IsList=false,
                    Xpath=new XpathExtractModel()
                    {
                        ExtractType= ExtractType.Text,
                        XpathRule="//div[@class='detial_01']/h1[1]"
                    },
                    Filter=new FilterModel()
                    {
                        ReplaceChars=new List<FilterReplaceChar>()
                        {
                            new FilterReplaceChar(){ IsRegex=false,OldChar="博彦科技：", NewChar=""}
                        }
                    }
                },
                new RuleExtractConfigModel()
                {
                    Key="date",
                    IsList=false,
                    Xpath=new XpathExtractModel()
                    {
                        ExtractType=ExtractType.Text,
                        XpathRule="//div[@class='lab clearfix mrg-btm20']/span[2]",
                    },
                    Filter=new FilterModel()
                    {
                        RemoveChar=new FilterRemoveChar()
                        {
                            IsRegex=false,
                            Chars=new List<string>(){ "时间：" }
                        }
                    }
                },
                new RuleExtractConfigModel()
                {
                    Key="images",
                    IsList=true,
                    Xpath=new XpathExtractModel()
                    {
                        ExtractType= ExtractType.Text,
                        XpathRule="//div[@class='detial_01']//img/@src",
                        XpathEndAttributes = new List<string>() { "src" }
                    }
                }
            };
            Console.WriteLine(JsonConvert.SerializeObject(rules));
        }

        static void RuleConfigFileTest()
        {
            string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}/rule.json";
            RuleConfigModel config = RuleConfigBuilder.GetConfigs(configPath).Where(c => c.Description == "163-news").FirstOrDefault();
            config.Url = "https://news.163.com/19/1127/09/EUVS3V370001899O.html";
            config.Level = 0;

            Collection collection = new Collection();
            collection.CollectListener = () => new CollectProcessListener();
            collection.IsContinue = false;
            collection.Execute(config);

            Console.ReadLine();
        }

        static void CollectionTest()
        {
            string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}/rule.json";

            Collection collection = new Collection(configPath);
            collection.CollectListener = () => new CollectProcessListener();
            collection.Execute();

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //UrlContentTest();

            //HtmlContentParseTest();

            //UrlParseTest();

            //ContentParseTest();

            //LevelParseTest();

            //RuleConfigTest();

            RuleConfigFileTest();

            //CollectionTest();
        }
    }
}
