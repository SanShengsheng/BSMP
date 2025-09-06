
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


using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.PlayerLabels.Dtos;
using MQKJ.BSMP.PlayerLabels.DomainService;
using MQKJ.BSMP.PlayerLabels.Authorization;


namespace MQKJ.BSMP.PlayerLabels
{
    /// <summary>
    /// PlayerLabel应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class PlayerLabelAppService : BSMPAppServiceBase, IPlayerLabelAppService
    {
        private readonly IRepository<PlayerLabel, Guid> _entityRepository;

        private readonly IPlayerLabelManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public PlayerLabelAppService(
        IRepository<PlayerLabel, Guid> entityRepository
        ,IPlayerLabelManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取PlayerLabel的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[AbpAuthorize(PlayerLabelPermissions.Query)] 
        public async Task<PagedResultDto<PlayerLabelListDto>> GetPaged(GetPlayerLabelsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<PlayerLabelListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<PlayerLabelListDto>>();

			return new PagedResultDto<PlayerLabelListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取PlayerLabelListDto信息
		/// </summary>
		[AbpAuthorize(PlayerLabelPermissions.Query)] 
		public async Task<PlayerLabelListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<PlayerLabelListDto>();
		}

		/// <summary>
		/// 获取编辑 PlayerLabel
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(PlayerLabelPermissions.Create,PlayerLabelPermissions.Edit)]
		public async Task<GetPlayerLabelForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetPlayerLabelForEditOutput();
PlayerLabelEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<PlayerLabelEditDto>();

				//playerLabelEditDto = ObjectMapper.Map<List<playerLabelEditDto>>(entity);
			}
			else
			{
				editDto = new PlayerLabelEditDto();
			}

			output.PlayerLabel = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改PlayerLabel的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(PlayerLabelPermissions.Create,PlayerLabelPermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdatePlayerLabelInput input)
		{

			if (input.PlayerLabel.Id.HasValue)
			{
				await Update(input.PlayerLabel);
			}
			else
			{
				await Create(input.PlayerLabel);
			}
		}

        //public Task CreatePlayerLabel(CreatePlayerLabelInput input)
        //{
        //    var entity = _entityRepository.FirstOrDefaultAsync(x => x.LoveCardId == input.LoveCardId);

        //    if (entity != null)
        //    {
        //        foreach (var item in input.Labels)
        //        {

        //        }
        //        _entityRepository.UpdateAsync(new PlayerLabel)
        //    }
        //    else
        //    {

        //    }
        //}


		/// <summary>
		/// 新增PlayerLabel
		/// </summary>
		[AbpAuthorize(PlayerLabelPermissions.Create)]
		protected virtual async Task<PlayerLabelEditDto> Create(PlayerLabelEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <PlayerLabel>(input);
            var entity=input.MapTo<PlayerLabel>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<PlayerLabelEditDto>();
		}

		/// <summary>
		/// 编辑PlayerLabel
		/// </summary>
		[AbpAuthorize(PlayerLabelPermissions.Edit)]
		protected virtual async Task Update(PlayerLabelEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除PlayerLabel信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(PlayerLabelPermissions.Delete)]
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除PlayerLabel的方法
		/// </summary>
		[AbpAuthorize(PlayerLabelPermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出PlayerLabel为excel表,等待开发。
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


