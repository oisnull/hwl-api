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
        //user(id)
        //near(lon,lat)
        //area(pos id)
        public PushPositionType PositionType { get; set; }

        public int UserId { get; set; }
        public int PosId { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
    }
}
