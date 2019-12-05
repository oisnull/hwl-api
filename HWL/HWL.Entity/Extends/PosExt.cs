using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Entity.Extends
{
    public class AreaModel
    {
        public int value { get; set; }
        public string text { get; set; }
        public List<AreaModel> children { get; set; }
    }

    public class PosDetails
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Details { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class GeoInfo
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return $"{this.Latitude},{this.Longitude},{this.Details}";
        }
    }
}
