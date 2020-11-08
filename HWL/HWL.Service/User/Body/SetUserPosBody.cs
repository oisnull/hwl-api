using HWL.Entity;
using HWL.Entity.Extends;
using System.Collections.Generic;

namespace HWL.Service.User.Body
{
    public class SetUserPosRequestBody
    {
        public bool IsDistance { get; set; }
        public int UserId { get; set; }
        public string LastGroupGuid { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Details { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CoorType { get; set; }
        public int LocationWhere { get; set; }
        public string LocationType { get; set; }
        public float Radius { get; set; }
    }

    public class SetUserPosResponseBody
    {
        public ResultStatus Status { get; set; }
        public string ErrorMessage { get; set; }

        public int UserPosId { get; set; }

        public string UserGroupGuid { get; set; }

        public List<NearUserInfo> GroupUserInfos { get; set; }
    }
}
