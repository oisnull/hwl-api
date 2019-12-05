using System;
using HWL.Entity;
using HWL.Entity.Models;
using HWL.PushStandard;
using HWL.Redis;
using HWL.ShareConfig;

namespace HWL.PushService
{
    public class NearCircleConsumeHandler : IConsumeHandler
    {
        private SourceType sourceType;
        private int userId;
        private PushMessageType messageType;
        private PushPositionModel positionModel;
        private PushMessageModel messageModel;

        public void Process(PushModel model)
        {
            if (model == null)
                throw new ArgumentNullException("PushModel");

            this.sourceType = model.SourceType;
            this.userId = model.PositionModel?.UserId ?? 0;
            this.messageType = model.PushMessageType;
            this.messageModel = model.MessageModel;
            this.positionModel = model.PositionModel;

            if (this.userId <= 0)
                throw new ArgumentNullException("UserId");

            if (this.positionModel == null)
                throw new ArgumentNullException("PushPositionModel");

            this.CheckMessageModel();

            this.AddNearCircle();
        }

        private void AddNearCircle()
        {
            var db = HWLDBContext.GetDBContext(ShareConfiguration.DBConnectionString);
            t_near_circle model = new t_near_circle()
            {
                user_id = this.userId,
                content_info = this.messageModel.Content,
                content_type = CustomerEnumDesc.GetCircleContentType(this.messageModel.Content, null, null, this.messageModel.ImageUrls?.Count ?? 0),
                lat = this.positionModel.Lat,
                lon = this.positionModel.Lon,
                pos_desc = this.positionModel.PosDetails,
                pos_id = 0,
                comment_count = 0,
                image_count = 0,
                like_count = 0,
                publish_time = DateTime.Now,
                update_time = DateTime.Now
            };
            db.t_near_circle.Add(model);
            db.SaveChanges();

            NearCircleStore.CreateNearCirclePos(model.id, model.lon, model.lat);
        }

        protected void CheckMessageModel()
        {
            if (this.messageModel == null)
                throw new ArgumentNullException("PushMessageModel");

            if (string.IsNullOrEmpty(this.messageModel.Title))
                throw new ArgumentNullException("PushMessageModel.Title");

            if (string.IsNullOrEmpty(this.messageModel.Content))
                throw new ArgumentNullException("PushMessageModel.Content");
        }
    }
}
