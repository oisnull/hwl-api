using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_circle_comment
    {
        public int id { get; set; }
        public int circle_id { get; set; }
        public int circle_user_id { get; set; }
        public int com_user_id { get; set; }
        public string com_content { get; set; }
        public int reply_user_id { get; set; }
        public DateTime comment_time { get; set; }
    }
}
