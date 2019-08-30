using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_province
    {
        public int id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }
    }
}
