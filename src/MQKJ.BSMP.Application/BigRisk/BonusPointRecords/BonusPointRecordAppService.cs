using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using System.Linq.Dynamic.Core;
 using Microsoft.EntityFrameworkCore; 
using MQKJ.BSMP.BonusPointRecords.Authorization;
using MQKJ.BSMP.BonusPointRecords.DomainServices;
using MQKJ.BSMP.BonusPointRecords.Dtos;
using MQKJ.BSMP.BonusPoints;
using System;

namespace MQKJ.BSMP.BonusPointRecords
{
    /// <summary>
    /// BonusPointRecord应用层服务的接口实现方法
    /// </summary>
    public class BonusPointRecordAppService : BSMPAppServiceBase, IBonusPointRecordAppService
    {
		private readonly IRepository<BonusPointRecord, Guid> _bonuspointrecordRepository;

		private readonly IBonusPointRecordManager _bonuspointrecordManager;
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public BonusPointRecordAppService(
			IRepository<BonusPointRecord, Guid> bonuspointrecordRepository
			,IBonusPointRecordManager bonuspointrecordManager
		)
		{
			_bonuspointrecordRepository = bonuspointrecordRepository;
			 _bonuspointrecordManager=bonuspointrecordManager;
		}
		
		
		/// <summary>
		/// 获取BonusPointRecord的分页列表信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public  async  Task<PagedResultDto<BonusPointRecordListDto>> GetPagedBonusPointRecords(GetBonusPointRecordsInput input)
		{
            var query = _bonuspointrecordRepository.GetAllIncluding(p => p.Player, q => q.BonusPoint)
                .WhereIf(!string.IsNullOrEmpty(input.UserName), p => p.Player.NickName.Contains(input.UserName))
                .WhereIf(input.StartTime != Convert.ToDateTime("0001/1/1 0:00:00") && input.EndTime != Convert.ToDateTime("0001/1/1 0:00:00"), b => b.CreationTime > input.StartTime && b.CreationTime < input.EndTime)
                .WhereIf(input.EventId != 0, e => e.BonusPoint.Id == input.EventId)
                .OrderByDescending(d => d.CreationTime).AsNoTracking();

            var recordCount = await query.CountAsync();

            var records = await query.PageBy(input).ToListAsync();

            var recordListDtos = records.MapTo<List<BonusPointRecordListDto>>();

            return new PagedResultDto<BonusPointRecordListDto>(
                    recordCount,
                    recordListDtos
                );

		 //   var query = _bonuspointrecordRepository.GetAll();
			//// TODO:根据传入的参数添加过滤条件
		
			//var bonuspointrecordCount = await query.CountAsync();
		
			//var bonuspointrecords = await query
   //                 .Include(p => p.Player)
   //                 .Include(e => e.BonusPoint)
			//		.OrderBy(input.Sorting).AsNoTracking()
			//		.PageBy(input)
			//		.ToListAsync();
		
			//	// var bonuspointrecordListDtos = ObjectMapper.Map<List <BonusPointRecordListDto>>(bonuspointrecords);
			//	var bonuspointrecordListDtos =bonuspointrecords.MapTo<List<BonusPointRecordListDto>>();
		
			//	return new PagedResultDto<BonusPointRecordListDto>(
			//				bonuspointrecordCount,
			//				bonuspointrecordListDtos
			//		);
		}
		

		/// <summary>
		/// 通过指定id获取BonusPointRecordListDto信息
		/// </summary>
		public async Task<BonusPointRecordListDto> GetBonusPointRecordByIdAsync(EntityDto<Guid> input)
		{
			var entity = await _bonuspointrecordRepository.GetAsync(input.Id);
		
		    return entity.MapTo<BonusPointRecordListDto>();
		}
		
		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetBonusPointRecordForEditOutput> GetBonusPointRecordForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetBonusPointRecordForEditOutput();
			BonusPointRecordEditDto bonuspointrecordEditDto;
		
			if (input.Id.HasValue)
			{
				var entity = await _bonuspointrecordRepository.GetAsync(input.Id.Value);
		
				bonuspointrecordEditDto = entity.MapTo<BonusPointRecordEditDto>();
		
				//bonuspointrecordEditDto = ObjectMapper.Map<List <bonuspointrecordEditDto>>(entity);
			}
			else
			{
				bonuspointrecordEditDto = new BonusPointRecordEditDto();
			}
		
			output.BonusPointRecord = bonuspointrecordEditDto;
			return output;
		}
		
		
		/// <summary>
		/// 添加或者修改BonusPointRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateBonusPointRecord(CreateOrUpdateBonusPointRecordInput input)
		{
		    
			if (input.BonusPointRecord.Id.HasValue)
			{
				await UpdateBonusPointRecordAsync(input.BonusPointRecord);
			}
			else
			{
				await CreateBonusPointRecordAsync(input.BonusPointRecord);
			}
		}
		

		/// <summary>
		/// 新增BonusPointRecord
		/// </summary>
		protected virtual async Task<BonusPointRecordEditDto> CreateBonusPointRecordAsync(BonusPointRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增
		
			var entity = ObjectMapper.Map <BonusPointRecord>(input);
		
			entity = await _bonuspointrecordRepository.InsertAsync(entity);
			return entity.MapTo<BonusPointRecordEditDto>();
		}
		
		/// <summary>
		/// 编辑BonusPointRecord
		/// </summary>
		protected virtual async Task UpdateBonusPointRecordAsync(BonusPointRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新
		
			var entity = await _bonuspointrecordRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
		
			// ObjectMapper.Map(input, entity);
		    await _bonuspointrecordRepository.UpdateAsync(entity);
		}
		

		
		/// <summary>
		/// 删除BonusPointRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(BonusPointRecordAppPermissions.BonusPointRecord_DeleteBonusPointRecord)]
		public async Task DeleteBonusPointRecord(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _bonuspointrecordRepository.DeleteAsync(input.Id);
		}
		
		
		
		/// <summary>
		/// 批量删除BonusPointRecord的方法
		/// </summary>
		[AbpAuthorize(BonusPointRecordAppPermissions.BonusPointRecord_BatchDeleteBonusPointRecords)]
		public async Task BatchDeleteBonusPointRecordsAsync(List<Guid> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _bonuspointrecordRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出BonusPointRecord为excel表
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetBonusPointRecordsToExcel()
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


 