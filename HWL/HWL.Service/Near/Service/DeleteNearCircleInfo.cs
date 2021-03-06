﻿using HWL.Entity;
using HWL.Entity.Models;
using HWL.Redis;
using HWL.Service.Near.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Near.Service
{
    public class DeleteNearCircleInfo : GMSF.ServiceHandler<DeleteNearCircleInfoRequestBody, DeleteNearCircleInfoResponseBody>
    {
        private readonly HWLEntities db;
        public DeleteNearCircleInfo(HWLEntities db, DeleteNearCircleInfoRequestBody request) : base(request)
        {
            this.db = db;
        }
        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();
            if (this.request.NearCircleId <= 0)
            {
                throw new Exception("信息参数错误");
            }
            if (this.request.UserId <= 0)
            {
                throw new Exception("用户参数错误");
            }
        }

        public override DeleteNearCircleInfoResponseBody ExecuteCore()
        {
            DeleteNearCircleInfoResponseBody res = new DeleteNearCircleInfoResponseBody() { Status = ResultStatus.Failed };

            t_near_circle model = db.t_near_circle.Where(l => l.id == this.request.NearCircleId && l.user_id == this.request.UserId).FirstOrDefault();
            if (model == null)
            {
                res.Status = ResultStatus.Success;
                return res;
            }

            bool succ = NearCircleStore.DeleteNearCircleId(this.request.NearCircleId);
            if (succ)
            {
                var comments = db.t_near_circle_comment.Where(l => l.near_circle_id == this.request.NearCircleId).ToList();
                if (comments != null && comments.Count > 0)
                {
                    db.t_near_circle_comment.RemoveRange(comments);
                }

                var likes = db.t_near_circle_like.Where(l => l.near_circle_id == this.request.NearCircleId).ToList();
                if (likes != null && likes.Count > 0)
                {
                    db.t_near_circle_like.RemoveRange(likes);
                }
            }

            db.t_near_circle.Remove(model);
            db.SaveChanges();
            res.Status = ResultStatus.Success;

            return res;
        }
    }
}
