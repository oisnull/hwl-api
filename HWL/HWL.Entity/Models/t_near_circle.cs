using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_near_circle
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public CircleContentType content_type { get; set; }
        public string content_info { get; set; }
        public string image_urls { get; set; }
        public string link_title { get; set; }
        public string link_url { get; set; }
        public string link_image { get; set; }
        public int pos_id { get; set; }
        public string pos_desc { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int image_count { get; set; }
        public int comment_count { get; set; }
        public int like_count { get; set; }
        public DateTime publish_time { get; set; }
        public DateTime? update_time { get; set; }
    }
}
