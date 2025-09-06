using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Tags.Dto;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.Tags.Dtos;

namespace MQKJ.BSMP.Tags
{
    /// <summary>
    /// Tag应用层服务的接口方法
    /// </summary>
    public interface ITagAppService : IApplicationService
    {
        /// <summary>
        /// 获取Tag的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<TagListDto>> GetPagedTags(GetTagsInput input);

		/// <summary>
		/// 通过指定id获取TagListDto信息
		/// </summary>
		Task<TagListDto> GetTagByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出Tag为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetTagsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetTagForEditOutput> GetTagForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetTagForEditOutput


        /// <summary>
        /// 添加或者修改Tag的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateTag(TagEditDto input);


        /// <summary>
        /// 删除Tag信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteTag(EntityDto<int> input);


        /// <summary>
        /// 批量删除Tag
        /// </summary>
        Task BatchDeleteTagsAsync(List<int> input);


		//// custom codes 
		
        //// custom codes end
    }
}
