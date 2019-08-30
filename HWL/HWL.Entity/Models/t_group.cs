using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_group
    {
        public int id { get; set; }
        public string group_guid { get; set; }
        public string group_name { get; set; }
        public int group_user_count { get; set; }
        public string group_note { get; set; }
        public int build_user_id { get; set; }
        public DateTime build_date { get; set; }
        public DateTime? update_date { get; set; }
    }
}
