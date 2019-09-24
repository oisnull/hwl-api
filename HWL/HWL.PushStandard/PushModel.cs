using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.PushStandard
{
    [Serializable]
    public class PushModel
    {
        //system coll
        //test
        //other
        public SourceType SourceType { get; set; }

        //user(id)
        //near(lon,lat)
        //area(pos id)
        public PushPositionModel PositionModel { get; set; }

        //near circle = 0
        public int PushMessageType { get; set; }

        public PushMessageModel MessageModel { get; set; }
    }

    public enum SourceType
    {
        SystemCollection = 0,
        TestCreate = 1,
        //...
    }
}
