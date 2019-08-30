﻿using HWL.Entity;
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
            if (user.status != UserStatus.Normal) throw new Exception("用户被禁用");
            if (user.password != this.request.Password) throw new Exception("密码错误");//CommonCs.GetMd5Str32(this.request.Password)

            ////获取用户之前是否已经登录过，如果登录过则需要发送消息通知用户在其它位置登录
            //string oldToken = userAction.GetUserToken(user.id);
            //if (!string.IsNullOrEmpty(oldToken))
            //{
            //    //AndroidChatMessage.SendLogoutMessage(user.id, oldToken, "您的帐号已经在其它位置登录,如果不是您本人操作,建议重新登录后立即更换密码!");
            //}

            //清除用户之前登录用过的TOKEN
            UserStore.RemoveUserToken(user.id);
            string userToken = UserUtility.BuildToken(user.id);
            bool succ = UserStore.SaveUserToken(user.id, userToken);
            if (!succ) throw new Exception("用户登录token生成失败");

            //获取地址信息
            var pos = (from country in db.t_country
                       join province in db.t_province on country.id equals province.country_id
                       join city in db.t_city on province.id equals city.province_id
                       join dist in db.t_district on city.id equals dist.city_id
                       where country.id == user.register_country && province.id == user.register_province && city.id == user.register_city && dist.id == user.register_district
                       select new
                       {
                           CountryName = country.name,
                           ProvinceName = province.name,
                           CityName = city.name,
                           DistName = dist.name,
                       }).FirstOrDefault();

            List<int> posIdList = new List<int>()
                {
                    user.register_country,
                    user.register_province,
                    user.register_city,
                    user.register_district,
                };

            List<string> posList = new List<string>()
                {
                    pos!=null?pos.CountryName:string.Empty,
                    pos!=null?pos.ProvinceName:string.Empty,
                    pos!=null?pos.CityName:string.Empty,
                    pos!=null?pos.DistName:string.Empty,
                };

            //输出
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
                RegisterPosIdList = posIdList,
                RegisterPosList = posList,
                FriendCount = db.t_user_friend.Where(f => f.user_id == user.id).Count(),
                GroupCount = db.t_group_user.Where(f => f.user_id == user.id).Count()
            };

            //new Redis.ImMessageAction().AddWelcomeImMessage(user.id,, pos != null ? pos.DistName : string.Empty);

            return res;
        }
    }
}
