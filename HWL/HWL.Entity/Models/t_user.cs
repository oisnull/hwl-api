using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_user
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string head_image { get; set; }
        public string life_notes { get; set; }
        public UserSex sex { get; set; }
        public UserStatus status { get; set; }
        public UserSource source { get; set; }
        public string circle_back_image { get; set; }
        public int register_country { get; set; }
        public int register_province { get; set; }
        public int register_city { get; set; }
        public int register_district { get; set; }
        public DateTime register_date { get; set; }
        public DateTime? update_date { get; set; }
    }
}
