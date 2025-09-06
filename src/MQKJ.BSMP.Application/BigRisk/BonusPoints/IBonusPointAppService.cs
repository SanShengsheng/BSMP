using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.BonusPoints.Dtos;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPoints
{
    /// <summary>
    /// BonusPoint应用层服务的接口方法
    /// </summary>
    public interface IBonusPointAppService : IApplicationService
    {
        /// <summary>
        /// 获取BonusPoint的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BonusPointListDto>> GetPagedBonusPoints(GetBonusPointsInput input);

		/// <summary>
		/// 通过指定id获取BonusPointListDto信息
		/// </summary>
		Task<BonusPointListDto> GetBonusPointByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 获取所有事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<BonusPointListDto>> GetAllScenesAsync(GetBonusPointsInput input);

        /// <summary>
        /// 导出BonusPoint为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetBonusPointsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBonusPointForEditOutput> GetBonusPointForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetBonusPointForEditOutput


        /// <summary>
        /// 添加或者修改BonusPoint的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateBonusPoint(CreateOrUpdateBonusPointInput input);


        /// <summary>
        /// 删除BonusPoint信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteBonusPoint(EntityDto<int> input);


        /// <summary>
        /// 批量删除BonusPoint
        /// </summary>
        Task BatchDeleteBonusPointsAsync(List<int> input);

        Task<BonusPoint> GetBounsPointByEventName(string eventName);

        //// custom codes 

        //// custom codes end
    }
}
