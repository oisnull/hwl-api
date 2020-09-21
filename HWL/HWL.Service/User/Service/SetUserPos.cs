using HWL.Entity.Models;
using HWL.Entity.Extends;
using HWL.Service.User.Body;
using HWL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using HWL.Entity;
using HWL.Redis;
using HWL.IMClient;
using HWL.IMCore.Protocol;
using HWL.ShareConfig;

namespace HWL.Service.User.Service
{
    public class SetUserPos : GMSF.ServiceHandler<SetUserPosRequestBody, SetUserPosResponseBody>
    {
        //double lat = 0;
        //double lon = 0;
        private readonly HWLEntities db;
        private bool isExistInGroup = false;
        public SetUserPos(HWLEntities db, SetUserPosRequestBody request) : base(request)
        {
            this.db = db;
        }

        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();

            if (this.request.UserId <= 0)
            {
                throw new ArgumentNullException("UserId");
            }

            if (this.request.Latitude <= 0 && this.request.Latitude <= 0)
            {
                throw new Exception("经度或者纬度的值是错误的");
            }
        }

        //{
        //    "UserId": 33,
        //    "LastGroupGuid": null,
        //    "Country": "中国",
        //    "Province": "上海市",
        //    "City": "上海市",
        //    "District": "闵行区",
        //    "Town": "浦锦街道",
        //    "Street": "联航路",
        //    "Details": "中国上海市闵行区浦锦街道联航路",
        //    "Latitude": 31.0718173980713,
        //    "Longitude": 121.499031066895
        //}

        public override SetUserPosResponseBody ExecuteCore()
        {
            //LogHelper.Debug($"UserId:{request.UserId},LastGroupGuid:{request.LastGroupGuid},Details:{request.Details},Lat:{request.Latitude},Lon:{request.Longitude}", typeof(SetUserPos));

            t_user_pos upos = this.SavePos();
            if (upos == null)
            {
                return new SetUserPosResponseBody()
                {
                    Status = ResultStatus.Failed,
                    ErrorMessage = "Save pos info failed for current user."
                };
            }

            if (!UserStore.SavePos(upos.user_id, upos.lon, upos.lat))
            {
                return new SetUserPosResponseBody()
                {
                    Status = ResultStatus.Failed,
                    ErrorMessage = "Save user info failed by lon and lat."
                };
            }

            string groupGuid = this.GetNearGroupGuid(upos);
            SetUserPosResponseBody res = new SetUserPosResponseBody()
            {
                Status = ResultStatus.Success,
                UserPosId = upos.id,
                UserGroupGuid = groupGuid,
                GroupUserInfos = GetGroupUsers(groupGuid)
            };

            //LogHelper.Debug($"CurrentGroupGuid:{groupGuid},LastGroupGuid:{request.LastGroupGuid},GroupUserCount:{res.GroupUserInfos?.Count}", typeof(SetUserPos));

            if (!isExistInGroup)
            {
                ImUserContent user = res.GroupUserInfos?.Where(g => g.UserId == request.UserId).Select(g => new ImUserContent
                {
                    UserId = (ulong)g.UserId,
                    UserName = g.UserName,
                    UserImage = g.UserImage,
                }).FirstOrDefault();
                if (user != null)
                    IMClientV.INSTANCE.SendSystemMessage(user, groupGuid, getNearDesc());
            }

            return res;
        }

        public string getNearDesc()
        {
            string desc = request.Street;
            if (string.IsNullOrEmpty(request.Street))
            {
                desc = request.District;
            }
            return desc + "附近";
        }

        public List<NearUserInfo> GetGroupUsers(string groupGuid)
        {
            List<int> userIds = GroupStore.GetGroupUserIds(groupGuid);
            if (userIds == null || userIds.Count <= 0) return null;

            List<NearUserInfo> users = db.t_user.Where(u => userIds.Contains(u.id))
                .Select(u => new NearUserInfo()
                {
                    UserId = u.id,
                    UserName = u.name,
                    UserImage = u.head_image,
                }).ToList();

            if (request.IsDistance)
            {
                var distDic = UserStore.GetUserDistances(request.UserId, userIds);
                users.ForEach(f =>
                {
                    if (distDic.ContainsKey(f.UserId))
                        f.Distance = distDic[f.UserId] ?? 0;
                });
            }
            return users;
        }

