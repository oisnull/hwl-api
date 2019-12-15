using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.ShareConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CommonTest
{
    public class t_circle_image
    {
        public int near_circle_id { get; set; }
        public int circle_id { get; set; }
        public string image_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = HWLDBContext.GetDBContext(ShareConfiguration.DBConnectionString))
            {
                //var circleImages = db.Database.SqlQuery($"select * from t_circle_image").ToList<t_circle_image>()
                //    .GroupBy(g => g.circle_id)
                //    .Select(g => new
                //    {
                //        circle_id = g.FirstOrDefault().circle_id,
                //        image_infos = g.Select(i => new ImageInfo
                //        {
                //            Url = i.image_url,
                //            Width = i.width,
                //            Height = i.height
                //        }).ToList()
                //    }).ToList();
                //foreach (var item in circleImages)
                //{
                //    t_circle circle = db.t_circle.Where(c => c.id == item.circle_id).FirstOrDefault();
                //    if (circle != null)
                //    {
                //        circle.image_urls = CircleImageParser.GetImageString(item.image_infos);
                //        db.SaveChanges();
                //    }
                //}

                var nearCircleImages = db.Database.SqlQuery($"select * from t_near_circle_image").ToList<t_circle_image>()
                    .GroupBy(g => g.near_circle_id)
                    .Select(g => new
                    {
                        near_circle_id = g.FirstOrDefault().near_circle_id,
                        image_infos = g.Select(i => new ImageInfo
                        {
                            Url = i.image_url,
                            Width = i.width,
                            Height = i.height
                        }).ToList()
                    }).ToList();
                Console.WriteLine(nearCircleImages.Count);
                foreach (var item in nearCircleImages)
                {
                    t_near_circle circle = db.t_near_circle.Where(c => c.id == item.near_circle_id).FirstOrDefault();
                    if (circle != null)
                    {
                        circle.image_urls = CircleImageParser.GetImageString(item.image_infos);
                        db.SaveChanges();
                    }
                }
            }

            Console.WriteLine("The end.");
            Console.ReadKey();
        }
    }
}
