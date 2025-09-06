using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using System.Linq.Dynamic.Core;
 using Microsoft.EntityFrameworkCore; 


using MQKJ.BSMP.TagTypes.Dtos;
using MQKJ.BSMP.TagTypes;
using System;

namespace MQKJ.BSMP.TagTypes
{
    /// <summary>
    /// TagType应用层服务的接口实现方法
    /// </summary>
	
    public class TagTypeAppService : BSMPAppServiceBase, ITagTypeAppService
    {
		private readonly IRepository<TagType, int> _tagtypeRepository;

		
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public TagTypeAppService(
			IRepository<TagType, int> tagtypeRepository
			
		)
		{
			_tagtypeRepository = tagtypeRepository;
			
		}
		
		
		/// <summary>
		/// 获取TagType的分页列表信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public  async  Task<PagedResultDto<TagTypeListDto>> GetPagedTagTypes(GetTagTypesInput input)
		{
		    
		    var query = _tagtypeRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
		
			var tagtypeCount = await query.CountAsync();
		    if (input.WithDetail)
		    {
		        query = query.Include(t => t.Tags);
		    }
            var tagtypes = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
		
				// var tagtypeListDtos = ObjectMapper.Map<List <TagTypeListDto>>(tagtypes);
				var tagtypeListDtos =tagtypes.MapTo<List<TagTypeListDto>>();
		
				return new PagedResultDto<TagTypeListDto>(
							tagtypeCount,
							tagtypeListDtos
					);
		}
		

		/// <summary>
		/// 通过指定id获取TagTypeListDto信息
		/// </summary>
		public async Task<TagTypeListDto> GetTagTypeByIdAsync(EntityDto<int> input)
		{
			var entity = await _tagtypeRepository.GetAsync(input.Id);
		
		    return entity.MapTo<TagTypeListDto>();
		}
		
		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetTagTypeForEditOutput> GetTagTypeForEdit(NullableIdDto<int> input)
		{
			var output = new GetTagTypeForEditOutput();
			TagTypeEditDto tagtypeEditDto;
		
			if (input.Id.HasValue)
			{
				var entity = await _tagtypeRepository.GetAsync(input.Id.Value);
		
				tagtypeEditDto = entity.MapTo<TagTypeEditDto>();
		
				//tagtypeEditDto = ObjectMapper.Map<List <tagtypeEditDto>>(entity);
			}
			else
			{
				tagtypeEditDto = new TagTypeEditDto();
			}
		
			output.TagType = tagtypeEditDto;
			return output;
		}
		
		
		/// <summary>
		/// 添加或者修改TagType的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateTagType(TagTypeEditDto input)
		{
		    
			if (input.Id.HasValue)
			{
				await UpdateTagTypeAsync(input);
			}
			else
			{
				await CreateTagTypeAsync(input);
			}
		}
		

		/// <summary>
		/// 新增TagType
		/// </summary>
		
		protected virtual async Task<TagTypeEditDto> CreateTagTypeAsync(TagTypeEditDto input)
		{
            //TODO:新增前的逻辑判断，是否允许新增

            try
            {
                var entity = ObjectMapper.Map<TagType>(input);
                entity = await _tagtypeRepository.InsertAsync(entity);
                return entity.MapTo<TagTypeEditDto>();
            }
			
            catch(Exception ex)
            {
                throw ex;
            }
			
		}
		
		/// <summary>
		/// 编辑TagType
		/// </summary>
		
		protected virtual async Task UpdateTagTypeAsync(TagTypeEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新
		
			var entity = await _tagtypeRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
		
			// ObjectMapper.Map(input, entity);
		    await _tagtypeRepository.UpdateAsync(entity);
		}
		

		
		/// <summary>
		/// 删除TagType信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteTagType(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _tagtypeRepository.DeleteAsync(input.Id);
		}
		
		
		
		/// <summary>
		/// 批量删除TagType的方法
		/// </summary>
		
		public async Task BatchDeleteTagTypesAsync(List<int> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _tagtypeRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出TagType为excel表
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetTagTypesToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}


		
		//// custom codes 
		 
		 

        
        
        //// custom codes end

    }
}


 