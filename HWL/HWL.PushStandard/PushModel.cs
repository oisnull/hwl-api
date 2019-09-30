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
        public PushPositionType PositionType { get; set; }

        //user(id)
        //near(lon,lat)
        //area(pos id)
        public PushPositionModel PositionModel { get; set; }

        //near circle = 0(default)
        //chat record = 1
        public PushMessageType PushMessageType { get; set; }

        public PushMessageModel MessageModel { get; set; }
    }

    public enum SourceType
    {
        SystemCollection = 0,
        TestCreate = 1,
        //...
    }

    public enum PushMessageType
    {
        NearCirlce = 0,
        ChatRecord = 1,
        //...
    }
}
