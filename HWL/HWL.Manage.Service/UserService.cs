using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.ShareConfig;
using HWL.Tools;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.Manage.Service
{
    public class UserService : BaseService
    {
        public UserService(HWLEntities db) : base(db)
        {
        }

        public int AddUser(UserEditInfo user)
        {
            #region check user
            if (user == null)
                throw new Exception("用户数据不能为空.");

            if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Mobile))
            {
                throw new Exception("手机或者邮箱不能为空");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("密码不能为空");
            }
            if (string.IsNullOrEmpty(user.PasswordOK))
            {
                throw new Exception("密码确认不能为空");
            }
            if (user.PasswordOK.Trim() != user.Password.Trim())
            {
                throw new Exception("两次密码输入不一致");
            }
            #endregion

            IQueryable<t_user> query = db.t_user;
            if (!string.IsNullOrEmpty(user.Mobile))
            {
                query = query.Where(u => u.mobile == user.Mobile);
            }
            else
            {
                query = query.Where(u => u.email == user.Email);
            }

            if (query.Count() >= 1) throw new Exception("该帐号已经被注册");

            t_user model = new t_user()
            {
                email = user.Email ?? "",
                mobile = user.Mobile ?? "",
                password = user.PasswordOK,
                name = string.IsNullOrEmpty(user.Name) ? "HWL-" + RandomText.GetNum() : user.Name,
                life_notes = user.LifeNotes,
                head_image = string.IsNullOrEmpty(user.HeadImage) ? AppConfigManager.UserDefaultHeadImage : user.HeadImage,

                source = UserSource.System,
                status = UserStatus.Normal,
                sex = UserSex.Unknow,
                register_date = DateTime.Now,
                update_date = DateTime.Now,
                circle_back_image = AppConfigManager.UserCircleBackImage,
            };
            db.t_user.Add(model);
            db.SaveChanges();

            return model.id;
        }

        public bool IsExistUser(int userId)
        {
            if (userId <= 0) return false;

            return db.t_user.Count(u => u.id == userId) > 0;
        }

        public int GetUserCount()
        {
            return db.t_user.Count();
        }

        public List<UserManageInfo> GetUserList(int pageIndex, int pageSize = 20)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            return db.t_user.OrderByDescending(v => v.id)
                .Select(v => new UserManageInfo
                {
                    Id = v.id,
                    CircleBackImage = v.circle_back_image,
                    Email = v.email,
                    RegisterDate = v.register_date,
                    HeadImage = v.head_image,
                    UpdateDate = v.update_date ?? DateTime.Now,
                    Mobile = v.mobile,
                    Name = v.name,
                    UserStatus = v.status,
                    Symbol = v.symbol,
                    UserSex = v.sex,
                })
              .Skip(pageSize * (pageIndex - 1))
              .Take(pageSize)
              .ToList();
        }

        public List<UserPosInfo> GetUserPosInfos(string pos, DateTime? startDate, DateTime? endDate, int pageSize = 30)
        {
            var query = from p in db.t_user_pos
                        join u in db.t_user on p.user_id equals u.id
                        orderby p.update_date descending
                        select new UserPosInfo
                        {
                            UserId = p.user_id,
                            UserName = u.name,
                            UserSymbol = u.symbol,
                            PosDetails = p.pos_details,
                            Longitude = p.lon,
                            Latitude = p.lat,
                            CreateDate = p.create_date,
                            UpdateDate = p.update_date
                        };

            if (!string.IsNullOrEmpty(pos))
                query = query.Where(q => q.PosDetails.Contains(pos));

            if (startDate != null && endDate != null)
                query = query.Where(q => q.UpdateDate >= startDate.Value && q.UpdateDate < endDate.Value);

            return query.Take(pageSize).ToList();
        }

        public List<GroupPosInfo> GetGroupAndUserInfos(double? lon, double? lat)
        {
            if (lon == null || lat == null) return null;

            List<string> groupGuids = Redis.GroupStore.GetGroupGuids(lon.Value, lat.Value);
            if (groupGuids == null || groupGuids.Count <= 0) return null;

            List<GroupPosInfo> infos = new List<GroupPosInfo>(groupGuids.Count);
            foreach (var item in groupGuids)
            {
                List<int> userIds = Redis.GroupStore.GetGroupUserIds(item);
                infos.Add(new GroupPosInfo
                {
                    GroupGuid = item,
                    GroupUserCount = userIds?.Count ?? 0,
                    GroupUserIds = string.Join(",", userIds)
                    //GroupUserIds = userIds
                });
            }
            return infos;
        }

        public List<UserRadiusInfo> GetNearUserRadius(double? lon, double? lat)
        {
            if (lon == null || lat == null) return null;

            GeoRadiusResult[] radius = Redis.UserStore.GetNearUserRadius(lon.Value, lat.Value);
            if (radius == null || radius.Length <= 0) return null;

            List<UserRadiusInfo> posInfos = new List<UserRadiusInfo>();
            UserRadiusInfo pos = null;
            foreach (var item in radius)
            {
                pos = new UserRadiusInfo()
                {
                    UserId = Convert.ToInt32(item.Member),
                    Longitude = item.Position.Value.Longitude,
                    Latitude = item.Position.Value.Latitude,
                    Distance = item.Distance.Value
                };
                var user = (from u in db.t_user
                            join p in db.t_user_pos
                            on u.id equals p.user_id
                            where u.id == pos.UserId && p.lat == pos.Latitude && p.lon == pos.Longitude
                            select new
                            {
                                u.name,
                                p.pos_details,
                                p.update_date
                            }).FirstOrDefault();
                if (user != null)
                {
                    pos.UserName = user.name;
                    pos.UpdateDate = user.update_date;
                    pos.PosDetails = user.pos_details;
                }
                posInfos.Add(pos);
            }
            return posInfos;
        }
    }
}
