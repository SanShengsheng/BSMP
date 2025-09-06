using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BonusPoints
{
    public class StaticEventDescription
    {
        /// <summary>
        /// 授权登录
        /// </summary>
        public const string authorizationLoginDescription = "授权登录用户";

        /// <summary>
        /// 普通登录
        /// </summary>
        public const string LoginDescription = "普通登录用户";

        /// <summary>
        /// 邀请方开始任务 触发
        /// </summary>
        public const string InviteFriendAndStartTaskDescription = "邀请方用户";

        /// <summary>
        /// 被邀请放进入任务并任务开始 触发 
        /// </summary>
        public const string InviteeJoinTaskDescription = "被邀请方用户";

        /// <summary>
        /// 主客场双方答案一致 触发
        /// </summary>
        public const string AnswerAgreementDescription = "主客场双方";

        /// <summary>
        /// (客场)被邀请方没答题
        /// </summary>
        public const string InviteeNoAnswerDescription = "客场用户";

        /// <summary>
        /// (主场)没有答题
        /// </summary>
        public const string InviterNoAnswerDescription = "主场用户";

        /// <summary>
        /// 三题关 触发
        /// </summary>
        public const string ThreeQuestionsBarrierDescription = "主客场双方";

        /// <summary>
        /// 五题关 触发
        /// </summary>
        public const string FiveQuestionsBarrierDescription = "主客场双方";

        /// <summary>
        /// 十题关触发
        /// </summary>
        public const string TenQuestionsBarrierDescription = "主客场双方";

        /// <summary>
        /// 使用延时卡
        /// </summary>
        public const string UseDelayCardDescription = "使用者";

        /// <summary>
        /// 使用数据卡
        /// </summary>
        public const string UseDataCardDescription = "使用者";

        /// <summary>
        /// 分享成功/失败 分享小程序
        /// </summary>
        public const string ShareFriendsDescription = "分享好友";
    }
}
