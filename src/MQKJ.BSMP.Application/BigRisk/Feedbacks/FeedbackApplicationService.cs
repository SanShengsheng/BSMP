
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


using MQKJ.BSMP.Feedbacks;
using MQKJ.BSMP.Feedbacks.Dtos;
using MQKJ.BSMP.Feedbacks.DomainService;
using MQKJ.BSMP.Feedbacks.Authorization;


namespace MQKJ.BSMP.Feedbacks
{
    /// <summary>
    /// Feedback应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class FeedbackAppService : BSMPAppServiceBase, IFeedbackAppService
    {
        private readonly IRepository<Feedback, Guid> _entityRepository;

        private readonly IFeedbackManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public FeedbackAppService(
        IRepository<Feedback, Guid> entityRepository
        ,IFeedbackManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取Feedback的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(FeedbackPermissions.Query)] 
        public async Task<PagedResultDto<FeedbackListDto>> GetPaged(GetFeedbacksInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<FeedbackListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<FeedbackListDto>>();

			return new PagedResultDto<FeedbackListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取FeedbackListDto信息
		/// </summary>
		//[AbpAuthorize(FeedbackPermissions.Query)] 
		public async Task<FeedbackListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<FeedbackListDto>();
		}

		/// <summary>
		/// 获取编辑 Feedback
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(FeedbackPermissions.Create,FeedbackPermissions.Edit)]
		public async Task<GetFeedbackForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetFeedbackForEditOutput();
FeedbackEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<FeedbackEditDto>();

				//feedbackEditDto = ObjectMapper.Map<List<feedbackEditDto>>(entity);
			}
			else
			{
				editDto = new FeedbackEditDto();
			}

			output.Feedback = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Feedback的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(FeedbackPermissions.Create,FeedbackPermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdateFeedbackInput input)
		{

			if (input.Feedback.Id.HasValue)
			{
				await Update(input.Feedback);
			}
			else
			{
				await Create(input.Feedback);
			}
		}


		/// <summary>
		/// 新增Feedback
		/// </summary>
		//[AbpAuthorize(FeedbackPermissions.Create)]
		protected virtual async Task<FeedbackEditDto> Create(FeedbackEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Feedback>(input);
            var entity=input.MapTo<Feedback>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<FeedbackEditDto>();
		}

		/// <summary>
		/// 编辑Feedback
		/// </summary>
		//[AbpAuthorize(FeedbackPermissions.Edit)]
		protected virtual async Task Update(FeedbackEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Feedback信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(FeedbackPermissions.Delete)]
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Feedback的方法
		/// </summary>
		//[AbpAuthorize(FeedbackPermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出Feedback为excel表,等待开发。
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


