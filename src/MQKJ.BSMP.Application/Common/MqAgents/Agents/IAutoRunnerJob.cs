using Abp.Dependency;
using MQKJ.BSMP.Common.MqAgents.Agents.Config;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Common.MqAgents.Agents
{
    public interface IAutoRunnerJob:ITransientDependency
    {
        /// <summary>
        /// 开启外挂
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task StartRunnerJob(StartRunnerRequest request);

        /// <summary>
        /// 自动停止脚本
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task StopRunnerJob(StopRunnerRequest request);

        /// <summary>
        /// 执行成长事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task ExecuteEvent(ExecuteEventRequest request);
        /// <summary>
        /// 执行学习事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task ExecuteStudyEvent(ExecuteEventRequest request);

        Task CheckCanAutoRunner(CheckCanAutoRunnerRequest request);

        Task AutoStartRunnerJob(StartAutoRunRequest request);
        /// <summary>
        /// 自动膜拜获取声望值
        /// </summary>
        string AutoStartWorshipJob();
        /// <summary>
        /// 清除自动膜拜任务
        /// </summary>
        /// <param name="job"></param>
        int ClearWorshipJob(string job);
        /// <summary>
        /// 配置膜拜参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ApplyWorshipJobConfig(WorshipJobConfigInput input);

        /// <summary>
        /// 设置膜拜测试时间配置
        /// </summary>
        /// <returns></returns>
        string ApplyWorshipJobTestConfig();
        /// <summary>
        /// 清除所有膜拜自动任务
        /// </summary>
        /// <returns></returns>
        int ClearAllWorshipJobs(ClearType type);
        /// <summary>
        /// 修改系统膜拜参数
        /// </summary>
        /// <param name="worshipedTimesMax">每天被膜拜的最大次数</param>
        /// <param name="toWorshipTimesMax">每天膜拜次数限制</param>
        /// <returns></returns>
        string ModifyWorshipSystemConfig(string worshipedTimesMax, string toWorshipTimesMax);
        /// <summary>
        /// 获取所有膜拜自动任务
        /// </summary>
        /// <returns></returns>
        IList<RecurringJobDtoOutput> GetAllWorshipJobs();
    }
}
