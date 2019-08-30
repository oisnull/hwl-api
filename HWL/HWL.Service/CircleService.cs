using GMSF;
using GMSF.Model;
using HWL.Entity.Models;
using HWL.Service.Circle.Body;
using HWL.Service.Circle.Service;

namespace HWL.Service
{
    public class CircleService
    {
        public static Response<AddCircleInfoResponseBody> AddCircleInfo(HWLEntities dbContext, Request<AddCircleInfoRequestBody> request)
        {
            var context = new ServiceContext<AddCircleInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddCircleInfo(dbContext, r.Body).Execute();
            });
        }
        public static Response<AddCommentInfoResponseBody> AddCommentInfo(HWLEntities dbContext, Request<AddCommentInfoRequestBody> request)
        {
            var context = new ServiceContext<AddCommentInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AddCommentInfo(dbContext, r.Body).Execute();
            });
        }
        public static Response<DeleteCircleInfoResponseBody> DeleteCircleInfo(HWLEntities dbContext, Request<DeleteCircleInfoRequestBody> request)
        {
            var context = new ServiceContext<DeleteCircleInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteCircleInfo(dbContext, r.Body).Execute();
            });
        }
        public static Response<DeleteCommentInfoResponseBody> DeleteCommentInfo(HWLEntities dbContext, Request<DeleteCommentInfoRequestBody> request)
        {
            var context = new ServiceContext<DeleteCommentInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new DeleteCommentInfo(dbContext, r.Body).Execute();
            });
        }
        public static Response<GetCircleDetailResponseBody> GetCircleDetail(HWLEntities dbContext, Request<GetCircleDetailRequestBody> request)
        {
            var context = new ServiceContext<GetCircleDetailRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetCircleDetail(dbContext, r.Body).Execute();
            });
        }
        public static Response<GetCircleInfosResponseBody> GetCircleInfos(HWLEntities dbContext, Request<GetCircleInfosRequestBody> request)
        {
            var context = new ServiceContext<GetCircleInfosRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetCircleInfos(dbContext, r.Body).Execute();
            });
        }
        public static Response<GetUserCircleInfosResponseBody> GetUserCircleInfos(HWLEntities dbContext, Request<GetUserCircleInfosRequestBody> request)
        {
            var context = new ServiceContext<GetUserCircleInfosRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetUserCircleInfos(dbContext, r.Body).Execute();
            });
        }
        public static Response<SetLikeInfoResponseBody> SetLikeInfo(HWLEntities dbContext, Request<SetLikeInfoRequestBody> request)
        {
            var context = new ServiceContext<SetLikeInfoRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new SetLikeInfo(dbContext, r.Body).Execute();
            });
        }
        public static Response<GetCircleCommentsResponseBody> GetCircleComments(HWLEntities dbContext, Request<GetCircleCommentsRequestBody> request)
        {
            var context = new ServiceContext<GetCircleCommentsRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new GetCircleComments(dbContext, r.Body).Execute();
            });
        }
    }
}
