using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Tools
{
    public class LogAction
    {
        private static object obj = new object();
        private string _logDir;
        private string _fileName;
        public LogAction(string logDir)
        {
            this._logDir = logDir;
            this._fileName = this._logDir + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        }


        public void WriterLog(string content)
        {
            lock (obj)
            {
                if (!File.Exists(this._fileName))
                {
                    File.Create(this._fileName).Close();
                }
                FileInfo finfo = new FileInfo(this._fileName);
                using (FileStream fs = finfo.OpenWrite())
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
        }

        public string ReadLog()
        {
            string s = "";
            StreamReader sr = new StreamReader(this._fileName, Encoding.UTF8);
            s = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return s;
        }

        //public static void ClearLog(string path)
        //{
        //    if (File.Exists(path))
        //    {
        //        FileInfo finfo = new FileInfo(path);
        //        using (FileStream fs = finfo.OpenWrite())
        //        {
        //            StreamWriter sw = new StreamWriter(fs);
        //            sw.WriteLine("");
        //            sw.Flush();
        //            sw.Close();
        //            fs.Close();
        //        }
        //    }
        //}
    }
}
