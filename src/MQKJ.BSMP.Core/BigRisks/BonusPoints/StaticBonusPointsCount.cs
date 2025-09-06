using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BonusPoints
{
    public class StaticBonusPointsCount
    {
        /// <summary>
        /// 授权登录获取的爱豆数量
        /// </summary>
        public const int AuthLoginCount = 5;

        /// <summary>
        /// 普通登录获取的爱豆数量
        /// </summary>
        public const int LoginCount = 1;

        /// <summary>
        /// 邀请好友获取的爱豆数量
        /// </summary>
        public const int InviteFirendCount = 2;

        /// <summary>
        /// 被邀请后开始任务获取的爱豆
        /// </summary>
        public const int InviteeCount = 1;

        /// <summary>
        /// 主客答案一致获取的爱豆
        /// </summary>
        public const int AnswerAgreementCount = 1;

        /// <summary>
        /// 客场没有答题获取的爱豆
        /// </summary>
        public const int InviteeNoAnswerCount = -1;

        /// <summary>
        /// 主场没有答题获取的爱豆
        /// </summary>
        public const int InviterNoAnswerCount = -2;

        /// <summary>
        /// 三题关获取的爱豆
        /// </summary>
        public const int ThreeQuestionCount = 1;

        /// <summary>
        /// 五题关获取的爱豆
        /// </summary>
        public const int FiveQuestionCount = 2;

        /// <summary>
        /// 十题关获取的爱豆
        /// </summary>
        public const int TenQuestionCount = 4;

        /// <summary>
        /// 使用延时卡
        /// </summary>
        public const int UseDelayCount = -10;

        /// <summary>
        /// 使用数据卡
        /// </summary>
        public const int UseDataCount = -10;

        /// <summary>
        /// 分享成功/失败 分享小程序
        /// </summary>
        public const int ShareFriendCount = 1;
    }
}
