using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.WeChat
{
    /// <summary>
    /// 获取socket接口名
    /// </summary>
    public static class StaticGetMethodName
    {

        /// <summary>
        /// 系统消息
        /// </summary>
        public static string SystemeInfo = "system";

        /// <summary>
        /// 邀请好友
        /// </summary>
        public static string InviteFriend = "InviteFriend";

        /// <summary>
        /// 游戏初始化
        /// </summary>
        public static string GameInitialization = "GameInitialization";

        /// <summary>
        /// 游戏准备
        /// </summary>
        public static string GameReady = "GameReady";

        /// <summary>
        /// 游戏开始
        /// </summary>
        public static string GameStart = "GameStart";

        /// <summary>
        /// 分发题目
        /// </summary>
        public static string DistributeQuestions = "DistributeQuestions";

        /// <summary>
        /// 进入游戏
        /// </summary>
        public static string EnterGame = "EnterGame";

        /// <summary>
        /// 提交答案
        /// </summary>
        public static string Submit = "Submit";

        /// <summary>
        /// 使用道具
        /// </summary>
        public static string UseProp = "UseProp";

        /// <summary>
        /// 答题超时
        /// </summary>
        public static string AnswerQuestionTimeout = "AnswerQuestionTimeout";

        /// <summary>
        /// 发送表情
        /// </summary>
        public static string SendEmoticon = "SendEmoticon";

        /// <summary>
        /// 再玩一次
        /// </summary>
        public static string PlayerGameAgain = "PlayerGameAgain";

        /// <summary>
        /// 同意
        /// </summary>
        public static string OtherAgree = "OtherAgree";

        /// <summary>
        /// 拒绝
        /// </summary>
        public static string OtherRefuse = "OtherRefuse";

        /// <summary>
        /// 移出组
        /// </summary>
        public static string RemoveFromGroup = "RemoveFromGroup";

        /// <summary>
        /// 获取游戏状态
        /// </summary>
        public static string GetGameState = "GetGameState";

        /// <summary>
        /// 获取对方的状态信息
        /// </summary>
        public static string GetOtherGameState = "GetOtherGameState";

        /// <summary>
        /// 开始抽奖
        /// </summary>
        public static string StartPrizeDraw = "StartPrizeDraw";

        /// <summary>
        /// 设置约定
        /// </summary>
        public static string SetAppointment = "SetAppointment";

        /// <summary>
        /// 抽奖结果
        /// </summary>
        public static   string PrizeDrawResult = "PrizeDrawResult";

        /// <summary>
        /// 播放文字音效
        /// </summary>
        public static string SendVoice = "SendVoice";
        /// <summary>
        /// 呼叫接待员
        /// </summary>
        public static string CallWaiter = "CallWaiter";

        /// <summary>
        /// 继续答题
        /// </summary>
        public static string ContinueAnswer = "ContinueAnswer";

        /// <summary>
        /// 恢复体力（分享群）
        /// </summary>
        public static string RecoverStrengthOutput = "RecoverStrengthOutput";
        /// <summary>
        /// 乏力，缺乏体力
        /// </summary>
        public static string Feeble = "Feeble";

    }
}
