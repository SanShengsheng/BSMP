
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


using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.WeChatWebUsers.Dtos;

namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// WeChatWebUser应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class WeChatWebUserAppService : BSMPAppServiceBase, IWeChatWebUserAppService
    {
        private readonly IRepository<WeChatWebUser, Guid> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public WeChatWebUserAppService(
        IRepository<WeChatWebUser, Guid> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取WeChatWebUser的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<WeChatWebUserListDto>> GetPaged(GetWeChatWebUsersInput input)
		{

		    var query = _entityRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.NickName),x => x.NickName.Contains(input.NickName));
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<WeChatWebUserListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<WeChatWebUserListDto>>();

			return new PagedResultDto<WeChatWebUserListDto>(count,entityListDtos);
		}

        public async Task SignUp(UpdateUserStateInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(a => a.Id == input.Id);

            if (entity == null)
            {
                throw new Exception("不存在该用户");
            }

            if (string.IsNullOrEmpty(input.WechatAccount))
            {
                throw new Exception("参数不能为空");
            }

            entity.WechatAccount = input.WechatAccount;

            entity.State = UserState.Enrolment;

            await _entityRepository.UpdateAsync(entity);
        }

        public async Task Match(UpdateUserStateInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(a => a.Id == input.Id);

            if (entity == null)
            {
                throw new Exception("不存在该用户");
            }

            if (string.IsNullOrEmpty(input.WechatAccount))
            {
                throw new Exception("参数不能为空");
            }

            entity.OtherWechatAccount = input.WechatAccount;

            entity.State = UserState.AlreadyMatched;

            await _entityRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 通过指定id获取WeChatWebUserListDto信息
        /// </summary>

        public async Task<WeChatWebUserListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<WeChatWebUserListDto>();
		}

		/// <summary>
		/// 获取编辑 WeChatWebUser
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetWeChatWebUserForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetWeChatWebUserForEditOutput();
WeChatWebUserEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<WeChatWebUserEditDto>();

				//weChatWebUserEditDto = ObjectMapper.Map<List<weChatWebUserEditDto>>(entity);
			}
			else
			{
				editDto = new WeChatWebUserEditDto();
			}

			output.WeChatWebUser = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改WeChatWebUser的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateWeChatWebUserInput input)
		{

			if (input.WeChatWebUser.Id.HasValue)
			{
				await Update(input.WeChatWebUser);
			}
			else
			{
				await Create(input.WeChatWebUser);
			}
		}


		/// <summary>
		/// 新增WeChatWebUser
		/// </summary>
		
		protected virtual async Task<WeChatWebUserEditDto> Create(WeChatWebUserEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <WeChatWebUser>(input);
            var entity=input.MapTo<WeChatWebUser>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<WeChatWebUserEditDto>();
		}

		/// <summary>
		/// 编辑WeChatWebUser
		/// </summary>
		
		protected virtual async Task Update(WeChatWebUserEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除WeChatWebUser信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除WeChatWebUser的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出WeChatWebUser为excel表,等待开发。
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


