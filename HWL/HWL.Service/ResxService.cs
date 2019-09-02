using GMSF;
using GMSF.Model;
using HWL.Service.Resx.Body;
using HWL.Service.Resx.Service;

namespace HWL.Service
{
    public class ResxService
    {
        public static Response<ResxUploadResponseBody> ResxUpload(Request<ResxUploadRequestBody> request)
        {
            var context = new ServiceContext<ResxUploadRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new ResxUpload(r.Body).Execute();
            });
        }

        public static Response<ImageUploadResponseBody> ImageUpload(Request<ImageUploadRequestBody> request)
        {
            var context = new ServiceContext<ImageUploadRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new ImageUpload(r.Body).Execute();
            });
        }

        public static Response<AudioUploadResponseBody> AudioUpload(Request<AudioUploadRequestBody> request)
        {
            var context = new ServiceContext<AudioUploadRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new AudioUpload(r.Body).Execute();
            });
        }

        public static Response<VideoUploadResponseBody> VideoUpload(Request<VideoUploadRequestBody> request)
        {
            var context = new ServiceContext<VideoUploadRequestBody>(request, new RequestValidate());
            return ContextProcessor.Execute(context, r =>
            {
                return new VideoUpload(r.Body).Execute();
            });
        }
    }
}
