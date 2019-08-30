﻿using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Service.Near.Body;
using HWL.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Near.Service
{
    public class GetNearComments : GMSF.ServiceHandler<GetNearCommentsRequestBody, GetNearCommentsResponseBody>
    {
        private readonly HWLEntities db;
        public GetNearComments(HWLEntities db, GetNearCommentsRequestBody request) : base(request)
        {
            this.db = db;
        }
        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();
            if (this.request.UserId <= 0)
            {
                throw new ArgumentNullException("UserId");
            }
            if (this.request.NearCircleId <= 0)
            {
                throw new ArgumentNullException("NearCircleId");
            }
            if (this.request.Count <= 0)
            {
                this.request.Count = 15;
            }
        }

        public override GetNearCommentsResponseBody ExecuteCore()
        {
            GetNearCommentsResponseBody res = new GetNearCommentsResponseBody();
            var comments = db.t_near_circle_comment.Where(c => c.near_circle_id == this.request.NearCircleId && c.id > this.request.LastCommentId)
                .Take(this.request.Count)
                .Select(c => new
                {
                    Id = c.id,
                    NearCircleId = c.near_circle_id,
                    CommentUserId = c.comment_user_id,
                    ReplyUserId = c.reply_user_id,
                    Content = c.content_info,
                    CommentTime = c.comment_time
                }).OrderBy(c => c.Id).ToList();
            if (comments == null || comments.Count <= 0) return res;
            res.NearCircleCommentInfos = new List<NearCircleCommentInfo>();

            var userIds = comments.Select(c => c.CommentUserId).Union(comments.Select(c => c.ReplyUserId)).ToList();
            var userList = db.t_user.Where(i => userIds.Contains(i.id)).Select(i => new { i.id, i.name, i.symbol, i.head_image }).ToList();
            var friendList = db.t_user_friend.Where(f => f.user_id == this.request.UserId && userIds.Contains(f.friend_user_id)).Select(f => new { f.friend_user_id, f.friend_user_remark }).ToList();

            comments.ForEach(f =>
            {
                NearCircleCommentInfo model = new NearCircleCommentInfo()
                {
                    CommentId = f.Id,
                    Content = f.Content,
                    NearCircleId = f.NearCircleId,
                    CommentTime = f.CommentTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    CommentUserId = f.CommentUserId,
                    CommentUserImage = null,
                    CommentUserName = null,
                    ReplyUserId = f.ReplyUserId,
                    ReplyUserName = null,
                    ReplyUserImage = null,
                };

                if (userList != null && userList.Count > 0)
                {
                    if (f.CommentUserId > 0)
                    {
                        var comUser = userList.Where(u => u.id == f.CommentUserId).FirstOrDefault();
                        string friendRemark = friendList != null ? friendList.Where(r => r.friend_user_id == f.CommentUserId).Select(r => r.friend_user_remark).FirstOrDefault() : null;
                        model.CommentUserName = UserUtility.GetShowName(friendRemark, comUser.name);
                        model.CommentUserImage = comUser.head_image;
                    }

                    if (f.ReplyUserId > 0)
                    {
                        var repUser = userList.Where(u => u.id == f.ReplyUserId).FirstOrDefault();
                        string friendRemark = friendList != null ? friendList.Where(r => r.friend_user_id == f.ReplyUserId).Select(r => r.friend_user_remark).FirstOrDefault() : null;
                        model.ReplyUserName = UserUtility.GetShowName(friendRemark, repUser.name);
                        model.ReplyUserImage = repUser.head_image;
                    }
                }
                res.NearCircleCommentInfos.Add(model);
            });

            return res;
        }
    }
}
