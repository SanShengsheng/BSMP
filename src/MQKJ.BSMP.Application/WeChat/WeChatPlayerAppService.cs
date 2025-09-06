using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Json;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.AnswerQuestions;
using MQKJ.BSMP.AnswerQuestions.Dtos;
using MQKJ.BSMP.Answers;
using MQKJ.BSMP.Appointments;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.BigRisks.WeChat;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.WechatPay;
using MQKJ.BSMP.Emoticons;
using MQKJ.BSMP.Events.Datas;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.Friends.Dtos;
using MQKJ.BSMP.GameRecords;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Players.Dtos;
using MQKJ.BSMP.Players.WeChat.Dtos;
using MQKJ.BSMP.PropUseRecords;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Questions.Dtos;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.StaminaRecords.Dtos;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;
using MQKJ.BSMP.Utils.Crypt;
using MQKJ.BSMP.Utils.Message;
using MQKJ.BSMP.Utils.Message.Dtos;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChat;
using MQKJ.BSMP.WeChat.Dtos;
using MQKJ.BSMP.WeChat.Dtos.Endless;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Players.WeChat
{
    //[AbpAuthorize]
    public class WeChatPlayerAppService : BSMPAppServiceBase, IWeChatPlayerAppService
    {
        private const string wechaBasetUrl = "https://api.weixin.qq.com/sns/jscode2session";
        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<PlayerExtension> _playerExtensionRepository;

        private readonly IRepository<GameTask, Guid> _gameTaskRepository;

        private readonly IRepository<GameRecord, Guid> _gameRecordRepository;

        private readonly IRepository<Question> _questiondRepository;

        private readonly IRepository<AnswerQuestion, Guid> _answerQuestionRepository;

        private readonly IRepository<Answer> _answerRepository;

        private readonly IRepository<PropUseRecord, Guid> _propUseRecord;

        private readonly IRepository<EmoticonRecord, Guid> _emoticonRecordRepository;

        private readonly IRepository<BonusPoint> _bonuspointRepository;

        private readonly IRepository<BonusPointRecord, Guid> _bonuspointRecordRepository;

        private readonly IRepository<Friend, Guid> _friendRepository;

        private readonly IRepository<Tag, int> _tagRepository;
        private readonly IRepository<TagType, int> _tagtypeRepository;

        private readonly IRepository<MqAgent> _mqAgentRepository;

        public IEventBus EventBus { get; set; }
        private readonly IAnswerQuestionAppService _answerQuestionAppService;
        private readonly IQuestionAppService _questionAppService;
        private readonly IMiniappService _miniappService;
        private readonly IPlayerAppService _playerAppService;
        private readonly IStaminaRecordAppService _staminaRecordAppService;
        private readonly IAnswerAppService _answerAppService;
        private readonly IGameTaskAppService _gameTaskAppService;
        private readonly IRepository<SceneFile, Guid> _sceneFileRepository;
        private WechatpublicPlatformConfig _wechatPubOption;
        private WechatPayConfig _wechatPayConfigOption;
        static object OAuthCodeCollectionLock = new object();
        private WechatWebConfig _wechatWebConfig;
        //private MinProgram_RelationEvolution _wechatConfig;
        //private MinProgram_Party _partyConfig;
        //private MinProgram_Adventure _adventureConfig;
        private readonly TagAppService _tagAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<WechatMpUser, Guid> _mpuserRepository;
        private const string FileRootPath = "Appointment";
        private string AppointmentFilePath = "Appointment.json";

        private static string wechatSessionkey = "wechatSessionkey";
        private static ConcurrentDictionary<string, List<int>> _submitAnswerDic { get; }
        //private readonly IDistributedCache _redisCache;

        private IBabyAppService _babyAppService;

        private readonly UserManager _userManager;

        private readonly IRepository<WeChatWebUser, Guid> _wechatWebUserRepository;
        private readonly ICacheManager _cacheManager;
        private static readonly object syncObj = new object();
        public WeChatPlayerAppService(
            IRepository<Player, Guid> playerRepository,
            IRepository<PlayerExtension> playerExtensionRepository,
            IRepository<GameTask, Guid> gameTaskRepository,
            IRepository<GameRecord, Guid> gameRecordRepository,
            IRepository<Question> questiondRepository,
            IOptions<WechatPayConfig> wechatPayConfigOption,
            IOptions<WechatpublicPlatformConfig> wechatPubOption,
            IOptions<WechatWebConfig> wechatWebOption,
            IRepository<AnswerQuestion, Guid> answerQuestionRepository,
            IRepository<Answer> answerRepository,
            IRepository<PropUseRecord, Guid> propUseRecordRepository,
            IRepository<EmoticonRecord, Guid> emoticonRecordRepository,
            IRepository<BonusPointRecord, Guid> bonuspointRecordRepository,
            IRepository<BonusPoint> bonuspointRepository,
            TagAppService tagAppService,
            StaminaRecordAppService staminaRecordAppService,
            //IOptions<MinProgram_RelationEvolution> option,
            //IOptions<MinProgram_Party> partyConfig,
            IHostingEnvironment hostingEnvironment,
            IRepository<SceneFile, Guid> sceneFileRepository,
            IRepository<Friend, Guid> friendRepository,
            IRepository<Tag, int> tagRepository,
            IRepository<WechatMpUser, Guid> mpuserRepository,
        IAnswerQuestionAppService answerQuestionAppService, IQuestionAppService questionAppService, IMiniappService miniappService, IPlayerAppService playerAppService,
            //BSMPDbContext dbContext
            IRepository<TagType, int> tagTypeRepository,
            IBabyAppService babyAppService,
            IAnswerAppService answerAppService,
            IGameTaskAppService gameTaskAppService,
            //IDistributedCache redisCache,
            UserManager userManager,
            IRepository<WeChatWebUser, Guid> wechatWebUserRepository,
            IRepository<MqAgent> mqagentRepository,
            ICacheManager cacheManager
            )
        {
            _playerRepository = playerRepository;

            _playerExtensionRepository = playerExtensionRepository;

            _gameTaskRepository = gameTaskRepository;

            _gameRecordRepository = gameRecordRepository;

            _questiondRepository = questiondRepository;

            _answerQuestionRepository = answerQuestionRepository;

            _answerRepository = answerRepository;

            _propUseRecord = propUseRecordRepository;

            _emoticonRecordRepository = emoticonRecordRepository;

            _bonuspointRecordRepository = bonuspointRecordRepository;

            _bonuspointRepository = bonuspointRepository;

            _friendRepository = friendRepository;

            _tagRepository = tagRepository;
            _answerQuestionAppService = answerQuestionAppService;
            _questionAppService = questionAppService;
            _miniappService = miniappService;
            _playerAppService = playerAppService;
            _staminaRecordAppService = staminaRecordAppService;
            _tagAppService = tagAppService;
            _sceneFileRepository = sceneFileRepository;
            EventBus = NullEventBus.Instance;
            _hostingEnvironment = hostingEnvironment;
            _tagtypeRepository = tagTypeRepository;
            _answerAppService = answerAppService;
            _gameTaskAppService = gameTaskAppService;


            _wechatPayConfigOption = wechatPayConfigOption.Value;
            _wechatPubOption = wechatPubOption.Value;
            _wechatWebConfig = wechatWebOption.Value;

            _babyAppService = babyAppService;
            _mpuserRepository = mpuserRepository;
            //_redisCache = redisCache;
            _userManager = userManager;
            _wechatWebUserRepository = wechatWebUserRepository;

            _mqAgentRepository = mqagentRepository;
            _cacheManager = cacheManager;
        }
        static WeChatPlayerAppService()
        {
            _submitAnswerDic = new ConcurrentDictionary<string, List<int>>();
        }

        public async Task<GetPlayerInfoOutput> GetPlayerCode(GetPlayerInfoInput input)
        {
            //Logger.Debug("执行GetPlayerCode");
            var response = new GetPlayerInfoOutput();
            try
            {
                var tenant = new Tenant();

                tenant = await GetCurrentTenantAsync();

                //if (tenant == null)
                //{
                //    tenant = new Tenant() { Id = 4, WechatAppId = "wxdb7e28315b6f3304", WechatAppSecret = "76fa54075b28bb9d62adfd3733fbaf19" };
                //}

                var getOpenIdResponse = _miniappService.GetOpenId(new GetOpenIdInput
                {
                    AppId = tenant.WechatAppId,
                    AppSecret = tenant.WechatAppSecret,
                    Code = input.Code
                });
                _cacheManager.GetCache(wechatSessionkey).AsTyped<string, string>().Set(input.Code, getOpenIdResponse.SessionKey, TimeSpan.FromMinutes(30));

                var output = new GetPlayerInfoOutput();
                Logger.Debug($"蓝屏 | GetPlayerCode | 微信返回用户信息：{getOpenIdResponse.ToJsonString()} | 请求参数：{input.ToJsonString()}");
                var player = await _playerAppService.PlayerLogin(new GetPlayerInput()
                {
                    OpenId = getOpenIdResponse.OpenId,
                    UnionId = getOpenIdResponse.Unionid,
                    TenantId = tenant.Id,
                    HeaderUrl = input.HeadUrl,
                    NickName = input.NickName,
                    DeviceModel = input.DeviceModel,
                    DeviceSystem = input.DeviceSystem,
                    InviterId = input.InviterId
                });
                output.IsAddMask = player.IsAddMask;

                output.Stamina = player.PlayerExtension.Stamina;
                output.BonusPointsCount = player.PlayerExtension.LoveScore;
                output.PlayerId = output.PlayerId.ToString() == "00000000-0000-0000-0000-000000000000" ? Guid.Parse("00000000-0000-0000-0000-000000000001") : output.PlayerId;
                output.IsHasIdentity = !string.IsNullOrWhiteSpace(player.UnionId);
                response = player.MapTo(output);
            }
            catch (Exception ex)
            {
                Logger.Error("GetPlayerCode出错啦！！！！！", ex);
            }


            return response;

        }

        public async Task UpdateWechatUserInfo(GetWechatUserInfoInput input)
        {

            var sessionkey = string.Empty;
            if (input.SessionKeyIsValidate)
            {
                sessionkey = _cacheManager.GetCache(wechatSessionkey).AsTyped<string, string>().Get(input.Code, () => null);
            }
            else
            {
                var tenant = new Tenant();
                tenant = await GetCurrentTenantAsync();
                var getOpenIdResponse = _miniappService.GetOpenId(new GetOpenIdInput
                {
                    AppId = tenant.WechatAppId,
                    AppSecret = tenant.WechatAppSecret,
                    Code = input.Code
                });
                sessionkey = getOpenIdResponse.SessionKey;
            }

            try
            {
                var result = TripleDESCrypt.AES_Decrypt(input.EncryptedData, sessionkey, input.Iv);

                var player = await _playerRepository.FirstOrDefaultAsync(p => p.OpenId == result.OpenId);

                if (player == null)
                {
                    throw new Exception("数据错误");
                }
                player.UnionId = result.UnionId;
                player.NickName = result.NickName;
                player.HeadUrl = result.AvatarUrl;

                await _playerRepository.UpdateAsync(player);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 发起邀请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<InviteFriendOutput> CreateInvite(InviteFriendInput input)
        {

            Logger.Debug($"邀请方playerid：{input.InviterPlayerId}");

            var gameTaskEntity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.Id);

            if (gameTaskEntity == null)//可以创建新任务了
            {
                gameTaskEntity = input.MapTo<GameTask>();

                gameTaskEntity = await _gameTaskRepository.InsertAsync(gameTaskEntity);
            }
            else
            {
                if (gameTaskEntity.InviteePlayerId == null)
                {
                    input.Id = gameTaskEntity.Id;

                    input.MapTo(gameTaskEntity);
                }

                await _gameTaskRepository.UpdateAsync(gameTaskEntity);
            }


            //存储记录
            var gameRecord = new GameRecord()
            {
                Id = Guid.NewGuid(),
                State = TaskState.TaskInitialization,
                GameTaskId = gameTaskEntity.Id
            };
            await _gameRecordRepository.InsertAsync(gameRecord);

            var InvalidTime = gameTaskEntity.CreationTime.AddMinutes(gameTaskEntity.ValidInterval).ToString("yyyy-MM-dd HH:mm:ss");
            var output = new InviteFriendOutput()
            {
                InviteUrl = "?GameId=" + gameTaskEntity.Id + "&InvalidTime=" + InvalidTime,
                ImageUrl = "",
                InviteMsg = gameTaskEntity.AppointmentContent,
            };
            return output;
        }

        /// <summary>
        /// 获取房间的状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetGameRoomStateOutput> GetGameTaskState(GetGameRoomStateInput input)
        {
            var output = new GetGameRoomStateOutput();

            var entity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameId);

            if (entity == null)//正常连接
            {
                output.MsgCodeEnum = MsgCodeEnum.ConnectSuccessFul;
            }
            else//邀请方或者被邀请方进来的(重连)
            {
                if (input.PlayerId == entity.InviterPlayerId || input.PlayerId == entity.InviteePlayerId)
                {
                    output.MsgCodeEnum = MsgCodeEnum.ConnectSuccessFul;

                    output.Result = entity;

                    if (input.PlayerId == entity.InviterPlayerId)
                        output.OtherPlayerId = entity.InviteePlayerId.Value;
                    else
                        output.OtherPlayerId = entity.InviterPlayerId;

                }
                else //不是房间里的人进来的
                {
                    return null;
                }
            }

            return output;
        }


        /// <summary>
        /// 进入游戏初始化(被邀请方点击链接)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GameInitializationOutput> InitializeGame(InitializeGameInput input)
        {
            var output = new GameInitializationOutput();

            var gameTaskEntity = _gameTaskRepository.FirstOrDefault(g => g.Id == input.Id);

            if (gameTaskEntity == null) //房间未创建
            {
                if (input.InviteePlayerId == input.InviterPlayerId)//邀请方没创建房间的时候自己点链接进来的
                {
                    output.MsgCodeEnum = MsgCodeEnum.JoinSelfRoom;

                    output.GameId = input.Id;
                }
                else //被邀请方进来的
                {
                    input.State = TaskState.TaskInitialization;

                    gameTaskEntity = input.MapTo<GameTask>();

                    gameTaskEntity = await _gameTaskRepository.InsertAsync(gameTaskEntity);

                    output = await GetPlayerInfo(gameTaskEntity, input, output);

                    await _gameRecordRepository.InsertAsync(new GameRecord()
                    {
                        GameTaskId = input.Id,
                        State = TaskState.TaskInitialization
                    });
                }
            }
            else //房间创建好的时候
            {
                if (input.InviteePlayerId == gameTaskEntity.InviterPlayerId)//邀请方点链接进来的
                {
                    input.InviteePlayerId = gameTaskEntity.InviteePlayerId.Value;

                    output = await GetPlayerInfo(gameTaskEntity, input, output);
                }
                else if (input.InviteePlayerId == gameTaskEntity.InviteePlayerId)//被邀请方点链接进来的
                {
                    output = await GetPlayerInfo(gameTaskEntity, input, output);
                }
                else if (input.InviteePlayerId != gameTaskEntity.InviteePlayerId && input.InviteePlayerId != gameTaskEntity.InviterPlayerId)//进来的人不是邀请方也不是被邀请方
                {
                    output = await GetPlayerInfo(gameTaskEntity, input, output);

                    output.MsgCodeEnum = MsgCodeEnum.RoomFull;
                }
                else if (gameTaskEntity.InviterPlayerId == gameTaskEntity.InviteePlayerId)
                {
                    output.MsgCodeEnum = MsgCodeEnum.Default;
                }
                else if (gameTaskEntity.State == TaskState.TaskFailure && gameTaskEntity.State == TaskState.TaskSuccess)//进来的时候 游戏结束了
                {
                    output.MsgCodeEnum = MsgCodeEnum.GameOver;
                }
                else
                {
                    output = await GetPlayerInfo(gameTaskEntity, input, output);
                }
            }
            return output;
        }

        /// <summary>
        /// 进入游戏初始化(被邀请方点击链接)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GameInitializationOutput> InitializeGameEndLess(InitializeGameInput input)
        {
            var output = new GameInitializationOutput();

            var gameTaskEntity = _gameTaskRepository.FirstOrDefault(g => g.Id == input.Id);

            if (gameTaskEntity == null) //房间未创建
            {
                if (input.InviteePlayerId == input.InviterPlayerId)//邀请方没创建房间的时候自己点链接进来的
                {
                    output.MsgCodeEnum = MsgCodeEnum.JoinSelfRoom;

                    output.GameId = input.Id;
                }
                else //被邀请方进来的
                {
                    input.State = TaskState.TaskInitialization;

                    gameTaskEntity = input.MapTo<GameTask>();

                    gameTaskEntity = await _gameTaskRepository.InsertAsync(gameTaskEntity);

                    output = await GetPlayerInfo(gameTaskEntity, input, output);

                    await _gameRecordRepository.InsertAsync(new GameRecord()
                    {
                        GameTaskId = input.Id,
                        State = TaskState.TaskInitialization
                    });
                }
            }
            else //房间创建好的时候
            {
                if (input.InviteePlayerId == gameTaskEntity.InviterPlayerId)//邀请方点链接进来的
                {
                    input.InviteePlayerId = gameTaskEntity.InviteePlayerId.Value;

                    output = await GetPlayerInfo(gameTaskEntity, input, output);
                }
                else if (input.InviteePlayerId == gameTaskEntity.InviteePlayerId)//被邀请方点链接进来的
                {
                    output = await GetPlayerInfo(gameTaskEntity, input, output);
                }
                else if (input.InviteePlayerId != gameTaskEntity.InviteePlayerId && input.InviteePlayerId != gameTaskEntity.InviterPlayerId)//进来的人不是邀请方也不是被邀请方
                {
                    output = await GetPlayerInfo(gameTaskEntity, input, output);

                    output.MsgCodeEnum = MsgCodeEnum.RoomFull;
                }
                else if (gameTaskEntity.InviterPlayerId == gameTaskEntity.InviteePlayerId)
                {
                    output.MsgCodeEnum = MsgCodeEnum.Default;
                }
                else if (gameTaskEntity.State == TaskState.TaskFailure && gameTaskEntity.State == TaskState.TaskSuccess)//进来的时候 游戏结束了
                {
                    output.MsgCodeEnum = MsgCodeEnum.GameOver;
                }
                else
                {
                    output = await GetPlayerInfo(gameTaskEntity, input, output);
                }
            }
            return output;
        }

        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="gameTaskEntity"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private async Task<GameInitializationOutput> GetPlayerInfo(GameTask gameTaskEntity, InitializeGameInput input, GameInitializationOutput output)
        {
            var inviterEntity = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.InviterPlayerId);

            var inviteeEntity = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.InviteePlayerId);

            if (gameTaskEntity.GameType == GameType.EndLess)
            {
                var friend = await _friendRepository.FirstOrDefaultAsync(s => s.PlayerId == inviterEntity.Id && s.FriendId == inviteeEntity.Id);
                output.Chapter = friend == null ? 1 : friend.Floor;
            }
            output.MsgCodeEnum = MsgCodeEnum.CreateCommunication;

            output.Relation = (int)gameTaskEntity.RelationDegree;

            output.GameBout = gameTaskEntity.TaskType;

            output.InviteeNickName = inviteeEntity.NickName;

            output.AppointmentContent = gameTaskEntity.AppointmentContent;

            output.InviteeAppointmentContent = gameTaskEntity.InviteeAppointmentContent;

            output.InviterHeadUrl = inviterEntity.HeadUrl;

            output.InviteeHeadUrl = inviteeEntity.HeadUrl;

            output.State = (int)gameTaskEntity.State;

            output.InviterPlayerId = gameTaskEntity.InviterPlayerId;

            output.InviteeGender = inviteeEntity.Gender;

            output.InviteePlayerId = gameTaskEntity.InviteePlayerId.Value;
            //if (input.InviterPlayerId == input.InviteePlayerId)//自己点链接进来的
            //{
            //    output.InviterPlayerId = gameTaskEntity.InviterPlayerId;

            //    output.InviteePlayerId = gameTaskEntity.InviterPlayerId;
            //}
            //else
            //{
            //    output.InviterPlayerId = gameTaskEntity.InviterPlayerId;

            //    output.InviteePlayerId = gameTaskEntity.InviteePlayerId.Value;
            //}

            output.Appointments = GetAppointments(new GetAppointmentDto()
            {
                AppointmentType = (AppointmentType)input.AppointmentType,
                GameTaskType = input.TaskType
            });

            output.GameId = gameTaskEntity.Id;

            return output;
        }

        /// <summary>
        /// 准备游戏(被邀请人点击准备)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> ReadyGame(ReadyGameInput input)
        {
            var gameTask = await _gameTaskRepository.GetAllIncluding(x => x.Invitee).FirstOrDefaultAsync(x => x.Id == input.Id);

            gameTask.State = TaskState.TaskReady;

            await _gameTaskRepository.UpdateAsync(gameTask);

            //更新游戏记录表
            var gameRecord = new GameRecord()
            {
                State = TaskState.TaskReady,
                GameTaskId = input.Id
            };
            await _gameRecordRepository.InsertAsync(gameRecord);

            return gameTask.Invitee.HeadUrl;

            //return new ReadyGameOutput() { ConnectionId = gameTask.ConnectionId, BeConnectionId = gameTask.BeConnectionId };
        }

        /// <summary>
        /// 存储玩家信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> CreateOrUpdatePlayerInfo(CreateOrUpdatePlayerInfoInput input)
        {
            if (input.PlayerInfoDto.Id.HasValue)
            {
                return await UpdatePlayerInfoAsync(input.PlayerInfoDto);
            }
            else
            {
                return await CreatePlayerInfoAsync(input.PlayerInfoDto);
            }
        }

        protected async Task<int> UpdatePlayerInfoAsync(PlayerInfoEditDto input)
        {
            var entity = await _playerRepository.GetAsync(input.Id.Value);

            if (entity.NickName == input.NickName || entity.HeadUrl == input.HeadUrl || entity.ModifyCount == 0)
            {
                entity.ModifyCount += 1;
            }

            if (entity.AuthorizeDateTime == null)
            {
                input.AuthorizeDateTime = DateTime.Now;
            }
            var playerExtension = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == entity.Id);

            //playerExtension.Stamina = await _playerAppService.CountDownRecoveryStamina(entity.Id, playerExtension.Stamina);

            await _playerExtensionRepository.UpdateAsync(playerExtension);

            await _playerRepository.UpdateAsync(input.MapTo(entity));

            return entity.ModifyCount;
        }

        protected async Task<int> CreatePlayerInfoAsync(PlayerInfoEditDto input)
        {
            input.State = (int)PlayerState.Authorized;

            var entity = input.MapTo<Player>();

            entity.AuthorizeDateTime = DateTime.Now;

            await _playerRepository.InsertAsync(entity);

            return entity.ModifyCount;
        }

        /// <summary>
        ///游戏开始   闯关爬梯模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StartGameOutput> DistributeQuestionsEndless(StartGameInput input)
        {
            var gameTask = await _gameTaskRepository.GetAllIncluding(g => g.Inviter, g => g.Invitee)
                .FirstOrDefaultAsync(x => x.Id == input.GameId && x.InviterPlayerId == input.PlayerId);

            var output = new StartGameOutput();

            if (gameTask != null && gameTask.InviteePlayerId.HasValue)
            {
                gameTask.State = TaskState.TaskDistributeQuestion;

                await _gameTaskRepository.UpdateAsync(gameTask);
                //改成事件的方式更新积分
                //使用EventBus触发时间
                await EventBus.TriggerAsync<StartGameComplatedEventData>(new StartGameComplatedEventData
                {
                    InviteePlayerId = gameTask.InviteePlayerId.Value,
                    InviterPlayerId = gameTask.InviterPlayerId
                });

                var relationDegreeDic = _tagAppService.GetRelationDegreeTagDic();
                //分发功能没出来之前，暂使用随机出题
                //调整分发逻辑
                //var homeField = GameTaskEntity.SeekType == QuestionGender.F ? QuestionGender.M : QuestionGender.F;
                var chat = _friendRepository.FirstOrDefault(f => f.PlayerId == input.PlayerId && f.FriendId == gameTask.Invitee.Id);
                var relationDegreeKey = 1;
                var floor = 9;
                if (chat != null)
                {
                    if (chat.Floor < 10)
                    {
                        relationDegreeKey = 1;
                        floor = 9;
                    }
                    else if (chat.Floor < 35)
                    {
                        relationDegreeKey = 2;
                        floor = 25;
                    }
                    else if (chat.Floor < 60)
                    {
                        relationDegreeKey = 3;
                        floor = 25;
                    }
                    else if (chat.Floor < 82)
                    {
                        floor = 22;
                        relationDegreeKey = 4;
                    }
                }
                var homeField = gameTask.Invitee.Gender == 1 ? QuestionGender.M : QuestionGender.F;
                var userRecords = (await _answerQuestionAppService.FindUserAnswerQuestions(
                    new FindUserAnswerQuestionsRequest(gameTask.InviterPlayerId, gameTask.InviteePlayerId.Value))).Select(r => r.QuesionId);
                var questionRequest = new GetQuestionsRequest
                {
                    AnswerCount = 6,
                    QuestionGender = homeField,
                    QuestionState = QuestionState.Online,
                    ExcludeIds = userRecords.ToArray(),
                    Top = floor
                };
                if (relationDegreeDic.Count > 0 && relationDegreeDic.ContainsKey(relationDegreeKey))
                {
                    questionRequest.TagId = relationDegreeDic[relationDegreeKey];
                }
                var questions = await _questionAppService.GetQuestions(questionRequest);
                //  题目不够，增加随机题目来凑，需要标明那些是凑得
                if (questions.Count() < floor)
                {
                    questionRequest.Top -= questions.Count();
                    questionRequest.ExcludeIds = questions.Select(q => q.Id).ToArray();
                    var unionQuestions = await _questionAppService.GetQuestions(questionRequest);
                    if (unionQuestions != null && unionQuestions.Any())
                    {
                        questions = questions.Union(unionQuestions);
                    }
                }
                //正则，计算字数长度
                string pattern = @"<[^>]*>";
                var regex = new Regex(pattern);
                //获取所有场景，如果问题没有设置场景且场景存在真实有效的默认图片则使用该默认图片
                var sceneFiles = await _sceneFileRepository.GetAllIncluding(s => s.File).ToListAsync();
                //获取所有私密度标签id
                var privateDensityTag = _tagtypeRepository.FirstOrDefault(s => s.Code == "Privacy");
                var privateDensityTagId = privateDensityTag != null ? privateDensityTag.Id : -1;
                string questionIds = string.Empty;

                //准备题目，模拟出题
                foreach (var item in questions)
                {
                    //选项
                    //var answersMale = _answerRepository.GetAll().Where(a => a.QuestionType == QuestionGender.M && a.QuestionID == item.Id).Select(s => new WechatQuestionAnswer { AnswerId = s.Id, AnswerText = s.Title, Sort = s.Sort }).ToList();
                    //var answersFemale = _answerRepository.GetAll().Where(a => a.QuestionType == QuestionGender.F && a.QuestionID == item.Id).Select(s => new WechatQuestionAnswer { AnswerId = s.Id, AnswerText = s.Title, Sort = s.Sort }).ToList();
                    //打乱女性选项的顺序
                    Logger.Debug("选项长度为：" + item.Answers.Count);
                    var answersMale = item.Answers.Where(a => a.QuestionType == QuestionGender.M)
                        .Select(s => new WechatQuestionAnswer(s)).ToList();
                    var answers = new List<Answer>();
                    if (answersMale == null || answersMale.Count == 0)
                    {
                        //为了避免answers为null引发小程序错误
                        Logger.Debug($"【自定义-WeChatPlayerAppService-DistributeQuestionsEndless】answersMale选项为空了，gameId为{gameTask.Id}，questionId为{item.Id}");
                        answers = await _answerAppService.GetAnswersByQuestionId(item.Id);
                        answersMale = answers.Where(a => a.QuestionType == QuestionGender.M)
                        .Select(s => new WechatQuestionAnswer(s)).ToList();
                    }
                    var answersFemale = item.Answers.Where(a => a.QuestionType == QuestionGender.F)
                        .Select(s => new WechatQuestionAnswer(s)).ToArray();
                    if (answersFemale == null || answersFemale.Count() == 0)
                    {
                        //为了避免answers为null引发小程序错误
                        Logger.Debug($"【自定义-WeChatPlayerAppService-DistributeQuestionsEndless】answersFemale选项为空了，gameId为{gameTask.Id}，questionId为{item.Id}");
                        var answersFemleOrigin = answers ?? await _answerAppService.GetAnswersByQuestionId(item.Id);
                        answersFemale = answersFemleOrigin.Where(a => a.QuestionType == QuestionGender.F)
                        .Select(s => new WechatQuestionAnswer(s)).ToArray();
                    }
                    var answersFemaleDisrupter = new Item<WechatQuestionAnswer>(answersFemale);
                    var answersFemaleDisrupterArray = answersFemaleDisrupter.GetDisruptedItems().ToList();
                    //背景图片和缩略图
                    //var backgroundImg = item.DefaultImg != null ? item.DefaultImg.FilePath : (sceneFiles.Count(s => item.SceneId == s.SceneId && s.IsDefault) > 0 ? (sceneFiles.FirstOrDefault(s => item.SceneId == s.SceneId && s.IsDefault).File == null ? null : sceneFiles.FirstOrDefault(s => item.SceneId == s.SceneId && s.IsDefault).File.FilePath) : null);
                    //var thumbnailPath = item.DefaultImg != null ? item.DefaultImg.FilePath : (sceneFiles.Count(s => item.SceneId == s.SceneId && s.IsDefault) > 0 ? (sceneFiles.FirstOrDefault(s => item.SceneId == s.SceneId && s.IsDefault).File == null ? null : sceneFiles.FirstOrDefault(s => item.SceneId == s.SceneId && s.IsDefault).File.ThumbnailImagePath) : null);

                    var backgroundImg = String.Empty;
                    var thumbnailPath = String.Empty;


                    if (item.DefaultImg == null)
                    {
                        var defaultImage = FindDefaultImg(item, sceneFiles);
                        backgroundImg = defaultImage?.File?.FilePath;
                        thumbnailPath = defaultImage?.File?.ThumbnailImagePath;
                    }
                    else
                    {
                        backgroundImg = item.DefaultImg.FilePath;
                        thumbnailPath = item.DefaultImg.ThumbnailImagePath;
                    }
                    var femaleStoryLength = regex.Replace(item.BackgroundStoryFemale, "").Length;
                    var maleStoryLength = regex.Replace(item.BackgroundStoryMale, "").Length;
                    var length = Convert.ToInt32(Math.Ceiling((femaleStoryLength > maleStoryLength ? femaleStoryLength : maleStoryLength) / 8.0) + 5);
                    //私密度
                    var questionTag = item.QuestionTags.FirstOrDefault(q => q.Tag.TagTypeId == privateDensityTagId);
                    var questionPrivateDensity = questionTag == null ? "none" : questionTag.Tag.TagName;
                    //问题
                    var questionMale = new QuestionList() { QuestionId = item.Id, Question = item.BackgroundStoryMale, BackImageUrl = backgroundImg, ThumbnailPath = thumbnailPath, Answers = answersMale, WordLength = length, SceneName = item.Scene.SceneName, Author = item.Author, CheckOneName = item.CheckOne == null ? "无" : item.CheckOne.Surname + item.CheckOne.Name, PrivateDensity = questionPrivateDensity };
                    var questionFemale = new QuestionList() { QuestionId = item.Id, Question = item.BackgroundStoryFemale, BackImageUrl = backgroundImg, ThumbnailPath = thumbnailPath, Answers = answersFemaleDisrupterArray, WordLength = length, SceneName = item.Scene.SceneName, PrivateDensity = questionPrivateDensity };
                    if (gameTask.Inviter.Gender == 1)//male
                    {
                        output.InviterQuestions.Add(questionMale);

                        output.InviteeQuestions.Add(questionFemale);
                    }
                    else
                    {
                        output.InviterQuestions.Add(questionFemale);

                        output.InviteeQuestions.Add(questionMale);
                    }

                    questionIds += item.Id + ",";
                }

                //更新游戏记录表
                var gameRecord = new GameRecord()
                {
                    State = TaskState.TaskDistributeQuestion,
                    GameTaskId = input.GameId,
                    QuestionIds = questionIds.Remove(questionIds.Length - 1, 1)
                };

                await _gameRecordRepository.InsertAsync(gameRecord);
            }
            return output;
        }

        /// <summary>
        /// 分发题目 flag模式
        /// </summary>
        /// <returns></returns>
        public async Task<StartGameOutput> DistributeQuestions(StartGameInput input)
        {
            var gameTask = await _gameTaskRepository.GetAllIncluding(g => g.Inviter, g => g.Invitee)
                .FirstOrDefaultAsync(x => x.Id == input.GameId && x.InviterPlayerId == input.PlayerId);

            var output = new StartGameOutput();

            if (gameTask != null && gameTask.InviteePlayerId.HasValue)
            {
                gameTask.State = TaskState.TaskDistributeQuestion;

                await _gameTaskRepository.UpdateAsync(gameTask);

                //改成事件的方式更新积分
                //使用EventBus触发时间
                await EventBus.TriggerAsync<StartGameComplatedEventData>(new StartGameComplatedEventData
                {
                    InviteePlayerId = gameTask.InviteePlayerId.Value,
                    InviterPlayerId = gameTask.InviterPlayerId
                });

                #region 原有方式

                //var inviterExtension = await _playerExtensionRepository.FirstOrDefaultAsync(x => x.PlayerGuid == GameTaskEntity.InviterPlayerId);

                ////output.InviterBonusPointCount = StaticBonusPointsCount.InviteFirendCount;

                //inviterExtension.LoveScore += StaticBonusPointsCount.InviteFirendCount;

                //await _playerExtensionRepository.UpdateAsync(inviterExtension);

                //var inviteeExtension = await _playerExtensionRepository.FirstOrDefaultAsync(x => x.PlayerGuid == GameTaskEntity.InviteePlayerId);

                ////output.InviteeBonusPointCount = StaticBonusPointsCount.InviteeCount;

                //inviteeExtension.LoveScore += StaticBonusPointsCount.InviteeCount;

                //await _playerExtensionRepository.UpdateAsync(inviteeExtension);

                #endregion

                var relationDegreeDic = _tagAppService.GetRelationDegreeTagDic();
                //分发功能没出来之前，暂使用随机出题
                //调整分发逻辑
                //var homeField = GameTaskEntity.SeekType == QuestionGender.F ? QuestionGender.M : QuestionGender.F;
                var homeField = gameTask.Invitee.Gender == 1 ? QuestionGender.M : QuestionGender.F;
                var userRecords = (await _answerQuestionAppService.FindUserAnswerQuestions(
                    new FindUserAnswerQuestionsRequest(gameTask.InviterPlayerId, gameTask.InviteePlayerId.Value))).Select(r => r.QuesionId);
                var questionRequest = new GetQuestionsRequest
                {
                    AnswerCount = 6,
                    QuestionGender = homeField,
                    QuestionState = QuestionState.Online,
                    ExcludeIds = userRecords.ToArray(),
                    Top = (int)gameTask.TaskType
                };

                if (relationDegreeDic.Count > 0 && relationDegreeDic.ContainsKey((int)gameTask.RelationDegree))
                {
                    questionRequest.TagId = relationDegreeDic[(int)gameTask.RelationDegree];
                }

                var questions = await _questionAppService.GetQuestions(questionRequest);
                if (questions.Count() < questionRequest.Top)
                {
                    questionRequest.Top -= questions.Count();
                    questionRequest.ExcludeIds = questions.Select(q => q.Id).ToArray();
                    var unionQuestions = await _questionAppService.GetQuestions(questionRequest);
                    if (unionQuestions != null && unionQuestions.Any())
                    {
                        questions = questions.Union(unionQuestions);
                    }
                }

                #region 之前的方法

                //var query = _questiondRepository
                //    .GetAllIncluding(i => i.Answers, i => i.DefaultImg, i => i.QuestionTags, i => i.Scene,
                //        i => i.CheckOne).Where(q =>
                //        q.Answers.Count >= 6 && q.Pursuer == homeField && q.State == QuestionState.Online);
                //if (relationDegreeDic.Count > 0)
                //{
                //    query = query.Where(q =>
                //        q.QuestionTags.Any(qt => qt.TagId == relationDegreeDic[gameTask.RelationDegree]));
                //}

                //if (userRecords.Any())
                //{
                //    var ids = userRecords.Select(u => u.QuesionId);
                //    query = query.Where(q => !ids.Contains(q.Id));
                //}

                //query = query.OrderBy(x => Guid.NewGuid()).Take((int)gameTask.TaskType);

                //  题目不够，增加随机题目来凑，需要标明那些是凑得
                //if (query.Count < (int)gameTask.TaskType)
                //{
                //    var vacancy = (int)gameTask.TaskType - query.Count;
                //    var questionsId = query.Select(qa => qa.Id);
                //    var supplyQuestions = _questiondRepository.GetAllIncluding(i => i.Answers, i => i.DefaultImg, i => i.QuestionTags, i => i.Scene, i => i.CheckOne).
                //        Where(q => q.Answers.Count >= 6 && q.Pursuer == homeField && q.State == QuestionState.Online)
                //        .Where(q => !questionsId.Contains(q.Id)).OrderBy(x => Guid.NewGuid()).Take(vacancy);
                //    query = query.Union(supplyQuestions).ToList();
                //}
                //if (query.Count < (int)gameTask.TaskType)
                //{
                //    var vacancy = (int)gameTask.TaskType - query.Count;
                //    var questionsId = query.Select(qa => qa.Id);
                //    var supplyQuestions = _questiondRepository.GetAllIncluding(i => i.Answers, i => i.DefaultImg, i => i.QuestionTags, i => i.Scene, i => i.CheckOne).
                //        Where(q => q.Answers.Count >= 6 && q.State == QuestionState.Online)
                //        .Where(q => !questionsId.Contains(q.Id)).OrderBy(x => Guid.NewGuid()).Take(vacancy);
                //    query = query.Union(supplyQuestions).ToList();

                //}

                #endregion

                //正则，计算字数长度
                string pattern = @"<[^>]*>";
                var regex = new Regex(pattern);
                //获取所有场景，如果问题没有设置场景且场景存在真实有效的默认图片则使用该默认图片
                var sceneFiles = await _sceneFileRepository.GetAllIncluding(s => s.File).ToListAsync();
                //获取所有私密度标签id
                var privateDensityTag = _tagtypeRepository.FirstOrDefault(s => s.Code == "Privacy");
                var privateDensityTagId = privateDensityTag != null ? privateDensityTag.Id : -1;
                string questionIds = string.Empty;
                //准备题目，模拟出题
                foreach (var item in questions)
                {
                    //选项
                    //var answersMale = _answerRepository.GetAll().Where(a => a.QuestionType == QuestionGender.M && a.QuestionID == item.Id).Select(s => new WechatQuestionAnswer { AnswerId = s.Id, AnswerText = s.Title, Sort = s.Sort }).ToList();
                    //var answersFemale = _answerRepository.GetAll().Where(a => a.QuestionType == QuestionGender.F && a.QuestionID == item.Id).Select(s => new WechatQuestionAnswer { AnswerId = s.Id, AnswerText = s.Title, Sort = s.Sort }).ToList();
                    var answersMale = item.Answers.Where(a => a.QuestionType == QuestionGender.M)
                      .Select(s => new WechatQuestionAnswer(s)).ToList();
                    var answers = new List<Answer>();
                    if (answersMale == null || answersMale.Count == 0)
                    {
                        //为了避免answers为null引发小程序错误
                        Logger.Debug($"【自定义-WeChatPlayerAppService-DistributeQuestionsEndless】answersMale选项为空了，gameId为{gameTask.Id}，questionId为{item.Id}");
                        answers = await _answerAppService.GetAnswersByQuestionId(item.Id);
                        answersMale = answers.Where(a => a.QuestionType == QuestionGender.M)
                        .Select(s => new WechatQuestionAnswer(s)).ToList();
                    }
                    var answersFemale = item.Answers.Where(a => a.QuestionType == QuestionGender.F)
                        .Select(s => new WechatQuestionAnswer(s)).ToList();
                    if (answersFemale == null || answersFemale.Count == 0)
                    {
                        //为了避免answers为null引发小程序错误
                        Logger.Debug($"【自定义-WeChatPlayerAppService-DistributeQuestionsEndless】answersFemale选项为空了，gameId为{gameTask.Id}，questionId为{item.Id}");
                        var answersFemleOrigin = answers ?? await _answerAppService.GetAnswersByQuestionId(item.Id);
                        answersFemale = answersFemleOrigin.Where(a => a.QuestionType == QuestionGender.F)
                        .Select(s => new WechatQuestionAnswer(s)).ToList();
                    }

                    //背景图片和缩略图
                    //Todo 这里需要做优化，查询速度太慢
                    var backgroundImg = String.Empty;
                    var thumbnailPath = String.Empty;

                    if (item.DefaultImg == null)
                    {
                        var defaultImage = FindDefaultImg(item, sceneFiles);
                        backgroundImg = defaultImage?.File?.FilePath;
                        thumbnailPath = defaultImage?.File?.ThumbnailImagePath;
                    }
                    else
                    {
                        backgroundImg = item.DefaultImg.FilePath;
                        thumbnailPath = item.DefaultImg.ThumbnailImagePath;
                    }

                    //var backgroundImg = item.DefaultImg != null ? 

                    //var thumbnailPath = item.DefaultImg != null ? 
                    //    item.DefaultImg.FilePath : (sceneFiles.Count(s => item.SceneId == s.SceneId && s.IsDefault) > 0 ? 
                    //        (sceneFiles.FirstOrDefault(s => item.SceneId == s.SceneId && s.IsDefault).File == null ? 
                    //            null : sceneFiles.FirstOrDefault(s => item.SceneId == s.SceneId && s.IsDefault).File.ThumbnailImagePath) : null);
                    var femaleStoryLength = regex.Replace(item.BackgroundStoryFemale, "").Length;
                    var maleStoryLength = regex.Replace(item.BackgroundStoryMale, "").Length;
                    var length = Convert.ToInt32(Math.Ceiling((femaleStoryLength > maleStoryLength ? femaleStoryLength : maleStoryLength) / 8.0) + 5);
                    //私密度
                    var questionTag = item.QuestionTags.FirstOrDefault(q => q.Tag.TagTypeId == privateDensityTagId);
                    var questionPrivateDensity = questionTag == null ? "none" : questionTag.Tag.TagName;
                    //问题
                    var questionMale = new QuestionList() { QuestionId = item.Id, Question = item.BackgroundStoryMale, BackImageUrl = backgroundImg, ThumbnailPath = thumbnailPath, Answers = answersMale, WordLength = length, SceneName = item.Scene.SceneName, Author = item.Author, CheckOneName = item.CheckOne == null ? "无" : item.CheckOne.Surname + item.CheckOne.Name, PrivateDensity = questionPrivateDensity };
                    var questionFemale = new QuestionList() { QuestionId = item.Id, Question = item.BackgroundStoryFemale, BackImageUrl = backgroundImg, ThumbnailPath = thumbnailPath, Answers = answersFemale, WordLength = length, SceneName = item.Scene.SceneName, PrivateDensity = questionPrivateDensity };
                    if (gameTask.Inviter.Gender == 1)//male
                    {
                        output.InviterQuestions.Add(questionMale);

                        output.InviteeQuestions.Add(questionFemale);
                    }
                    else
                    {
                        output.InviterQuestions.Add(questionFemale);

                        output.InviteeQuestions.Add(questionMale);
                    }

                    questionIds += item.Id + ",";
                }

                //更新游戏记录表
                var gameRecord = new GameRecord()
                {
                    State = TaskState.TaskDistributeQuestion,
                    GameTaskId = input.GameId,
                    QuestionIds = string.IsNullOrEmpty(questionIds) ? null : questionIds.Remove(questionIds.Length - 1, 1)
                };

                await _gameRecordRepository.InsertAsync(gameRecord);
            }
            return output;
        }

        private SceneFile FindDefaultImg(Question item, List<SceneFile> sceneFiles)
        {
            var find = sceneFiles.FirstOrDefault(s => s.SceneId == item.SceneId && s.IsDefault);
            if (find == null)
            {
                return null;
            }

            return find;
        }

        /// <summary>
        /// 进入对战页面 flag模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EnterGameOutput> EnterGame(EnterGameInput input)
        {

            var gameRecordEntity = await _gameRecordRepository.FirstOrDefaultAsync(g => g.GameTaskId == input.GameId);

            var gameTaskEntity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameId);

            var startTime = string.Empty;

            var entity = new GameRecord()
            {
                Id = new Guid(),
                GameTaskId = gameTaskEntity.Id,
                State = TaskState.TaskProgress
            };

            await _gameRecordRepository.InsertAsync(entity);

            gameTaskEntity.State = TaskState.TaskProgress;

            await _gameTaskRepository.UpdateAsync(gameTaskEntity);

            var output = new EnterGameOutput();

            //加好友
            var friendEntity = await _friendRepository.FirstOrDefaultAsync(f => f.PlayerId == gameTaskEntity.InviterPlayerId && f.FriendId == gameTaskEntity.InviteePlayerId);


            if (gameRecordEntity.State == TaskState.TaskProgress)
            {
                output.StartTime = gameTaskEntity.LastModificationTime.ToString();
            }

            var stamina = 0;

            var relationDegree = 0;

            if (input.GameType == GameType.EndLess)
            {
                if (friendEntity == null)
                {
                    friendEntity = await _friendRepository.InsertAsync(new FriendEditDto()
                    {
                        PlayerId = gameTaskEntity.InviterPlayerId,
                        FriendId = gameTaskEntity.InviteePlayerId.Value
                    }.MapTo<Friend>());
                }
                else
                {
                    await _friendRepository.UpdateAsync(friendEntity);
                }

                //更新体力值
                var playerEntity = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

                var playerExtensionEntity = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.PlayerId);

                //playerExtensionEntity.Stamina = await _playerAppService.CountDownRecoveryStamina(playerEntity.Id, playerExtensionEntity.Stamina);

                playerExtensionEntity.Stamina -= 6;

                if (playerExtensionEntity.Stamina <= 0)
                    playerExtensionEntity.Stamina = 0;


                await _playerExtensionRepository.UpdateAsync(playerExtensionEntity);

                stamina = playerExtensionEntity.Stamina;

                StaminaRecordEditDto staminaRecordDto = new StaminaRecordEditDto()
                {
                    PlayerId = playerEntity.Id,
                    StaminaCount = -6
                };

                await _staminaRecordAppService.CreateOrUpdate(new CreateOrUpdateStaminaRecordInput()
                {
                    StaminaRecord = staminaRecordDto
                });


                if (friendEntity.Floor >= 0 && friendEntity.Floor < 10)
                {
                    relationDegree = 1;
                }
                else if (friendEntity.Floor > 9 && friendEntity.Floor < 35)
                {
                    relationDegree = 2;
                }
                else if (friendEntity.Floor > 34 && friendEntity.Floor < 59)
                {
                    relationDegree = 3;
                }
                else if (friendEntity.Floor > 59)
                {
                    relationDegree = 4;
                }

                output.Stamina = stamina;

                output.RelationDegree = relationDegree;

                output.Floor = friendEntity.Floor;
            }
            else
            {
                output.RelationDegree = (int)gameTaskEntity.RelationDegree;
            }

            output.AppointmentContent = gameTaskEntity.AppointmentContent;

            output.InviteeAppointmentContent = gameTaskEntity.InviteeAppointmentContent;

            return output;
        }

        /// <summary>
        /// 提交答案 flag
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="questionId"></param>
        /// <param name="answerId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<SubmitAnswerOutputDto> Submit(SubmitAnswerInputDto input)
        {

            var output = new SubmitAnswerOutputDto();

            var answerQuestion = await _answerQuestionRepository.FirstOrDefaultAsync(s =>
                 s.QuesionId == input.QuestionId && s.GameTaskId == input.GameId);

            var gameTask = await _gameTaskRepository.GetAsync(input.GameId);

            var sort = 0;

            if (gameTask != null)
            {
                answerQuestion = answerQuestion ?? new AnswerQuestion();

                answerQuestion.GameTaskId = input.GameId;

                answerQuestion.QuesionId = input.QuestionId;

                if (input.PlayerId == gameTask.InviterPlayerId)
                {
                    answerQuestion.InviterAnswerId = input.AnswerId;

                    sort = (await _answerRepository.GetAsync(answerQuestion.InviterAnswerId)).Sort;

                    answerQuestion.InviterAnswerSort = sort;
                }
                else if (gameTask.InviteePlayerId == input.PlayerId)
                {
                    answerQuestion.InviteeAnswerId = input.AnswerId;

                    sort = (await _answerRepository.GetAsync(answerQuestion.InviteeAnswerId)).Sort;

                    answerQuestion.InviteeAnswerSort = sort;
                }
            }

            var result = await _answerQuestionRepository.InsertOrUpdateAsync(answerQuestion);
            //答案一致
            var inviterAnswer = await _answerRepository.FirstOrDefaultAsync(a => a.Id == result.InviterAnswerId);
            var inviteeAnswer = await _answerRepository.FirstOrDefaultAsync(a => a.Id == result.InviteeAnswerId);
            if (inviterAnswer != null && inviteeAnswer != null && inviterAnswer.Sort == inviteeAnswer.Sort)
            {
                //更新邀请方
                await UpdateBonusPoints(gameTask.InviterPlayerId, StaticBonusPointsCount.AnswerAgreementCount);

                //更新被邀请方
                await UpdateBonusPoints(gameTask.InviteePlayerId.Value, StaticBonusPointsCount.AnswerAgreementCount);
            }

            //通关
            var questionList = _answerQuestionRepository.GetAllList(x => x.GameTaskId == gameTask.Id);

            if (questionList.Count == (int)gameTask.TaskType)
            {
                //await UpdateBonusPoints(gameTask.InviterPlayerId, (int)gameTask.TaskType);

                //await UpdateBonusPoints(gameTask.InviteePlayerId.Value, (int)gameTask.TaskType);

                gameTask.State = TaskState.TaskSuccess;

                await _gameTaskRepository.UpdateAsync(gameTask);
            }


            return new SubmitAnswerOutputDto() { Sort = sort, PlayerId = input.PlayerId };
        }

        /// <summary>
        /// 提交答案 无尽模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SubmitAnswerOutputDto Submit_EndLess(SubmitAnswerInputDto input)
        {
            lock (syncObj)
            {
                var output = new SubmitAnswerOutputDto();

                var player = _playerRepository.GetAllIncluding(s => s.PlayerExtension).FirstOrDefault(s => s.Id == input.PlayerId);

                var answerQuestionNewest = _answerQuestionRepository.GetAllList(s =>
                    s.GameTaskId == input.GameId).OrderByDescending(s => s.CreationTime).FirstOrDefault();//取最新的一次回答，防止第二个回答的人判断错误

                var answerQuestion = answerQuestionNewest;
                if (answerQuestion == null || answerQuestionNewest.QuesionId != input.QuestionId)
                {
                    answerQuestion = new AnswerQuestion();
                }
                var gameTask = _gameTaskRepository.Get(input.GameId);

                var sort = 0;

                var friendEntity = _friendRepository.FirstOrDefault(f => f.PlayerId == gameTask.InviterPlayerId && f.FriendId == gameTask.InviteePlayerId);

                if (gameTask != null)
                {
                    answerQuestion = answerQuestion ?? new AnswerQuestion();
                    if (input.PlayerId == gameTask.InviterPlayerId)
                    {
                        if (answerQuestion.InviterAnswerId != 0)
                        {
                            //如果不为0，则表明answerQuestion是之前答得题目（同一局游戏同一道题）
                            answerQuestion = new AnswerQuestion();
                        }
                        answerQuestion.InviterAnswerId = input.AnswerId;

                        sort = (_answerRepository.Get(answerQuestion.InviterAnswerId)).Sort;

                        answerQuestion.InviterAnswerSort = sort;
                    }
                    else if (gameTask.InviteePlayerId == input.PlayerId)
                    {
                        if (answerQuestion.InviteeAnswerId != 0)
                        {
                            //如果不为0，则表明answerQuestion是之前答得题目（同一局游戏同一道题）
                            answerQuestion = new AnswerQuestion();
                        }

                        answerQuestion.InviteeAnswerId = input.AnswerId;

                        sort = (_answerRepository.Get(answerQuestion.InviteeAnswerId)).Sort;

                        answerQuestion.InviteeAnswerSort = sort;
                    }

                    answerQuestion.GameTaskId = input.GameId;

                    answerQuestion.QuesionId = input.QuestionId;
                    //是否作弊
                    if (answerQuestion.InviteeAnswerId != 0 && answerQuestion.InviterAnswerId != 0 && answerQuestion.InviteeAnswerSort != answerQuestion.InviterAnswerSort)
                    {
                        var isCheat = IsCheat(friendEntity.Floor);
                        answerQuestion.IsCheat = isCheat;
                    }

                }
                var result = _answerQuestionRepository.InsertOrUpdate(answerQuestion);
                //答案一致
                var inviterAnswer = _answerRepository.FirstOrDefault(a => a.Id == result.InviterAnswerId);
                var inviteeAnswer = _answerRepository.FirstOrDefault(a => a.Id == result.InviteeAnswerId);
                if (result.InviteeAnswerId != 0 && result.InviterAnswerId != 0 && inviterAnswer != null && inviteeAnswer != null && (inviterAnswer.Sort == inviteeAnswer.Sort || (inviterAnswer.Sort != inviteeAnswer.Sort && answerQuestion.IsCheat)))
                {
                    //更新邀请方
                    //await UpdateBonusPoints(gameTask.InviterPlayerId, StaticBonusPointsCount.AnswerAgreementCount);

                    //更新被邀请方
                    //await UpdateBonusPoints(gameTask.InviteePlayerId.Value, StaticBonusPointsCount.AnswerAgreementCount);

                    //更新关卡
                    friendEntity.Floor += 1;
                    _friendRepository.Update(friendEntity);

                }

                return new SubmitAnswerOutputDto()
                {
                    Sort = sort,
                    IsCheat = answerQuestion.IsCheat,
                    PlayerId = input.PlayerId,
                    Floor = friendEntity.Floor,
                    Stamina = player.PlayerExtension == null ? 0 : player.PlayerExtension.Stamina
                };
            }
            //else if (inviterAnswer != null && inviteeAnswer != null && inviterAnswer.Sort != inviteeAnswer.Sort) //答案不一致
            //{

            //    if (friendEntity.Floor > 10 && friendEntity.Floor <= 36)//暧昧 10-34
            //    {
            //        if (friendEntity.Floor != 35)
            //            friendEntity.Floor -= 1;
            //    }
            //    else if (friendEntity.Floor > 36 && friendEntity.Floor <= 62) //情侣
            //    {
            //        if (friendEntity.Floor == 60)
            //            friendEntity.Floor -= 0;
            //        else if (friendEntity.Floor == 61)
            //            friendEntity.Floor -= 1;
            //        else
            //            friendEntity.Floor -= 2;
            //    }
            //    else if (friendEntity.Floor > 62 && friendEntity.Floor < 81)//夫妻
            //    {
            //        friendEntity.Floor -= 3;
            //    }

            //    await _friendRepository.UpdateAsync(friendEntity);
            //}
        }

        /// <summary>
        /// 提交答案 无尽模式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SubmitAnswerOutputDto> Submit_EndLess_1(SubmitAnswerInputDto input)
        {
            var output = new SubmitAnswerOutputDto();

            List<int> answerSorts;
            answerSorts = _submitAnswerDic.FirstOrDefault(x => x.Key == input.GameId.ToString() + input.QuestionId).Value;

            if (answerSorts == null)
            {
                answerSorts = new List<int>();
            }

            answerSorts.Add(input.Sort);

            _submitAnswerDic[input.GameId.ToString() + input.QuestionId] = answerSorts;

            var gameTask = _gameTaskRepository.FirstOrDefault(input.GameId);

            if (gameTask != null)
            {
                AnswerQuestion answerQuestion;

                var friendEntity = await _friendRepository.FirstOrDefaultAsync(f => f.PlayerId == gameTask.InviterPlayerId && f.FriendId == gameTask.InviteePlayerId);

                var isCheat = IsCheat(friendEntity.Floor);

                if (answerSorts.Count == 2)
                {
                    if (isCheat || answerSorts[0] == answerSorts[1]) //作弊了或者答案相等
                    {
                        friendEntity.Floor += 1;

                        _friendRepository.Update(friendEntity);
                    }
                    else { }//答案不相等

                    answerQuestion = await _answerQuestionRepository.FirstOrDefaultAsync(s =>
                 s.QuesionId == input.QuestionId && s.GameTaskId == input.GameId);

                    if (input.PlayerId == gameTask.InviterPlayerId)
                    {
                        answerQuestion.InviterAnswerId = input.AnswerId;

                        answerQuestion.InviterAnswerSort = input.Sort;
                    }
                    else
                    {
                        answerQuestion.InviterAnswerId = input.AnswerId;

                        answerQuestion.InviteeAnswerSort = input.Sort;
                    }

                    answerQuestion.IsCheat = isCheat;


                    _answerQuestionRepository.Update(answerQuestion);

                    _submitAnswerDic.TryRemove(input.GameId.ToString() + input.QuestionId, out answerSorts);

                    return new SubmitAnswerOutputDto()
                    {
                        InviterSort = answerQuestion.InviterAnswerSort,
                        InviteeSort = answerQuestion.InviteeAnswerSort,
                        IsCheat = isCheat,
                        PlayerId = input.PlayerId,
                        Floor = friendEntity.Floor,
                    };
                }
                else
                {
                    answerQuestion = new AnswerQuestion();

                    if (input.PlayerId == gameTask.InviterPlayerId)
                    {
                        answerQuestion.InviterAnswerId = input.AnswerId;

                        answerQuestion.InviterAnswerSort = input.Sort;
                    }
                    else
                    {
                        answerQuestion.InviterAnswerId = input.AnswerId;

                        answerQuestion.InviteeAnswerSort = input.Sort;
                    }

                    answerQuestion.GameTaskId = input.GameId;

                    answerQuestion.QuesionId = input.QuestionId;

                    answerQuestion.IsCheat = false;//另一方未提交答案 默认不作弊

                    answerQuestion.Floor = friendEntity.Floor;

                    await _answerQuestionRepository.InsertAsync(answerQuestion);

                    return null;
                }

            }
            else
            {
                return null; //游戏不存在
            }
            //lock (syncObj)
            //{

            //}
        }

        /// <summary>
        /// 是否作弊
        /// </summary>
        /// <returns></returns>
        public bool IsCheat(int currentFloor)
        {
            //关卡概率字典
            var probabilityFloors = new Dictionary<int, int>() {
                {5,8 }, {9,7 },{20,5 },{35,3 },{60,0},{81,0}
            };
            // 根据当前关卡得到作弊的概率
            var probability = 0;
            for (int i = 0; i < probabilityFloors.Count; i++)
            {
                var key = probabilityFloors.Keys.ElementAt(i);
                if (key > currentFloor)
                {
                    probability = probabilityFloors.GetValueOrDefault(key);
                    break;
                }
            }
            //随机一个10以内的数字，根据是否小于10-概率来判断是否作弊
            var ran = new Random();
            var ranValue = ran.Next(1, 11);
            if (ranValue > 10 - probability)
            {
                return true;
            }
            return false;
        }

        protected async Task UpdateBonusPoints(Guid playerId, int count)
        {
            var playerExtension = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == playerId);

            playerExtension.LoveScore += count;

            await _playerExtensionRepository.UpdateAsync(playerExtension);

        }

        public async Task StartPrizeDraw(StartPrizeDrawInput input)
        {
            var entity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameId);

            if (input.PlayerId == entity.InviterPlayerId)
            {
                entity.State = TaskState.TaskinviterStartPrize;
            }
            else
            {
                entity.State = TaskState.TaskinviteeStartPrize;
            }

            await _gameTaskRepository.UpdateAsync(entity);

            await _gameRecordRepository.InsertAsync(new GameRecord() { GameTaskId = entity.Id, State = entity.State });
        }

        public async Task PrizeDrawResult(PrizeDrawResultInput input)
        {
            var entity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameId);

            if (input.PlayerId == entity.InviterPlayerId)
            {
                entity.AppointmentContent = input.Appointment;

                entity.State = TaskState.TaskinviterPrizeResult;
            }
            else
            {
                entity.InviteeAppointmentContent = input.Appointment;

                entity.State = TaskState.TaskinviteePrizeResult;
            }

            await _gameTaskRepository.UpdateAsync(entity);

            await _gameRecordRepository.InsertAsync(new GameRecord() { GameTaskId = entity.Id, State = entity.State });
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<PlayerLeaveOutput> PlayerLeave(EntityDto<Guid> input)
        //{

        //var entity = _gameTaskRepository.FirstOrDefault(g => g.InviterPlayerId == input.Id && g.InviteePlayerId == null);

        ////还没创建房间就断开
        //if (entity == null)
        //{
        //    return null;
        //}
        //else
        //{
        //    entity.
        //    return new PlayerLeaveOutput()
        //    {
        //        MsgCodeEnum = MsgCodeEnum.对方离线
        //    };
        //}

        //await _gameTaskRepository.UpdateAsync(entity);

        //return output;
        //}
        //public async Task<PlayerLeaveOutput> PlayerLeave(EntityDto<string> input)
        //{
        //    var output = new PlayerLeaveOutput();

        //    //var entity = _gameTaskRepository.FirstOrDefault(g => g.ConnectionId == input.Id && g.ConnectionState == (int)ConnectionState.Connected);
        //    var entity = _gameTaskRepository.FirstOrDefault(g => g.ConnectionId == input.Id || g.BeConnectionId == input.Id);

        //    if (entity == null)
        //    {
        //        return output;
        //    }
        //    else
        //    {
        //        entity.ConnectionState = (int)ConnectionState.DisConnect;

        //        if (input.Id == entity.ConnectionId)//邀请方断开
        //        {
        //            output.MsgCodeEnum = MsgCodeEnum.对方离线;

        //            output.playerConnectionId = entity.BeConnectionId;

        //            Logger.Info("对方已经断开");
        //            entity.ConnectionId = null;
        //        }
        //        else if (input.Id == entity.BeConnectionId)//被邀请方断开
        //        {
        //            output.MsgCodeEnum = MsgCodeEnum.对方离线;

        //            output.playerConnectionId = entity.ConnectionId;

        //            Logger.Info("对方已经断开");
        //            entity.BeConnectionId = null;
        //        }

        //    }

        //    await _gameTaskRepository.UpdateAsync(entity);

        //    return output;
        //}

        /// <summary>
        /// 断线重新连接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ReConnection(CreateConnectionDto input)
        {
            var entity = await _gameTaskRepository.FirstOrDefaultAsync(x => x.Id == input.GameId);

            await _gameTaskRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 没答题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AnswerTimeoutOutPut> AnswerTimeout(AnswerTimeoutDto input)
        {
            var gametask = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameId);

            gametask.State = TaskState.TaskFailure;

            await _gameTaskRepository.UpdateAsync(gametask);

            //int count = 0;

            string connectionId = string.Empty;

            if (input.PlayerId == gametask.InviterPlayerId)
            {
                //count = StaticBonusPointsCount.InviterNoAnswerCount;

                await UpdateBonusPoints(input.PlayerId, StaticBonusPointsCount.InviterNoAnswerCount);
            }
            else if (input.PlayerId == gametask.InviteePlayerId)
            {
                //count = StaticBonusPointsCount.InviteeNoAnswerCount;

                await UpdateBonusPoints(input.PlayerId, StaticBonusPointsCount.InviteeNoAnswerCount);
            }

            return new AnswerTimeoutOutPut()
            {
                MsgCodeEnum = MsgCodeEnum.AnswerTimeout,
                //BonusPointCount = count
            };
        }

        /// <summary>
        /// 使用道具  
        /// </summary>
        /// <returns></returns>
        public async Task<UsePropOutput> UseProp(UsePropDto input)
        {

            await _propUseRecord.InsertAsync(new PropUseRecord()
            {
                PlayerId = input.PlayerId,
                PropType = input.PropType
            });

            var gameTask = await _gameTaskRepository.FirstOrDefaultAsync(x => x.Id == input.GameId);

            var playerExtension = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.PlayerId);

            Guid playerId = Guid.Empty;

            if (input.PropType == PropType.ResurrectionCar)
            {
                return new UsePropOutput()
                {
                    PropType = PropType.ResurrectionCar,
                    MsgCodeEnum = MsgCodeEnum.UserReviveCard
                };
            }
            else
            {
                if (playerExtension.LoveScore < StaticBonusPointsCount.UseDelayCount)
                {
                    return new UsePropOutput()
                    {
                        MsgCodeEnum = MsgCodeEnum.IntegralNotEnough,
                        PropType = PropType.DelayCard
                    };
                }
                else
                {
                    playerExtension = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.PlayerId);

                    playerExtension.LoveScore += StaticBonusPointsCount.UseDelayCount;

                    await _playerExtensionRepository.UpdateAsync(playerExtension);

                    return new UsePropOutput()
                    {
                        MsgCodeEnum = MsgCodeEnum.UserDelayCard,
                        PropType = PropType.DelayCard,
                    };
                }
            }
        }

        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="EmoticonCode"></param>
        /// <returns></returns>
        public async Task<SendEmoticonOutput> SendEmoticon(SendEmoticonDto input)
        {

            var result = await _emoticonRecordRepository.InsertAsync(input.MapTo<EmoticonRecord>());

            var gameTask = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameTaskId);

            SendEmoticonOutput output = new SendEmoticonOutput();
            output.PlayerId = input.PlayerId;

            output.EmoticonCode = result.Code;

            return output;
        }

        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ShareFriends(EntityDto<Guid> input)
        {
            var entity = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.Id);

            entity.LoveScore += StaticBonusPointsCount.ShareFriendCount;

            await _playerExtensionRepository.UpdateAsync(entity);

        }

        /// <summary>
        /// 结果页接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBonusPointsCountOutput> GetBonusPointsCountAndAnswerProportion(GetBonusPointsCountInput input)
        {
            if (input.playerId == Guid.Empty)
            {
                return new GetBonusPointsCountOutput()
                {
                    ErrMessage = "playerid参数为空"
                };
            }
            var entity = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.playerId);

            //所有已答题
            var answerQuestions = await _answerQuestionRepository.GetAllListAsync(a => a.QuesionId == input.QuestionId);

            if (answerQuestions.Count() == 0)
            {
                return new GetBonusPointsCountOutput()
                {
                    BonusPointCount = entity.LoveScore,
                    Percents = new[] { 3 / 100.0, 3 / 100.0, 3 / 100.0 },
                    ErrMessage = ""
                };
            }
            else
            {
                //做此道题的总人数
                var totalCount = Convert.ToDouble(answerQuestions.Count()) * 2;

                //选第一个选项的人
                var optionACount = Convert.ToDouble(answerQuestions.Count(a => a.InviterAnswerSort == 0 || a.InviteeAnswerSort == 0));

                // 选第二个选项的人
                var optionBCount = Convert.ToDouble(answerQuestions.Count(a => a.InviterAnswerSort == 1 || a.InviteeAnswerSort == 1));

                // 选第三个选项的人
                var optionCCount = Convert.ToDouble(answerQuestions.Count(a => a.InviterAnswerSort == 2 || a.InviteeAnswerSort == 2));

                var Options = new[] { optionACount, optionBCount, optionCCount };

                var zeroCount = Options.Count(o => o == 0);

                if (zeroCount > 0)
                {
                    totalCount = totalCount + Options.Count();

                    Options = Options.Select(v => (v + 1) / totalCount * 100).ToArray();
                }

                return new GetBonusPointsCountOutput()
                {
                    BonusPointCount = entity.LoveScore,
                    Percents = Options,
                };
            }
        }

        /// <summary>
        /// 发起再玩一次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PlayGameAgainOutput> PlayGameAgain(PlayGameAgainDto input)
        {
            var output = new PlayGameAgainOutput();

            var gameEntity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.GameId);

            if (gameEntity == null)
            {
                return null;
            }
            else
            {

                output.PlayerId = input.PlayerId;

                var newGameEntity = await _gameTaskRepository.InsertAsync(new GameTask()
                {
                    InviterPlayerId = input.PlayerId,
                    AppointmentContent = gameEntity.AppointmentContent,
                    SeekType = gameEntity.SeekType,
                    TaskType = gameEntity.TaskType,
                    State = gameEntity.State,
                    RelationDegree = gameEntity.RelationDegree,
                    InvitationLink = gameEntity.InvitationLink,
                });

                output.GameId = newGameEntity.Id;

                return output;
            }
        }

        /// <summary>
        /// 对方同意
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OtherAgreeOutput> OtherAgree(OtherAgreeOrRefuseDto input)
        {

            var output = new OtherAgreeOutput();

            var entity = await _gameTaskRepository.FirstOrDefaultAsync(x => x.Id == input.OldGameId);

            var newGameTask = new GameTask();

            newGameTask.Id = input.GameId;

            newGameTask.AppointmentContent = entity.AppointmentContent;

            newGameTask.RelationDegree = entity.RelationDegree;

            newGameTask.SeekType = entity.SeekType;

            newGameTask.State = entity.State;

            newGameTask.TaskType = entity.TaskType;

            newGameTask.ValidInterval = entity.ValidInterval;

            newGameTask.InviteePlayerId = input.PlayerId;

            newGameTask.InviterPlayerId = input.OtherPlayerId;

            await _gameTaskRepository.InsertAsync(newGameTask);

            return newGameTask.MapTo(output);
        }

        /// <summary>
        /// 对方拒绝玩游戏
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OtherAgreeOrRefuseOutput> OtherRefuse(OtherAgreeOrRefuseDto input)
        {

            var output = new OtherAgreeOrRefuseOutput();

            var entity = await _gameTaskRepository.FirstOrDefaultAsync(x => x.Id == input.GameId);

            if (input.PlayerId == entity.InviteePlayerId)
            {
                output.OtherPlayerId = entity.InviterPlayerId;
            }
            else
            {
                output.OtherPlayerId = entity.InviteePlayerId.Value;
            }

            Logger.Debug($"玩家:{input.PlayerId} 拒绝在玩一次");

            return output;
        }

        public async Task<Guid?> GameIsExist(EntityDto<Guid> input)
        {
            var entity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.Id);

            if (entity == null)
            {
                return null;
            }
            else
            {
                return entity.InviteePlayerId.Value;
            }

        }

        public async Task<GetGameInfoOutput> GetGameInfo(GetGameInfoInput input)
        {

            if (input.GameId == Guid.Empty)
            {
                return new GetGameInfoOutput()
                {
                    ErrMessage = $"gameId为空:传的gameId:{input.GameId}"
                };

            }

            //所有已答题
            var allAnswerQuestions = _answerQuestionRepository.GetAllIncluding(g => g.GameTask).AsNoTracking();

            allAnswerQuestions = allAnswerQuestions.Where(a => a.GameTask.GameType == GameType.Flag || a.GameTask.GameType == GameType.UnKnow);

            var gameEntity = await _gameTaskRepository.GetAllIncluding(p => p.Inviter).Include(p => p.Invitee).FirstOrDefaultAsync(g => g.Id == input.GameId);

            if (gameEntity == null)
            {
                return new GetGameInfoOutput()
                {
                    ErrMessage = $"游戏信息不存在:gameId:{input.GameId}"
                };
            }

            //默契度计算
            //1 计算本次任务已答题数量
            var currentAnswerQuestionCount = allAnswerQuestions.Count(a => a.GameTaskId == input.GameId);

            var underCounts = 0;

            var groups = allAnswerQuestions.GroupBy(x => x.GameTaskId);

            if (currentAnswerQuestionCount > 0)
            {

                foreach (var item in groups)
                {
                    if (item.Count() < currentAnswerQuestionCount)
                    {
                        underCounts++;
                    }
                }
            }
            else
            {
                var lastGameTask = await _gameTaskRepository.FirstOrDefaultAsync(g => g.InviterPlayerId == gameEntity.InviterPlayerId && g.InviteePlayerId == gameEntity.InviteePlayerId);
                if (lastGameTask == null)
                {
                    underCounts = 0;
                }
                else
                {
                    var lastAnswerQuestionCount = allAnswerQuestions.Count(a => a.GameTaskId == lastGameTask.Id);

                    if (lastAnswerQuestionCount == 0)
                    {
                        underCounts = 0;
                    }
                    else
                    {
                        foreach (var item in groups)
                        {
                            if (item.Count() < lastAnswerQuestionCount)
                            {
                                underCounts++;
                            }
                        }
                    }
                }

            }
            var totalCount = allAnswerQuestions.Count() + 0.0;

            if (totalCount == 0)
            {
                totalCount = 1.0;
            }
            var tacitDegree = Math.Round(Convert.ToDouble(underCounts / totalCount), 2) * 100;

            return new GetGameInfoOutput()
            {
                InviterHeadUrl = gameEntity.Inviter.HeadUrl,
                InviteeHeadUrl = gameEntity.Invitee.HeadUrl,
                AppointmentContent = gameEntity.AppointmentContent,
                TacitDegree = tacitDegree
            };
        }

        public async Task<GetGameProgressOutput> GetGameProgress(GetGameProgressInput input)
        {
            var output = new GetGameProgressOutput();

            var entity = await _gameTaskRepository.FirstOrDefaultAsync(g => g.Id == input.Id);

            if (entity == null)
            {
                return null;
            }
            else
            {
                var player = new Player();
                if (input.PlayerId == entity.InviterPlayerId)//邀请方
                {
                    player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == entity.InviteePlayerId);

                    player.MapTo(output.OtherPlayer);
                }
                else if (input.PlayerId == entity.InviteePlayerId)
                {
                    player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == entity.InviterPlayerId);

                    player.MapTo(output.OtherPlayer);
                }
                else
                {
                    output.MsgCodeEnum = MsgCodeEnum.RoomFull;
                }

                output.CurrentTime = DateTime.Now;

                entity.MapTo(output);

                return output;
            }
        }

        public List<AppointmentContent> GetAppointments(GetAppointmentDto input)
        {
            var folder = Path.Combine(_hostingEnvironment.WebRootPath, FileRootPath, AppointmentFilePath);

            var build = new ConfigurationBuilder().AddJsonFile(folder, optional: true, reloadOnChange: true);

            var configuration = build.Build();

            var appointment = new Appointment();

            if (input.AppointmentType == AppointmentType.OnLine)
                configuration.Bind("OnLine", appointment);
            else
                configuration.Bind("OffLine", appointment);

            /// 3道题是 简单  5道题是普通 10道题是复杂
            if (input.GameTaskType == TaskType.ThreeQuesion)
                return appointment.Easys;
            else if (input.GameTaskType == TaskType.FiveQuestion)
                return appointment.Normals;
            else
                return appointment.Purgatorys;
        }

        /// <summary>
        /// 减去体力-6
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<MinusStaminaOutput> MinusStamina(Guid playerId)
        {
            var playerExtension = await _playerExtensionRepository.FirstOrDefaultAsync(a => a.PlayerGuid == playerId);
            if (playerExtension != null && playerExtension.Stamina >= 6)
            {
                playerExtension.Stamina -= 6;
                await _playerExtensionRepository.UpdateAsync(playerExtension);

            }

            StaminaRecordEditDto staminaRecordDto = new StaminaRecordEditDto()
            {
                PlayerId = playerId,
                StaminaCount = -6
            };

            await _staminaRecordAppService.CreateOrUpdate(new CreateOrUpdateStaminaRecordInput()
            {
                StaminaRecord = staminaRecordDto
            });

            return new MinusStaminaOutput() { Stamina = playerExtension.Stamina };
        }
        /// <summary>
        /// 检查体力是否够6个
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public bool CheckStamina(Guid playerId)
        {
            var playerExtension = _playerExtensionRepository.FirstOrDefault(a => a.PlayerGuid == playerId);

            if (playerExtension != null)
            {
                if (playerExtension != null && playerExtension.Stamina < 6)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        ///// <summary>
        ///// 使用复活卡
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task UseResurrectionCard(UseResurrectionCardDto input)
        //{
        //    await _propUseRecord.InsertAsync(input.MapTo<PropUseRecord>());

        //}


        /// <summary>
        /// 发送小程序模板消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult> SendMiniProgramTemplateMessage(WechatTemplateMessageInInput input)
        {
            var output = new WechatTemplateMessageOutput();
            var tenant = await GetCurrentTenantAsync();
            var appId = tenant.WechatAppId;
            var secret = tenant.WechatAppSecret;
            TemplateMessage templateMessage = new TemplateMessage(appId, secret, Logger);
            var player = await _playerRepository.FirstOrDefaultAsync(input.PlayerId);
            if (player != null)
            {
                WechatTemplateMessageDataDto wechatTemplateMessageDataDto = new WechatTemplateMessageDataDto();
                wechatTemplateMessageDataDto.touser = player.OpenId.ToString();
                wechatTemplateMessageDataDto.template_id = input.TemplateId;
                wechatTemplateMessageDataDto.page = input.Page;
                wechatTemplateMessageDataDto.form_id = input.FormId;
                wechatTemplateMessageDataDto.data = input.Data;
                wechatTemplateMessageDataDto.emphasis_keyword = input.EmphasisKeyword;
                var response = await templateMessage.SendMiniProgramTemplateMessage(wechatTemplateMessageDataDto);
                output = JsonConvert.DeserializeObject<WechatTemplateMessageOutput>(response, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
            }
            else
            {
                output.Errmsg = "该玩家不存在！";
            }
            return new JsonResult(output);
        }

        public async Task<JsonResult> GetPosterInfo(GetPosterInfoInput input)
        {
            var entity = await _friendRepository.FirstOrDefaultAsync(f => f.PlayerId == input.PlayerId && f.FriendId == input.FriendId);

            var output = new GetPosterInfoOutput();

            if (entity != null)
            {
                TimeSpan startTime = new TimeSpan(entity.CreationTime.Ticks);

                TimeSpan endTime = new TimeSpan(DateTime.Now.Ticks);

                var diff = endTime - startTime;

                if (diff.Days <= 0)
                {
                    output.FriendDays = 1;
                }
                else
                {
                    output.FriendDays = Convert.ToInt32(diff.TotalDays);
                }
            }
            else
            {
                //output.FriendDays = 1;

                Logger.Error($"error reason:玩家id{input.PlayerId},好友Id:{input.FriendId}");
                return null;
            }

            var gameTasks = await _gameTaskRepository.GetAllListAsync(g => (g.InviterPlayerId == input.PlayerId && g.InviteePlayerId == input.FriendId) ||
                                                                       (g.InviterPlayerId == input.FriendId && g.InviteePlayerId == input.PlayerId) && g.GameType == GameType.EndLess);
            if (input.GameId != null || Guid.Empty != input.GameId)
            {
                var gameTask = gameTasks.FirstOrDefault(g => g.Id == input.GameId);

                gameTask.State = TaskState.TaskSuccess;

                await _gameTaskRepository.UpdateAsync(gameTask);
            }

            var gameIds = gameTasks.Select(g => g.Id);

            var allanswerQuestions = _answerQuestionRepository.GetAll().Where(a => a.CreationTime >= entity.CreationTime).AsNoTracking();

            var answerQuestions = new List<AnswerQuestion>();

            foreach (var gameId in gameIds)
            {
                var answerQuestion = allanswerQuestions.Where(a => a.GameTaskId == gameId);

                answerQuestions.AddRange(answerQuestion
                    );
            }

            output.SceneCount = answerQuestions.Count;

            IEnumerable<IGrouping<DateTime, AnswerQuestion>> lst = answerQuestions.GroupBy(a => a.CreationTime.Date).OrderByDescending(x => x.Count());

            output.BarrierCount = lst.FirstOrDefault().Count();

            output.TopicCount = new Random().Next(output.BarrierCount, output.BarrierCount * 3);

            var tags = await _tagRepository.GetAllListAsync();

            output.TopicName = tags[new Random().Next(0, tags.Count)].TagName;

            var inviter = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

            var invitee = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.FriendId);

            StringBuilder titleBuilder = new StringBuilder();

            string title = $"✧⁺⸜(●˙▾˙●)⸝⁺✧我又来啦～ 恭喜{inviter.NickName}和{invitee.NickName}," +
               $"你们真的很棒哦～ 历时{output.FriendDays}天，在{output.SceneCount}个不同的场景中，" +
               $"你们共同经历了{output.TopicCount}个话题的考验，是不是也吓了一跳呢？" +
               $"你们曾在一天内连续闯过了{output.BarrierCount}关哦!你们最钟爱的话题是{output.TopicName}话题,好羞羞哦(⺣◡⺣)♡" +
               $"不过在XX话题中彼此的了解还不是很深刻呢！爱情，先从真正了解对方开始。" +
               $"谢谢你们愿意陪伴彼此这么长的时间去相互了解。你们于茫茫人海中相遇，愿以深情共余生";

            if (entity.Floor >= 81)
            {
                entity.Floor = 1;

                await _friendRepository.UpdateAsync(entity);
            }

            return new JsonResult(title);
        }


        //private async Task<string> HttpGet(string requestUrl)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        return await client.GetStringAsync(requestUrl);
        //    }

        //}
        public async Task<JsonResult> GetLatestPosterInfo(GetPosterInfoInput input)
        {
            var entity = await _friendRepository.FirstOrDefaultAsync(f => f.PlayerId == input.PlayerId && f.FriendId == input.FriendId);

            var output = new GetPosterInfoOutput();

            if (entity != null)
            {
                TimeSpan startTime = new TimeSpan(entity.CreationTime.Ticks);

                TimeSpan endTime = new TimeSpan(DateTime.Now.Ticks);

                var diff = endTime - startTime;

                if (diff.Days <= 0)
                {
                    output.FriendDays = 1;
                }
                else
                {
                    output.FriendDays = Convert.ToInt32(diff.TotalDays);
                }
            }
            else
            {
                //output.FriendDays = 1;

                Logger.Error($"error reason:玩家id{input.PlayerId},好友Id:{input.FriendId}");
                return null;
            }

            var gameTasks = await _gameTaskRepository.GetAllListAsync(g => (g.InviterPlayerId == input.PlayerId && g.InviteePlayerId == input.FriendId) ||
                                                                       (g.InviterPlayerId == input.FriendId && g.InviteePlayerId == input.PlayerId) && g.GameType == GameType.EndLess);

            if (input.GameId != null)
            {
                var gameTask = gameTasks.FirstOrDefault(g => g.Id == input.GameId);

                gameTask.State = TaskState.TaskSuccess;

                await _gameTaskRepository.UpdateAsync(gameTask);
            }

            var gameIds = gameTasks.Select(g => g.Id);

            var allanswerQuestions = _answerQuestionRepository.GetAll().Where(a => a.CreationTime >= entity.CreationTime).AsNoTracking();

            var answerQuestions = new List<AnswerQuestion>();

            foreach (var gameId in gameIds)
            {
                var answerQuestion = allanswerQuestions.Where(a => a.GameTaskId == gameId);

                answerQuestions.AddRange(answerQuestion);
            }

            output.SceneCount = answerQuestions.Count;

            IEnumerable<IGrouping<DateTime, AnswerQuestion>> lst = answerQuestions.GroupBy(a => a.CreationTime.Date).OrderByDescending(x => x.Count());

            output.BarrierCount = lst.FirstOrDefault().Count();

            output.TopicCount = new Random().Next(output.BarrierCount, output.BarrierCount * 3);

            var tags = await _tagRepository.GetAllListAsync();

            output.TopicName = tags[new Random().Next(0, tags.Count)].TagName;

            var inviter = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

            var invitee = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.FriendId);

            StringBuilder titleBuilder = new StringBuilder();

            switch (entity.Floor)
            {
                case 10:
                    titleBuilder.Append($"&emsp;&emsp;恭喜{inviter.NickName}和{invitee.NickName}，你们真的很棒哦～\n");
                    titleBuilder.Append($"&emsp;&emsp;历时{output.FriendDays}天，在{output.SceneCount}个不同的场景中，你们共同经历\n");
                    titleBuilder.Append($"了{output.TopicCount}个话题的考验，是不是也吓了一跳呢？\n");
                    titleBuilder.Append($"&emsp;&emsp;你们曾在一天内连续闯过了{output.BarrierCount}关哦！真的好棒哦！\n");
                    titleBuilder.Append($"&emsp;&emsp;相遇总是点点头，想说总是难开口，在100万个可能中\n");
                    titleBuilder.Append($"感受彼此的温柔，祝福你们经久考验的爱情终成佳话哦！");
                    break;
                case 35:
                    titleBuilder.Append($"&emsp;&emsp;恭喜{inviter.NickName}和{invitee.NickName}，\n");
                    titleBuilder.Append("你们真的很棒哦～\n");
                    titleBuilder.Append($"&emsp;&emsp;历时{output.FriendDays}天，在{output.SceneCount}个不同的场\n");
                    titleBuilder.Append($"景中，你们共同经历了{output.TopicCount}个话题的\n");
                    titleBuilder.Append($"考验，是不是也吓了一跳呢？\n");
                    titleBuilder.Append($"&emsp;&emsp;你们曾在一天内连续闯过了{output.BarrierCount}关\n");
                    titleBuilder.Append($"哦！真的好棒哦！\n");
                    titleBuilder.Append($"生命有期望，爱情无期限。\n");
                    titleBuilder.Append($"&emsp;&emsp;愿你俩用爱去缠着对方，彼此互相\n");
                    titleBuilder.Append($"体谅和关怀，共同分享今后的苦与乐");
                    break;
                case 60:
                    titleBuilder.Append($"&emsp;&emsp;恭喜{inviter.NickName}和{invitee.NickName}，你们真的很棒哦～\n");
                    titleBuilder.Append($"&emsp;&emsp;历时{output.FriendDays}天，在{output.SceneCount}个不同的场景中，你们共同经历\n");
                    titleBuilder.Append($"了{output.TopicCount}个话题的考验，是不是也吓了一跳呢？\n");
                    titleBuilder.Append($"&emsp;&emsp;你们曾在一天内连续闯过了{output.BarrierCount}关哦！真的好棒哦！\n");
                    titleBuilder.Append($"&emsp;&emsp;在100万个可能中，是偶然，还是奇迹，引彼此翩翩走\n");
                    titleBuilder.Append($"进自己的生命，用那沾满柔情的爱意，挥洒永不退色的诗句。\n");
                    titleBuilder.Append($"&emsp;&emsp;愿你们真诚的相爱之火，如初升的太阳，越久越旺；让\n");
                    titleBuilder.Append($"众水也不能熄灭，大水也不能淹没！");
                    break;
                case 81:
                    titleBuilder.Append($"&emsp;&emsp;恭喜{inviter.NickName}和{invitee.NickName}，你们真的很棒\n");
                    titleBuilder.Append($"哦～\n");
                    titleBuilder.Append($"&emsp;&emsp;历时{output.FriendDays}天，在{output.SceneCount}个不同的场景中，你们共同经历\n");
                    titleBuilder.Append($"了{output.TopicCount}个话题的考验，是不是也吓了一跳呢？\n");
                    titleBuilder.Append($"&emsp;&emsp;你们曾在一天内连续闯过了{output.BarrierCount}关哦！真的好棒哦！\n");
                    titleBuilder.Append($"&emsp;&emsp;由相知而相爱，由相爱而更加相知。人们常说的神仙眷\n");
                    titleBuilder.Append($"侣就是你们了！祝相爱年年岁岁，相知岁岁年年哦~");
                    break;
                case 1://防止一方请求完后已经更新到第一关
                    titleBuilder.Append($"&emsp;&emsp;恭喜{inviter.NickName}和{invitee.NickName}，你们真的很棒\n");
                    titleBuilder.Append($"哦～\n");
                    titleBuilder.Append($"&emsp;&emsp;历时{output.FriendDays}天，在{output.SceneCount}个不同的场景中，你们共同经历\n");
                    titleBuilder.Append($"了{output.TopicCount}个话题的考验，是不是也吓了一跳呢？\n");
                    titleBuilder.Append($"&emsp;&emsp;你们曾在一天内连续闯过了{output.BarrierCount}关哦！真的好棒哦！\n");
                    titleBuilder.Append($"&emsp;&emsp;由相知而相爱，由相爱而更加相知。人们常说的神仙眷\n");
                    titleBuilder.Append($"侣就是你们了！祝相爱年年岁岁，相知岁岁年年哦~");
                    break;
                default:
                    break;
            }

            if (entity.Floor >= 81)
            {
                entity.Floor = 1;
                await _friendRepository.UpdateAsync(entity);
            }

            return new JsonResult(titleBuilder.ToString());
        }

        public async Task UpdateGameState(EntityDto<Guid> input)
        {
            await _gameTaskAppService.UpdateGameState(input);
        }

        //public async Task<GetBabiesOutput> VaildPubPlayer(VaildPubPlayerInput input)
        //{
        //    var getUnionIdResponse = await GetUnionId(input);
        //    var tenantId = AbpSession.GetTenantId();


        //    if (getUnionIdResponse == null)
        //    {
        //        throw new AbpException("获取用户信息错误");
        //    }

        //    await UpdateMpUserInfo(getUnionIdResponse);


        //    var player = await _playerRepository.FirstOrDefaultAsync(p => p.UnionId == getUnionIdResponse.UnionId && p.TenantId == tenantId);

        //    if (player == null)
        //    {
        //        player = await _playerRepository.FirstOrDefaultAsync(p => p.HeadUrl == getUnionIdResponse.HeadUrl && p.TenantId == tenantId);
        //        if (player == null)
        //        {
        //            var findplaysers = _playerRepository.GetAll()
        //                .Where(p => p.NickName == getUnionIdResponse.NickName && p.TenantId == tenantId);

        //            if (findplaysers.Count() == 1)
        //            {
        //                player = findplaysers.FirstOrDefault();
        //            }
        //            else
        //            {
        //                return new GetBabiesOutput();
        //            }

        //        }

        //        if (player != null)
        //        {
        //            player.UnionId = getUnionIdResponse.UnionId;
        //            await _playerRepository.UpdateAsync(player);
        //        }
        //    }

        //    if (getUnionIdResponse.OpenId != null && player.ChineseBabyPubOpenId == null)
        //    {
        //        player.ChineseBabyPubOpenId = getUnionIdResponse.OpenId;

        //        await _playerRepository.UpdateAsync(player);
        //    }

        //    var result = await _babyAppService.GetBabies(new GetBabiesInput()
        //    {
        //        PlayerGuid = player.Id,
        //    });


        //    result.PlayerId = player.Id;

        //    return result;

        //}
        public async Task<GetBabiesOutput> VaildPubPlayer(VaildPubPlayerInput input)
        {
            var getUnionIdResponse = await GetUnionId(input);
            var tenantId = AbpSession.GetTenantId();


            if (getUnionIdResponse == null)
            {
                throw new AbpException("获取用户信息错误");
            }

            await UpdateMpUserInfo(getUnionIdResponse);


            var player = await _playerRepository.FirstOrDefaultAsync(p => p.UnionId == getUnionIdResponse.UnionId && p.TenantId == tenantId);

            if (player == null)
            {
                player = await _playerRepository.FirstOrDefaultAsync(p => p.HeadUrl == getUnionIdResponse.HeadUrl && p.TenantId == tenantId);
                if (player == null)
                {
                    var findplaysers = _playerRepository.GetAll()
                        .Where(p => p.NickName == getUnionIdResponse.NickName && p.TenantId == tenantId);

                    if (findplaysers.Count() == 1)
                    {
                        player = findplaysers.FirstOrDefault();
                    }
                    else
                    {
                        return new GetBabiesOutput();
                    }

                }

                if (player != null)
                {
                    player.UnionId = getUnionIdResponse.UnionId;
                    await _playerRepository.UpdateAsync(player);
                }
            }

            if (getUnionIdResponse.OpenId != null && player.ChineseBabyPubOpenId == null)
            {
                player.ChineseBabyPubOpenId = getUnionIdResponse.OpenId;

                await _playerRepository.UpdateAsync(player);
            }

            var result = new GetBabiesOutput();


            result.PlayerId = player.Id;

            return result;

        }
        public async Task<VaildPubPlayerOutput> V2_VaildPubPlayer(VaildPubPlayerInput input)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //var agent1 = await _mqAgentRepository.GetAll().Select(c => new { c.Id, c.UnionId }).FirstOrDefaultAsync(c => c.UnionId == input.Code);
            //Console.WriteLine(agent1.Id);
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

            var getUnionIdResponse = await GetUnionId(input);
            var tenantId = AbpSession.GetTenantId();


            if (getUnionIdResponse == null)
            {
                throw new AbpException("获取用户信息错误");
            }

            var getUnionIdResponseStr = $"| 租户id:{AbpSession.GetTenantId()} | 输入参数：" + input.ToJsonString() + " | 微信接口返回信息：" + getUnionIdResponse.ToJsonString();
            // Logger.Debug($"蓝屏 | 验证微信用户信息：租户id:{AbpSession.GetTenantId()} {getUnionIdResponseStr}");

            await UpdateMpUserInfo(getUnionIdResponse);


            var player = await _playerRepository.FirstOrDefaultAsync(p => p.UnionId == getUnionIdResponse.UnionId && p.TenantId == tenantId);

            if (player == null)
            {
                Logger.Debug($"蓝屏 | 玩家信息为空1，正在通过 头像跟昵称去查找。 {getUnionIdResponseStr}");
                player = await _playerRepository.FirstOrDefaultAsync(p => p.ChineseBabyPubOpenId == getUnionIdResponse.OpenId && p.TenantId == tenantId);

                if (player != null)
                {
                    await UpdatePlayerUnionInfo(getUnionIdResponse, player);
                }
                else
                {
                    Logger.Debug($"蓝屏 | 玩家信息为2，即将根据昵称查找。{getUnionIdResponseStr}");
                    var playerList = await _playerRepository.GetAllListAsync(p => p.TenantId == tenantId && p.NickName == getUnionIdResponse.NickName);
                    if (playerList == null || playerList.Count <= 0)
                    {
                        Logger.Warn($"蓝屏 | 玩家信息为空3，根据昵称未查找到目标用户，终止查询。{getUnionIdResponseStr}");
                        var playeres = await _playerRepository.GetAll().IgnoreQueryFilters().Where(p => p.NickName == getUnionIdResponse.NickName).ToListAsync();
                        Logger.Debug($"蓝屏 | 玩家信息为空3.1，禁用过滤器后查询结果条数：{playeres?.Count}");
                        foreach (var item in playeres)
                        {
                            Logger.Debug($"蓝屏 | 玩家信息为空3.1，禁用过滤器后查询结果明细：{item.ToJsonString()}");
                        }
                        throw new UserFriendlyException("1");
                    }
                    else
                    {
                        Logger.Debug($"蓝屏 | 找到了可能匹配公众号的用户4。数量为：{playerList.Count}个 。{getUnionIdResponseStr}");
                        var playerList2 = playerList.Where(p => p.ChineseBabyPubOpenId == null && p.UnionId == null).ToList();
                        Logger.Debug($"蓝屏 | 找到了可能匹配公众号的用户4.1。筛选unionId、ChineseBabyPubOpenId不为空的记录，筛选后数量为：{playerList2?.Count}个 {getUnionIdResponseStr}");
                        if (playerList2.Count > 1)
                        {
                            Logger.Debug($"蓝屏 | 找到了可能匹配公众号的多个用户5，取最近更新的玩家。{getUnionIdResponseStr}");
                            player = playerList2.OrderByDescending(s => s.LastLoginTime).FirstOrDefault();
                            Logger.Debug($"蓝屏 | 找到了可能匹配公众号的多个用户5.1，取最近更新的玩家。{getUnionIdResponseStr}，用户信息：{player.ToJsonString()}");
                            await UpdatePlayerUnionInfo(getUnionIdResponse, player);
                        }
                        else if (playerList2.Count == 1)
                        {
                            player = playerList2.FirstOrDefault();
                            Logger.Debug($"蓝屏 | 找到了可能匹配公众号的用户6。{getUnionIdResponseStr}，用户信息：{player.ToJsonString()}");
                            await UpdatePlayerUnionInfo(getUnionIdResponse, player);
                        }
                        else
                        {
                            Logger.Warn($"蓝屏 | 找到了可能匹配公众号的用户7，但用户的 UnionId 和 ChineseBabyPubOpenId 不为 null，停止更新。{getUnionIdResponseStr}");
                            foreach (var item in playerList)
                            {
                                Logger.Warn($"蓝屏 | 找到了可能匹配公众号的用户7.1，但用户的 UnionId 和 ChineseBabyPubOpenId 不为 null，停止更新，循环输出查询到的记录，以供开发使用。{getUnionIdResponseStr}，用户信息：{item.ToJsonString()}");
                            }
                            throw new UserFriendlyException("1");
                        }
                    }
                }
            }
            else
            {
                await UpdatePlayerUnionInfo(getUnionIdResponse, player);
            }
            if (player != null && player.ChineseBabyPubOpenId == null)
            {
                Logger.Debug($"系统异常，无法获取玩家OpenId。玩家编号：{player.Id}，玩家昵称：{player.NickName},ChineseBabyPubOpenId：{player.ChineseBabyPubOpenId},getUnionIdResponse.OpenId != null && player.ChineseBabyPubOpenId == null:{getUnionIdResponse.OpenId != null && player.ChineseBabyPubOpenId == null}。getUnionIdResponse：{getUnionIdResponse.ToJson()}");
            }
            var output = new VaildPubPlayerOutput();

            var agent = await _mqAgentRepository.GetAll().Select(c => new { c.Id, c.UnionId }).FirstOrDefaultAsync(c => c.UnionId == getUnionIdResponse.UnionId);
            if (agent != null)
            {
                output.AgentId = agent.Id;
            }

            output.PlayerId = player == null ? Guid.Empty : player.Id;
            output.NickName = getUnionIdResponse.NickName;
            output.HeadUrl = getUnionIdResponse.HeadUrl;
            output.OpenId = getUnionIdResponse.OpenId;
            output.UnionId = getUnionIdResponse.UnionId;
            return output;

        }

        private async Task UpdatePlayerUnionInfo(GetOfficeAccountUnionIdOutput getUnionIdResponse, Player player)
        {
            player.ChineseBabyPubOpenId = getUnionIdResponse.OpenId;
            player.UnionId = getUnionIdResponse.UnionId;
            if (player.NickName != getUnionIdResponse.NickName)
            {
                player.NickName = getUnionIdResponse.NickName;
            }
            if (player.HeadUrl != getUnionIdResponse.HeadUrl)
            {
                player.HeadUrl = getUnionIdResponse.HeadUrl;
            }
            await _playerRepository.UpdateAsync(player);
        }

        /// <summary>
        /// 获取微信UnionID
        /// 参考文章 https://www.cnblogs.com/szw/p/5875485.html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<GetOfficeAccountUnionIdOutput> GetUnionId(VaildPubPlayerInput input)
        {
            var cacheKey = $"account_union_by_code_{input.Code}";
            GetOfficeAccountUnionIdOutput result = null;
            try
            {
                //通过，用code换取access_token
                var isSecondRequest = false;
                isSecondRequest = await RedisHelper.ExistsAsync(cacheKey);

                if (!isSecondRequest)
                {
                    //第一次请求
                    Logger.DebugFormat("第一次微信OAuth到达，code：{0}", input.Code);
                    lock (OAuthCodeCollectionLock)
                    {
                        RedisHelper.Set(cacheKey, null, 2 * 60 * 60);
                    }
                }
                else
                {
                    //第二次请求
                    Logger.DebugFormat("第二次微信OAuth到达，code：{0}", input.Code);
                    lock (OAuthCodeCollectionLock)
                    {
                        result = JsonConvert.DeserializeObject<GetOfficeAccountUnionIdOutput>(RedisHelper.Get(cacheKey));
                    }
                }

                try
                {
                    var getUnionIdResponse = new GetOfficeAccountUnionIdOutput();
                    try
                    {
                        if (result == null)
                        {
                            var getOpenIdResponse = _miniappService.GetOfficeAccountOpenId(new GetOpenIdInput
                            {
                                AppId = _wechatPubOption.AppId,
                                AppSecret = _wechatPubOption.Secret,
                                Code = input.Code
                            });

                            if (getOpenIdResponse.OpenId == null || !string.IsNullOrEmpty(getOpenIdResponse.ErrMsg))
                            {
                                throw new AbpException($"获取openId错误，错误信息：{getOpenIdResponse.ErrMsg}");
                            }
                            //获取unionID
                            getUnionIdResponse = _miniappService.GetOfficeAccountUnionId(new GetOfficeAccountUnionIdInput()
                            {
                                AccessToken = getOpenIdResponse.AccessToken,
                                OpenId = getOpenIdResponse.OpenId
                            });
                            if (getUnionIdResponse.OpenId == null || !string.IsNullOrEmpty(getUnionIdResponse.ErrMsg))
                            {
                                Logger.Debug($"获取openId错误，错误信息：{getUnionIdResponse.ErrMsg}");
                                if (getUnionIdResponse.ErrCode == "40029")
                                {
                                    result = JsonConvert.DeserializeObject<GetOfficeAccountUnionIdOutput>(await RedisHelper.GetAsync(cacheKey));
                                }
                            }
                            result = getUnionIdResponse;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new AbpException($"获取unionId错误", ex);
                    }

                    if (result != null)
                    {
                        lock (OAuthCodeCollectionLock)
                        {
                            RedisHelper.Set(cacheKey, JsonConvert.SerializeObject(getUnionIdResponse), 2 * 60 * 60);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("获取accessToken失败！", ex);
                }
            }
            catch (Exception ex)
            {
                throw new AbpException($"从微信获取用户信息失败", ex);
            }
            return result;
        }

        private async Task UpdateMpUserInfo(GetOfficeAccountUnionIdOutput getUnionIdResponse)
        {
            var find = await _mpuserRepository.FirstOrDefaultAsync(p => p.OpenId == getUnionIdResponse.OpenId);
            if (find == null)
            {
                find = new WechatMpUser
                {
                    HeadUrl = getUnionIdResponse.HeadUrl,
                    City = getUnionIdResponse.City,
                    Country = getUnionIdResponse.Country,
                    CreationTime = DateTime.Now,
                    Gender = getUnionIdResponse.Gender,
                    NickName = getUnionIdResponse.NickName,
                    OpenId = getUnionIdResponse.OpenId,
                    UnionId = getUnionIdResponse.UnionId,
                    Province = getUnionIdResponse.Province
                };
                await _mpuserRepository.InsertAsync(find);
            }
            else
            {
                if (find.UnionId != getUnionIdResponse.UnionId)
                {
                    find.UnionId = getUnionIdResponse.UnionId;
                }

                if (find.HeadUrl != getUnionIdResponse.HeadUrl)
                {
                    find.HeadUrl = getUnionIdResponse.HeadUrl;
                }

                if (find.NickName != getUnionIdResponse.NickName)
                {
                    find.NickName = getUnionIdResponse.NickName;
                }

                await _mpuserRepository.UpdateAsync(find);
            }
        }

        public GetAccessTokenWithCodeOutput GetAccessTokenWithCode(GetAccessTokenWithCodeDto input)
        {
            var response = _miniappService.GetWebAccessTokenWithCode(new GetAccessTokenWithCodeInput()
            {
                AppId = _wechatWebConfig.AppId,
                Secret = _wechatWebConfig.Secret,
                GrantType = _wechatWebConfig.Grant_Type,
                Code = input.Code,
            });

            return response;
        }

        public async Task<LoginSignUpSystemOutput> LoginSignUpSystem(LoginSignUpSystemInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId.Value);

            var output = new LoginSignUpSystemOutput();

            var wechatUser = await _wechatWebUserRepository.FirstOrDefaultAsync(w => w.OpenId == user.UserName);

            if (wechatUser == null)
            {
                output.IsRegister = false;
            }
            else
            {
                output.IsRegister = true;
            }
            var count = _wechatWebUserRepository.Count();

            output.UserCount = count;

            return output;
        }

        public async Task<LoginSignUpSystemOutput> RegisterSignUpSystem(RegisterSignUpSystemInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId.Value);

            if (user == null)
            {
                throw new Exception("Token有误");
            }

            var output = new LoginSignUpSystemOutput();

            var wechatUser = await _wechatWebUserRepository.FirstOrDefaultAsync(w => w.OpenId == user.UserName);

            if (wechatUser != null)
            {
                throw new Exception("您已注册，无需再注册");
            }

            var count = _wechatWebUserRepository.Count();

            await _wechatWebUserRepository.InsertAsync(new WeChatWebUser()
            {
                Age = input.Age,
                City = input.City,
                Profession = input.Profession,
                Gender = input.Gender,
                OpenId = user.UserName,
                UnionId = user.Surname,
                NickName = user.Name,
                UserName = input.UserName,
                Interest = input.Interest,
                State = UserState.SumitInfo,
                HeadUrl = user.PasswordResetCode
            });

            output.IsRegister = true;

            output.UserCount = _wechatWebUserRepository.Count();

            return output;
        }
    }
}
