using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_user_pos
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string pos_details { get; set; }
        public string coordinate_type { get; set; }
        public int? Location_where { get; set; }
        public string location_type { get; set; }
        public double? radius { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string geohash_key { get; set; }
        public int country_id { get; set; }
        public int province_id { get; set; }
        public int city_id { get; set; }
        public int district_id { get; set; }
        public int town_id { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
    }
}
