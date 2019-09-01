using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace HWL.Tools.Resx
{
    public class VideoHandler : ResxHandler
    {
        const string ffmpegPath = "/content/ffmpeg/ffmpeg.exe";

        public bool IsThumbnail { get; set; }
        /// <summary>
        /// Default value is 1280
        /// </summary>
        public int ThumbnailImageWidth { get; set; } = 1280;
        /// <summary>
        /// Default value is 960
        /// </summary>
        public int ThumbnailImageHeight { get; set; } = 960;

        private string ThumbnailImageName;
        private string SaveThumbnailLocalPath;
        private ResxVideoResult VideoResult;

        public VideoHandler()
        {
            base.ResxTypes = new List<string>() { ".mp4" };
            base.ResxSize = 20 * 1024 * 1024;
        }

        private void BuildThumbnailPath()
        {
            string ext = Path.GetExtension(base.SaveLocalPath);
            this.ThumbnailImageName = Path.GetFileName(base.SaveLocalPath).Replace(ext, "_s" + ext);
            this.SaveThumbnailLocalPath = string.Format("{0}{1}", base.SaveLocalDirectory, this.ThumbnailImageName);
        }

        public override ResxResult Upload(IFormFile file, bool useNewFileName = true)
        {
            ResxResult result = base.Upload(file, useNewFileName);
            if (!result.Success) return result;

            if (!this.IsThumbnail) return result;

            this.BuildThumbnailPath();
            var ret = this.CatchImage(base.SaveLocalPath, this.SaveThumbnailLocalPath);

            VideoResult = new ResxVideoResult()
            {
                Success = result.Success,
                Message = result.Message,
                ResxAccessUrl = result.ResxAccessUrl,
                ImagePreviewUrl = ret.Item1 ? string.Format("{0}/{1}", base.AccessUrl, this.ThumbnailImageName) : null,
                ImageWidth = ret.Item2,
                ImageHeight = ret.Item3,
                Duration = ret.Item4
            };

            return VideoResult;
        }

        public ResxVideoResult GetUploadResult()
        {
            return this.VideoResult;
        }

        private void ExecuteCommand(string command, out string output)
        {
            try
            {
                //创建一个进程
                Process pc = new Process();
                pc.StartInfo.FileName = "cmd.exe";
                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.RedirectStandardInput = true;
                pc.StartInfo.RedirectStandardOutput = true;
                pc.StartInfo.RedirectStandardError = true;
                pc.StartInfo.CreateNoWindow = true;

                pc.Start();
                pc.StandardInput.WriteLine(command);

                pc.StandardInput.WriteLine("exit");

                output = pc.StandardError.ReadToEnd();
                pc.Close();
            }
            catch (Exception)
            {
                output = null;
            }
        }

        private Tuple<bool, int, int, double> CatchImage(string videoFullPath, string saveLocalPath)
        {
            string toolPath = string.Format("{0}/{1}", AppContext.BaseDirectory, ffmpegPath);
            string output;
            this.ExecuteCommand("\"" + toolPath + "\"" + " -i " + "\"" + videoFullPath + "\"", out output);

            //获取视频的高度和宽度
            string sizeString = Regex.Match(output, "(\\d{2,4})x(\\d{2,4})").Value;
            if (string.IsNullOrEmpty(sizeString))
            {
                sizeString = string.Format("{0}x{1}", this.ThumbnailImageWidth, this.ThumbnailImageHeight);
            }

            //获取视频的时长
            string timeSize = output.Substring(output.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00").Length);
            double time = 0;
            if (!string.IsNullOrEmpty(timeSize))
            {
                time = TimeSpan.Parse(timeSize).TotalSeconds;
            }

            bool flag = true;
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo(toolPath)
                {
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = "   -i   " + videoFullPath + "  -y  -f  image2   -ss 2 -vframes 1  -s   " + sizeString + "   " + saveLocalPath
                };
                Process.Start(pro);
            }
            catch (Exception)
            {
                flag = false;
            }

            var sizes = sizeString.Split('x');
            return new Tuple<bool, int, int, double>(flag, int.Parse(sizes[0]), int.Parse(sizes[1]), time);
        }
    }
}
