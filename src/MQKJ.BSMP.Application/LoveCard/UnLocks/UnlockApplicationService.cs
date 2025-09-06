
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


using MQKJ.BSMP.UnLocks;
using MQKJ.BSMP.UnLocks.Dtos;
using MQKJ.BSMP.UnLocks.DomainService;
using MQKJ.BSMP.UnLocks.Authorization;

namespace MQKJ.BSMP.UnLocks
{
    /// <summary>
    /// Unlock应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class UnlockAppService : BSMPAppServiceBase, IUnlockAppService
    {
        private readonly IRepository<Unlock, Guid> _entityRepository;

        private readonly IUnlockManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public UnlockAppService(
        IRepository<Unlock, Guid> entityRepository
        ,IUnlockManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取Unlock的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(UnlockPermissions.Query)] 
        public async Task<PagedResultDto<UnlockListDto>> GetPaged(GetUnlocksInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<UnlockListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<UnlockListDto>>();

			return new PagedResultDto<UnlockListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取UnlockListDto信息
		/// </summary>
		//[AbpAuthorize(UnlockPermissions.Query)] 
		public async Task<UnlockListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<UnlockListDto>();
		}

		/// <summary>
		/// 获取编辑 Unlock
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(UnlockPermissions.Create,UnlockPermissions.Edit)]
		public async Task<GetUnlockForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetUnlockForEditOutput();
UnlockEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<UnlockEditDto>();

				//unlockEditDto = ObjectMapper.Map<List<unlockEditDto>>(entity);
			}
			else
			{
				editDto = new UnlockEditDto();
			}

			output.Unlock = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Unlock的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(UnlockPermissions.Create,UnlockPermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdateUnlockInput input)
		{

			if (input.Unlock.Id.HasValue)
			{
				await Update(input.Unlock);
			}
			else
			{
				await Create(input.Unlock);
			}
		}


		/// <summary>
		/// 新增Unlock
		/// </summary>
		//[AbpAuthorize(UnlockPermissions.Create)]
		protected virtual async Task<UnlockEditDto> Create(UnlockEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Unlock>(input);
            var entity=input.MapTo<Unlock>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<UnlockEditDto>();
		}

		/// <summary>
		/// 编辑Unlock
		/// </summary>
		//[AbpAuthorize(UnlockPermissions.Edit)]
		protected virtual async Task Update(UnlockEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Unlock信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(UnlockPermissions.Delete)]
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Unlock的方法
		/// </summary>
		//[AbpAuthorize(UnlockPermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        //public async Task UnlockWeChatAccount(UnlockWeChatAccountInput input)
        //{
        //    await _entityRepository.InsertAsync(new Unlock()
        //    {
        //        UnLockerId = input.UnLockerId,
        //        BeUnLockerId = input.BeUnLockerId
        //    });
        //}


        /// <summary>
        /// 导出Unlock为excel表,等待开发。
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


