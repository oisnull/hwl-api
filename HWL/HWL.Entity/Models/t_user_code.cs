using System;
using System.Collections.Generic;

namespace HWL.Entity.Models
{
    public partial class t_user_code
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string user_account { get; set; }
        public CodeType code_type { get; set; }
        public string code { get; set; }
        public DateTime create_date { get; set; }
        public DateTime expire_time { get; set; }
        public string remark { get; set; }
    }
}
