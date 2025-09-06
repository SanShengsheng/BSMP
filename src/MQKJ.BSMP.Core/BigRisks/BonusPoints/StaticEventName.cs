using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BonusPoints
{
    public class StaticEventName
    {
        /// <summary>
        /// 授权登录 触发
        /// </summary>
        public const string authorizationLogin = "授权登录";

        /// <summary>
        /// 每次获取玩家信息 触发
        /// </summary>
        public const string Login = "普通登录";

        /// <summary>
        /// 邀请方开始任务 触发
        /// </summary>
        public const string InviteFriendAndStartTask = "邀请好友并开始任务";

        /// <summary>
        /// 被邀请放进入任务并任务开始 触发 
        /// </summary>
        public const string InviteeJoinTask = "被好友邀请后并开始任务";

        /// <summary>
        /// 主客场双方答案一致 触发
        /// </summary>
        public const string AnswerAgreement = "主客场双方答案一致";

        /// <summary>
        /// (客场)被邀请方没答题
        /// </summary>
        public const string InviteeNoAnswer = "客场没有答题";

        /// <summary>
        /// (主场)没有答题
        /// </summary>
        public const string InviterNoAnswer = "主场没有答题";

        /// <summary>
        /// 三题关 触发
        /// </summary>
        public const string ThreeQuestionsBarrier = "三题关";

        /// <summary>
        /// 五题关 触发
        /// </summary>
        public const string FiveQuestionsBarrier = "五题关";

        /// <summary>
        /// 十题关触发
        /// </summary>
        public const string TenQuestionsBarrier = "十题关";

        /// <summary>
        /// 使用延时卡
        /// </summary>
        public const string UseDelayCard = "使用延时卡";

        /// <summary>
        /// 使用数据卡
        /// </summary>
        public const string UseDataCard = "使用数据卡";

        /// <summary>
        /// 分享成功/失败  分享小程序
        /// </summary>
        public const string ShareFriend = "分享好友";
    }
}
