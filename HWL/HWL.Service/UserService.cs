
using GMSF;
using GMSF.Model;
using HWL.Entity.Models;
using HWL.Service.User.Body;
using HWL.Service.User.Service;

namespace HWL.Service
{
    public class UserService
    {
        public static UserLoginResponseBody UserLogin(HWLEntities dbContext, UserLoginRequestBody request)
        {
            IService<UserLoginResponseBody> service = new UserLogin(dbContext, request);
            return service.Execute();
        }

        public static Response<UserLoginResponseBody> UserLogin(HWLEntities dbContext, Request<UserLoginRequestBody> request)
        {
            var context = new ServiceContext<UserLoginRequestBody>(request, new RequestValidate(false, false));
            return ContextProcessor.Execute(context, r => UserLogin(dbContext, r.Body));
        }

        public static Response<UserRegisterResponseBody> UserRegister(HWLEntities dbContext, Request<UserRegisterRequestBody> request)
        {
            var context = new ServiceContext<UserRegisterRequestBody>(request, new RequestValidate(false, false));
            return ContextProcessor.Execute(context, r =>
            {
                return new UserRegister(dbContext, r.Body).Execute();
            });
        }

        public static Response<UserLoginAndRegisterResponseBody> UserLoginAndRegister(HWLEntities dbContext, Request<UserLoginAndRegisterRequestBody> request)
        {
            var context = new ServiceContext<UserLoginAndRegisterRequestBody>(request, new RequestValidate(false, false));
            return ContextProcessor.Execute(context, r =>
            {
                return new UserLoginAndRegister(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserPasswordResponseBody> SetUserPassword(HWLEntities dbContext, Request<SetUserPasswordRequestBody> request)
        {
            var context = new ServiceContext<SetUserPasswordRequestBody>(request, new RequestValidate(false, false));
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserPassword(dbContext, r.Body).Execute();
            });
        }

        public static Response<ResetUserPasswordResponseBody> ResetUserPassword(HWLEntities dbContext, Request<ResetUserPasswordRequestBody> request)
        {
            var context = new ServiceContext<ResetUserPasswordRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new ResetUserPassword(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserPosResponseBody> SetUserPos(HWLEntities dbContext, Request<SetUserPosRequestBody> request)
        {
            var context = new ServiceContext<SetUserPosRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserPos(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetFriendsResponseBody> GetFriends(HWLEntities dbContext, Request<GetFriendsRequestBody> request)
        {
            var context = new ServiceContext<GetFriendsRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetFriends(dbContext, r.Body).Execute();
            });
        }

        public static Response<AddFriendResponseBody> AddFriend(HWLEntities dbContext, Request<AddFriendRequestBody> request)
        {
            var context = new ServiceContext<AddFriendRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddFriend(dbContext, r.Body).Execute();
            });
        }

        public static Response<DeleteFriendResponseBody> DeleteFriend(HWLEntities dbContext, Request<DeleteFriendRequestBody> request)
        {
            var context = new ServiceContext<DeleteFriendRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteFriend(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetFriendRemarkResponseBody> SetFriendRemark(HWLEntities dbContext, Request<SetFriendRemarkRequestBody> request)
        {
            var context = new ServiceContext<SetFriendRemarkRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetFriendRemark(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserInfoResponseBody> SetUserSymbol(HWLEntities dbContext, Request<SetUserSymbolRequestBody> request)
        {
            var context = new ServiceContext<SetUserSymbolRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserSymbol(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetUserDetailsResponseBody> GetUserDetails(HWLEntities dbContext, Request<GetUserDetailsRequestBody> request)
        {
            var context = new ServiceContext<GetUserDetailsRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetUserDetails(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserInfoResponseBody> SetUserLifeNotes(HWLEntities dbContext, Request<SetUserLifeNotesRequestBody> request)
        {
            var context = new ServiceContext<SetUserLifeNotesRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserLifeNotes(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserInfoResponseBody> SetUserName(HWLEntities dbContext, Request<SetUserNameRequestBody> request)
        {
            var context = new ServiceContext<SetUserNameRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserName(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserInfoResponseBody> SetUserHeadImage(HWLEntities dbContext, Request<SetUserHeadImageRequestBody> request)
        {
            var context = new ServiceContext<SetUserHeadImageRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserHeadImage(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserCircleBackImageResponseBody> SetUserCircleBackImage(HWLEntities dbContext, Request<SetUserCircleBackImageRequestBody> request)
        {
            var context = new ServiceContext<SetUserCircleBackImageRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserCircleBackImage(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetUserInfoResponseBody> SetUserSex(HWLEntities dbContext, Request<SetUserSexRequestBody> request)
        {
            var context = new ServiceContext<SetUserSexRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetUserSex(dbContext, r.Body).Execute();
            });
        }

        public static Response<SearchUserResponseBody> SearchUser(HWLEntities dbContext, Request<SearchUserRequestBody> request)
        {
            var context = new ServiceContext<SearchUserRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SearchUser(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetUserRelationInfoResponseBody> GetUserRelationInfo(HWLEntities dbContext, Request<GetUserRelationInfoRequestBody> request)
        {
            var context = new ServiceContext<GetUserRelationInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetUserRelationInfo(dbContext, r.Body).Execute();
            });
        }
    }
}
