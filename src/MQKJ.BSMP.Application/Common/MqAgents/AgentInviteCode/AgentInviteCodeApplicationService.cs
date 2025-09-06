
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


using MQKJ.BSMP.Common.MqAgents;
using MQKJ.BSMP.Common.MqAgents.Dtos;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Common.MqAgents
{
    /// <summary>
    /// AgentInviteCode应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class AgentInviteCodeAppService : BSMPAppServiceBase, IAgentInviteCodeAppService
    {
        private readonly IRepository<AgentInviteCode, int> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public AgentInviteCodeAppService(
        IRepository<AgentInviteCode, int> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取AgentInviteCode的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<AgentInviteCodeListDto>> GetPaged(GetAgentInviteCodesInput input)
		{

		    var query = _entityRepository.GetAll().Include(a => a.MqAgent)
                .WhereIf(input.MqAgentCategory != 0,a => a.MqAgentCategory == input.MqAgentCategory)
                .WhereIf(input.InviteCodeState != 0, a => a.State == input.InviteCodeState);
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<AgentInviteCodeListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<AgentInviteCodeListDto>>();

			return new PagedResultDto<AgentInviteCodeListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取AgentInviteCodeListDto信息
		/// </summary>
		 
		public async Task<AgentInviteCodeListDto> GetById(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<AgentInviteCodeListDto>();
		}

		/// <summary>
		/// 获取编辑 AgentInviteCode
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetAgentInviteCodeForEditOutput> GetForEdit(NullableIdDto<int> input)
		{
			var output = new GetAgentInviteCodeForEditOutput();
AgentInviteCodeEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<AgentInviteCodeEditDto>();

				//agentInviteCodeEditDto = ObjectMapper.Map<List<agentInviteCodeEditDto>>(entity);
			}
			else
			{
				editDto = new AgentInviteCodeEditDto();
			}

			output.AgentInviteCode = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改AgentInviteCode的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        //[AbpAuthorize(MqAgentPermissions.GetAgentInviteCodes)]
        public async Task CreateOrUpdate(CreateOrUpdateAgentInviteCodeInput input)
		{

			if (input.AgentInviteCode.Id.HasValue)
			{
				await Update(input.AgentInviteCode);
			}
			else
			{
				await Create(input.AgentInviteCode);
			}
		}


        /// <summary>
        /// 新增AgentInviteCode
        /// </summary>
        protected virtual async Task<AgentInviteCodeEditDto> Create(AgentInviteCodeEditDto input)
		{
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <AgentInviteCode>(input);

            var codes = await _entityRepository.GetAll().Select(x => x.Code).AsNoTracking().ToListAsync();

            var code = RandomHelper.GetRandomCode(codes);

            input.Code = code;

            //input.MqAgentCategory = MqAgentCategory.AdministratorCategory;

            var entity=input.MapTo<AgentInviteCode>();
			

			entity = await _entityRepository.InsertAsync(entity);

			return entity.MapTo<AgentInviteCodeEditDto>();
		}

        public async Task AddInviteCode(AgentInviteCodeEditDto input)
        {
            var codes = await _entityRepository.GetAll().Select(x => x.Code).AsNoTracking().ToListAsync();

            var code = RandomHelper.GetRandomCode(codes);

            input.Code = code;

            //input.MqAgentCategory = MqAgentCategory.AdministratorCategory;

            var entity = input.MapTo<AgentInviteCode>();

            await _entityRepository.InsertAsync(entity);
        }

		/// <summary>
		/// 编辑AgentInviteCode
		/// </summary>
		
		protected virtual async Task Update(AgentInviteCodeEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除AgentInviteCode信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除AgentInviteCode的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出AgentInviteCode为excel表,等待开发。
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