        public string GetNearGroupGuid(t_user_pos upos)
        {
            string groupGuid = GroupStore.GetAvailableNearGroupGuid(upos.lon, upos.lat);
            if (groupGuid != request.LastGroupGuid && !string.IsNullOrEmpty(request.LastGroupGuid))
            {
                GroupStore.DeleteGroupUsers(request.LastGroupGuid, upos.user_id);
            }

            isExistInGroup = GroupStore.ExistsInGroup(groupGuid, request.UserId);
            if (!isExistInGroup)
            {
                GroupStore.SaveGroupUser(groupGuid, upos.user_id);
            }

            return groupGuid;
        }

        public t_user_pos SavePos()
        {
            try
            {
                #region Save t_country,t_province,t_city,t_district,t_town
                t_country country = db.t_country.Where(c => c.name == this.request.Country).FirstOrDefault();
                if (country == null)
                {
                    country = new t_country()
                    {
                        id = 0,
                        name = this.request.Country,
                    };
                    db.t_country.Add(country);
                    db.SaveChanges();
                }

                t_province province = db.t_province.Where(p => p.name == this.request.Province).FirstOrDefault();
                if (province == null)
                {
                    province = new t_province()
                    {
                        id = 0,
                        country_id = country.id,
                        name = this.request.Province,
                    };
                    db.t_province.Add(province);
                    db.SaveChanges();
                }

                t_city city = db.t_city.Where(c => c.name == this.request.City).FirstOrDefault();
                if (city == null)
                {
                    city = new t_city()
                    {
                        id = 0,
                        province_id = province.id,
                        name = this.request.City,
                    };
                    db.t_city.Add(city);
                    db.SaveChanges();
                }

                t_district district = db.t_district.Where(p => p.name == this.request.District).FirstOrDefault();
                if (district == null)
                {
                    district = new t_district()
                    {
                        id = 0,
                        city_id = city.id,
                        name = this.request.District,
                    };
                    db.t_district.Add(district);
                    db.SaveChanges();
                }

                t_town town = db.t_town.Where(p => p.name == this.request.Town).FirstOrDefault();
                if (town == null)
                {
                    town = new t_town()
                    {
                        id = 0,
                        district_id = district.id,
                        name = this.request.Town,
                    };
                    db.t_town.Add(town);
                    db.SaveChanges();
                }
                #endregion

                //t_user_pos upos = db.t_user_pos.Where(u => u.user_id == this.request.UserId &&
                //                                            u.country_id == country.id &&
                //                                            u.province_id == province.id &&
                //                                            u.city_id == city.id &&
                //                                            u.district_id == district.id &&
                //                                            u.pos_details == this.request.Details
                //                                        ).FirstOrDefault();
                t_user_pos upos = db.t_user_pos.Where(u => u.user_id == this.request.UserId &&
                                                            u.lon == request.Longitude &&
                                                            u.lat == request.Latitude
                                                        ).FirstOrDefault();
                if (upos == null)
                {
                    upos = new t_user_pos()
                    {
                        id = 0,
                        create_date = DateTime.Now,
                        update_date = DateTime.Now,
                        geohash_key = Geohash.Encode(this.request.Latitude, this.request.Longitude),
                        lat = this.request.Latitude,
                        lon = this.request.Longitude,
                        pos_details = this.request.Details,
                        user_id = this.request.UserId,
                        city_id = city.id,
                        country_id = country.id,
                        district_id = district.id,
                        province_id = province.id,
                        coordinate_type = request.CoorType,
                        location_type = request.LocationType,
                        Location_where = request.LocationWhere,
                        radius = request.Radius,
                        town_id = town.id,
                    };
                    db.t_user_pos.Add(upos);
                }
                else
                {
                    upos.update_date = DateTime.Now;
                }

                db.SaveChanges();
                return upos;
            }
            catch (Exception ex)
            {
                string currentParams = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                LogHelper.Error(currentParams + "___" + ex.ToString(), typeof(SetUserPos));
            }
            return null;
        }
    }
}
