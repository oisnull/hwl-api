using HWL.Redis;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading;

namespace HWL.IMSessionMonitor
{
    class Program
    {
        //static void AddTestData()
        //{
        //    UserStore.SaveUserSessionToOffline(1);
        //    Thread.Sleep(2000);
        //    UserStore.SaveUserSessionToOffline(2);
        //    Thread.Sleep(2000);
        //    UserStore.SaveUserSessionToOffline(3);
        //    Thread.Sleep(2000);
        //    UserStore.SaveUserSessionToOffline(4);
        //}

        static IScheduler scheduler = null;
        static async void JobSchedulerAsync()
        {
            //LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<CheckUserOfflineSessionJob>()
                //.WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                //.WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    //.WithIntervalInSeconds(10)
                    .WithIntervalInHours(1)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }

        static async void SchedulerStop()
        {
            if (scheduler == null) return;

            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();
            scheduler = null;
        }

        static void Main(string[] args)
        {
            //Command: HWL.IMSessionMonitor.exe prod

            Init(args);
            //AddTestData();

            JobSchedulerAsync();
            Console.WriteLine("Scheduler jobs for check im offline session start ...");
            Console.WriteLine("Press 'quit' to close the application");

            Quit();
        }

        static void Init(string[] args)
        {
            string env = args != null && args.Length > 0 ? args[0] : "prod";
            Environment.SetEnvironmentVariable("Environment", env);
            ShareConfig.LogHelper.InitConfigure("ims");
            ShareConfig.LogHelper.Debug($"environment:{ShareConfig.ShareConfiguration.CurrentEnvironment}");
        }

        static void Quit()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input?.ToLower() == "quit")
                {
                    SchedulerStop();
                    break;
                }
            }
        }
    }
}
