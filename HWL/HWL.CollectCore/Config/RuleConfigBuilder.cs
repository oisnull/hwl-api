using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HWL.CollectCore.Config
{
    public class RuleConfigBuilder
    {
        public static string GetConfigString(string configPath)
        {
            if (string.IsNullOrEmpty(configPath))
                throw new ArgumentNullException("configPath");

            using (FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                {
                    return sr.ReadToEnd().ToString();
                }
            }
        }

        public static List<RuleConfigModel> GetConfigs(string configPath)
        {
            string jsonString = GetConfigString(configPath);
            if (string.IsNullOrEmpty(jsonString))
                throw new Exception("The rule config content can't be empty.");

            return JsonConvert.DeserializeObject<List<RuleConfigModel>>(jsonString);
        }
    }
}
