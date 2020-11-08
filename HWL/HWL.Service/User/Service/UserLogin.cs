using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Redis;
using HWL.Service.User.Body;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.Service.User.Service
{
    public class UserLogin : GMSF.ServiceHandler<UserLoginRequestBody, UserLoginResponseBody>
    {
        private readonly HWLEntities db;

        public UserLogin(HWLEntities dbContext, UserLoginRequestBody request) : base(request)
        {
            this.db = dbContext;
        }

        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();

            if (string.IsNullOrEmpty(this.request.Email) && string.IsNullOrEmpty(this.request.Mobile))
            {
                throw new Exception("手机或者邮箱不能为空");
            }
            if (string.IsNullOrEmpty(this.request.Password))
            {
                throw new Exception("密码不能为空");
            }
        }

        public override UserLoginResponseBody ExecuteCore()
        {
            UserLoginResponseBody res = new UserLoginResponseBody();
            IQueryable<t_user> query = db.t_user;

            if (!string.IsNullOrEmpty(this.request.Mobile))
            {
                query = query.Where(u => u.mobile == this.request.Mobile);
            }
            else
            {
                query = query.Where(u => u.email == this.request.Email);
            }

            t_user user = query.FirstOrDefault();
            if (user == null) throw new Exception("用户不存在");
            if (user.status != UserStatus.Normal) throw new Exception("用户已经被禁用");
            if (user.password != this.request.Password) throw new Exception("密码错误");//CommonCs.GetMd5Str32(this.request.Password)

            UserStore.RemoveUserToken(user.id);
            string userToken = UserUtility.BuildToken(user.id);
            bool succ = UserStore.SaveUserToken(user.id, userToken);
            if (!succ) throw new Exception("用户登录token生成失败");

            UserRegisterAreaInfo pos = (from country in db.t_country
                                        join province in db.t_province on country.id equals province.country_id
                                        join city in db.t_city on province.id equals city.province_id
                                        join dist in db.t_district on city.id equals dist.city_id
                                        where country.id == user.register_country &&
                                        province.id == user.register_province &&
                                        city.id == user.register_city &&
                                        dist.id == user.register_district
                                        select new UserRegisterAreaInfo
                                        {
                                            CountryId = country.id,
                                            Country = country.name,
                                            ProvinceId = province.id,
                                            Province = province.name,
                                            CityId = city.id,
                                            City = city.name,
                                            DistrictId = dist.id,
                                            District = dist.name,
                                        }).FirstOrDefault();

            res.UserInfo = new Entity.Extends.UserBaseInfo()
            {
                Id = user.id,
                Symbol = user.symbol,
                Email = user.email,
                Mobile = user.mobile,
                Name = user.name,
                Token = userToken,
                HeadImage = user.head_image,
                CircleBackImage = user.circle_back_image,
                UserSex = user.sex,
                LifeNotes = user.life_notes,
                //RegisterPosIdList = posIdList,
                //RegisterPosList = posList,
                RegAreaInfo = pos,
                FriendCount = db.t_user_friend.Where(f => f.user_id == user.id).Count(),
                GroupCount = db.t_group_user.Where(f => f.user_id == user.id).Count()
            };

            return res;
        }
    }
}
