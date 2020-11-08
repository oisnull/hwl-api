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

    public class UserPosInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSymbol { get; set; }
        public string PosDetails { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public string CoorType { get; set; }
        public string LocationType { get; set; }
        public double Radius { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class UserRadiusInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PosDetails { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class GroupPosInfo
    {
        public string GroupGuid { get; set; }
        public int GroupUserCount { get; set; }
        public string GroupUserIds { get; set; }
    }

    public class UserRegisterAreaInfo
    {
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int DistrictId { get; set; }
        public string District { get; set; }
    }
}
