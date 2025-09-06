using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using MQKJ.BSMP.Controllers;
using Abp.Quartz;
using Quartz;
using System.IO;
using System;
using Hangfire;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions;
using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies.Athletics;

namespace MQKJ.BSMP.Web.Host.Controllers
{
    public class HomeController : BSMPControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;

        //private readonly IQuartzScheduleJobManager _quartzScheduleJobManager;

        //private readonly ISchedulerFactory _schedulerFactory;

        //private IScheduler _scheduler;

        //private readonly IRepository<SeasonManagement> _seasonManagementRepository;

        public HomeController(INotificationPublisher notificationPublisher/*IQuartzScheduleJobManager quartzScheduleJobManager*/
            //ISchedulerFactory schedulerFactory,
            //IRepository<SeasonManagement> seasonManagementRepository
            )
        {
            _notificationPublisher = notificationPublisher;

            //_quartzScheduleJobManager = quartzScheduleJobManager;
            //_schedulerFactory = schedulerFactory;

            //_seasonManagementRepository = seasonManagementRepository;
        }

        public IActionResult Index()
        {
            //_schedulerFactory.GetScheduler();

            //var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);
            //RecurringJob.AddOrUpdate("UTC", () => Console.WriteLine("UTC"), "15 18 * * *");
            //RecurringJob.AddOrUpdate("Russian", () => Console.WriteLine("Russian"), "15 21 * * *", TimeZoneInfo.Local);
            //var time = DateTime.Now.Minute;
            return Redirect("/swagger");
        }

        [HttpGet("TestQuartz")]
        public async Task<string[]> TestQuartz()
        {
            ////通过调度工厂获取调度器
            //_scheduler = await _schedulerFactory.GetScheduler();
            ////开启调度器
            //await _scheduler.Start();
            ////创建一个触发器
            //var trigger = TriggerBuilder.Create()
            //    .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
            //    .Build();
            ////创建任务
            //var jobDetail = JobBuilder.Create<QueryOrderJob>()
            //    .WithIdentity("job", "group")
            //    .Build();

            ////将触发器跟任务绑定dao 
            //await _scheduler.ScheduleJob(jobDetail, trigger);

            return await Task.FromResult(new string[] { "value1", "value2" });
        }

        public class QueryOrderJob : IJob
        {
            public Task Execute(IJobExecutionContext jobExecutionContext)
            {
                return Task.Run(() =>
                {
                    using (StreamWriter sw = new StreamWriter(@"C:\Users\www\error.log"))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                    }
                });
            }
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }

        /// <summary>
        /// 测试CD
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [HttpGet("TestCD")]
        public  ActionResult TestCD(int cd)
        {
             var endTimeStamp = new DateTimeOffset(DateTime.UtcNow.AddSeconds(cd));
            var timeSeconds= endTimeStamp.ToUnixTimeSeconds();
            return  new JsonResult(new {CD=cd, EndTimeStamp = timeSeconds }) ;
        }

        [HttpGet("TestRedis")]
        public ActionResult TestRedis()
        {
            RedisHelper.Set("test", 2,5);

            var str = RedisHelper.Get("test");

            //RedisHelper.Set("user", new Person()
            //{
            //    Age = 23,
            //    IsDeveloper =true,
            //    NickName = "admin",
            //    UserName = "张三"
            //},5);

            var person = RedisHelper.Get<Person>("user");

            var keys = RedisHelper.Scan<Person>("user",3,null,null);

            return Content(str);
        }
     
    }

    public class Person
    {
        public string UserName { get; set; }

        public string NickName { get; set; }

        public int Age { get; set; }

        public bool IsDeveloper { get; set; }
    }
}
