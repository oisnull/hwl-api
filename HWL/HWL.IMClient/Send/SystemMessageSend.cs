using HWL.IMClient.Core;
using HWL.IMCore.Protocol;

namespace HWL.IMClient.Send
{
    public class SystemMessageSend : AbstractMessageSendExecutor
    {
        ulong toUserId = 0L;
        string toUserName = null;
        string toGroupGuid = null;
        string groupName = null;

        public SystemMessageSend(ulong toUserId, string toUserName, string groupGuid, string groupName)
        {
            this.toUserId = toUserId;
            this.toUserName = toUserName;
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
                    ToUserId = toUserId,
                    ToGroupGuid = toGroupGuid ?? "",
                    SystemMessageContent = new ImSystemMessageContent()
                    {
                        SystemMessageType = ImSystemMessageType.AddNearGroup,
                        AddGroupDesc = string.Format("Welcome {0} to {1} group.", toUserName, groupName),
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
            base.failure(message);
        }
    }
}
