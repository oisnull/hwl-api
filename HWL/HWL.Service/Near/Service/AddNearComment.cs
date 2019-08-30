﻿using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Service.Generic;
using HWL.Service.Near.Body;
using HWL.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Near.Service
{
    public class AddNearComment : GMSF.ServiceHandler<AddNearCommentRequestBody, AddNearCommentResponseBody>
    {
        private readonly HWLEntities db;
        public AddNearComment(HWLEntities db, AddNearCommentRequestBody request) : base(request)
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
            if (this.request.CommentUserId <= 0 && this.request.ReplyUserId <= 0)
            {
                throw new Exception("评论或者回复用户参数错误");
            }
            if (string.IsNullOrEmpty(this.request.Content))
            {
                throw new Exception("评论内容不能为空");
            }
        }

        public override AddNearCommentResponseBody ExecuteCore()
        {
            AddNearCommentResponseBody res = new AddNearCommentResponseBody();

            var circleModel = db.t_near_circle.Where(c => c.id == this.request.NearCircleId).FirstOrDefault();
            if (circleModel == null)
            {
                throw new Exception("你评论的信息已经被用户删除");
            }

            bool isChanged = string.IsNullOrEmpty(this.request.NearCircleUpdateTime) || this.request.NearCircleUpdateTime != GenericUtility.FormatDate2(circleModel.update_time);

            t_near_circle_comment model = new t_near_circle_comment()
            {
                comment_user_id = this.request.CommentUserId,
                content_info = this.request.Content,
                near_circle_id = this.request.NearCircleId,
                reply_user_id = this.request.ReplyUserId,
                id = 0,
                comment_time = DateTime.Now,
            };
            db.t_near_circle_comment.Add(model);
            circleModel.comment_count = circleModel.comment_count + 1;
            circleModel.update_time = DateTime.Now;
            db.SaveChanges();

            var userList = db.t_user.Where(i => i.id == model.comment_user_id || i.id == model.reply_user_id).Select(i => new { i.id, i.name, i.symbol, i.head_image }).ToList();
            NearCircleCommentInfo info = new NearCircleCommentInfo()
            {
                CommentId = model.id,
                Content = model.content_info,
                CommentTime = GenericUtility.FormatDate(model.comment_time),
                CommentUserId = model.comment_user_id,
                //CommentUserImage = model.com,
                //CommentUserName = model.content_info,
                NearCircleId = model.near_circle_id,
                ReplyUserId = model.reply_user_id,
                //ReplyUserImage = model.content_info,
                //ReplyUserName = model.content_info,
            };
            if (userList != null && userList.Count > 0)
            {
                if (info.CommentUserId > 0)
                {
                    var comUser = userList.Where(u => u.id == info.CommentUserId).FirstOrDefault();
                    info.CommentUserName = UserUtility.GetShowName(comUser.name, comUser.symbol);
                    info.CommentUserImage = comUser.head_image;
                }

                if (info.ReplyUserId > 0)
                {
                    var repUser = userList.Where(u => u.id == info.ReplyUserId).FirstOrDefault();
                    info.ReplyUserName = UserUtility.GetShowName(repUser.name, repUser.symbol);
                    info.ReplyUserImage = repUser.head_image;
                }
            }

            res.NearCirclePublishUserId = circleModel.user_id;
            res.NearCircleCommentInfo = info;
            if (!isChanged)
                res.NearCircleLastUpdateTime = GenericUtility.FormatDate2(circleModel.update_time);

            return res;
        }
    }
}
