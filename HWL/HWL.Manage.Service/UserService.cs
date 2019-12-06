using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.ShareConfig;
using HWL.Tools;
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
    }
}
