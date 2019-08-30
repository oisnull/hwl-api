using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_admin
    {
        public int id { get; set; }
        public string login_name { get; set; }
        public string login_pwd { get; set; }
        public string real_name { get; set; }
        public DateTime create_date { get; set; }
    }
}
