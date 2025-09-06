using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.PropUseRecords.Dtos;
using MQKJ.BSMP.PropUseRecords;
using System;

namespace MQKJ.BSMP.PropUseRecords
{
    /// <summary>
    /// PropUseRecord应用层服务的接口方法
    /// </summary>
    public interface IPropUseRecordAppService : IApplicationService
    {
        /// <summary>
        /// 获取PropUseRecord的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PropUseRecordListDto>> GetPagedPropUseRecords(GetPropUseRecordsInput input);

		/// <summary>
		/// 通过指定id获取PropUseRecordListDto信息
		/// </summary>
		Task<PropUseRecordListDto> GetPropUseRecordByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 导出PropUseRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetPropUseRecordsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPropUseRecordForEditOutput> GetPropUseRecordForEdit(NullableIdDto<Guid> input);

        //todo:缺少Dto的生成GetPropUseRecordForEditOutput


        /// <summary>
        /// 添加或者修改PropUseRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdatePropUseRecord(CreateOrUpdatePropUseRecordInput input);


        /// <summary>
        /// 删除PropUseRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePropUseRecord(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除PropUseRecord
        /// </summary>
        Task BatchDeletePropUseRecordsAsync(List<Guid> input);


		//// custom codes 
		
        //// custom codes end
    }
}
