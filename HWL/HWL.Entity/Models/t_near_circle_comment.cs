using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_near_circle_comment
    {
        public int id { get; set; }
        public int near_circle_id { get; set; }
        public int comment_user_id { get; set; }
        public string content_info { get; set; }
        public int reply_user_id { get; set; }
        public DateTime comment_time { get; set; }
    }
}
