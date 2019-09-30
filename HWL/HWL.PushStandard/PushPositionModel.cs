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
        public int PosId { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }
    }
}

