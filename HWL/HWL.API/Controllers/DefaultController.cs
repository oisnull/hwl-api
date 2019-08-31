using System;
using System.Collections.Generic;
using System.ComponentModel;
using GMSF.Model;
using HWL.API.Middleware;
using HWL.Entity.Models;
using HWL.Service;
using HWL.Service.Circle.Body;
using HWL.Service.Generic.Body;
using HWL.Service.Group.Body;
using HWL.Service.Near.Body;
using HWL.Service.User.Body;
using HWL.ShareConfig;
using Microsoft.AspNetCore.Mvc;

namespace HWL.API.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly HWLEntities dbContext;

        public DefaultController(HWLEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Description("用户登陆")]
        public Response<UserLoginResponseBody> UserLogin(Request<UserLoginRequestBody> request)
        {
            return UserService.UserLogin(dbContext, request);
        }

        [HttpPost]
        [Description("用户注册")]
        public Response<UserRegisterResponseBody> UserRegister(Request<UserRegisterRequestBody> request)
        {
            return UserService.UserRegister(dbContext,request);
        }

        [HttpPost]
        [Description("发送邮件")]
        public Response<SendEmailResponseBody> SendEmail(Request<SendEmailRequestBody> request)
        {
            return GenericService.SendEmail(dbContext, request);
        }

        [HttpPost]
        [Description("发送短信")]
        public Response<SendSMSResponseBody> SendSMS(Request<SendSMSRequestBody> request)
        {
            return GenericService.SendSMS(dbContext, request);
        }

        [HttpPost]
        [Description("找回密码")]
        public Response<SetUserPasswordResponseBody> SetUserPassword(Request<SetUserPasswordRequestBody> request)
        {
            return UserService.SetUserPassword(dbContext, request);
        }

        [HttpPost]
        [Description("重置密码")]
        public Response<ResetUserPasswordResponseBody> ResetUserPassword(Request<ResetUserPasswordRequestBody> request)
        {
            return UserService.ResetUserPassword(dbContext, request);
        }

        [HttpPost]
        [Description("设置用户位置信息")]
        public Response<SetUserPosResponseBody> SetUserPos(Request<SetUserPosRequestBody> request)
        {
            return UserService.SetUserPos(dbContext, request);
        }

        [HttpPost]
        [Description("获取好友列表")]
        public Response<GetFriendsResponseBody> GetFriends(Request<GetFriendsRequestBody> request)
        {
            return UserService.GetFriends(dbContext, request);
        }

        [HttpPost]
        [Description("添加好友")]
        public Response<AddFriendResponseBody> AddFriend(Request<AddFriendRequestBody> request)
        {
            return UserService.AddFriend(dbContext, request);
        }

        [HttpPost]
        [Description("删除好友")]
        public Response<DeleteFriendResponseBody> DeleteFriend(Request<DeleteFriendRequestBody> request)
        {
            return UserService.DeleteFriend(dbContext, request);
        }

        [HttpPost]
        [Description("设置好友备注")]
        public Response<SetFriendRemarkResponseBody> SetFriendRemark(Request<SetFriendRemarkRequestBody> request)
        {
            return UserService.SetFriendRemark(dbContext, request);
        }

        [HttpPost]
        [Description("获取用户详细信息")]
        public Response<GetUserDetailsResponseBody> GetUserDetails(Request<GetUserDetailsRequestBody> request)
        {
            return UserService.GetUserDetails(dbContext, request);
        }

        [HttpPost]
        [Description("获取用户关系信息")]
        public Response<GetUserRelationInfoResponseBody> GetUserRelationInfo(Request<GetUserRelationInfoRequestBody> request)
        {
            return UserService.GetUserRelationInfo(dbContext, request);
        }

        [HttpPost]
        [Description("设置用户头像")]
        public Response<SetUserInfoResponseBody> SetUserHeadImage(Request<SetUserHeadImageRequestBody> request)
        {
            return UserService.SetUserHeadImage(dbContext, request);
        }

        [HttpPost]
        [Description("设置用户名称")]
        public Response<SetUserInfoResponseBody> SetUserName(Request<SetUserNameRequestBody> request)
        {
            return UserService.SetUserName(dbContext, request);
        }

        [HttpPost]
        [Description("设置用户描述")]
        public Response<SetUserInfoResponseBody> SetUserLifeNotes(Request<SetUserLifeNotesRequestBody> request)
        {
            return UserService.SetUserLifeNotes(dbContext, request);
        }

        [HttpPost]
        [Description("设置用户性别")]
        public Response<SetUserInfoResponseBody> SetUserSex(Request<SetUserSexRequestBody> request)
        {
            return UserService.SetUserSex(dbContext, request);
        }


        [HttpPost]
        [Description("设置用户标识")]
        public Response<SetUserInfoResponseBody> SetUserSymbol(Request<SetUserSymbolRequestBody> request)
        {
            return UserService.SetUserSymbol(dbContext, request);
        }

        [HttpPost]
        [Description("查找用户信息")]
        public Response<SearchUserResponseBody> SearchUser(Request<SearchUserRequestBody> request)
        {
            return UserService.SearchUser(dbContext, request);
        }

        [HttpPost]
        [Description("获取组用户列表")]
        public Response<GroupUsersResponseBody> GroupUsers(Request<GroupUsersRequestBody> request)
        {
            return GroupService.GroupUsers(dbContext, request);
        }

        [HttpPost]
        [Description("获取所有群组")]
        public Response<GetGroupsResponseBody> GetGroups(Request<GetGroupsRequestBody> request)
        {
            return GroupService.GetGroups(dbContext, request);
        }

        [HttpPost]
        [Description("添加群组")]
        public Response<AddGroupResponseBody> AddGroup(Request<AddGroupRequestBody> request)
        {
            return GroupService.AddGroup(dbContext, request);
        }

        [HttpPost]
        [Description("添加用户的所有群组用户列表")]
        public Response<AddGroupUsersResponseBody> AddGroupUsers(Request<AddGroupUsersRequestBody> request)
        {
            return GroupService.AddGroupUsers(dbContext, request);
        }

        [HttpPost]
        [Description("添加群组用户列表")]
        public Response<GetGroupAndUsersResponseBody> GetGroupAndUsers(Request<GetGroupAndUsersRequestBody> request)
        {
            return GroupService.GetGroupAndUsers(dbContext, request);
        }

        [HttpPost]
        [Description("解散群组")]
        public Response<DeleteGroupResponseBody> DeleteGroup(Request<DeleteGroupRequestBody> request)
        {
            return GroupService.DeleteGroup(dbContext, request);
        }

        [HttpPost]
        [Description("退出群组")]
        public Response<DeleteGroupUserResponseBody> DeleteGroupUser(Request<DeleteGroupUserRequestBody> request)
        {
            return GroupService.DeleteGroupUser(dbContext, request);
        }

        [HttpPost]
        [Description("修改群组公告")]
        public Response<SetGroupNoteResponseBody> SetGroupNote(Request<SetGroupNoteRequestBody> request)
        {
            return GroupService.SetGroupNote(dbContext, request);
        }

        [HttpPost]
        [Description("修改群组名称")]
        public Response<SetGroupNameResponseBody> SetGroupName(Request<SetGroupNameRequestBody> request)
        {
            return GroupService.SetGroupName(dbContext, request);
        }

        [HttpPost]
        [Description("发布附近圈子信息")]
        public Response<AddNearCircleInfoResponseBody> AddNearCircleInfo(Request<AddNearCircleInfoRequestBody> request)
        {
            return NearService.AddNearCircleInfo(dbContext, request);
        }

        [HttpPost]
        [Description("获取附近圈子信息列表")]
        public Response<GetNearCircleInfosResponseBody> GetNearCircleInfos(Request<GetNearCircleInfosRequestBody> request)
        {
            return NearService.GetNearCircleInfos(dbContext, request);
        }

        [HttpPost]
        [Description("获取附近圈子详细信息")]
        public Response<GetNearCircleDetailResponseBody> GetNearCircleDetail(Request<GetNearCircleDetailRequestBody> request)
        {
            return NearService.GetNearCircleDetail(dbContext, request);
        }

        [HttpPost]
        [Description("附近圈子信息点赞设置")]
        public Response<SetNearLikeInfoResponseBody> SetNearLikeInfo(Request<SetNearLikeInfoRequestBody> request)
        {
            return NearService.SetNearLikeInfo(dbContext, request);
        }

        [HttpPost]
        [Description("添加附近圈子信息评论,回复评论")]
        public Response<AddNearCommentResponseBody> AddNearComment(Request<AddNearCommentRequestBody> request)
        {
            return NearService.AddNearComment(dbContext, request);
        }

        [HttpPost]
        [Description("获取附近圈子信息评论,回复评论列表")]
        public Response<GetNearCommentsResponseBody> GetNearComments(Request<GetNearCommentsRequestBody> request)
        {
            return NearService.GetNearComments(dbContext, request);
        }

        [HttpPost]
        [Description("删除附近圈子信息评论,回复评论")]
        public Response<DeleteNearCommentResponseBody> DeleteNearComment(Request<DeleteNearCommentRequestBody> request)
        {
            return NearService.DeleteNearComment(dbContext, request);
        }

        [HttpPost]
        [Description("获取附近圈子信息点赞用户列表")]
        public Response<GetNearLikesResponseBody> GetNearLikes(Request<GetNearLikesRequestBody> request)
        {
            return NearService.GetNearLikes(dbContext, request);
        }

        [HttpPost]
        [Description("删除附近圈子信息")]
        public Response<DeleteNearCircleInfoResponseBody> DeleteNearCircleInfo(Request<DeleteNearCircleInfoRequestBody> request)
        {
            return NearService.DeleteNearCircleInfo(dbContext, request);
        }


        [HttpPost]
        [Description("添加朋友圈子信息")]
        public Response<AddCircleInfoResponseBody> AddCircleInfo(Request<AddCircleInfoRequestBody> request)
        {
            return CircleService.AddCircleInfo(dbContext, request);
        }


        [HttpPost]
        [Description("获取朋友圈子评论列表")]
        public Response<GetCircleCommentsResponseBody> GetCircleComments(Request<GetCircleCommentsRequestBody> request)
        {
            return CircleService.GetCircleComments(dbContext, request);
        }

        [HttpPost]
        [Description("添加朋友圈子评论信息")]
        public Response<AddCommentInfoResponseBody> AddCircleCommentInfo(Request<AddCommentInfoRequestBody> request)
        {
            return CircleService.AddCommentInfo(dbContext, request);
        }

        [HttpPost]
        [Description("删除朋友圈子信息")]
        public Response<DeleteCircleInfoResponseBody> DeleteCircleInfo(Request<DeleteCircleInfoRequestBody> request)
        {
            return CircleService.DeleteCircleInfo(dbContext, request);
        }

        [HttpPost]
        [Description("删除朋友圈子评论信息")]
        public Response<DeleteCommentInfoResponseBody> DeleteCircleCommentInfo(Request<DeleteCommentInfoRequestBody> request)
        {
            return CircleService.DeleteCommentInfo(dbContext, request);
        }

        [HttpPost]
        [Description("获取朋友圈子详细信息")]
        public Response<GetCircleDetailResponseBody> GetCircleDetail(Request<GetCircleDetailRequestBody> request)
        {
            return CircleService.GetCircleDetail(dbContext, request);
        }

        [HttpPost]
        [Description("获取朋友圈子信息列表")]
        public Response<GetCircleInfosResponseBody> GetCircleInfos(Request<GetCircleInfosRequestBody> request)
        {
            return CircleService.GetCircleInfos(dbContext, request);
        }

        [HttpPost]
        [Description("获取指定朋友圈子信息列表")]
        public Response<GetUserCircleInfosResponseBody> GetUserCircleInfos(Request<GetUserCircleInfosRequestBody> request)
        {
            return CircleService.GetUserCircleInfos(dbContext, request);
        }

        [HttpPost]
        [Description("设置朋友圈点赞信息")]
        public Response<SetLikeInfoResponseBody> SetCircleLikeInfo(Request<SetLikeInfoRequestBody> request)
        {
            return CircleService.SetLikeInfo(dbContext, request);
        }

        [HttpPost]
        [Description("APP版本检查")]
        public Response<CheckVersionResponseBody> CheckVersion(Request<CheckVersionRequestBody> request)
        {
            return GenericService.CheckVersion(dbContext, request);
        }

        [HttpPost]
        [Description("设置用户圈子背景图片")]
        public Response<SetUserCircleBackImageResponseBody> SetUserCircleBackImage(Request<SetUserCircleBackImageRequestBody> request)
        {
            return UserService.SetUserCircleBackImage(dbContext, request);
        }
    }
}
