using System;
using System.Collections.Generic;
using System.Text;
using HWL.PushStandard;

namespace HWL.PushService
{
    public class UserConsumeHandler : IConsumeHandler
    {
        private SourceType sourceType;
        private long userId;
        private PushMessageType messageType;
        private PushMessageModel messageModel;

        public void Process(PushModel model)
        {
            if (model == null)
                throw new ArgumentNullException("PushModel");

            this.sourceType = model.SourceType;
            this.userId = model.PositionModel?.UserId ?? 0;
            this.messageType = model.PushMessageType;
            this.messageModel = model.MessageModel;

            if (this.userId <= 0)
                throw new ArgumentNullException("UserId");

            this.CheckMessageModel();

            //save to db for current user id 


        }

        protected void CheckMessageModel()
        {
            if (this.messageModel == null)
                throw new ArgumentNullException("PushMessageModel");

            if (string.IsNullOrEmpty(this.messageModel.Content))
                throw new ArgumentNullException("PushMessageModel.Content");
        }
    }
}
