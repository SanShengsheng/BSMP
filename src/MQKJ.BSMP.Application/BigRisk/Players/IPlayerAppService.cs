using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Players.Dtos;
using MQKJ.BSMP.Players;
using System;
using MQKJ.BSMP.BigRisk.Players.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MQKJ.BSMP.Players
{
    /// <summary>
    /// Player应用层服务的接口方法
    /// </summary>
    public interface IPlayerAppService : IApplicationService
    {
        /// <summary>
        /// 获取Player的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PlayerListDto>> GetPagedPlayers(GetPlayersInput input);

		/// <summary>
		/// 通过指定id获取PlayerListDto信息
		/// </summary>
		Task<PlayerListDto> GetPlayerByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 导出Player为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetPlayersToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPlayerForEditOutput> GetPlayerForEdit(NullableIdDto<Guid> input);

        //todo:缺少Dto的生成GetPlayerForEditOutput


        /// <summary>
        /// 添加或者修改Player的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdatePlayer(CreateOrUpdatePlayerInput input);


        /// <summary>
        /// 删除Player信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePlayer(EntityDto<Guid> input);

        /// <summary>
        /// 更新isdelete状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DummyDeletePlayer(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Player
        /// </summary>
        Task BatchDeletePlayersAsync(List<Guid> input);

        Task UpdatePlayerState(EntityDto<Guid> input);


        Task<Player> PlayerLogin(GetPlayerInput input);
        //Task CreateNewPlayer(Player input);

        Task<int> CountDownRecoveryStamina(Guid playerId, int currentStamina);
        Task UpdatePlayerWechatInfo(WechatPlayerInfoRequest request);

        Task<GetPlayerWithUnionIdOutput> GetPlayerWithUnionId(GetPlayerWithUnionIdInput input);

        /// <summary>
        /// 点击广告增加体力
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddStaminaWithAdOutput> AddStaminaWithAd(AddStaminaWithAdInput input);
    }
}
