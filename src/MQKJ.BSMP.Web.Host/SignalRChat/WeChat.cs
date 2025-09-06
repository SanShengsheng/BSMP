using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players.WeChat;
using MQKJ.BSMP.Players.WeChat.Dtos;
using MQKJ.BSMP.PropUseRecords;
using MQKJ.BSMP.WeChat;
using MQKJ.BSMP.WeChat.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.SignalRChat
{
    /// <summary>
    /// 通讯类 中心
    /// </summary>
    public class WeChat : Hub, ITransientDependency
    {
        /// <summary>
        /// 会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly IWeChatPlayerAppService _weChatPlayerAppService;

        //private const string receiveInfo = "receive";

        //private IDistributedCache _memoryCache;

        //private RedisHelpers.CustomRedisHelper _redisHelper;
        //key为questionId value为双方的答案id
        private static ConcurrentDictionary<int, List<int>> _submitAnswerDic { get; }

        private static ConcurrentDictionary<string, string> _connectionDic { get; }

        private static readonly object syncObj = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        public WeChat(IWeChatPlayerAppService weChatPlayerAppService)
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            _weChatPlayerAppService = weChatPlayerAppService;

            //_memoryCache = memoryCache;

            //_redisHelper = new RedisHelpers.CustomRedisHelper(memoryCache);

        }

        static WeChat()
        {
            _submitAnswerDic = new ConcurrentDictionary<int, List<int>>();

            _connectionDic = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// 连接的时候调用
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var http = Context.GetHttpContext();

            var gameId = http.Request.Query["gameId"].ToString();

            var playerId = http.Request.Query["playerId"].ToString();

            //var isTest = http.Request.Query["isTest"].ToString();

            //添加到缓冲
            //await _memoryCache.SetStringAsync(Context.ConnectionId, playerId);

            Logger.Debug($"player:{playerId} connect successful!");

            GetGameRoomStateInput input = new GetGameRoomStateInput();

            try
            {
                input.GameId = Guid.Parse(gameId);

                input.PlayerId = Guid.Parse(playerId);
            }
            catch (Exception exp)
            {

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    Error = new { errMsg = "参数错误", reason = exp }
                });

                Logger.Error($"player:{playerId}  connect err:{exp}");
            }

            var output = await _weChatPlayerAppService.GetGameTaskState(input);

            //房间满
            if (output == null)
            {
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.RoomFull,
                    Success = true,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.RoomFull)
                });
            }
            else //连接成功
            {
                //await _memoryCache.SetStringAsync(Context.ConnectionId, playerId);

                lock (syncObj)
                {
                    _connectionDic[Context.ConnectionId] = playerId;
                }

                if (Guid.Empty == output.OtherPlayerId) //被邀请人第一次进入游戏
                {
                    output.OtherIsOnLine = true;

                    await Clients.GroupExcept(gameId, Context.ConnectionId).SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                    {
                        Result = output,
                        MsgCode = MsgCodeEnum.OtherConnectSuccessFul,
                        Success = true,
                        Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OtherConnectSuccessFul)
                    });

                    await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                }
                else
                {
                    //var otherPlayerId = await _memoryCache.GetStringAsync(output.OtherPlayerId.ToString());

                    var otherPlayerId = "";

                    lock (syncObj)
                    {
                        otherPlayerId = _connectionDic.FirstOrDefault(c => c.Value == output.OtherPlayerId.ToString()).Value;
                    }

                    if (string.IsNullOrEmpty(otherPlayerId))//对方离线
                    {
                        //更新游戏状态为结束状态
                        await _weChatPlayerAppService.UpdateGameState(new EntityDto<Guid>() { Id = Guid.Parse(gameId) });

                        output.OtherIsOnLine = false;

                        await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                        {
                            Result = output,
                            MsgCode = MsgCodeEnum.ConnectSuccessFul,
                            Success = true,
                            Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.ConnectSuccessFul)
                        });

                    }
                    else
                    {
                        output.OtherIsOnLine = true;

                        await Clients.GroupExcept(gameId, Context.ConnectionId).SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                        {
                            Result = output,
                            MsgCode = MsgCodeEnum.OtherConnectSuccessFul,
                            Success = true,
                            Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OtherConnectSuccessFul)
                        });

                        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                    }
                }

                Logger.Debug($"player:{playerId} join group:{gameId}");

                await Groups.AddToGroupAsync(Context.ConnectionId, playerId);// 添加到分组，组名为玩家编号

                await base.OnConnectedAsync();

                Logger.Debug($"A player connected to Group：{gameId}  connect successful,connectionId:{Context.ConnectionId}");
            }
        }

        /// <summary>
        /// 失去连接的时候调用
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            var http = Context.GetHttpContext();

            var gameId = http.Request.Query["gameId"].ToString();

            var playerId = http.Request.Query["playerId"].ToString();

            Logger.Debug($"OnDisconnectedAsync ------- gameId:{gameId},playerId:{playerId}");

            //bool isRemoved;

            //object info = _redisHelper.Get(gameId);

            //string con = await _memoryCache.GetStringAsync(Context.ConnectionId);
            //从字典移除
            //从缓冲移除
            //await _memoryCache.RemoveAsync(Context.ConnectionId);

            //2 判断是否有这个房间
            Logger.Debug($"player:{playerId}  disconnected");
            await Clients.GroupExcept(gameId, new[] { Context.ConnectionId }).SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
            {
                Result = new { GameId = gameId, PlayerId = playerId },
                MsgCode = MsgCodeEnum.OtherOffLine,
                Success = true,
                Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OtherOffLine)
            });

            //从redis移除
            //_redisHelper.Remove(gameId);
            lock (syncObj)
            {
                _connectionDic.TryRemove(Context.ConnectionId,out playerId);
            }
            //从组里移除
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);

            Logger.Debug($"A client disconnected from MyChatHub: {Context.ConnectionId},reason：{exception}");
             
        }
         
        /// <summary>
        /// 获取对方的时间
        /// </summary>
        /// <param name="gameInfoJson"></param>
        /// <returns></returns>
        public async Task GetOtherGameState(string gameInfoJson)
        {
            var gameInfoObj = JObject.Parse(gameInfoJson);
            var gameId = gameInfoObj.SelectToken("roomInfo.gameId").ToString();
            await Clients.GroupExcept(gameId, Context.ConnectionId).SendAsync(StaticGetMethodName.GetOtherGameState,gameInfoObj);
        }

        /// <summary>
        /// 断线重连
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        //public async Task ReConnection(string playerId)
        //{
        //    CreateConnectionDto input = new CreateConnectionDto();

        //    input.GameId = Guid.Parse(playerId);

        //    input.ConnectionId = Context.ConnectionId;

        //    await _weChatPlayerAppService.ReConnection(input);
        //}

        /// <summary>
        /// 发起邀请
        /// </summary>
        /// /// <param name="gameId"></param>
        /// <param name="relation"></param>
        /// <param name="seekType"></param>
        /// <param name="gameBout"></param>
        /// <param name="convention"></param>
        /// <param name="playerGuid"></param>
        /// <returns></returns>
        public async Task InviteFriend(string gameId, string playerGuid)
        {
            Logger.Debug($"InviteFriend ------- gameId:{gameId},playerId:{playerGuid}");

            var connectionId = Context.ConnectionId;

            Guid? output = Guid.Empty;
            try
            {
                //房间存在说明之前有过则重新加入到新的组 
                output = await _weChatPlayerAppService.GameIsExist(new EntityDto<Guid>()
                {
                    Id = Guid.Parse(gameId)
                });
            }
            catch (Exception exp)
            {
                await Clients.Client(connectionId).SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }

            if (output != null)
            {
                await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.InviteFriend, new SocketMessage()
                {
                    Result = playerGuid,
                    Success = true,
                    MsgCode = MsgCodeEnum.InviteFriend
                });
                var otherPlayerId = output.Value.ToString();

                //var otherConnectionId = await _memoryCache.GetStringAsync(otherPlayerId);

                //await Groups.RemoveFromGroupAsync(otherConnectionId, gameId);
            }
            else
            {
                await Clients.Group(gameId).SendAsync(StaticGetMethodName.InviteFriend, new SocketMessage()
                {
                    Result = playerGuid,
                    Success = true,
                    MsgCode = MsgCodeEnum.InviteFriend
                });
            }

            await Groups.RemoveFromGroupAsync(connectionId, gameId);
        }

        //public Task GetGameInfo()

        //public async Task InviteFriend(string gameId, int relation, int seekType, int gameBout, string convention, string playerGuid)
        //{
        //    var connectionId = OnlineClientsDic.FirstOrDefault(c => c.Value == playerGuid).Key;
        //    if (connectionId == null)
        //    {
        //        Logger.Error($"player:{playerGuid} disconnected");
        //    }
        //    else
        //    {
        //        Logger.Info("InviteFriend:" + relation + "---" + seekType + "------" + gameBout + "---" + convention + "---" + playerGuid);

        //        InviteFriendInput input = new InviteFriendInput()
        //        {
        //            Id = Guid.Parse(gameId),
        //            RelationDegree = relation,
        //            SeekType = (SeekType)seekType,
        //            TaskType = (TaskType)gameBout,
        //            AppointmentContent = convention,
        //            InviterPlayerId = Guid.Parse(playerGuid)
        //        };
        //        var response = await _weChatPlayerAppService.CreateInvite(input);

        //        string tempName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        //        string methedName = tempName.Replace("<", "").Remove(tempName.IndexOf('>') - 1);

        //        await Clients.Client(connectionId).SendAsync(methedName, new SocketMessage
        //        {
        //            MsgCode = MsgCodeEnum.邀请,
        //            Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.邀请)
        //        });
        //    }
        //}

        /// <summary>
        /// 游戏初始化(被邀请方点击邀请链接)
        /// <summary>
        /// <param name="gameId"></param>
        /// <param name="inviterId">邀请方id</param>
        /// <param name="inviteeId">被邀请方id</param>
        /// <param name="seekType"></param>
        /// <param name="appointmentContent"></param>
        /// <param name="relationDegree"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public async Task GameInitialization(string gameId, string inviterId, string inviteeId, int seekType, string appointmentContent, int relationDegree, int taskType,int appointmentType)
        {
            Logger.Debug($"GameInitialization ----- GameId:{gameId},inviterId:{inviterId},invieeId:{inviteeId},seektype:{seekType},appointmentContent:{appointmentContent},relation:{relationDegree},tasktype:{taskType}");

            //var connectionId = OnlineClientsDic.FirstOrDefault(c => c.Value == inviterId).Key;

            var selfConnectionId = this.Context.ConnectionId;   //OnlineClientsDic.FirstOrDefault(c => c.Value == inviteeId).Key;

            InitializeGameInput input = new InitializeGameInput();
            try
            {
                input.Id = Guid.Parse(gameId);
                input.InviterPlayerId = Guid.Parse(inviterId);
                input.InviteePlayerId = Guid.Parse(inviteeId);
                input.SeekType = (SeekType)seekType;
                input.AppointmentContent = appointmentContent;
                input.RelationDegree = relationDegree;
                input.TaskType = (TaskType)taskType;
                input.AppointmentType = appointmentType;
            }
            catch (Exception exp)
            {
                Logger.Error($"GameInitialization：{exp}");
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
                return;
            }

            var output = await _weChatPlayerAppService.InitializeGame(input);

            //被邀请方进来的 1、当两个人同时进入邀请页然后去邀请对方 2、当在玩一次的时候
            await Groups.AddToGroupAsync(selfConnectionId, gameId);

            if (inviterId == inviteeId) //邀请方点链接进来的
            {
                await Clients.Caller.SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                {
                    Result = output,
                    MsgCode = output.MsgCodeEnum,
                    Remark = EnumHelper.EnumHelper.GetDescription(output.MsgCodeEnum),
                    Success = true,
                    Error = null
                });
                return;
            }

            if (output.MsgCodeEnum == MsgCodeEnum.RoomFull)
            {
                await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.RoomFull,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.RoomFull),
                    Success = true,
                    Error = null
                });

                await Groups.RemoveFromGroupAsync(selfConnectionId, gameId);
            }
            else
            {
                if (output.MsgCodeEnum == MsgCodeEnum.GameOver)
                {
                    await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                    {
                        MsgCode = MsgCodeEnum.GameOver,
                        Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.GameOver),
                        Success = true,
                        Error = null
                    });
                }
                else
                {
                    await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                    {
                        Result = output,
                        MsgCode = output.MsgCodeEnum,
                        Remark = EnumHelper.EnumHelper.GetDescription(output.MsgCodeEnum),
                        Success = true,
                        Error = null
                    });
                }
            }

            Logger.Debug($"selfConnectionId:{selfConnectionId},playerid:{inviteeId}");

        }

        /// <summary>
        /// 游戏初始化(被邀请方点击邀请链接)
        /// <summary>
        /// <param name="gameId"></param>
        /// <param name="inviterId">邀请方id</param>
        /// <param name="inviteeId">被邀请方id</param>
        /// <param name="seekType"></param>
        /// <param name="appointmentContent"></param>
        /// <param name="relationDegree"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public async Task GameInitializationEndLess(string gameId, string inviterId, string inviteeId, int seekType, string appointmentContent, int relationDegree, int taskType, int gameType)
        {
            Logger.Debug($"GameInitialization ----- GameId:{gameId},inviterId:{inviterId},invieeId:{inviteeId},seektype:{seekType},appointmentContent:{appointmentContent},relation:{relationDegree},tasktype:{taskType}");

            //var connectionId = OnlineClientsDic.FirstOrDefault(c => c.Value == inviterId).Key;

            var selfConnectionId = this.Context.ConnectionId;   //OnlineClientsDic.FirstOrDefault(c => c.Value == inviteeId).Key;

            InitializeGameInput input = new InitializeGameInput();
            try
            {
                input.Id = Guid.Parse(gameId);
                input.InviterPlayerId = Guid.Parse(inviterId);
                input.InviteePlayerId = Guid.Parse(inviteeId);
                input.SeekType = (SeekType)seekType;
                input.AppointmentContent = appointmentContent;
                input.RelationDegree = relationDegree;
                input.TaskType = (TaskType)taskType;
                input.GameType = (GameType)gameType;
            }
            catch (Exception exp)
            {
                Logger.Error($"GameInitializationEndLess：{exp}");
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
                return;
            }

            var output = await _weChatPlayerAppService.InitializeGameEndLess(input);

            //被邀请方进来的 1、当两个人同时进入邀请页然后去邀请对方 2、当在玩一次的时候
            await Groups.AddToGroupAsync(selfConnectionId, gameId);

            if (inviterId == inviteeId) //邀请方点链接进来的
            {
                await Clients.Caller.SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                {
                    Result = output,
                    MsgCode = output.MsgCodeEnum,
                    Remark = EnumHelper.EnumHelper.GetDescription(output.MsgCodeEnum),
                    Success = true,
                    Error = null
                });
                return;
            }

            if (output.MsgCodeEnum == MsgCodeEnum.RoomFull)
            {
                await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.RoomFull,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.RoomFull),
                    Success = true,
                    Error = null
                });

                await Groups.RemoveFromGroupAsync(selfConnectionId, gameId);
            }
            else
            {
                if (output.MsgCodeEnum == MsgCodeEnum.GameOver)
                {
                    await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                    {
                        MsgCode = MsgCodeEnum.GameOver,
                        Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.GameOver),
                        Success = true,
                        Error = null
                    });
                }
                else
                {
                    await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameInitialization, new SocketMessage()
                    {
                        Result = output,
                        MsgCode = output.MsgCodeEnum,
                        Remark = EnumHelper.EnumHelper.GetDescription(output.MsgCodeEnum),
                        Success = true,
                        Error = null
                    });
                }
            }

            Logger.Debug($"selfConnectionId:{selfConnectionId},playerid:{inviteeId}");

        }

        /// <summary>
        /// 游戏准备 被邀请方点击准备
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public async Task GameReady(string gameId, string appointment)
        {
            Logger.Debug($"GameReady  ------- gameId:{gameId}");

            ReadyGameInput input = new ReadyGameInput();
            try
            {
                input.Id = Guid.Parse(gameId);
            }
            catch (Exception exp)
            {
                Logger.Error($"GameReady：{exp}");

                await Clients.Group(gameId).SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }
            var headurl = await _weChatPlayerAppService.ReadyGame(input);

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.GameReady, new SocketMessage { MsgCode = MsgCodeEnum.ClickReady, Remark = "被邀请方点击准备", Result = new { Appointment = appointment, inviteeUrl = headurl } });

        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="inviterId"></param>
        /// <param name="gameType"></param>
        //public async Task GameStart(string gameId, string inviterId)
        //{
        //    Logger.Info($"GameStart ------- gameId:{gameId},playerId:{inviterId}");

        //    string connectionId = Context.ConnectionId;

        //    StartGameInput input = new StartGameInput();
        //    try
        //    {
        //        input.GameId = Guid.Parse(gameId);
        //        input.PlayerId = Guid.Parse(inviterId);
        //        //input.GameType = (GameType)gameType;
        //    }
        //    catch (Exception exp)
        //    {
        //        await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
        //        {
        //            MsgCode = MsgCodeEnum.异常,
        //            Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.异常),
        //            Success = true,
        //            Error = exp
        //        });
        //    }

        //    var output = await _weChatPlayerAppService.StartGame(input);

        //    var inviterMsg = new SocketMessage
        //    {
        //        Result = output.InviterQuestions,
        //        MsgCode = MsgCodeEnum.邀请方点击开始,
        //        Remark = "邀请方点击开始"
        //    };
        //    var inviteeMsg = new SocketMessage
        //    {
        //        Result = output.InviteeQuestions,
        //        MsgCode = MsgCodeEnum.邀请方点击开始,
        //        Remark = "邀请方点击开始"
        //    };

        //    await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.GameStart, inviteeMsg);//发送给被邀请人

        //    await Clients.Caller.SendAsync(StaticGetMethodName.GameStart, inviterMsg);

        //}

        /// <summary>
        /// 分发题目 flag模式
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="inviterId"></param>
        ///// <param name="gameType"></param>
        /// <returns></returns>
        public async Task DistributeQuestions(string gameId, string inviterId)
        {
            Logger.Debug($"DistributeQuestions ------- gameId:{gameId},playerId:{inviterId}");

            string connectionId = Context.ConnectionId;


            StartGameInput input = new StartGameInput();
            try
            {
                input.GameId = Guid.Parse(gameId);
                input.PlayerId = Guid.Parse(inviterId);
                //input.GameType = (GameType)gameType;
            }
            catch (Exception exp)
            {
                Logger.Error($"DistributeQuestions：{exp}");
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }

            var output = await _weChatPlayerAppService.DistributeQuestions(input);

            var inviterMsg = new SocketMessage
            {
                Result = output.InviterQuestions,
                MsgCode = MsgCodeEnum.DistributeQuestion,
                Remark = "分发题目"
            };
            var inviteeMsg = new SocketMessage
            {
                Result = output.InviteeQuestions,
                MsgCode = MsgCodeEnum.DistributeQuestion,
                Remark = "分发题目"
            };

            //if (questions == null)
            //{
            //    string questionStr = null;
            //    try
            //    {
            //        questionStr = JsonConvert.SerializeObject(inviterMsg);
            //    }
            //    catch (Exception exp)
            //    {

            //    }
            //    await _memoryCache.SetStringAsync(gameId, questionStr);
            //}

            await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.DistributeQuestions, inviteeMsg);//发送给被邀请人

            await Clients.Caller.SendAsync(StaticGetMethodName.DistributeQuestions, inviterMsg);
        }

        /// <summary>
        /// 分发题目 闯关模式
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="inviterId"></param>
        ///// <param name="gameType"></param>
        /// <returns></returns>
        public async Task DistributeQuestionsEndless(string gameId, string inviterId)
        {
            Logger.Debug($"DistributeQuestionsEndless ------- gameId:{gameId},playerId:{inviterId}");

            string connectionId = Context.ConnectionId;


            StartGameInput input = new StartGameInput();
            try
            {
                input.GameId = Guid.Parse(gameId);
                input.PlayerId = Guid.Parse(inviterId);
                //input.GameType = (GameType)gameType;
            }
            catch (Exception exp)
            {
                Logger.Error($"DistributeQuestionsEndless：{exp}");

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }

            var output = await _weChatPlayerAppService.DistributeQuestionsEndless(input);

            var inviterMsg = new SocketMessage
            {
                Result = output.InviterQuestions,
                MsgCode = MsgCodeEnum.DistributeQuestion,
                Remark = "分发题目"
            };
            var inviteeMsg = new SocketMessage
            {
                Result = output.InviteeQuestions,
                MsgCode = MsgCodeEnum.DistributeQuestion,
                Remark = "分发题目"
            };

            //if (questions == null)
            //{
            //    string questionStr = null;
            //    try
            //    {
            //        questionStr = JsonConvert.SerializeObject(inviterMsg);
            //    }
            //    catch (Exception exp)
            //    {

            //    }
            //    await _memoryCache.SetStringAsync(gameId, questionStr);
            //}

            await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.DistributeQuestions, inviteeMsg);//发送给被邀请人

            await Clients.Caller.SendAsync(StaticGetMethodName.DistributeQuestions, inviterMsg);
        }
        /// <summary>
        /// 进入游戏
        /// </summary>
        /// <param name="GameId"></param>
        /// <returns></returns>
        public async Task EnterGame(string GameId, string playerId, GameType gameType)
        {
            var input = new EnterGameInput();

            try
            {
                input.GameId = Guid.Parse(GameId);
                input.PlayerId = Guid.Parse(playerId);
                input.GameType = gameType;
            }
            catch (Exception exp)
            {
                Logger.Error($"EnterGame:{exp}");

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
                return;
            }

            var result = await _weChatPlayerAppService.EnterGame(input);

            await Clients.Group(GameId).SendAsync(StaticGetMethodName.EnterGame, new SocketMessage()
            {
                Result = result,
                MsgCode = MsgCodeEnum.ClickStart,
                Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.ClickStart),
                Success = true,
            });


        }

        /// <summary>
        /// 提交答案
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="questionId"></param>
        /// <param name="answerId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task Submit(string gameId, int questionId, int answerId, string playerId)
        {
            Logger.Debug($"Submit ----- GameId:{gameId},questionId:{questionId},answerId:{answerId},playerId:{playerId}");

            var output = new SubmitAnswerOutputDto();

            try
            {
                var input = new SubmitAnswerInputDto() { GameId = Guid.Parse(gameId), PlayerId = Guid.Parse(playerId), QuestionId = questionId, AnswerId = answerId };

                output = await _weChatPlayerAppService.Submit(input);
            }
            catch (Exception exp)
            {
                Logger.Error($"Submit:{exp}");

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.Submit, new SocketMessage { Result = output, Success = true, MsgCode = MsgCodeEnum.CommitAnswer, Remark = "提交答案", });
        }
        /// <summary>
        /// 提交答案-闯关模式
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="questionId"></param>
        /// <param name="answerId"></param>
        /// <param name="playerId"></param>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public async Task Submit_EndLess(string gameId, int questionId, int answerId, string playerId, int gameType, string inviteePlayerId)
        {
            Logger.Debug($"CallerSubmit ----- GameId:{gameId},questionId:{questionId},answerId:{answerId},playerId:{playerId}");

            var output = new SubmitAnswerOutputDto();

            try
            {
                var input = new SubmitAnswerInputDto() { GameId = Guid.Parse(gameId), PlayerId = Guid.Parse(playerId), QuestionId = questionId, AnswerId = answerId, GameType = (GameType)gameType, InviteePlayerId = Guid.Parse(inviteePlayerId) };

                output = _weChatPlayerAppService.Submit_EndLess(input);
            }
            catch (Exception exp)
            {
                Logger.Error($"Submit_EndLess:{exp}");
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.Submit, new SocketMessage { Result = output, Success = true, MsgCode = MsgCodeEnum.CommitAnswer, Remark = "提交答案", });
        }

        /// <summary>
        /// 提交答案-闯关模式
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="questionId"></param>
        /// <param name="answerId"></param>
        /// <param name="playerId"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task Submit_EndLess_1(string gameId, string playerId, int questionId, int answerId,int sort)
        {
            Logger.Debug($"CallerSubmit ----- GameId:{gameId},questionId:{questionId},answerId:{answerId},playerId:{playerId},sort:{sort}");

            var output = new SubmitAnswerOutputDto();

            try
            {
                var input = new SubmitAnswerInputDto() { GameId = Guid.Parse(gameId), PlayerId = Guid.Parse(playerId), QuestionId = questionId, AnswerId = answerId,Sort = sort  };

                output = await _weChatPlayerAppService.Submit_EndLess_1(input);

                if (output != null)
                {
                    await Clients.Group(gameId).SendAsync(StaticGetMethodName.Submit, new SocketMessage
                    {
                        Result = output,
                        Success = true,
                        MsgCode = MsgCodeEnum.CommitAnswer,
                        Remark = "提交答案",
                    });
                }
            }
            catch (Exception exp)
            {
                Logger.Error($"Submit_EndLess:{exp}");
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }
        }
        /// <summary>
        /// 使用道具
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        public async Task UseProp(string gameId, string playerId, int propType)
        {
            Logger.Debug($"UseProp ------ GameId:{gameId},playerId:{playerId},propType:{propType}");

            var output = new UsePropOutput();

            try
            {
                var input = new UsePropDto() { GameId = Guid.Parse(gameId), PlayerId = Guid.Parse(playerId), PropType = (PropType)propType };

                output = await _weChatPlayerAppService.UseProp(input);
            }
            catch (Exception exp)
            {
                Logger.Error($"UseProp:{exp}");

                await Clients.Group(gameId).SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.UseProp, new SocketMessage()
            {
                Result = output,
                Success = true,
                MsgCode = output.MsgCodeEnum,
                Remark = EnumHelper.EnumHelper.GetDescription(output.MsgCodeEnum)
            });
        }

        /// <summary>
        /// 使用复活卡
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        //public async Task UseResurrectionCard(string playerId, int propType)
        //{
        //    UseResurrectionCardDto dto = new UseResurrectionCardDto()
        //    {
        //        PlayerId = Guid.Parse(playerId),
        //        PropType = (PropType)propType
        //    };
        //    await _weChatPlayerAppService.UseResurrectionCard(dto);
        //}

        /// <summary>
        /// 答题超时
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task AnswerQuestionTimeout(string gameId, string playerId)
        {
            var input = new AnswerTimeoutDto();
            try
            {
                input.GameId = Guid.Parse(gameId);

                input.PlayerId = Guid.Parse(playerId);
            }
            catch (Exception exp)
            {
                Logger.Error($"AnswerQuestionTimeout:{exp}");

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
                return;
            }

            await _weChatPlayerAppService.AnswerTimeout(input);

            string connectionId = Context.ConnectionId;

            await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.AnswerQuestionTimeout, new SocketMessage()
            {
                Result = null,
                MsgCode = MsgCodeEnum.AnswerTimeout,
                Success = true,
                Remark = ""
            });
        }

        /// <summary>
        /// 发送表情包
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task SendEmoticon(string gameId, string playerId, int code)
        {

            Logger.Debug("SendEmoticon-----" + "GameId:" + gameId + "---PlayerId:" + playerId + "Code:" + code);

            var input = new SendEmoticonDto();
            try
            {
                input.GameTaskId = Guid.Parse(gameId);

                input.PlayerId = Guid.Parse(playerId);
                input.Code = code;
            }
            catch (Exception exp)
            {
                Logger.Error($"SendEmoticon:{exp}");

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
                return;
            }

            var output = await _weChatPlayerAppService.SendEmoticon(input);

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.SendEmoticon, new SocketMessage()
            {
                Result = output,
                MsgCode = MsgCodeEnum.SendEmoticon,
                Success = true,
                Remark = "发送表情成功"
            });
        }

        /// <summary>
        /// 发送语音
        /// </summary>
        /// <param name="gameId"></param>
        ///<param name="src"></param>
        ///<param name="word">文字</param>
        /// <returns></returns>
        public async Task SendVoice(string gameId, string src, string word)
        {
            string connectionId = Context.ConnectionId;

            await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.SendVoice, new SocketMessage()
            {
                Result = new { Src = src, AudioText = word },
                MsgCode = MsgCodeEnum.SendVoice,
                Success = true,
                Remark = "发送语音成功"
            });
        }

        /// <summary>
        /// 玩家在玩一次
        /// </summary>
        /// <param name="oldGameId"></param>
        /// <param name="newGameId"></param>
        /// <param name="playerId"></param>
        public async Task PlayerGameAgain(string oldGameId, string newGameId, string playerId)
        {
            Logger.Debug($"CallPlayerGameAgain：{oldGameId},playerId:{playerId}");

            string connectionId = Context.ConnectionId; /*OnlineClientsDic.FirstOrDefault(x => x.Value == playerId).Key*/;

            //通知
            await Clients.GroupExcept(oldGameId, connectionId).SendAsync(StaticGetMethodName.PlayerGameAgain, new SocketMessage()
            {
                Result = new { GameId = newGameId, OtherPlayerId = playerId },
                MsgCode = MsgCodeEnum.PlayerAgain,
                Success = true,
                Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.PlayerAgain)
            });

            //从原来的组移除掉
            await Groups.RemoveFromGroupAsync(connectionId, oldGameId);

            //加到新的组里
            await Groups.AddToGroupAsync(connectionId, newGameId);
        }

        /// <summary>
        /// 对方同意 该接口暂时不用  初始化游戏的方法替代
        /// </summary>
        /// <param name="oldGameId"></param>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <param name="otherPlayerId"></param>
        /// <returns></returns>
        public async Task OtherAgree(string oldGameId, string gameId, string playerId, string otherPlayerId)
        {
            Logger.Debug($"CallOtherAgree----------GameId：{gameId},playerId:{playerId}");

            var output = await _weChatPlayerAppService.OtherAgree(new OtherAgreeOrRefuseDto()
            {
                OldGameId = Guid.Parse(oldGameId),
                GameId = Guid.Parse(gameId),
                PlayerId = Guid.Parse(playerId),
                OtherPlayerId = Guid.Parse(otherPlayerId)
            });
            if (output == null)
            {
                Logger.Debug("No Room---------------------------------------");
            }
            else
            {

                string connectionId = Context.ConnectionId;

                await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.OtherAgree, new SocketMessage()
                {
                    Result = output,
                    MsgCode = MsgCodeEnum.OtherAgree,
                    Success = true,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OtherAgree)
                });
            }
        }

        /// <summary>
        /// 对方拒绝
        /// </summary>
        /// <param name="gameId">房间id</param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task OtherRefuse(string gameId, string playerId)
        {
            //var output = await _weChatPlayerAppService.OtherRefuse(new OtherAgreeOrRefuseDto()
            //{
            //    GameId = Guid.Parse(gameId),
            //    PlayerId = Guid.Parse(playerId)
            //});

            Logger.Debug($"CallOtherRefuse------- GameId：{gameId},playerId:{playerId}");

            //string connectionId = OnlineClientsDic.FirstOrDefault(x => x.Value == playerId).Key;

            //string otherConnectionId = OnlineClientsDic.FirstOrDefault(x => x.Value == playerId).Key;

            //string otherPlayerId = output.OtherPlayerId.ToString();

            string connectionId = Context.ConnectionId;

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.OtherRefuse, new SocketMessage() //组里只有对方
            {
                Result = null,
                MsgCode = MsgCodeEnum.OtherRefuse,
                Success = true,
                Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OtherRefuse)
            });
            //remove from group
            //await Groups.RemoveFromGroupAsync(connectionId, gameId);

            //await Groups.RemoveFromGroupAsync(otherConnectionId, gameId);
        }

        /// <summary>
        /// 从组移除
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        public async Task RemoveFromGroup(string gameId, string playerId)
        {
            string connectionId = Context.ConnectionId/*OnlineClientsDic.FirstOrDefault(x => x.Value == playerId).Key*/;

            //string otherConnectionId = OnlineClientsDic.FirstOrDefault(x => x.Value == OtherPlayerId).Key;



            await _weChatPlayerAppService.UpdateGameState(new EntityDto<Guid> { Id = Guid.Parse(gameId) });

            await Clients.GroupExcept(gameId, connectionId).SendAsync(StaticGetMethodName.RemoveFromGroup, new SocketMessage()
            {
                Success = true,
                MsgCode = MsgCodeEnum.OhterLeaveRoom
            });

            await Groups.RemoveFromGroupAsync(connectionId, gameId);

            //await Groups.RemoveFromGroupAsync(otherConnectionId, gameId);
        }

        /// <summary>
        /// 开始抽奖 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task StartPrizeDraw(string gameId,string playerId)
        {
            if (string.IsNullOrEmpty(gameId))
            {
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Error = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr)
                });
            }
            else
            {
                await _weChatPlayerAppService.StartPrizeDraw(new StartPrizeDrawInput()
                {
                    GameId = Guid.Parse(gameId),
                    PlayerId = Guid.Parse(playerId),
                });
                await Clients.GroupExcept(gameId, Context.ConnectionId).SendAsync(StaticGetMethodName.StartPrizeDraw, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.StartPrize,
                    Error = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.StartPrize)
                });

            }
        }

        /// <summary>
        /// 抽奖结果
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="appointment"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        //[ApiVersion()]
        public async Task PrizeDrawResult(string gameId, string appointment, string playerId)
        {
            if (string.IsNullOrEmpty(gameId))
            {
                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Error = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr)
                });
            }
            else
            {
                await _weChatPlayerAppService.PrizeDrawResult(new PrizeDrawResultInput()
                {
                    GameId = Guid.Parse(gameId),
                    PlayerId = Guid.Parse(playerId),
                    Appointment = appointment
                });
                await Clients.Group(gameId).SendAsync(StaticGetMethodName.PrizeDrawResult, new SocketMessage()
                {
                    Result = new { Appointment = appointment },
                    MsgCode = MsgCodeEnum.PrizeResult,
                    Error = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.PrizeResult)
                });
                //await Clients.GroupExcept(gameId, Context.ConnectionId).SendAsync(StaticGetMethodName.PrizeDrawResult, new SocketMessage()
                //{
                //    Result = new { Appointment = appointment },
                //    MsgCode = MsgCodeEnum.PrizeResult,
                //    Error = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.PrizeResult)
                //});

            }
        }

        /// <summary>
        /// 设置约定
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public async Task SetAppointment(string gameId, string appointment)
        {
            Logger.Debug($"SetAppointment  ------- gameId:{gameId}");

            ReadyGameInput input = new ReadyGameInput();
            try
            {
                input.Id = Guid.Parse(gameId);
            }
            catch (Exception exp)
            {
                Logger.Error($"SetAppointment:{exp}");

                await Clients.Caller.SendAsync(StaticGetMethodName.SystemeInfo, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.OccurErr,
                    Remark = EnumHelper.EnumHelper.GetDescription(MsgCodeEnum.OccurErr),
                    Success = true,
                    Error = exp
                });
            }
            var headurl = await _weChatPlayerAppService.ReadyGame(input);

            await Clients.Group(gameId).SendAsync(StaticGetMethodName.SetAppointment, new SocketMessage { MsgCode = MsgCodeEnum.ClickReady, Remark = "被邀请方点击准备", Result = new { Appointment = appointment, inviteeUrl = headurl } });
        }

        /// <summary>
        /// 呼叫接待员
        /// </summary>
        /// <param name="playerId">接待员编号</param>
        ///<param name="inviteUrl">邀请链接</param>
        /// <returns></returns>
        public async Task CallWaiter(Guid playerId, string inviteUrl)
        {
            await Clients.Group(playerId.ToString()).SendAsync(StaticGetMethodName.CallWaiter, new SocketMessage()
            {
                MsgCode = MsgCodeEnum.CallReceptionist,
                Remark = "呼叫接待员",
                Result = new
                {
                    inviteUrl = inviteUrl
                }
            });
        }
        /// <summary>
        /// 通知对方继续闯关（闯关模式）
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task ContinueAnswer(Guid gameId, Guid playerId)
        {
            var isStaminaEnough = _weChatPlayerAppService.CheckStamina(playerId);
            if (isStaminaEnough)
            {
                var player = await _weChatPlayerAppService.MinusStamina(playerId);

                await Clients.Group(gameId.ToString()).SendAsync(StaticGetMethodName.ContinueAnswer, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.ContinueAnswer,
                    Remark = "继续答题",
                    Result = player
                });
            }
            else
            {
                await Clients.Group(gameId.ToString()).SendAsync(StaticGetMethodName.Feeble, new SocketMessage()
                {
                    MsgCode = MsgCodeEnum.Feeble,
                    Remark = "体力不够啦~",
                    Result = null
                });
            }
        }
    }
}
