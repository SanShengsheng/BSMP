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


using MQKJ.BSMP.PropUseRecords.Dtos;
using MQKJ.BSMP.PropUseRecords;
using System;

namespace MQKJ.BSMP.PropUseRecords
{
    /// <summary>
    /// PropUseRecord应用层服务的接口实现方法
    /// </summary>
	
    public class PropUseRecordAppService : BSMPAppServiceBase, IPropUseRecordAppService
    {
		private readonly IRepository<PropUseRecord, Guid> _propuserecordRepository;

		
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public PropUseRecordAppService(
			IRepository<PropUseRecord, Guid> propuserecordRepository
			
		)
		{
			_propuserecordRepository = propuserecordRepository;
			
		}
		
		
		/// <summary>
		/// 获取PropUseRecord的分页列表信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public  async  Task<PagedResultDto<PropUseRecordListDto>> GetPagedPropUseRecords(GetPropUseRecordsInput input)
		{
		    
		    var query = _propuserecordRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
		
			var propuserecordCount = await query.CountAsync();
		
			var propuserecords = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
		
				// var propuserecordListDtos = ObjectMapper.Map<List <PropUseRecordListDto>>(propuserecords);
				var propuserecordListDtos =propuserecords.MapTo<List<PropUseRecordListDto>>();
		
				return new PagedResultDto<PropUseRecordListDto>(
							propuserecordCount,
							propuserecordListDtos
					);
		}
		

		/// <summary>
		/// 通过指定id获取PropUseRecordListDto信息
		/// </summary>
		public async Task<PropUseRecordListDto> GetPropUseRecordByIdAsync(EntityDto<Guid> input)
		{
			var entity = await _propuserecordRepository.GetAsync(input.Id);
		
		    return entity.MapTo<PropUseRecordListDto>();
		}
		
		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetPropUseRecordForEditOutput> GetPropUseRecordForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetPropUseRecordForEditOutput();
			PropUseRecordEditDto propuserecordEditDto;
		
			if (input.Id.HasValue)
			{
				var entity = await _propuserecordRepository.GetAsync(input.Id.Value);
		
				propuserecordEditDto = entity.MapTo<PropUseRecordEditDto>();
		
				//propuserecordEditDto = ObjectMapper.Map<List <propuserecordEditDto>>(entity);
			}
			else
			{
				propuserecordEditDto = new PropUseRecordEditDto();
			}
		
			output.PropUseRecord = propuserecordEditDto;
			return output;
		}
		
		
		/// <summary>
		/// 添加或者修改PropUseRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdatePropUseRecord(CreateOrUpdatePropUseRecordInput input)
		{
		    
			if (input.PropUseRecord.Id.HasValue)
			{
				await UpdatePropUseRecordAsync(input.PropUseRecord);
			}
			else
			{
				await CreatePropUseRecordAsync(input.PropUseRecord);
			}
		}
		

		/// <summary>
		/// 新增PropUseRecord
		/// </summary>
		
		protected virtual async Task<PropUseRecordEditDto> CreatePropUseRecordAsync(PropUseRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增
		
			var entity = ObjectMapper.Map <PropUseRecord>(input);
		
			entity = await _propuserecordRepository.InsertAsync(entity);
			return entity.MapTo<PropUseRecordEditDto>();
		}
		
		/// <summary>
		/// 编辑PropUseRecord
		/// </summary>
		
		protected virtual async Task UpdatePropUseRecordAsync(PropUseRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新
		
			var entity = await _propuserecordRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
		
			// ObjectMapper.Map(input, entity);
		    await _propuserecordRepository.UpdateAsync(entity);
		}
		

		
		/// <summary>
		/// 删除PropUseRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeletePropUseRecord(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _propuserecordRepository.DeleteAsync(input.Id);
		}
		
		
		
		/// <summary>
		/// 批量删除PropUseRecord的方法
		/// </summary>
		
		public async Task BatchDeletePropUseRecordsAsync(List<Guid> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _propuserecordRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出PropUseRecord为excel表
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetPropUseRecordsToExcel()
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


 