﻿using HWL.Entity;
using HWL.Entity.Models;
using HWL.Service.Generic;
using HWL.Service.Near.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Near.Service
{
    public class SetNearLikeInfo : GMSF.ServiceHandler<SetNearLikeInfoRequestBody, SetNearLikeInfoResponseBody>
    {
        private readonly HWLEntities db;
        public SetNearLikeInfo(HWLEntities db, SetNearLikeInfoRequestBody request) : base(request)
        {
            this.db = db;
        }
        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();
            if (this.request.ActionType != 0 && this.request.ActionType != 1)
            {
                throw new Exception("操作类型错误");
            }
            if (this.request.NearCircleId <= 0)
            {
                throw new Exception("信息参数错误");
            }
            if (this.request.LikeUserId <= 0)
            {
                throw new Exception("点赞的用户参数错误");
            }
        }

        public override SetNearLikeInfoResponseBody ExecuteCore()
        {
            SetNearLikeInfoResponseBody res = new SetNearLikeInfoResponseBody() { Status = ResultStatus.Failed };

            var circleModel = db.t_near_circle.Where(c => c.id == this.request.NearCircleId).FirstOrDefault();
            if (circleModel == null)
            {
                throw new Exception("你点赞的信息已经被用户删除");
            }

            bool isChanged = string.IsNullOrEmpty(this.request.NearCircleUpdateTime) || this.request.NearCircleUpdateTime != GenericUtility.FormatDate2(circleModel.update_time);

            t_near_circle_like model = db.t_near_circle_like.Where(l => l.near_circle_id == this.request.NearCircleId && l.like_user_id == this.request.LikeUserId).FirstOrDefault();
            if (this.request.ActionType == 0)//取消点赞
            {
                if (model == null)
                {
                    throw new Exception("取消点赞的信息不存在");
                }
                if (model.is_delete)
                {
                    res.Status = ResultStatus.Success;
                    return res;
                }
                else
                {
                    model.is_delete = true;
                    circleModel.like_count = circleModel.like_count - 1;
                    circleModel.update_time = DateTime.Now;
                    if (circleModel.like_count < 0)
                    {
                        circleModel.like_count = 0;
                    }
                    db.SaveChanges();
                    res.Status = ResultStatus.Success;
                    if (!isChanged)
                        res.NearCircleLastUpdateTime = GenericUtility.FormatDate2(circleModel.update_time);
                    return res;
                }
            }
            else if (this.request.ActionType == 1)//点赞
            {
                if (model == null)
                {
                    model = new t_near_circle_like()
                    {
                        like_user_id = this.request.LikeUserId,
                        near_circle_id = this.request.NearCircleId,
                        is_delete = false,
                        id = 0,
                        like_time = DateTime.Now,
                    };
                    db.t_near_circle_like.Add(model);
                    circleModel.like_count = circleModel.like_count + 1;
                    circleModel.update_time = DateTime.Now;
                    db.SaveChanges();
                    res.Status = ResultStatus.Success;
                    if (!isChanged)
                        res.NearCircleLastUpdateTime = GenericUtility.FormatDate2(circleModel.update_time);
                    return res;
                }
                else
                {
                    if (model.is_delete)
                    {
                        model.is_delete = false;
                        circleModel.like_count = circleModel.like_count + 1;
                        circleModel.update_time = DateTime.Now;
                        db.SaveChanges();
                        res.Status = ResultStatus.Success;
                        if (!isChanged)
                            res.NearCircleLastUpdateTime = GenericUtility.FormatDate2(circleModel.update_time);
                        return res;
                    }
                    else
                    {
                        res.Status = ResultStatus.Success;
                        return res;
                    }
                }
            }

            return res;
        }
    }
}
