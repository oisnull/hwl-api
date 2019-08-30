using GMSF;
using GMSF.Model;
using HWL.Entity.Models;
using HWL.Service.Near.Body;
using HWL.Service.Near.Service;
using System;

namespace HWL.Service
{
    public class NearService
    {
        public static Response<AddNearCircleInfoResponseBody> AddNearCircleInfo(HWLEntities dbContext, Request<AddNearCircleInfoRequestBody> request)
        {
            var context = new ServiceContext<AddNearCircleInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddNearCircleInfo(dbContext, r.Body).Execute();
            });
        }
        public static Response<GetNearCircleInfosResponseBody> GetNearCircleInfos(HWLEntities dbContext, Request<GetNearCircleInfosRequestBody> request)
        {
            var context = new ServiceContext<GetNearCircleInfosRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetNearCircleInfos(dbContext, r.Body).Execute();
            });
        }
        public static Response<GetNearCircleDetailResponseBody> GetNearCircleDetail(HWLEntities dbContext, Request<GetNearCircleDetailRequestBody> request)
        {
            var context = new ServiceContext<GetNearCircleDetailRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetNearCircleDetail(dbContext, r.Body).Execute();
            });
        }

        public static Response<AddNearCommentResponseBody> AddNearComment(HWLEntities dbContext, Request<AddNearCommentRequestBody> request)
        {
            var context = new ServiceContext<AddNearCommentRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddNearComment(dbContext, r.Body).Execute();
            });
        }

        public static Response<SetNearLikeInfoResponseBody> SetNearLikeInfo(HWLEntities dbContext, Request<SetNearLikeInfoRequestBody> request)
        {
            var context = new ServiceContext<SetNearLikeInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetNearLikeInfo(dbContext, r.Body).Execute();
            });
        }

        public static Response<DeleteNearCommentResponseBody> DeleteNearComment(HWLEntities dbContext, Request<DeleteNearCommentRequestBody> request)
        {
            var context = new ServiceContext<DeleteNearCommentRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteNearComment(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetNearCommentsResponseBody> GetNearComments(HWLEntities dbContext, Request<GetNearCommentsRequestBody> request)
        {
            var context = new ServiceContext<GetNearCommentsRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetNearComments(dbContext, r.Body).Execute();
            });
        }

        public static Response<GetNearLikesResponseBody> GetNearLikes(HWLEntities dbContext, Request<GetNearLikesRequestBody> request)
        {
            var context = new ServiceContext<GetNearLikesRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetNearLikes(dbContext, r.Body).Execute();
            });
        }

        public static Response<DeleteNearCircleInfoResponseBody> DeleteNearCircleInfo(HWLEntities dbContext, Request<DeleteNearCircleInfoRequestBody> request)
        {
            var context = new ServiceContext<DeleteNearCircleInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteNearCircleInfo(dbContext, r.Body).Execute();
            });
        }
    }
}
