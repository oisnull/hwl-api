using HWL.IMClient;
using HWL.IMClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.IMClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ShareConfig.LogHelper.InitConfigure("imtest");

            IMClientV.SetConnectListener(new DefaultClientConnectListenerV2());
            //IMClientV.INSTANCE.SendSystemMessage(new IMCore.Protocol.ImUserContent()
            //{
            //    UserId = 3,
            //    UserImage = "http://192.168.1.6:8033/images/default.png",
            //    UserName = "HWL-4028333"
            //}, "2311d5f9-19ec-4567-aac8-557f62b6bbc6", "浦晓南路(test-1)");

            //IMClientV.INSTANCE.SendAppVersionMessage(new ulong[] { 16, 2 }, new IMCore.Protocol.ImAppVersionContent()
            //{
            //    AppName = "hwl",
            //    AppSize = 3449122,
            //    AppVersion = "1.0.0",
            //    DownloadUrl = "http://10.61.8.23:8081/upload/apkversion/sampledebug20200827155826.apk",
            //    UpgradeLog = "1,DomainLevelMetrics.script\n2,DomainToUETTile.script\n3,SparkUETGivenMUID.script\n4,UETGivenMUID.script"
            //});

            Console.ReadLine();
        }
    }

    public class DefaultClientConnectListenerV2 : IClientConnectListener
    {
        public void onBuildConnectionFailure(string clientAddress, string errorInfo)
        {
            Console.WriteLine("onBuildConnectionFailure : {0},{1}", clientAddress, errorInfo);
        }

        public void onBuildConnectionSuccess(string clientAddress, string serverAddress)
        {
            Console.WriteLine("onBuildConnectionSuccess : {0},{1}", clientAddress, serverAddress);
        }

        public void onClosed(string clientAddress)
        {
            Console.WriteLine("onClosed : " + clientAddress);
        }

        public void onConnected(string clientAddress, string serverAddress)
        {
            Console.WriteLine("onConnected : {0},{1}", clientAddress, serverAddress);
        }

        public void onDisconnected(string clientAddress)
        {
            Console.WriteLine("onDisconnected : " + clientAddress);
        }

        public void onError(string clientAddress, string errorInfo)
        {
            Console.WriteLine("onError : {0},{1}", clientAddress, errorInfo);
        }
    }
}
