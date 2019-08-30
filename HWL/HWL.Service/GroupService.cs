using GMSF;
using GMSF.Model;
using HWL.Entity.Models;
using HWL.Service.Group.Body;
using HWL.Service.Group.Service;
using System;

namespace HWL.Service
{
    public class GroupService
    {
        public static Response<GroupUsersResponseBody> GroupUsers(HWLEntities dbContext, Request<GroupUsersRequestBody> request)
        {
            var context = new ServiceContext<GroupUsersRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GroupUsers(dbContext, r.Body).Execute();
            });
        }

        public static Response<AddGroupResponseBody> AddGroup(HWLEntities dbContext, Request<AddGroupRequestBody> request)
        {
            var context = new ServiceContext<AddGroupRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddGroup(dbContext, r.Body).Execute();
            });
        }

        public static Response<AddGroupUsersResponseBody> AddGroupUsers(HWLEntities dbContext, Request<AddGroupUsersRequestBody> request)
        {
            var context = new ServiceContext<AddGroupUsersRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddGroupUsers(dbContext, r.Body).Execute();
            });
        }

        public static Response<DeleteGroupResponseBody> DeleteGroup(HWLEntities dbContext, Request<DeleteGroupRequestBody> request)
        {
            var context = new ServiceContext<DeleteGroupRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteGroup(dbContext, r.Body).Execute();
            });
        }

        public static Response<DeleteGroupUserResponseBody> DeleteGroupUser(HWLEntities dbContext, Request<DeleteGroupUserRequestBody> request)
        {
            var context = new ServiceContext<DeleteGroupUserRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteGroupUser(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetGroupNameResponseBody> SetGroupName(HWLEntities dbContext, Request<SetGroupNameRequestBody> request)
        {
            var context = new ServiceContext<SetGroupNameRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetGroupName(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetGroupNoteResponseBody> SetGroupNote(HWLEntities dbContext, Request<SetGroupNoteRequestBody> request)
        {
            var context = new ServiceContext<SetGroupNoteRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetGroupNote(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetGroupsResponseBody> GetGroups(HWLEntities dbContext, Request<GetGroupsRequestBody> request)
        {
            var context = new ServiceContext<GetGroupsRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetGroups(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetGroupAndUsersResponseBody> GetGroupAndUsers(HWLEntities dbContext, Request<GetGroupAndUsersRequestBody> request)
        {
            var context = new ServiceContext<GetGroupAndUsersRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetGroupAndUsers(dbContext, r.Body).Execute();
            });
        }
    }
}
