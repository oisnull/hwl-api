using HWL.CollectCore.Config;
using HWL.CollectCore.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HWL.CollectCore.Parse
{
    public class RuleParser
    {
        private string entranceUrl;
        private Encoding encoding;
        private int configLevel;
        private List<RuleExtractConfigModel> rules;
        private IFilterText filterText;
        private IFilterHref filterHref;
        //0:entrance page -1:unlimited 
        private readonly static int INIT_LEVEL = 0;
        private readonly static XpathExtractModel XPATH_FOR_HREFS = new XpathExtractModel()
        {
            ExtractType = ExtractType.Text,
            XpathRule = "//a[@href]",
            XpathEndAttributes = new List<string>() { "href" }
        };

        public RuleParser(string entranceUrl, List<RuleExtractConfigModel> rules) : this(entranceUrl, INIT_LEVEL, Encoding.Default, rules) { }

        public RuleParser(string entranceUrl, int level, List<RuleExtractConfigModel> rules) : this(entranceUrl, level, Encoding.Default, rules) { }

        public RuleParser(string entranceUrl, int level, Encoding encoding, List<RuleExtractConfigModel> rules)
        {
            this.entranceUrl = entranceUrl;
            this.configLevel = level;
            this.encoding = encoding;
            this.rules = rules;
            this.filterText = new DefaultFilterText();
            this.filterHref = new DefaultFilterHref(entranceUrl);
        }

        public void LevelProcess(IProcessListener processListener)
        {
            if (processListener == null)
                throw new ArgumentNullException(typeof(IProcessListener).Name);

            LevelProcess(this.entranceUrl, INIT_LEVEL, processListener);
        }

        public void LevelProcessContinue(string continueUrl, int continueLevel, IProcessListener processListener)
        {
            if (processListener == null)
                throw new ArgumentNullException(typeof(IProcessListener).Name);

            if (string.IsNullOrEmpty(continueUrl) || string.IsNullOrWhiteSpace(continueUrl))
                return;

            LevelProcess(continueUrl, continueLevel, processListener);
        }

        private void LevelProcess(string levelUrl, int extractLevel, IProcessListener processListener)
        {
            List<string> processUrls = null;
            try
            {
                ExtractResult result = Process(levelUrl, extractLevel);
                processUrls = processListener.OnNext(result);
            }
            catch (Exception ex)
            {
                processListener.OnFailed(levelUrl, extractLevel, ex);
                return;
            }

            if (configLevel <= -1 || extractLevel < this.configLevel)
            {
                if (processUrls != null && processUrls.Count > 0)
                {
                    extractLevel++;
                    foreach (var item in processUrls)
                    {
                        this.LevelProcess(item, extractLevel, processListener);
                    }
                }
            }
        }

        public ExtractResult Process()
        {
            return Process(entranceUrl, 0);
        }

        public ExtractResult Process(string extractUrl, int currentLevel)
        {
            if (this.rules == null || this.rules.Count <= 0)
                return null;

            HtmlExtractor htmlExtractor = new HtmlExtractor(extractUrl, encoding);
            htmlExtractor.LoadHtml();
            List<string> hrefs = htmlExtractor.ParseList(XPATH_FOR_HREFS);

            Dictionary<string, ContentResult> contents = new Dictionary<string, ContentResult>();
            foreach (var item in this.rules)
            {
                ContentResult content = new ContentResult()
                {
                    IsList = item.IsList
                };
                if (item.IsList)
                {
                    content.Contents = filterText.GetList(htmlExtractor.ParseList(item.Xpath), item.Filter);
                }
                else
                {
                    content.Content = filterText.GetFirst(htmlExtractor.ParseFirst(item.Xpath), item.Filter);
                }
                contents.Add(item.Key, content);
            }

            return new ExtractResult()
            {
                OriginUrl = extractUrl,
                Level = currentLevel,
                Hrefs = filterHref.SupplyList(hrefs),
                ContentResults = contents
            };
        }
    }
}
