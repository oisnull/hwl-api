using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HWL.Tools.Resx
{
    public class ResxHandler
    {
        public List<string> ResxTypes { get; set; }
        /// <summary>
        /// Unit:byte
        /// </summary>
        public long ResxSize { get; set; }
        public string SaveLocalDirectory { get; set; }
        public string AccessUrl { get; set; }

        protected string SaveLocalPath;

        protected virtual void CheckParams(IFormFile file)
        {
            if (file == null)
                throw new Exception("Upload file is empty.");

            if (this.ResxTypes != null && !this.ResxTypes.Contains(Path.GetExtension(file.FileName)))
                throw new Exception("The file format is invalid.");

            if (this.ResxSize > 0 && file.Length > this.ResxSize)
                throw new Exception(string.Format("The upload file size more than {0}B", this.ResxSize));

            if (string.IsNullOrEmpty(this.SaveLocalDirectory) || string.IsNullOrWhiteSpace(this.SaveLocalDirectory))
                throw new ArgumentNullException("SaveLocalDirectory");

            if (!Directory.Exists(this.SaveLocalDirectory))
                Directory.CreateDirectory(this.SaveLocalDirectory);

            if (string.IsNullOrEmpty(this.AccessUrl) || string.IsNullOrWhiteSpace(this.AccessUrl))
                throw new ArgumentNullException("AccessUrl");
        }

        protected virtual string GetNewFileName(string oldFileName)
        {
            string newFileName = "";
            string fileExt = Path.GetExtension(oldFileName).ToLower();
            newFileName = Regex.Replace(oldFileName, fileExt, "");
            newFileName = Regex.Replace(newFileName, @"[^\u4e00-\u9fa5_a-zA-Z0-9]", "");
            newFileName = newFileName + DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + fileExt;
            return newFileName;
        }

        public ResxResult Upload(IFormFile file, bool useNewFileName = true)
        {
            ResxResult result = new ResxResult();
            FileStream fileStream = null;
            try
            {
                this.CheckParams(file);

                string newFileName = useNewFileName ? GetNewFileName(file.FileName) : file.FileName;
                this.SaveLocalPath = Path.Combine(this.SaveLocalDirectory, newFileName);

                fileStream = File.Create(this.SaveLocalPath);
                file.CopyTo(fileStream);
                fileStream.Flush();

                result.ResxSize = file.Length;
                result.Success = true;
                result.ResxAccessUrl = string.Format("{0}/{1}", this.AccessUrl, newFileName);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
            finally
            {
                fileStream?.Close();
            }

            return result;
        }
    }
}
