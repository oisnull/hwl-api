using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_circle_image
    {
        public int id { get; set; }
        public int circle_id { get; set; }
        public int user_id { get; set; }
        public string image_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
