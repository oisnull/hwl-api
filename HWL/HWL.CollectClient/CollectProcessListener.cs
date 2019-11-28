using HWL.CollectClient.Storage;
using HWL.CollectCore;
using HWL.Redis;
using System;
using System.Collections.Generic;

namespace HWL.CollectClient
{
    public class CollectProcessListener : ICollectListener
    {
        private string rootUrl;
        public IDataProcess DataProcess { get; set; }

        public void OnStart(string desc, string url, int level)
        {
            rootUrl = url;

            Console.WriteLine($"Start collect {desc} site, entrance page is {url},level is {level}.");
        }

        public CollectContinueModel OnContinue(string desc, string url)
        {
            var model = CollectionStore.GetFirstUrl(url);
            if (model != null && model.IsValid())
            {
                return new CollectContinueModel()
                {
                    ContinueUrl = model.Url,
                    ContinueLevel = model.Level
                };
            }
            return null;
        }

        public void OnEnd(string desc, string url, int level)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{desc} collect completed, end page is {url},current level is {level}.");
            Console.ResetColor();
        }

        public void OnFailed(string processUrl, int processLevel, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{processLevel}, {processUrl}, {CollectTools.GetExceptionMessages(ex)}");
            Console.ResetColor();

            //delete url from 0
            //move url to 1
            CollectionStore.MoveUrl(rootUrl, new CollectionStore.UrlModel()
            {
                Url = processUrl,
                Level = processLevel,
                localFileName = null
            });
        }

        public List<string> OnNext(ExtractResult e)
        {
            List<string> existUrls = CollectionStore.GetExistUrls(rootUrl, e.Hrefs);
            if (existUrls != null && existUrls.Count > 0)
                e.Hrefs.RemoveAll(u => existUrls.Contains(u));

            string localFileName = DataProcess?.Save(rootUrl, e);

            //delete url from 0
            //move url to 1
            CollectionStore.MoveUrl(rootUrl, new CollectionStore.UrlModel()
            {
                Url = e.OriginUrl,
                Level = e.Level,
                localFileName = localFileName
            });

            //save into redis
            CollectionStore.AddUrls(rootUrl, e.Hrefs, e.Level);

            Console.WriteLine($"{e.Level}, {e.OriginUrl}, {e.Hrefs?.Count}");

            return e.Hrefs;
        }
    }
}
