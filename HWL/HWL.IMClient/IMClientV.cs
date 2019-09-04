using HWL.IMClient.Core;
using HWL.IMClient.Send;
using HWL.ShareConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.IMClient
{
    public class IMClientV
    {
        private static IMClientV instance = new IMClientV();
        private static IClientConnectListener clientConnectListener;

        public static void SetConnectListener(IClientConnectListener connectListener)
        {
            clientConnectListener = connectListener;
        }

        public static IMClientV INSTANCE
        {
            get
            {
                if (instance == null)
                {
                    instance = new IMClientV();
                }
                return instance;
            }
        }

        private IMClientEngine im;

        private void checkConnect()
        {
            if (im == null || !im.isConnected())
            {
                im = new IMClientEngine(IMConfigManager.IMHost, IMConfigManager.IMPort);
                im.setConnectListener(clientConnectListener);
                im.connect();
            }
        }

        public void SendSystemMessage(ulong toUserId, string toUserName, string toGroupGuid, string toGroupName)
        {
            checkConnect();

            im.send(new SystemMessageSend(toUserId, toUserName, toGroupGuid, toGroupName));
        }
    }
}
