
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.StaminaRecords.Dtos;
using MQKJ.BSMP.StaminaRecords.DomainService;
using MQKJ.BSMP.StaminaRecords.Authorization;


namespace MQKJ.BSMP.StaminaRecords
{
    /// <summary>
    /// StaminaRecord应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class StaminaRecordAppService : BSMPAppServiceBase, IStaminaRecordAppService
    {
        private readonly IRepository<StaminaRecord, Guid> _entityRepository;

        private readonly IStaminaRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public StaminaRecordAppService(
        IRepository<StaminaRecord, Guid> entityRepository
        ,IStaminaRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取StaminaRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(StaminaRecordPermissions.Query)] 
        public async Task<PagedResultDto<StaminaRecordListDto>> GetPaged(GetStaminaRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<StaminaRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<StaminaRecordListDto>>();

			return new PagedResultDto<StaminaRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取StaminaRecordListDto信息
		/// </summary>
		//[AbpAuthorize(StaminaRecordPermissions.Query)] 
		public async Task<StaminaRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<StaminaRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 StaminaRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(StaminaRecordPermissions.Create,StaminaRecordPermissions.Edit)]
		public async Task<GetStaminaRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetStaminaRecordForEditOutput();
StaminaRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<StaminaRecordEditDto>();

				//staminaRecordEditDto = ObjectMapper.Map<List<staminaRecordEditDto>>(entity);
			}
			else
			{
				editDto = new StaminaRecordEditDto();
			}

			output.StaminaRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改StaminaRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(StaminaRecordPermissions.Create,StaminaRecordPermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdateStaminaRecordInput input)
		{

			if (input.StaminaRecord.Id.HasValue)
			{
				await Update(input.StaminaRecord);
			}
			else
			{
				await Create(input.StaminaRecord);
			}
		}


		/// <summary>
		/// 新增StaminaRecord
		/// </summary>
		//[AbpAuthorize(StaminaRecordPermissions.Create)]
		protected virtual async Task<StaminaRecordEditDto> Create(StaminaRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <StaminaRecord>(input);
            var entity=input.MapTo<StaminaRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<StaminaRecordEditDto>();
		}

		/// <summary>
		/// 编辑StaminaRecord
		/// </summary>
		//[AbpAuthorize(StaminaRecordPermissions.Edit)]
		protected virtual async Task Update(StaminaRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除StaminaRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(StaminaRecordPermissions.Delete)]
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除StaminaRecord的方法
		/// </summary>
		//[AbpAuthorize(StaminaRecordPermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出StaminaRecord为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}

    }
}


