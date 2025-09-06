using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.GameTasks.Dtos;
using MQKJ.BSMP.GameTasks;
using System;

namespace MQKJ.BSMP.GameTasks
{
    /// <summary>
    /// GameTask应用层服务的接口方法
    /// </summary>
    public interface IGameTaskAppService : IApplicationService
    {
        /// <summary>
        /// 获取GameTask的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GameTaskListDto>> GetPagedGameTasks(GetGameTasksInput input);

		/// <summary>
		/// 通过指定id获取GameTaskListDto信息
		/// </summary>
		Task<GameTaskListDto> GetGameTaskByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 导出GameTask为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetGameTasksToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetGameTaskForEditOutput> GetGameTaskForEdit(NullableIdDto<Guid> input);

        //todo:缺少Dto的生成GetGameTaskForEditOutput


        /// <summary>
        /// 添加或者修改GameTask的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateGameTask(CreateOrUpdateGameTaskInput input);


        /// <summary>
        /// 删除GameTask信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteGameTask(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除GameTask
        /// </summary>
        Task BatchDeleteGameTasksAsync(List<Guid> input);

        /// <summary>
        /// 发起邀请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task InviteFriends(InviteFriendsDto input);

        Task CheckInviteLink();

        /// <summary>
        /// 设置游戏状态为失败
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateGameState(EntityDto<Guid> input);
        //// custom codes 

        //// custom codes end
    }
}
