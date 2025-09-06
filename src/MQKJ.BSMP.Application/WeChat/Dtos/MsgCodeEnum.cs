using MQKJ.BSMP.EnumHelper;

namespace MQKJ.BSMP.WeChat.Dtos
{
    /// <summary>
    /// 消息码
    /// </summary>
    public enum MsgCodeEnum : byte
    {
        [EnumDescription("默认")]
        Default = 0x000,

        [EnumDescription("邀请")]
        InviteFriend = 0x001,

        [EnumDescription("被邀请方建立通信")]
        CreateCommunication = 0x002,

        [EnumDescription("被邀请方点击准备")]
        ClickReady = 0x003,

        [EnumDescription("邀请方点击开始")]
        ClickStart = 0x004,

        [EnumDescription("分发题目")]
        DistributeQuestion = 0x005,

        [EnumDescription("提交答案")]
        CommitAnswer = 0x006,

        [EnumDescription("使用延时卡")]
        UserDelayCard = 0x007,

        [EnumDescription("使用复活卡")]
        UserReviveCard = 0x008,

        [EnumDescription("积分不足")]
        IntegralNotEnough = 0x009,

        [EnumDescription("答题超时")]
        AnswerTimeout = 0x010,

        [EnumDescription("发送表情")]
        SendEmoticon = 0x011,

        [EnumDescription("对方连接成功")]
        OtherConnectSuccessFul = 0x012,

        [EnumDescription("再玩一次")]
        PlayerAgain = 0x013,

        [EnumDescription("对方同意")]
        OtherAgree = 0x014,

        [EnumDescription("对方拒绝")]
        OtherRefuse = 0x015,

        [EnumDescription("对方离线")]
        OtherOffLine = 0x016,

        [EnumDescription("房间已满")]
        RoomFull = 0x017,

        [EnumDescription("对方离开房间")]
        OhterLeaveRoom = 0x018,

        [EnumDescription("邀请方进入房间")]
        JoinSelfRoom = 0x019,

        [EnumDescription("被邀请方进入房间")]
        InviteeJoinRoom = 0x020,

        [EnumDescription("异常")]
        OccurErr = 0x021,

        [EnumDescription("游戏结束")]
        GameOver = 0x022,

        [EnumDescription("发送语音")]
        SendVoice = 0x023,

        [EnumDescription("开始抽奖")]
        StartPrize = 0x024,

        [EnumDescription("抽奖结果")]
        PrizeResult = 0x025,

        [EnumDescription("呼叫接待员")]
        CallReceptionist = 0x026,

        [EnumDescription("继续答题")]
        ContinueAnswer = 0x027,
        [EnumDescription("恢复体力（分享群）")]
        RecoverStrengthOutput=0x028,
        [EnumDescription("体力不够6个，乏力")]
        Feeble = 0x029,

        [EnumDescription("自己连接成功")]
        ConnectSuccessFul = 0x030,
    }
}
