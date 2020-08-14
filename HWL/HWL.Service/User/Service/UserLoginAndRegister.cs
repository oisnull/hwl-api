using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Redis;
using HWL.Service.User.Body;
using HWL.ShareConfig;
using HWL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.Service.User.Service
{
    public class UserLoginAndRegister : GMSF.ServiceHandler<UserLoginAndRegisterRequestBody, UserLoginAndRegisterResponseBody>
    {
        private readonly HWLEntities db;
        private bool isMail = false;
        private bool isMobile = false;

        public UserLoginAndRegister(HWLEntities dbContext, UserLoginAndRegisterRequestBody request) : base(request)
        {
            this.db = dbContext;
        }

        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();

            isMail = !string.IsNullOrEmpty(this.request.Email) && Generic.GenericUtility.IsValidMail(this.request.Email);

            isMobile = !string.IsNullOrEmpty(this.request.Mobile) && Generic.GenericUtility.IsValidPhone(this.request.Mobile);

            if (!isMail && !isMobile)
            {
                throw new Exception("手机或者邮箱格式错误");
            }

            if (string.IsNullOrEmpty(this.request.CheckCode))
            {
                throw new Exception("验证码不能为空");
            }
        }

        public override UserLoginAndRegisterResponseBody ExecuteCore()
        {
            UserLoginAndRegisterResponseBody res = new UserLoginAndRegisterResponseBody();
            IQueryable<t_user> query = db.t_user;
            IQueryable<t_user_code> codeQuery = db.t_user_code;

            if (this.isMobile)
            {
                query = query.Where(u => u.mobile == this.request.Mobile);
                codeQuery = db.t_user_code.Where(u => u.user_account == this.request.Mobile);
            }
            else
            {
                query = query.Where(u => u.email == this.request.Email);
                codeQuery = db.t_user_code.Where(u => u.user_account == this.request.Email);
            }

            if (this.request.CheckCode != AppConfigManager.CheckCodeForDebug)
            {
                t_user_code oldCode = codeQuery.OrderByDescending(u => u.id).FirstOrDefault();
                if (oldCode == null || oldCode.code != this.request.CheckCode) throw new Exception("验证码错误");
                if (oldCode.expire_time <= DateTime.Now) throw new Exception("验证码已过期");
            }

            t_user user = query.FirstOrDefault();
            if (user == null) user = this.CreateUser();
            if (user.status != UserStatus.Normal) throw new Exception("用户已经被禁用");

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
                RegAreaInfo = pos,
                FriendCount = db.t_user_friend.Where(f => f.user_id == user.id).Count(),
                GroupCount = db.t_group_user.Where(f => f.user_id == user.id).Count()
            };

            return res;
        }

        private t_user CreateUser()
        {
            t_user model = new t_user()
            {
                //id = 0,
                email = this.request.Email ?? "",
                mobile = this.request.Mobile ?? "",
                password = "123456",

                source = UserSource.Register,
                status = UserStatus.Normal,
                sex = UserSex.Unknow,
                register_date = DateTime.Now,
                update_date = DateTime.Now,
                name = $"{AppConfigManager.DefaultEAppName}-{RandomText.GetNum()}",
                head_image = AppConfigManager.UserDefaultHeadImage,
                circle_back_image = AppConfigManager.UserCircleBackImage,
            };
            db.t_user.Add(model);
            db.SaveChanges();

            return model;
        }
    }
}
