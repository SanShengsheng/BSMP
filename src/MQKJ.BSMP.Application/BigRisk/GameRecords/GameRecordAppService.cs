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


using MQKJ.BSMP.GameRecords.Dtos;
using MQKJ.BSMP.GameRecords;
using System;

namespace MQKJ.BSMP.GameRecords
{
    /// <summary>
    /// GameRecord应用层服务的接口实现方法
    /// </summary>
	
    public class GameRecordAppService : BSMPAppServiceBase, IGameRecordAppService
    {
		private readonly IRepository<GameRecord, Guid> _gamerecordRepository;
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public GameRecordAppService(IRepository<GameRecord, Guid> gamerecordRepository)
		{
			_gamerecordRepository = gamerecordRepository;
		}
		
		
		/// <summary>
		/// 获取GameRecord的分页列表信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public  async  Task<PagedResultDto<GameRecordListDto>> GetPagedGameRecords(GetGameRecordsInput input)
		{
		    
		    var query = _gamerecordRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
		
			var gamerecordCount = await query.CountAsync();
		
			var gamerecords = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
		
				// var gamerecordListDtos = ObjectMapper.Map<List <GameRecordListDto>>(gamerecords);
				var gamerecordListDtos =gamerecords.MapTo<List<GameRecordListDto>>();
		
				return new PagedResultDto<GameRecordListDto>(
							gamerecordCount,
							gamerecordListDtos
					);
		}
		

		/// <summary>
		/// 通过指定id获取GameRecordListDto信息
		/// </summary>
		public async Task<GameRecordListDto> GetGameRecordByIdAsync(EntityDto<Guid> input)
		{
			var entity = await _gamerecordRepository.GetAsync(input.Id);
		
		    return entity.MapTo<GameRecordListDto>();
		}
		
		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetGameRecordForEditOutput> GetGameRecordForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetGameRecordForEditOutput();
			GameRecordEditDto gamerecordEditDto;
		
			if (input.Id.HasValue)
			{
				var entity = await _gamerecordRepository.GetAsync(input.Id.Value);
		
				gamerecordEditDto = entity.MapTo<GameRecordEditDto>();
		
				//gamerecordEditDto = ObjectMapper.Map<List <gamerecordEditDto>>(entity);
			}
			else
			{
				gamerecordEditDto = new GameRecordEditDto();
			}
		
			output.GameRecord = gamerecordEditDto;
			return output;
		}
		
		
		/// <summary>
		/// 添加或者修改GameRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateGameRecord(CreateOrUpdateGameRecordInput input)
		{
		    
			if (input.GameRecord.Id.HasValue)
			{
				await UpdateGameRecordAsync(input.GameRecord);
			}
			else
			{
				await CreateGameRecordAsync(input.GameRecord);
			}
		}
		

		/// <summary>
		/// 新增GameRecord
		/// </summary>
		
		protected virtual async Task<GameRecordEditDto> CreateGameRecordAsync(GameRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增
		
			var entity = ObjectMapper.Map <GameRecord>(input);
		
			entity = await _gamerecordRepository.InsertAsync(entity);
			return entity.MapTo<GameRecordEditDto>();
		}
		
		/// <summary>
		/// 编辑GameRecord
		/// </summary>
		
		protected virtual async Task UpdateGameRecordAsync(GameRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新
		
			var entity = await _gamerecordRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
		
			// ObjectMapper.Map(input, entity);
		    await _gamerecordRepository.UpdateAsync(entity);
		}
		

		
		/// <summary>
		/// 删除GameRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteGameRecord(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _gamerecordRepository.DeleteAsync(input.Id);
		}
		
		
		
		/// <summary>
		/// 批量删除GameRecord的方法
		/// </summary>
		
		public async Task BatchDeleteGameRecordsAsync(List<Guid> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _gamerecordRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出GameRecord为excel表
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetGameRecordsToExcel()
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


 