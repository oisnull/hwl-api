using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_app_version
    {
        public int id { get; set; }
        public string app_name { get; set; }
        public string app_version { get; set; }
        public long? app_size { get; set; }
        public string download_url { get; set; }
        public string upgrade_log { get; set; }
        public DateTime publish_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
