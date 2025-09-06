using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.PlayerExtensions.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.WeChat.Dtos;

namespace MQKJ.BSMP.PlayerExtensions
{
    /// <summary>
    /// PlayerExtension应用层服务的接口方法
    /// </summary>
    public interface IPlayerExtensionAppService : IApplicationService
    {
        /// <summary>
        /// 获取PlayerExtension的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PlayerExtensionListDto>> GetPagedPlayerExtensions(GetPlayerExtensionsInput input);

		/// <summary>
		/// 通过指定id获取PlayerExtensionListDto信息
		/// </summary>
		Task<PlayerExtensionListDto> GetPlayerExtensionByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出PlayerExtension为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetPlayerExtensionsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPlayerExtensionForEditOutput> GetPlayerExtensionForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetPlayerExtensionForEditOutput


        /// <summary>
        /// 添加或者修改PlayerExtension的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdatePlayerExtension(CreateOrUpdatePlayerExtensionInput input);


        /// <summary>
        /// 删除PlayerExtension信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePlayerExtension(EntityDto<int> input);


        /// <summary>
        /// 批量删除PlayerExtension
        /// </summary>
        Task BatchDeletePlayerExtensionsAsync(List<int> input);

        Task UpdateUserPoint(UpdateUserPointRequest request);
        //// custom codes 
        /// <summary>
        /// 更新体力值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateStamina(EntityDto<Guid> input);

        RecoverStrengthOutput RecoverStrength(RecoverStrengthInput input);

        //// custom codes end
    }
}
