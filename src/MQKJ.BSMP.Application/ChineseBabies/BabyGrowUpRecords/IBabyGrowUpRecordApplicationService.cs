using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyGrowUpRecord应用层服务的接口方法
    ///</summary>
    public interface IBabyGrowUpRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取BabyGrowUpRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BabyGrowUpRecordListDto>> GetPaged(GetBabyGrowUpRecordsInput input);

        /// <summary>
        /// 通过指定id获取BabyGrowUpRecordListDto信息
        /// </summary>
        Task<BabyGrowUpRecordListDto> GetById(EntityDto<Guid> input);

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBabyGrowUpRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);

        /// <summary>
        /// 添加或者修改BabyGrowUpRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateBabyGrowUpRecordInput input);

        Task AddBabyGrowUpRecord(BabyGrowUpRecordEditDto input);

        /// <summary>
        /// 删除BabyGrowUpRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);

        /// <summary>
        /// 批量删除BabyGrowUpRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);

        /// <summary>
        /// 导出BabyGrowUpRecord为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();
    }
}