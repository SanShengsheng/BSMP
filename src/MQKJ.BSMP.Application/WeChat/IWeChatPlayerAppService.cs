using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.CustomResponses;
using MQKJ.BSMP.Players.WeChat.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQKJ.BSMP.WeChat.Dtos;
using MQKJ.BSMP.WeChat;
using MQKJ.BSMP.WeChat.Dtos.Endless;
using MQKJ.BSMP.Appointments;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Utils.WechatPay.Dtos;

namespace MQKJ.BSMP.Players.WeChat
{
    public interface IWeChatPlayerAppService : IApplicationService
    {


        /// <summary>
        /// 初始化 获取玩家状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<GetPlayerInfoOutput> GetPlayerInfo(GetPlayerInfoInput input);

        /// <summary>
        /// 存储、更新玩家信息 微信授权、玩家选择 性别 年龄层次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> CreateOrUpdatePlayerInfo(CreateOrUpdatePlayerInfoInput input);

        /// <summary>
        /// 邀请好友
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<InviteFriendOutput> CreateInvite(InviteFriendInput input);

        /// <summary>
        /// 获取房间的状态 是否满
        /// </summary>
        /// <returns></returns>
        Task<GetGameRoomStateOutput> GetGameTaskState(GetGameRoomStateInput input);

        /// <summary>
        /// 游戏初始化 被邀请人点击链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GameInitializationOutput> InitializeGame(InitializeGameInput input);

        /// <summary>
        /// 无尽模式 初始化游戏
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GameInitializationOutput> InitializeGameEndLess(InitializeGameInput input);

        /// <summary>
        /// 准备  被邀请人进入游戏 点击了准备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<ReadyGameOutput> ReadyGame(ReadyGameInput input);
        Task<string> ReadyGame(ReadyGameInput input);

        /// <summary>
        /// 开始抽奖
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task StartPrizeDraw(StartPrizeDrawInput input);

        /// <summary>
        /// 抽约定的结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PrizeDrawResult(PrizeDrawResultInput input);

        /// <summary>
        /// 游戏开始  邀请方点击开始游戏
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StartGameOutput> DistributeQuestionsEndless(StartGameInput input);

        /// <summary>
        /// 分发题目 flag模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StartGameOutput> DistributeQuestions(StartGameInput input);

        /// <summary>
        /// 进入游戏 flag模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EnterGameOutput> EnterGame(EnterGameInput input);

        /// <summary>
        /// 玩家离开 离线
        /// </summary>
        /// <returns></returns>
        //Task<PlayerLeaveOutput> PlayerLeave(EntityDto<Guid> input);

        /// <summary>
        ///通过code保存 用户openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPlayerInfoOutput> GetPlayerCode(GetPlayerInfoInput input);

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateWechatUserInfo(GetWechatUserInfoInput input);

        Task ReConnection(CreateConnectionDto input);

        /// <summary>
        /// 提交答案 flag模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SubmitAnswerOutputDto> Submit(SubmitAnswerInputDto input);


        /// <summary>
        /// 提交答案 无尽模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SubmitAnswerOutputDto Submit_EndLess(SubmitAnswerInputDto input);

        Task<SubmitAnswerOutputDto> Submit_EndLess_1(SubmitAnswerInputDto input);

        /// <summary>
        /// 主客场没有答题
        /// </summary>
        /// <returns></returns>
        Task<AnswerTimeoutOutPut> AnswerTimeout(AnswerTimeoutDto input);

        /// <summary>
        /// 使用道具
        /// </summary>
        /// <returns></returns>
        Task<UsePropOutput> UseProp(UsePropDto input);

        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SendEmoticonOutput> SendEmoticon(SendEmoticonDto input);

        /// <summary>
        /// 分享好友
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ShareFriends(EntityDto<Guid> input);


        /// <summary>
        /// 使用复活卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task UseResurrectionCard(UseResurrectionCardDto input);

        /// <summary>
        /// 获取积分与选项占比
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetBonusPointsCountOutput> GetBonusPointsCountAndAnswerProportion(GetBonusPointsCountInput input);


        /// <summary>
        /// 再玩一次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PlayGameAgainOutput> PlayGameAgain(PlayGameAgainDto input);

        /// <summary>
        /// 对方同意
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OtherAgreeOutput> OtherAgree(OtherAgreeOrRefuseDto input);

        /// <summary>
        /// 对方拒绝
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OtherAgreeOrRefuseOutput> OtherRefuse(OtherAgreeOrRefuseDto input);

        /// <summary>
        /// 房间是否存在
        /// </summary>
        /// <returns></returns>
        Task<Guid?> GameIsExist(EntityDto<Guid> input);

        /// <summary>
        /// 获取房间的信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetGameInfoOutput> GetGameInfo(GetGameInfoInput input);

        Task<GetGameProgressOutput> GetGameProgress(GetGameProgressInput input);

        /// <summary>
        /// 获取海报信息
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> GetPosterInfo(GetPosterInfoInput input);

        /// <summary>
        /// 获取海报信息第二版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> GetLatestPosterInfo(GetPosterInfoInput input);
        /// <summary>
        /// 减去体力
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<MinusStaminaOutput> MinusStamina(Guid playerId);
        /// <summary>
        /// 检查体力是否够6个
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        bool CheckStamina(Guid playerId);

        /// <summary>
        /// 返回约定内容
        /// </summary>
        /// <returns></returns>
        List<AppointmentContent> GetAppointments(GetAppointmentDto input);

        Task UpdateGameState(EntityDto<Guid> input);

        //BarrierPassRateStatistics

        /// <summary>
        /// 验证公众号用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<GetBabiesOutput> VaildPubPlayer(VaildPubPlayerInput input);
        Task<VaildPubPlayerOutput> V2_VaildPubPlayer(VaildPubPlayerInput input);
        Task<GetBabiesOutput> VaildPubPlayer(VaildPubPlayerInput input);

        GetAccessTokenWithCodeOutput GetAccessTokenWithCode(GetAccessTokenWithCodeDto input);

        /// <summary>
        /// 登录签到页面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginSignUpSystemOutput> LoginSignUpSystem(LoginSignUpSystemInput input);

        /// <summary>
        /// 活动注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginSignUpSystemOutput> RegisterSignUpSystem(RegisterSignUpSystemInput input);

    }
}
