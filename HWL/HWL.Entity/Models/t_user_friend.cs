using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_user_friend
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int friend_user_id { get; set; }
        public string friend_user_remark { get; set; }
        public string friend_first_spell { get; set; }
        public DateTime add_time { get; set; }
    }
}
