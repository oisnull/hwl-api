using HWL.CollectCore;
using HWL.CollectCore.Config;
using HWL.CollectCore.Filter;
using HWL.CollectCore.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HWL.CollectXUnitTest
{
    public class RuleParserUnitTest
    {
        //[Fact]
        //public void ContentParseTest()
        //{
        //    List<RuleExtractConfigModel> rules = new List<RuleExtractConfigModel>()
        //    {
        //        new RuleExtractConfigModel()
        //        {
        //            Key="title",
        //            IsList=false,
        //            Xpath=new XpathExtractModel()
        //            {
        //                ExtractType= ExtractType.Text,
        //                XpathRule="//div[@class='detial_01']/h1[1]"
        //            },
        //            Filter=new FilterModel()
        //            {
        //                ReplaceChars=new List<FilterReplaceChar>()
        //                {
        //                    new FilterReplaceChar(){ IsRegex=false,OldChar="博彦科技：", NewChar=""}
        //                }
        //            }
        //        },
        //        new RuleExtractConfigModel()
        //        {
        //            Key="date",
        //            IsList=false,
        //            Xpath=new XpathExtractModel()
        //            {
        //                ExtractType=ExtractType.Text,
        //                XpathRule="//div[@class='lab clearfix mrg-btm20']/span[2]",
        //            },
        //            Filter=new FilterModel()
        //            {
        //                RemoveChar=new FilterRemoveChar()
        //                {
        //                    IsRegex=false,
        //                    Chars=new List<string>(){ "时间：" }
        //                }
        //            }
        //        },
        //        new RuleExtractConfigModel()
        //        {
        //            Key="images",
        //            IsList=true,
        //            Xpath=new XpathExtractModel()
        //            {
        //                ExtractType= ExtractType.Text,
        //                XpathRule="//div[@class='detial_01']//img/@src",
        //                XpathEndAttributes = new List<string>() { "src" }
        //            }
        //        }
        //    };
        //    RuleParser ruleParser = new RuleParser("https://www.beyondsoft.com/content/details9_2740.html", rules);
        //    ExtractResult extractResult = ruleParser.Process();

        //    string title = "“后楼宇时代”的建筑精细化管理";
        //    string date = "2019/10/17";
        //    int imageCount = 2;

        //    Assert.True(extractResult.ContentResults["title"].Content == title);
        //    Assert.True(extractResult.ContentResults["date"].Content == date);
        //    Assert.True(extractResult.ContentResults["images"].Contents.Count() == imageCount);
        //}

        //[Fact]
        //public void LevelContentParseTest()
        //{
        //    List<RuleExtractConfigModel> rules = new List<RuleExtractConfigModel>()
        //    {
        //        new RuleExtractConfigModel()
        //        {
        //            Key="title",
        //            IsList=false,
        //            Xpath=new XpathExtractModel()
        //            {
        //                ExtractType= ExtractType.Text,
        //                XpathRule="//div[@class='detial_01']/h1[1]"
        //            }
        //        },
        //        new RuleExtractConfigModel()
        //        {
        //            Key="date",
        //            IsList=false,
        //            Xpath=new XpathExtractModel()
        //            {
        //                ExtractType=ExtractType.Text,
        //                XpathRule="//div[@class='lab clearfix mrg-btm20']/span[2]",
        //            },
        //            Filter=new FilterModel()
        //            {
        //                RemoveChar=new FilterRemoveChar()
        //                {
        //                    IsRegex=false,
        //                    Chars=new List<string>(){ "时间：" }
        //                }
        //            }
        //        },
        //        new RuleExtractConfigModel()
        //        {
        //            Key="images",
        //            IsList=true,
        //            Xpath=new XpathExtractModel()
        //            {
        //                ExtractType= ExtractType.Text,
        //                XpathRule="//div[@class='detial_01']//img/@src",
        //                XpathEndAttributes = new List<string>() { "src" }
        //            }
        //        }
        //    };
        //    RuleParser ruleParser = new RuleParser("https://www.beyondsoft.com/index.html", rules);
        //    ruleParser.LevelProcess(null);
        //}
    }
}
