﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HWL.Tools
{
    public class ImageAction
    {
        public class ThumbnailResult
        {
            public bool Success { get; set; }
            public int ImageWidth { get; set; }
            public int ImageHeight { get; set; }
        }
        public readonly static int FIX_WIDTH = 960;
        public readonly static int FIX_HEIGHT = 1280;
        public readonly static int FIX_QUALITY = 20;

        ///// 压缩图片    
        ///// <param name="orgImagePath">原图片</param>    
        ///// <param name="newImagePath">压缩后保存位置</param>    
        ///// <param name="flag">压缩质量(数字越小压缩率越高) 1-100</param>    
        ///// <returns></returns>    
        //public static ThumbnailResult ThumbnailImage(string orgImagePath, string newImagePath)
        //{
        //    Image iSource = Image.FromFile(orgImagePath);
        //    int dHeight = FIX_HEIGHT;
        //    int dWidth = FIX_WIDTH;
        //    ImageFormat tFormat = iSource.RawFormat;
        //    int sW = 0, sH = 0;

        //    //按比例缩放  
        //    Size tem_size = new Size(iSource.Width, iSource.Height);

        //    if (tem_size.Width > dHeight || tem_size.Width > dWidth)
        //    {
        //        if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
        //        {
        //            sW = dWidth;
        //            sH = (dWidth * tem_size.Height) / tem_size.Width;
        //        }
        //        else
        //        {
        //            sH = dHeight;
        //            sW = (tem_size.Width * dHeight) / tem_size.Height;
        //        }
        //    }
        //    else
        //    {
        //        sW = tem_size.Width;
        //        sH = tem_size.Height;
        //    }

        //    Bitmap ob = new Bitmap(sW, sH);
        //    Graphics g = Graphics.FromImage(ob);

        //    g.Clear(Color.WhiteSmoke);
        //    g.CompositingQuality = CompositingQuality.HighQuality;
        //    g.SmoothingMode = SmoothingMode.HighQuality;
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, sW, sH, GraphicsUnit.Pixel);

        //    g.Dispose();

        //    //以下代码为保存图片时，设置压缩质量    
        //    EncoderParameters ep = new EncoderParameters();
        //    long[] qy = new long[1];
        //    qy[0] = FIX_QUALITY;//设置压缩的比例1-100    
        //    EncoderParameter eParam = new EncoderParameter(Encoder.Quality, qy);
        //    ep.Param[0] = eParam;
        //    try
        //    {
        //        ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
        //        ImageCodecInfo jpegICIinfo = null;
        //        for (int x = 0; x < arrayICI.Length; x++)
        //        {
        //            if (arrayICI[x].FormatDescription.Equals("JPEG"))
        //            {
        //                jpegICIinfo = arrayICI[x];
        //                break;
        //            }
        //        }
        //        if (jpegICIinfo != null)
        //        {
        //            ob.Save(newImagePath, jpegICIinfo, ep);//newImagePath是压缩后的新路径    
        //        }
        //        else
        //        {
        //            ob.Save(newImagePath, tFormat);
        //        }
        //        return new ThumbnailResult()
        //        {
        //            Success = true,
        //            ImageHeight = dHeight,
        //            ImageWidth = dWidth,
        //        };
        //    }
        //    catch
        //    {
        //        return new ThumbnailResult()
        //        {
        //            Success = false,
        //            ImageHeight = dHeight,
        //            ImageWidth = dWidth,
        //        };
        //    }
        //    finally
        //    {
        //        iSource.Dispose();
        //        ob.Dispose();
        //    }
        //}

        private static Size NewSize(int maxWidth, int maxHeight, int Width, int Height)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(Width);
            double sh = Convert.ToDouble(Height);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);

            if (sw < mw && sh < mh)//如果maxWidth和maxHeight大于源图像，则缩略图的长和高不变  
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }


        public static ThumbnailResult ThumbnailImage(string orgImagePath, string newImagePath)
        {
            return MakeThumbnail(orgImagePath, newImagePath, FIX_WIDTH, FIX_HEIGHT, FIX_QUALITY);
        }

        public static Tuple<int, int> GetSize(string imagePath)
        {
            Image originalImage = null;
            try
            {
                originalImage = Image.FromFile(imagePath);
                return new Tuple<int, int>(originalImage.Width, originalImage.Height);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (originalImage != null)
                    originalImage.Dispose();
            }

        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        private static ThumbnailResult MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, int quality = 50, string mode = "W")
        {

            Image originalImage = Image.FromFile(originalImagePath);
            Size newSize = NewSize(width, height, originalImage.Width, originalImage.Height);

            //新建一个bmp图片
            Image bitmap = new Bitmap(newSize.Width, newSize.Height);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, newSize.Width, newSize.Height),
                new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                GraphicsUnit.Pixel);

            //以下代码为保存图片时，设置压缩质量    
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = quality;//设置压缩的比例1-100    
            EncoderParameter eParam = new EncoderParameter(Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int i = 0; i < arrayICI.Length; i++)
                {
                    if (arrayICI[i].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[i];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    bitmap.Save(thumbnailPath, jpegICIinfo, ep);//newImagePath是压缩后的新路径    
                }
                else
                {
                    bitmap.Save(thumbnailPath, bitmap.RawFormat);
                }
                return new ThumbnailResult()
                {
                    Success = true,
                    ImageHeight = bitmap.Height,
                    ImageWidth = bitmap.Width,
                };
            }
            catch (Exception)
            {
                return new ThumbnailResult()
                {
                    Success = false,
                    ImageHeight = 0,
                    ImageWidth = 0,
                };
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

    }
}
