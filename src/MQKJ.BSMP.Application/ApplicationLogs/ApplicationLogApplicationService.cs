
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


using MQKJ.BSMP.ApplicationLogs;
using MQKJ.BSMP.ApplicationLogs.Dtos;
using MQKJ.BSMP.ApplicationLogs.DomainService;
using MQKJ.BSMP.ApplicationLogs.Authorization;


namespace MQKJ.BSMP.ApplicationLogs
{
    /// <summary>
    /// ApplicationLog应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ApplicationLogAppService : BSMPAppServiceBase, IApplicationLogAppService
    {
        private readonly IRepository<ApplicationLog, int> _entityRepository;

        private readonly IApplicationLogManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ApplicationLogAppService(
        IRepository<ApplicationLog, int> entityRepository
        ,IApplicationLogManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取ApplicationLog的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[AbpAuthorize(ApplicationLogPermissions.Query)] 
        public async Task<PagedResultDto<ApplicationLogListDto>> GetPaged(GetApplicationLogsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ApplicationLogListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ApplicationLogListDto>>();

			return new PagedResultDto<ApplicationLogListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ApplicationLogListDto信息
		/// </summary>
		[AbpAuthorize(ApplicationLogPermissions.Query)] 
		public async Task<ApplicationLogListDto> GetById(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ApplicationLogListDto>();
		}

		/// <summary>
		/// 获取编辑 ApplicationLog
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(ApplicationLogPermissions.Create,ApplicationLogPermissions.Edit)]
		public async Task<GetApplicationLogForEditOutput> GetForEdit(NullableIdDto<int> input)
		{
			var output = new GetApplicationLogForEditOutput();
ApplicationLogEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ApplicationLogEditDto>();

				//applicationLogEditDto = ObjectMapper.Map<List<applicationLogEditDto>>(entity);
			}
			else
			{
				editDto = new ApplicationLogEditDto();
			}

			output.ApplicationLog = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ApplicationLog的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(ApplicationLogPermissions.Create,ApplicationLogPermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdateApplicationLogInput input)
		{

			if (input.ApplicationLog.Id.HasValue)
			{
				await Update(input.ApplicationLog);
			}
			else
			{
				await Create(input.ApplicationLog);
			}
		}


		/// <summary>
		/// 新增ApplicationLog
		/// </summary>
		[AbpAuthorize(ApplicationLogPermissions.Create)]
		protected virtual async Task<ApplicationLogEditDto> Create(ApplicationLogEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ApplicationLog>(input);
            var entity=input.MapTo<ApplicationLog>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<ApplicationLogEditDto>();
		}

		/// <summary>
		/// 编辑ApplicationLog
		/// </summary>
		[AbpAuthorize(ApplicationLogPermissions.Edit)]
		protected virtual async Task Update(ApplicationLogEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ApplicationLog信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(ApplicationLogPermissions.Delete)]
		public async Task Delete(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ApplicationLog的方法
		/// </summary>
		[AbpAuthorize(ApplicationLogPermissions.BatchDelete)]
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出ApplicationLog为excel表,等待开发。
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


