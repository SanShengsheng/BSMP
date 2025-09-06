using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.GameTasks
{
    public enum TaskState
    {

        [EnumDescription("未创建")]
        UnCreate = -1,

        [EnumDescription("任务初始化")]
        TaskInitialization = 0,
        /// <summary>
        /// 任务准备
        /// </summary>
        [EnumDescription("任务准备")]
        TaskReady = 1,
        /// <summary>
        /// 任务进行
        /// </summary>
        [EnumDescription("任务进行")]
        TaskProgress = 2,
        /// <summary>
        /// 任务成功
        /// </summary>
        [EnumDescription("任务成功")]
        TaskSuccess = 3,
        /// <summary>
        /// 任务失败
        /// </summary>
        [EnumDescription("任务失败")]
        TaskFailure = 4,
        /// <summary>
        /// 在玩一次
        /// </summary>
        [EnumDescription("在玩一次")]
        TaskAgain = 5,

        [EnumDescription("邀请方抽奖结果")]
        TaskinviterPrizeResult = 6,

        [EnumDescription("被邀请方抽奖结果")]
        TaskinviteePrizeResult = 7,

        [EnumDescription("分发题目")]
        TaskDistributeQuestion = 8,

        [EnumDescription("邀请方开始抽奖")]
        TaskinviterStartPrize = 9,

        [EnumDescription("被邀请方开始抽奖")]
        TaskinviteeStartPrize = 10,
    }
}
