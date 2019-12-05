using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.PushStandard
{
    [Serializable]
    public class PushPositionModel
    {
        public int UserId { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string PosDetails { get; set; }
    }
}

