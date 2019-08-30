using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_city
    {
        public int id { get; set; }
        public string name { get; set; }
        public int province_id { get; set; }
    }
}
