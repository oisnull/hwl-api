using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HWL.CollectCore
{
    public class CollectTools
    {
        public static string GetHtmlContent(string url, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) return null;
            if (encoding == null) encoding = Encoding.Default;

            string html = null;
            Stream myStream = null;
            WebClient webClient = new WebClient();

            try
            {
                myStream = webClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, encoding);
                html = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myStream != null) myStream.Close();
            }
            return html;
        }

        public static string GetExceptionMessages(Exception e)
        {
            List<string> messages = new List<string>() { e.Message };
            Exception ex = e;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                messages.Add(ex.Message);
            }

            return string.Join(";", messages);
        }
    }
}
