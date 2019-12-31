using HWL.IMClient.Core;
using HWL.IMCore.Protocol;

namespace HWL.IMClient.Send
{
    public class SystemMessageSend : AbstractMessageSendExecutor
    {
        ImUserContent toUser = null;
        string toGroupGuid = null;
        string groupName = null;

        public SystemMessageSend(ImUserContent toUser, string groupGuid, string groupName)
        {
            this.toUser = toUser;
            this.toGroupGuid = groupGuid;
            this.groupName = groupName;
        }

        public override ImMessageType getMessageType()
        {
            return ImMessageType.SystemMessage;
        }

        public override ImMessageRequest getMessageRequest()
        {
            return new ImMessageRequest()
            {
                SystemMessageRequest = new ImSystemMessageRequest()
                {
                    ToUser = this.toUser,
                    ToGroupGuid = toGroupGuid ?? "",
                    SystemMessageContent = new ImSystemMessageContent()
                    {
                        SystemMessageType = ImSystemMessageType.AddNearGroup,
                        AddGroupDesc = string.Format("Welcome {0} to {1} group.", this.toUser.UserName, groupName),
                        ToUserDesc = string.Format("Welcome to {0}", groupName)
                    }
                }
            };
        }

        public override void success()
        {
            base.success();
        }

        public override void failure(string message)
        {
            ShareConfig.Log4NetManager.Error($"SystemMessageSend:{message}");
        }
    }
}
