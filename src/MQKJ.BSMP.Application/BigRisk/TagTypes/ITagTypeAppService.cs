using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.TagTypes.Dtos;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.TagTypes
{
    /// <summary>
    /// TagType应用层服务的接口方法
    /// </summary>
    public interface ITagTypeAppService : IApplicationService
    {
        /// <summary>
        /// 获取TagType的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<TagTypeListDto>> GetPagedTagTypes(GetTagTypesInput input);

		/// <summary>
		/// 通过指定id获取TagTypeListDto信息
		/// </summary>
		Task<TagTypeListDto> GetTagTypeByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出TagType为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetTagTypesToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetTagTypeForEditOutput> GetTagTypeForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetTagTypeForEditOutput


        /// <summary>
        /// 添加或者修改TagType的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateTagType(TagTypeEditDto input);


        /// <summary>
        /// 删除TagType信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteTagType(EntityDto<int> input);


        /// <summary>
        /// 批量删除TagType
        /// </summary>
        Task BatchDeleteTagTypesAsync(List<int> input);


		//// custom codes 
		 
		 
		
        
        
        //// custom codes end
    }
}
