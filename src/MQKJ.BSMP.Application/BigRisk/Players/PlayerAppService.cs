using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Json;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.BigRisk.Players.Dtos;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players.Authorization;
using MQKJ.BSMP.Players.Dtos;
using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.StaminaRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Players
{
    /// <summary>
    /// Player应用层服务的接口实现方法
    /// </summary>

    public class PlayerAppService : BSMPAppServiceBase, IPlayerAppService
    {
        private readonly IRepository<Player, Guid> _playerRepository;
        public IEventBus EventBus { get; set; }
        private readonly IBonusPointAppService _bonusPointAppService;

        private readonly IRepository<GameTask, Guid> _gameTaskRepository;

        private readonly IRepository<StaminaRecord, Guid> _staminaRecordRepository;

        private readonly IStaminaRecordAppService _staminaRecordAppService;

        private readonly IRepository<MqAgent> _mqAgentRepository;
        private readonly IRepository<PlayerExtension> _playerExtensionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlayerAppService(
            IRepository<Player, Guid> playerRepository
            , IRepository<PlayerExtension> playerExtensionRepository
            , IBonusPointAppService bonusPointAppService
            , IRepository<GameTask, Guid> gameTaskRepository
            , IStaminaRecordAppService staminaRecordAppService
            , IRepository<StaminaRecord, Guid> staminaRecordRepository
            , IRepository<MqAgent> mqAgentRepository)
        {
            _playerRepository = playerRepository;
            _bonusPointAppService = bonusPointAppService;
            EventBus = NullEventBus.Instance;
            _gameTaskRepository = gameTaskRepository;
            _staminaRecordAppService = staminaRecordAppService;
            _staminaRecordRepository = staminaRecordRepository;

            _mqAgentRepository = mqAgentRepository;
            _playerExtensionRepository = playerExtensionRepository;
        }


        /// <summary>
        /// 获取Player的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PlayerListDto>> GetPagedPlayers(GetPlayersInput input)
        {

            // TODO:根据传入的参数添加过滤条件
            // TODO:根据传入的参数添加过滤条件
            var query = _playerRepository.GetAll()
                        .WhereIf(!string.IsNullOrEmpty(input.NickName), n => n.NickName.Contains(input.NickName))
                        .WhereIf(input.Gender != 0, g => g.Gender == input.Gender)
                        .WhereIf(input.AgeRange != 0, a => a.AgeRange == input.AgeRange)
                        .WhereIf(input.StartTime != Convert.ToDateTime("0001/1/1 0:00:00") && input.EndTime != Convert.ToDateTime("0001/1/1 0:00:00"), p => p.CreationTime >= input.StartTime && p.CreationTime < input.EndTime)
                        .OrderByDescending(d => d.CreationTime).AsNoTracking();

            var playerCount = await query.CountAsync();

            var players = await query.PageBy(input).ToListAsync();

            var playerListDtos = players.MapTo<List<PlayerListDto>>();

            return new PagedResultDto<PlayerListDto>(
                        playerCount,
                        playerListDtos
                );
        }


        /// <summary>
        /// 通过指定id获取PlayerListDto信息
        /// </summary>
        public async Task<PlayerListDto> GetPlayerByIdAsync(EntityDto<Guid> input)
        {
            var entity = await _playerRepository.GetAsync(input.Id);

            return entity.MapTo<PlayerListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPlayerForEditOutput> GetPlayerForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetPlayerForEditOutput();
            PlayerEditDto playerEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _playerRepository.GetAsync(input.Id.Value);

                playerEditDto = entity.MapTo<PlayerEditDto>();

                //playerEditDto = ObjectMapper.Map<List <playerEditDto>>(entity);
            }
            else
            {
                playerEditDto = new PlayerEditDto();
            }

            output.Player = playerEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Player的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdatePlayer(CreateOrUpdatePlayerInput input)
        {

            if (input.Player.Id.HasValue)
            {
                await UpdatePlayerAsync(input.Player);
            }
            else
            {
                await CreatePlayerAsync(input.Player);
            }
        }


        /// <summary>
        /// 新增Player
        /// </summary>
        [AbpAuthorize(PlayerAppPermissions.Player_CreatePlayer)]
        protected virtual async Task<PlayerEditDto> CreatePlayerAsync(PlayerEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<Player>(input);

            entity = await _playerRepository.InsertAsync(entity);
            return entity.MapTo<PlayerEditDto>();
        }

        /// <summary>
        /// 编辑Player
        /// </summary>
        [AbpAuthorize(PlayerAppPermissions.Player_EditPlayer)]
        protected virtual async Task UpdatePlayerAsync(PlayerEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _playerRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _playerRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Player信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PlayerAppPermissions.Player_DeletePlayer)]
        public async Task DeletePlayer(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _playerRepository.DeleteAsync(input.Id);
        }

        [HttpGet]
        public async Task DummyDeletePlayer(EntityDto<Guid> input)
        {
            var entity = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.Id);

            entity.IsDeleted = true;

            await _playerRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 批量删除Player的方法
        /// </summary>
        [AbpAuthorize(PlayerAppPermissions.Player_DeletePlayer)]
        public async Task BatchDeletePlayersAsync(List<Guid> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _playerRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task UpdatePlayerState(EntityDto<Guid> input)
        {
            var entity = await _playerRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity.State == (int)PlayerState.Freeze)
            {
                entity.State = (int)PlayerState.Unauthorize;
            }
            else if (entity.State == (int)PlayerState.Unauthorize || entity.State == (int)PlayerState.Authorized)
            {
                entity.State = (int)PlayerState.Freeze;
            }

            await _playerRepository.UpdateAsync(entity);
        }

        public async Task<Player> PlayerLogin(GetPlayerInput input)
        {
            var find = await _playerRepository.GetAll()
               .Include(p => p.PlayerExtension)
               .WhereIf(input.TenantId.HasValue, p => p.TenantId == input.TenantId.Value)
               .WhereIf(!String.IsNullOrEmpty(input.OpenId), q => q.OpenId == input.OpenId)
               .FirstOrDefaultAsync();
            //Logger.Debug($"蓝屏 | PlayerLogin | 用户信息：{find?.ToJsonString()} | 请求参数：{input.ToJsonString()}");

            if ((find == null || find.NickName == "undefined" || find.NickName == null) && DateTime.Now < DateTime.Parse("2021-12-25"))
            {
                // 查找之前可能存在的无 unionID 的账户（因为被封，迁移数据）
                var possibleAccounts = await _playerRepository.GetAll()
               .Include(p => p.PlayerExtension)
               .WhereIf(input.TenantId.HasValue, p => p.TenantId == input.TenantId.Value)
               .WhereIf(!String.IsNullOrEmpty(input.OpenId), q => q.OpenId != input.OpenId)
               .Where(s => s.NickName == input.NickName
               && s.CreationTime >= DateTime.Parse("2021-01-01")
               && s.CreationTime <= DateTime.Parse("2021-12-06"))
               //&& s.UnionId == null)
               .ToListAsync();
                Guid? obsolutePlayerId = null;
                if (possibleAccounts.Count == 1)
                {
                    obsolutePlayerId = find.Id;
                    // 使用默认的账号
                    find = possibleAccounts.FirstOrDefault();
                    // 修改用户 openID
                    find.OpenId = input.OpenId;
                }
                else if (possibleAccounts.Count > 1)
                {
                    Logger.Warn($"可能为代理用户，通过 UNIONID 匹配，input.UnionID:{input.UnionId}");
                    // 因为代理的昵称大部分重复，所以通过 unionID 的方式处理
                    var account = await _playerRepository.GetAll()
               .Include(p => p.PlayerExtension)
               .WhereIf(input.TenantId.HasValue, p => p.TenantId == input.TenantId.Value)
               .FirstOrDefaultAsync(s => s.UnionId == input.UnionId && s.UnionId != null);
                    if (account != null)
                    {
                        obsolutePlayerId = find.Id;
                        // 使用默认的账号
                        find = account;
                        // 修改用户 openID
                        find.OpenId = input.OpenId;
                    }
                }
                // 删除第一次进入时增加的用户
                if (obsolutePlayerId.HasValue)
                {
                    var obsolutePlayer = await _playerRepository.GetAll().FirstOrDefaultAsync(s => s.Id == obsolutePlayerId);
                    obsolutePlayer.IsDeleted = true;
                    obsolutePlayer.DeletionTime = DateTime.Now;
                    obsolutePlayer.NickName = obsolutePlayer.NickName + "_obsolute_migrate";
                }
            }
            //var eventName = find == null ? StaticEventName.authorizationLogin : StaticEventName.Login;
            // 如果邀请方为代理，则对被邀请方不加壳。# 第一次玩
            // 如果用户本身为代理，则不加壳
            // 如果用户的邀请人（邀请用户的人）为代理，则不加壳。# 老用户
            var isAgent = false;
            if (find != null)
            {
                if (input.InviterId.HasValue)
                {
                    isAgent = await _mqAgentRepository.GetAll().AnyAsync(a => a.PlayerId == input.InviterId || a.PlayerId == find.Id);
                    if (!isAgent)
                    {
                        var inviter = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.InviterId.Value);
                        isAgent = !inviter.IsAddMask;
                    }
                }
                else
                {
                    isAgent = await _mqAgentRepository.GetAll().AnyAsync(a => a.PlayerId == find.Id);
                }

                Logger.Debug($"{find.NickName}/{find.Id}是否加壳：" + isAgent);
            }

            if (find == null)
            {
                find = new Player()
                {
                    OpenId = input.OpenId,
                    NickName = input.NickName,
                    HeadUrl = input.HeaderUrl,
                    DeviceModel = input.DeviceModel,
                    DeviceSystem = input.DeviceSystem,
                    TenantId = input.TenantId.Value,
                    UnionId = input.UnionId,
                    IsAddMask = !isAgent,// 如果不是代理邀请进入的则加壳
                    LastLoginTime = DateTime.Now
                };
                if (find.PlayerExtension == null)
                {
                    find.PlayerExtension = new PlayerExtension
                    {
                        LoveScore = StaticBonusPointsCount.AuthLoginCount,
                        Stamina = 10
                    };
                }

                find = await _playerRepository.InsertAsync(find);
            }
            else
            {
                if (find.PlayerExtension == null)
                {
                    find.PlayerExtension = new PlayerExtension
                    {
                        LoveScore = StaticBonusPointsCount.AuthLoginCount,
                        Stamina = 10
                    };
                }
                //else
                //{
                //    find.PlayerExtension.LoveScore += StaticBonusPointsCount.AuthLoginCount;

                //    //find.PlayerExtension.Stamina = await CountDownRecoveryStamina(find.Id, find.PlayerExtension.Stamina);
                //}
                if (!string.IsNullOrEmpty(input.NickName) && find.NickName != input.NickName)
                {
                    find.NickName = input.NickName;
                }

                if (!string.IsNullOrEmpty(input.HeaderUrl) && find.HeadUrl != input.HeaderUrl)
                {
                    find.HeadUrl = input.HeaderUrl;
                }

                if (!string.IsNullOrEmpty(input.DeviceModel) && find.DeviceModel != input.DeviceModel)
                {
                    find.DeviceModel = input.DeviceModel;
                }

                if (!string.IsNullOrEmpty(input.DeviceSystem) && find.DeviceSystem != input.DeviceSystem)
                {
                    find.DeviceSystem = input.DeviceSystem;
                }
                // 邀请方为代理，并且用户处于加壳状态，则将其去壳
                if (isAgent && find.IsAddMask)
                {
                    find.IsAddMask = false;
                }
                find.UnionId = input.UnionId;
                find.LastLoginTime = DateTime.Now;
                await _playerRepository.UpdateAsync(find);
            }
            var agent = await _mqAgentRepository.FirstOrDefaultAsync(a => a.UnionId == input.UnionId && a.TenantId == input.TenantId.Value);

            if (agent != null && agent.PlayerId != find.Id)
            {
                agent.PlayerId = find.Id;
                await _mqAgentRepository.UpdateAsync(agent);
            }
            if (agent != null && !find.IsAgenter)
            {
                find.IsAgenter = true;
            }

            return find;
        }

        //public async Task<Player> PlayerLogin(GetPlayerInput input)
        //{
        //    var find = await _playerRepository.GetAll()
        //        .Include(p => p.PlayerExtension)
        //        .WhereIf(input.TenantId.HasValue, p => p.TenantId == input.TenantId.Value)
        //        .WhereIf(!String.IsNullOrEmpty(input.OpenId), q => q.OpenId == input.OpenId)
        //        .FirstOrDefaultAsync();

        //    //var eventName = find == null ? StaticEventName.authorizationLogin : StaticEventName.Login;
        //    if (find == null)
        //    {
        //        find = new Player()
        //        {
        //            OpenId = input.OpenId,
        //            NickName = input.NickName,
        //            HeadUrl = input.HeaderUrl,
        //            DeviceModel = input.DeviceModel,
        //            DeviceSystem = input.DeviceSystem,
        //            TenantId = input.TenantId.Value,
        //            UnionId = input.UnionId,
        //            LastLoginTime = DateTime.Now
        //        };
        //        if (find.PlayerExtension == null)
        //        {
        //            find.PlayerExtension = new PlayerExtension
        //            {
        //                LoveScore = StaticBonusPointsCount.AuthLoginCount,
        //                Stamina = 10
        //            };
        //        }

        //        find = await _playerRepository.InsertAsync(find);
        //    }
        //    else
        //    {
        //        if (find.PlayerExtension == null)
        //        {
        //            find.PlayerExtension = new PlayerExtension
        //            {
        //                LoveScore = StaticBonusPointsCount.AuthLoginCount,
        //                Stamina = 10
        //            };
        //        }
        //        //else
        //        //{
        //        //    find.PlayerExtension.LoveScore += StaticBonusPointsCount.AuthLoginCount;

        //        //    //find.PlayerExtension.Stamina = await CountDownRecoveryStamina(find.Id, find.PlayerExtension.Stamina);
        //        //}

        //        if (!String.IsNullOrEmpty(input.NickName) && find.NickName != input.NickName)
        //        {
        //            find.NickName = input.NickName;
        //        }

        //        if (!String.IsNullOrEmpty(input.HeaderUrl) && find.HeadUrl != input.HeaderUrl)
        //        {
        //            find.HeadUrl = input.HeaderUrl;
        //        }

        //        if (find.DeviceModel == null || find.DeviceModel != input.DeviceModel)
        //        {
        //            find.DeviceModel = input.DeviceModel;
        //        }

        //        if (find.DeviceSystem == null || find.DeviceSystem != input.DeviceSystem)
        //        {
        //            find.DeviceSystem = input.DeviceSystem;
        //        }
        //        if (find.UnionId != input.UnionId)
        //        {
        //            find.UnionId = input.UnionId;
        //        }
        //        find.LastLoginTime = DateTime.Now;
        //        await _playerRepository.UpdateAsync(find);
        //    }
        //    var agent = await _mqAgentRepository.FirstOrDefaultAsync(a => a.UnionId == input.UnionId && a.TenantId == input.TenantId.Value);

        //    if (agent != null && agent.PlayerId != find.Id)
        //    {
        //        agent.PlayerId = find.Id;
        //        await _mqAgentRepository.UpdateAsync(agent);
        //    }
        //    if (agent != null && !find.IsAgenter)
        //    {
        //        find.IsAgenter = true;
        //    }

        //    return find;
        //}
        public async Task CreateNewPlayer(Player input)
        {
            if (input.PlayerExtension == null)
            {
                input.PlayerExtension = new PlayerExtension
                {
                    LoveScore = StaticBonusPointsCount.AuthLoginCount,
                    Stamina = 10
                };
            }
            await _playerRepository.InsertAsync(input);
        }


        public async Task<AddStaminaWithAdOutput> AddStaminaWithAd(AddStaminaWithAdInput input)
        {
            var addValue = 6;
            var output = new AddStaminaWithAdOutput();
            var player = await _playerRepository.GetAll().Include(p => p.PlayerExtension)
                .FirstOrDefaultAsync(p => p.Id == input.PlayerId);
            if (player != null)
            {
                player.PlayerExtension.Stamina += addValue;
                await _playerRepository.UpdateAsync(player);
                await _staminaRecordAppService.CreateOrUpdate(new CreateOrUpdateStaminaRecordInput()
                {
                    StaminaRecord = new StaminaRecordEditDto()
                    {
                        PlayerId = input.PlayerId,
                        StaminaCount = addValue
                    }
                });
                output.CurrentStamina = player.PlayerExtension.Stamina;
            }
            return output;
        }

        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> CountDownRecoveryStamina(Guid playerId, int currentStamina)
        {

            var result = 0;

            var query = _staminaRecordRepository.GetAll();

            var LastStaminaRecord = await query.OrderBy(s => s.CreationTime).LastOrDefaultAsync(p => p.PlayerId == playerId);

            var currentTime = DateTime.Now;
            //排除对超级会员体力最大值为30的限制
            var player = _playerRepository.Get(playerId);
            var isSuperMember = player.IsSuperMember;


            if (LastStaminaRecord == null)
            {
                result = currentStamina;
            }
            else
            {
                if (player != null && (isSuperMember || player.IsDeveloper) && currentStamina <= 0)
                {
                    result = 30;//如果为超级成员或者开发者，无限体力
                }
                else
                {
                    var totalHourDiff = Convert.ToInt32((currentTime - LastStaminaRecord.CreationTime).TotalHours);

                    if (totalHourDiff >= 5 || (totalHourDiff == 4 && currentTime.Minute == 0 || currentTime.Second == 0)) //大于五个小时 每个小时恢复6点体力 最大30
                    {
                        currentStamina += 30;
                    }
                    else
                    {
                        var totalMinuteDiff = (currentTime - LastStaminaRecord.CreationTime).TotalMinutes;

                        var diff = 60 - (LastStaminaRecord.CreationTime.Minute);  //距离整点差多长时间

                        if (totalMinuteDiff - diff == 0)
                        {
                            result = 6;
                        }
                        else if (totalMinuteDiff - diff > 0)
                        {
                            result = 6 * ((totalHourDiff - diff) / 60 + 1);
                        }
                        else
                        {
                            result = 0;
                        }
                    }
                    result = result + currentStamina;

                }
                if (result != currentStamina)
                {

                    StaminaRecordEditDto dto = new StaminaRecordEditDto()
                    {
                        PlayerId = playerId,
                        StaminaCount = result,
                    };

                    await _staminaRecordAppService.CreateOrUpdate(new CreateOrUpdateStaminaRecordInput()
                    {
                        StaminaRecord = dto
                    });
                }
            }


            return result;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task UpdatePlayerWechatInfo(WechatPlayerInfoRequest request)
        {
            var find = await _playerRepository.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (find != null)
            {
                find.HeadUrl = request.HeadUrl;
                find.NickName = request.NickName;
                find.Gender = request.Gender;
                await _playerRepository.UpdateAsync(find);
            }
        }

        public async Task<GetPlayerWithUnionIdOutput> GetPlayerWithUnionId(GetPlayerWithUnionIdInput input)
        {
            var output = new GetPlayerWithUnionIdOutput();
            var player = await _playerRepository.FirstOrDefaultAsync(p => p.UnionId == input.UnionId);

            if (player == null)
            {
                return null;
            }

            output.PlayerId = player.Id;

            return output;
        }

        public async Task CloseAd()
        {

        }

        public async Task LoadAd()
        {

        }

        public async Task ClickAd()
        {

        }

        /// <summary>
        /// 导出Player为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetPlayersToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}



        //// custom codes 

        //// custom codes end

    }
}


