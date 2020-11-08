using HWL.IMClient.Core;
using HWL.IMCore.Protocol;

namespace HWL.IMClient.Send
{
    public class AppVersionMessageSend : AbstractMessageSendExecutor
    {
        ulong[] _userIds;
        ImAppVersionContent _versionContent;
        public AppVersionMessageSend(ulong[] userIds, ImAppVersionContent versionContent)
        {
            this._userIds = userIds;
            this._versionContent = versionContent;
        }

        public override ImMessageType getMessageType()
        {
            return ImMessageType.AppVersion;
        }

        public override ImMessageRequest getMessageRequest()
        {
            ImMessageRequest request = new ImMessageRequest()
            {
                AppVersionRequest = new ImAppVersionRequest()
                {
                    AppVersionContent = this._versionContent,
                }
            };
            request.AppVersionRequest.UserIds.AddRange(_userIds);
            return request;
        }

        public override void success()
        {
            base.success();
        }

        public override void failure(string message)
        {
            ShareConfig.LogHelper.Error($"AppVersionMessageSend:{message}", typeof(AppVersionMessageSend));
        }
    }
}
