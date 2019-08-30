using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_group_user
    {
        public int id { get; set; }
        public string group_guid { get; set; }
        public int user_id { get; set; }
        public DateTime add_date { get; set; }
    }
}
