using HWL.Entity.Extends;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Entity
{
    public class CircleImageParser
    {
        const string FIRST_SEPARATOR = "|";
        const string SECOND_SEPARATOR = ",";

        /// <summary>
        /// image url format:http://images1,w,h|http://images2,w,h
        /// </summary>
        public static List<ImageInfo> GetImages(string imageUrls)
        {
            if (string.IsNullOrEmpty(imageUrls)) return null;

            string[] infos = imageUrls.Split(FIRST_SEPARATOR);
            List<ImageInfo> images = new List<ImageInfo>();
            foreach (var item in infos)
            {
                string[] urls = item?.Split(SECOND_SEPARATOR);
                if (urls != null && urls.Length > 0)
                {
                    images.Add(new ImageInfo()
                    {
                        Url = urls[0],
                        Width = urls.Length >= 2 ? int.Parse(urls[1]) : 0,
                        Height = urls.Length >= 3 ? int.Parse(urls[2]) : 0,
                    });
                }
            }

            return images;
        }

        private static string ConvertToString(ImageInfo img)
        {
            string str = null;
            if (!string.IsNullOrEmpty(img.Url))
            {
                str = img.Url;
            }
            if (img.Width > 0)
            {
                str = $"{str}{SECOND_SEPARATOR}{img.Width}";
            }
            if (img.Height > 0)
            {
                str = $"{str}{SECOND_SEPARATOR}{img.Height}";
            }
            return str;
        }

        public static string GetImageString(List<ImageInfo> images)
        {
            if (images == null || images.Count <= 0) return null;

            return string.Join(FIRST_SEPARATOR, images.ConvertAll(i => ConvertToString(i)));
        }

        public static string GetImageString(List<string> images)
        {
            images?.RemoveAll(r => string.IsNullOrEmpty(r?.Trim()));

            if (images == null || images.Count <= 0) return null;

            return string.Join(FIRST_SEPARATOR, images);
        }
    }
}
