using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.GameRecords;
using MQKJ.BSMP.GameTasks.Authorization;
using MQKJ.BSMP.GameTasks.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.GameTasks
{
    /// <summary>
    /// GameTask应用层服务的接口实现方法
    /// </summary>
	//[AbpAuthorize(GameTaskAppPermissions.GameTask)]
    public class GameTaskAppService : BSMPAppServiceBase, IGameTaskAppService
    {
        private readonly IRepository<GameTask, Guid> _gametaskRepository;

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<GameRecord, Guid> _gameRecordRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameTaskAppService(
            IRepository<GameTask, Guid> gametaskRepository,
            IRepository<Player, Guid> playerRepository,
            IRepository<GameRecord, Guid> gameRecordRepository
        )
        {
            _gametaskRepository = gametaskRepository;

            _playerRepository = playerRepository;

            _gameRecordRepository = gameRecordRepository;

        }


        /// <summary>
        /// 获取GameTask的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GameTaskListDto>> GetPagedGameTasks(GetGameTasksInput input)
        {
            var taskType = (TaskType)input.TaskType;
            var taskState = (TaskState)input.TaskState;
            var seekTyep = (QuestionGender)input.SeekType;
            //var relationType = (RelationDegree)input.RelationType;
            var query = _gametaskRepository.GetAllIncluding(g => g.Inviter, g => g.Invitee)
                .WhereIf(input.StartTime != Convert.ToDateTime("0001/1/1 0:00:00") && input.EndTime != Convert.ToDateTime("0001/1/1 0:00:00"), g => g.CreationTime >= input.StartTime && g.CreationTime < input.EndTime)
                .WhereIf(!string.IsNullOrEmpty(input.NickName), g => g.Invitee.NickName.Contains(input.NickName) || g.Inviter.NickName.Contains(input.NickName))
                .WhereIf(input.TaskType != 0, g => g.TaskType == taskType)
                .WhereIf(input.TaskState != 0, g => g.State == taskState)
                .WhereIf(input.SeekType != 0, g => g.SeekType == seekTyep)
                .WhereIf(input.RelationType != 0, g => g.RelationDegree == (RelationDegree)input.RelationType)
                .OrderByDescending(d => d.CreationTime).AsNoTracking();

            var gameTaskCount = await query.CountAsync();

            var gameTasks = await query.PageBy(input).ToListAsync();

            var gametaskListDtos = gameTasks.MapTo<List<GameTaskListDto>>();

            return new PagedResultDto<GameTaskListDto>(
                        gameTaskCount,
                        gametaskListDtos
                );
        }


        /// <summary>
        /// 通过指定id获取GameTaskListDto信息
        /// </summary>
        public async Task<GameTaskListDto> GetGameTaskByIdAsync(EntityDto<Guid> input)
        {
            var entity = await _gametaskRepository.GetAsync(input.Id);

            return entity.MapTo<GameTaskListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetGameTaskForEditOutput> GetGameTaskForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetGameTaskForEditOutput();
            GameTaskEditDto gametaskEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _gametaskRepository.GetAsync(input.Id.Value);

                gametaskEditDto = entity.MapTo<GameTaskEditDto>();

                //gametaskEditDto = ObjectMapper.Map<List <gametaskEditDto>>(entity);
            }
            else
            {
                gametaskEditDto = new GameTaskEditDto();
            }

            output.GameTask = gametaskEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改GameTask的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateGameTask(CreateOrUpdateGameTaskInput input)
        {
            //用户是否冻结
            var playerEntity = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.GameTask.InviterPlayerId);
            if (playerEntity.State == (int)PlayerState.Freeze)//冻结
            {

            }
            else
            {
                if (input.GameTask.Id.HasValue)
                {
                    await UpdateGameTaskAsync(input.GameTask);
                }
                else
                {
                    await CreateGameTaskAsync(input.GameTask);
                }
            }
        }


        /// <summary>
        /// 新增GameTask
        /// </summary>
        //[AbpAuthorize(GameTaskAppPermissions.GameTask_CreateGameTask)]
        protected virtual async Task<GameTaskEditDto> CreateGameTaskAsync(GameTaskEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<GameTask>(input);

            entity = await _gametaskRepository.InsertAsync(entity);
            return entity.MapTo<GameTaskEditDto>();
        }

        /// <summary>
        /// 编辑GameTask
        /// </summary>
        //[AbpAuthorize(GameTaskAppPermissions.GameTask_EditGameTask)]
        protected virtual async Task UpdateGameTaskAsync(GameTaskEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _gametaskRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _gametaskRepository.UpdateAsync(entity);

            //更新记录表
            var recordEntity = new GameRecord()
            {
                Id = Guid.NewGuid(),
                State = input.State,
                GameTaskId = input.Id.Value
            };
            await _gameRecordRepository.InsertAsync(recordEntity);
        }



        /// <summary>
        /// 删除GameTask信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(GameTaskAppPermissions.GameTask_DeleteGameTask)]
        public async Task DeleteGameTask(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _gametaskRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除GameTask的方法
        /// </summary>
        //[AbpAuthorize(GameTaskAppPermissions.GameTask_DeleteGameTask)]
        public async Task BatchDeleteGameTasksAsync(List<Guid> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _gametaskRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 发起邀请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task InviteFriends(InviteFriendsDto input)
        {
            input.State = TaskState.TaskInitialization;

            await _gametaskRepository.InsertAsync(input.MapTo<GameTask>());
        }

        public Task CheckInviteLink()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGameState(EntityDto<Guid> input)
        {
            var entity = await _gametaskRepository.FirstOrDefaultAsync(g => g.Id == input.Id);

            if (entity != null)
            {
                entity.State = TaskState.TaskFailure;

                await _gametaskRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 导出GameTask为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetGameTasksToExcel()
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


