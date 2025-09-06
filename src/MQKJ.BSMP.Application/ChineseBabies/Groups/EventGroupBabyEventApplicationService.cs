
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


using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;




namespace MQKJ.BSMP.ChineseBabies
{

    public class EventGroupBabyEventAppService : BSMPAppServiceBase, IEventGroupBabyEventAppService
    {
        private readonly IRepository<EventGroupBabyEvent, int> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EventGroupBabyEventAppService(
        IRepository<EventGroupBabyEvent, int> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取EventGroupBabyEvent的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<EventGroupBabyEventListDto>> GetPaged(GetEventGroupBabyEventsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<EventGroupBabyEventListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<EventGroupBabyEventListDto>>();

			return new PagedResultDto<EventGroupBabyEventListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取EventGroupBabyEventListDto信息
		/// </summary>
		 
		public async Task<EventGroupBabyEventListDto> GetById(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<EventGroupBabyEventListDto>();
		}

		/// <summary>
		/// 获取编辑 EventGroupBabyEvent
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetEventGroupBabyEventForEditOutput> GetForEdit(NullableIdDto<int> input)
		{
			var output = new GetEventGroupBabyEventForEditOutput();
EventGroupBabyEventEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<EventGroupBabyEventEditDto>();

				//eventGroupBabyEventEditDto = ObjectMapper.Map<List<eventGroupBabyEventEditDto>>(entity);
			}
			else
			{
				editDto = new EventGroupBabyEventEditDto();
			}

			output.EventGroupBabyEvent = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改EventGroupBabyEvent的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateEventGroupBabyEventInput input)
		{

			if (input.EventGroupBabyEvent.Id.HasValue)
			{
				await Update(input.EventGroupBabyEvent);
			}
			else
			{
				await Create(input.EventGroupBabyEvent);
			}
		}


		/// <summary>
		/// 新增EventGroupBabyEvent
		/// </summary>
		
		protected virtual async Task<EventGroupBabyEventEditDto> Create(EventGroupBabyEventEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <EventGroupBabyEvent>(input);
            var entity=input.MapTo<EventGroupBabyEvent>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<EventGroupBabyEventEditDto>();
		}

		/// <summary>
		/// 编辑EventGroupBabyEvent
		/// </summary>
		
		protected virtual async Task Update(EventGroupBabyEventEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除EventGroupBabyEvent信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除EventGroupBabyEvent的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出EventGroupBabyEvent为excel表,等待开发。
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


