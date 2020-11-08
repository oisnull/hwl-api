using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_app_log
    {
        public int id { get; set; }
        public string app_type { get; set; }
        public string app_version { get; set; }
        public string crash_info { get; set; }
        public string crash_details { get; set; }
        public DateTime create_time { get; set; }
    }
}
