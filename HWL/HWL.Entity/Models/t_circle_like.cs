using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_circle_like
    {
        public int id { get; set; }
        public int circle_id { get; set; }
        public int like_user_id { get; set; }
        public DateTime like_time { get; set; }
        public bool is_delete { get; set; }
    }
}
