using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_app_version_push
    {
        public int id { get; set; }
        public int app_version_id { get; set; }
        public string pushed_users { get; set; }
        public DateTime push_date { get; set; }
    }
}
