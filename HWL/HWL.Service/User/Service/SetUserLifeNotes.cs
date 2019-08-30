﻿using HWL.Entity;
using HWL.Entity.Models;
using HWL.Service.User.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.User.Service
{
    public class SetUserLifeNotes : GMSF.ServiceHandler<SetUserLifeNotesRequestBody, SetUserInfoResponseBody>
    {
        private readonly HWLEntities db;
        public SetUserLifeNotes(HWLEntities db, SetUserLifeNotesRequestBody request) : base(request)
        {
            this.db = db;
        }
        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();
            if (this.request.UserId <= 0)
            {
                throw new Exception("用户参数错误");
            }
        }

        public override SetUserInfoResponseBody ExecuteCore()
        {
            SetUserInfoResponseBody res = new SetUserInfoResponseBody() { Status = ResultStatus.Failed };

            var user = db.t_user.Where(u => u.id == this.request.UserId).FirstOrDefault();
            if (user == null) throw new Exception("用户不存在");

            user.life_notes = this.request.LifeNotes;
            user.update_date = DateTime.Now;

            db.SaveChanges();
            res.Status = ResultStatus.Success;

            return res;
        }
    }
}
