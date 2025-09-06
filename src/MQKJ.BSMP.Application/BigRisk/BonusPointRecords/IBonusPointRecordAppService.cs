using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.BonusPointRecords.Dtos;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPointRecords
{
    /// <summary>
    /// BonusPointRecord应用层服务的接口方法
    /// </summary>
    public interface IBonusPointRecordAppService : IApplicationService
    {
        /// <summary>
        /// 获取BonusPointRecord的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BonusPointRecordListDto>> GetPagedBonusPointRecords(GetBonusPointRecordsInput input);

		/// <summary>
		/// 通过指定id获取BonusPointRecordListDto信息
		/// </summary>
		Task<BonusPointRecordListDto> GetBonusPointRecordByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 导出BonusPointRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetBonusPointRecordsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBonusPointRecordForEditOutput> GetBonusPointRecordForEdit(NullableIdDto<Guid> input);

        //todo:缺少Dto的生成GetBonusPointRecordForEditOutput


        /// <summary>
        /// 添加或者修改BonusPointRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateBonusPointRecord(CreateOrUpdateBonusPointRecordInput input);


        /// <summary>
        /// 删除BonusPointRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteBonusPointRecord(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除BonusPointRecord
        /// </summary>
        Task BatchDeleteBonusPointRecordsAsync(List<Guid> input);


		//// custom codes 
		
        //// custom codes end
    }
}
