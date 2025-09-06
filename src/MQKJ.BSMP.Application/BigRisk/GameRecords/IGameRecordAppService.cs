using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.GameRecords.Dtos;
using MQKJ.BSMP.GameRecords;
using System;

namespace MQKJ.BSMP.GameRecords
{
    /// <summary>
    /// GameRecord应用层服务的接口方法
    /// </summary>
    public interface IGameRecordAppService : IApplicationService
    {
        /// <summary>
        /// 获取GameRecord的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GameRecordListDto>> GetPagedGameRecords(GetGameRecordsInput input);

		/// <summary>
		/// 通过指定id获取GameRecordListDto信息
		/// </summary>
		Task<GameRecordListDto> GetGameRecordByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 导出GameRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetGameRecordsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetGameRecordForEditOutput> GetGameRecordForEdit(NullableIdDto<Guid> input);

        //todo:缺少Dto的生成GetGameRecordForEditOutput


        /// <summary>
        /// 添加或者修改GameRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateGameRecord(CreateOrUpdateGameRecordInput input);


        /// <summary>
        /// 删除GameRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteGameRecord(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除GameRecord
        /// </summary>
        Task BatchDeleteGameRecordsAsync(List<Guid> input);


		//// custom codes 
		
        //// custom codes end
    }
}
