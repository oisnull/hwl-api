using HWL.Redis;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HWL.IMSessionMonitor
{
    public class CheckUserOfflineSessionJob : IJob
    {
        static bool IsRunning = false;

        public async Task Execute(IJobExecutionContext context)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Thread.Sleep(30 * 1000);
                Tuple<int, int> response = Process();
                if (response.Item2 > 0)
                    await Console.Out.WriteLineAsync($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Total: {response.Item1}, Process: {response.Item2}");

                IsRunning = false;
            }
        }

        private Tuple<int, int> Process()
        {
            //userid,offlineTime
            Dictionary<int, string> infos = UserStore.GetUserSessionOfflineInfos();
            if (infos == null || infos.Count <= 0)
                return new Tuple<int, int>(0, 0);

            int handleCount = 0;
            DateTime offlineTime;
            foreach (var item in infos)
            {
                if (!string.IsNullOrEmpty(item.Value) &&
                    DateTime.TryParse(item.Value, out offlineTime) &&
                    offlineTime.AddHours(2) <= DateTime.Now &&
                    UserStore.DeleteSessionOfflineUserInfo(item.Key, item.Value))
                {
                    //Get near group guid recently used by the user and then remove the user from the group according to near group guid
                    string nearGrouGuid = GroupStore.GetRecentUserInNearGroupGuid(item.Key);
                    GroupStore.DeleteGroupUsers(nearGrouGuid, item.Key);
                    handleCount++;
                }
            }
            return new Tuple<int, int>(infos.Count, handleCount);
        }
    }
}
