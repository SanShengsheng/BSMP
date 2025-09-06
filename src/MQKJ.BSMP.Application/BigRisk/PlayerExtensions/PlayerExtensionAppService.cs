using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.PlayerExtensions.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.StaminaRecords.Dtos;
using MQKJ.BSMP.WeChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.PlayerExtensions
{
    /// <summary>
    /// PlayerExtension应用层服务的接口实现方法
    /// </summary>

    public class PlayerExtensionAppService : BSMPAppServiceBase, IPlayerExtensionAppService
    {
        private readonly IRepository<PlayerExtension, int> _playerextensionRepository;
        private readonly IStaminaRecordAppService _staminaRecordAppService;
        //private readonly IRepository<StaminaRecord, Guid> _staminaRecordRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlayerExtensionAppService(
            IRepository<PlayerExtension, int> playerextensionRepository
             //, IRepository<StaminaRecord, Guid> staminaRecordRepository
                        , IStaminaRecordAppService staminaRecordAppServic
        )
        {
            _playerextensionRepository = playerextensionRepository;
            _staminaRecordAppService = staminaRecordAppServic;
        }


        /// <summary>
        /// 获取PlayerExtension的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PlayerExtensionListDto>> GetPagedPlayerExtensions(GetPlayerExtensionsInput input)
        {

            var query = _playerextensionRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件

            var playerextensionCount = await query.CountAsync();

            var playerextensions = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var playerextensionListDtos = ObjectMapper.Map<List <PlayerExtensionListDto>>(playerextensions);
            var playerextensionListDtos = playerextensions.MapTo<List<PlayerExtensionListDto>>();

            return new PagedResultDto<PlayerExtensionListDto>(
                        playerextensionCount,
                        playerextensionListDtos
                );
        }


        /// <summary>
        /// 通过指定id获取PlayerExtensionListDto信息
        /// </summary>
        public async Task<PlayerExtensionListDto> GetPlayerExtensionByIdAsync(EntityDto<int> input)
        {
            var entity = await _playerextensionRepository.GetAsync(input.Id);

            return entity.MapTo<PlayerExtensionListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPlayerExtensionForEditOutput> GetPlayerExtensionForEdit(NullableIdDto<int> input)
        {
            var output = new GetPlayerExtensionForEditOutput();
            PlayerExtensionEditDto playerextensionEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _playerextensionRepository.GetAsync(input.Id.Value);

                playerextensionEditDto = entity.MapTo<PlayerExtensionEditDto>();

                //playerextensionEditDto = ObjectMapper.Map<List <playerextensionEditDto>>(entity);
            }
            else
            {
                playerextensionEditDto = new PlayerExtensionEditDto();
            }

            output.PlayerExtension = playerextensionEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改PlayerExtension的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdatePlayerExtension(CreateOrUpdatePlayerExtensionInput input)
        {

            if (input.PlayerExtension.Id.HasValue)
            {
                await UpdatePlayerExtensionAsync(input.PlayerExtension);
            }
            else
            {
                await CreatePlayerExtensionAsync(input.PlayerExtension);
            }
        }


        /// <summary>
        /// 新增PlayerExtension
        /// </summary>

        protected virtual async Task<PlayerExtensionEditDto> CreatePlayerExtensionAsync(PlayerExtensionEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<PlayerExtension>(input);

            entity = await _playerextensionRepository.InsertAsync(entity);
            return entity.MapTo<PlayerExtensionEditDto>();
        }

        /// <summary>
        /// 编辑PlayerExtension
        /// </summary>

        protected virtual async Task UpdatePlayerExtensionAsync(PlayerExtensionEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _playerextensionRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _playerextensionRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除PlayerExtension信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeletePlayerExtension(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _playerextensionRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除PlayerExtension的方法
        /// </summary>

        public async Task BatchDeletePlayerExtensionsAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _playerextensionRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task UpdateStamina(EntityDto<Guid> input)
        {
            var entity = await _playerextensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.Id);

            try
            {
                entity.Stamina -= 6;

                if (entity.Stamina <= 0)
                    entity.Stamina = 0;

                await _playerextensionRepository.UpdateAsync(entity);
            }
            catch (Exception exp)
            {
                Logger.Error($"更新体力值失败:{exp}");
            }
        }
        public async Task UpdateUserPoint(UpdateUserPointRequest request)
        {
            var user = await _playerextensionRepository.FirstOrDefaultAsync(u => u.PlayerGuid == request.UserId);
            if (user != null)
            {
                user.LoveScore += request.Score;
                await _playerextensionRepository.UpdateAsync(user);
                Logger.Debug($"用户{request.UserId}增加了{request.Score}积分");
            }
            else
            {
                Logger.Error($"未找到相应用户，用户ID：{request.UserId}, 积分：{request.Score}");
            }
        }

        public async Task SetIntroduceOfOneSelf(SetIntroduceOfOneSelfInput input)
        {
            var playerExtension = await _playerextensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.PlayerId);

            playerExtension.Introduce = input.Introduce;

            await _playerextensionRepository.UpdateAsync(playerExtension);
        }

        /// <summary>
        /// 导出PlayerExtension为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetPlayerExtensionsToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        private  void RecordStaminaChange(Guid playerId)
        {
            StaminaRecordEditDto dto = new StaminaRecordEditDto()
            {
                PlayerId = playerId,
                StaminaCount = 6,
            };

             _staminaRecordAppService.CreateOrUpdate(new CreateOrUpdateStaminaRecordInput()
            {
                StaminaRecord = dto
            });
        }

        //// custom codes 
        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public RecoverStrengthOutput RecoverStrength(RecoverStrengthInput input)
        {
            var playerExtension = _playerextensionRepository.FirstOrDefault(a => a.PlayerGuid == input.PlayerId);
            if (playerExtension != null && playerExtension.Stamina == 0)//只有体力为0时才能恢复
            {
                playerExtension.Stamina = 6;
                _playerextensionRepository.Update(playerExtension);
                RecordStaminaChange(input.PlayerId);
            }
            return new RecoverStrengthOutput() { Stamina = playerExtension.Stamina };
        }
        //// custom codes end

    }
}


