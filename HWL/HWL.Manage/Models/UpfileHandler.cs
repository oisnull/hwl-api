using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using Microsoft.AspNetCore.Http;
using HWL.ShareConfig;
using System.Text.RegularExpressions;

namespace HWL.Manage
{
    public class UpfileHandler
    {
        /// <summary>
        /// 文件保存目录路径
        /// </summary>
        static string savePath = "/upload/";

        /// <summary>
        /// 定义允许上传的文件扩展名
        /// </summary>
        static readonly string[] fileTypes = { ".gif", ".jpg", ".jpeg", ".png", ".bmp", ".apk" };
        /// <summary>
        /// 最大文件大小
        /// </summary>
        static readonly int maxSize = 50 * 1024 * 1024;//默认50M


        /// <summary>
        /// 返回img_url列表
        /// </summary>
        /// <param name="files"></param>
        /// <param name="folder"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static List<string> Process(IFormFileCollection files, string folder, out string error)
        {
            error = "";
            var dic = ProcessDic(files, folder, out error);
            if (dic != null && dic.Count > 0)
            {
                return dic.Values.ToList();
            }
            return null;
        }

        private static string GetNewFileName(string oldFileName)
        {
            string newFileName = "";
            string fileExt = Path.GetExtension(oldFileName).ToLower();
            newFileName = Regex.Replace(oldFileName, fileExt, "");
            newFileName = Regex.Replace(newFileName, @"[^\u4e00-\u9fa5_a-zA-Z0-9]", "");
            newFileName = newFileName + DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + fileExt;
            return newFileName;
        }

        /// <summary>
        /// 返回《input_name,img_url》
        /// </summary>
        /// <param name="files"></param>
        /// <param name="folder"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ProcessDic(IFormFileCollection files, string folder, out string error)
        {
            error = "";
            if (string.IsNullOrEmpty(folder))
            {
                error = "保存的目录不能为空";
                return null;
            }

            if (files == null || files.Count <= 0)
            {
                error = "上传的文件不能为空";
                return null;
            }

            IFormFile file = files.Where(f => f.Length > maxSize).FirstOrDefault();
            if (file != null)
            {
                error = file.FileName + "文件超出上传大小" + maxSize + "M";
                return null;
            }

            file = files.Where(f => !fileTypes.Contains(Path.GetExtension(f.FileName))).FirstOrDefault();
            if (file != null)
            {
                error = file.FileName + "文件格式错误";
                return null;
            }

            string savePath = DateTime.Now.ToString("yyyyMM");
            string saveDirectory = string.Format("{0}/{1}", folder, savePath);
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            Dictionary<string, string> resultPaths = new Dictionary<string, string>();
            //判断文件是否超过上传大小
            foreach (IFormFile item in files)
            {
                if (resultPaths.ContainsKey(item.FileName)) continue;

                string newFileName = GetNewFileName(item.FileName);
                using (FileStream fs = File.Create(string.Format("{0}/{1}", saveDirectory, newFileName)))
                {
                    item.CopyTo(fs);
                    fs.Flush();
                }
                resultPaths.Add(item.FileName, string.Format("{0}/{1}", savePath, newFileName));
            }

            return resultPaths;
        }
    }
}