using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_town
    {
        public int id { get; set; }
        public string name { get; set; }
        public int district_id { get; set; }
    }
}
