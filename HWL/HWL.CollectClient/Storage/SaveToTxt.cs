using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using HWL.CollectCore;

namespace HWL.CollectClient.Storage
{
    public class SaveToTxt : IDataProcess
    {
        public string Save(string rootUrl, ExtractResult result)
        {
            if (result == null || result.ContentResults == null) return null;
            if (result.ContentResults.ContainsKey("title") && !result.ContentResults["title"].HasValue()) return null;
            if (result.ContentResults.ContainsKey("content") && !result.ContentResults["content"].HasValue()) return null;

            string saveDir = $"E:\\hwl-collect\\{HttpUtility.UrlEncode(rootUrl)}";
            string savePath = $"{saveDir}\\{Guid.NewGuid().ToString()}.txt";

            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                File.AppendAllLines(savePath, new string[] { result.OriginUrl, result.Level.ToString(), result.ConvertContentToJsonString() }, Encoding.UTF8);
                return savePath;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Save html content to path of {savePath} failed, {CollectTools.GetExceptionMessages(ex)}.");
                Console.ResetColor();
            }

            return null;
        }
    }
}
