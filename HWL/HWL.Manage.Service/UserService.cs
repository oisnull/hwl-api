using HWL.Entity.Extends;
using HWL.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Manage.Service
{
    public class UserService : BaseService
    {
        public UserService(HWLEntities db) : base(db)
        {
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
