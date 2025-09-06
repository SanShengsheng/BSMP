
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.WeChatWebUsers.Dtos;

namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// WeChatWebUser应用层服务的接口方法
    ///</summary>
    public interface IWeChatWebUserAppService : IApplicationService
    {
        /// <summary>
		/// 获取WeChatWebUser的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<WeChatWebUserListDto>> GetPaged(GetWeChatWebUsersInput input);


		/// <summary>
		/// 通过指定id获取WeChatWebUserListDto信息
		/// </summary>
		Task<WeChatWebUserListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetWeChatWebUserForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改WeChatWebUser的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateWeChatWebUserInput input);


        /// <summary>
        /// 删除WeChatWebUser信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除WeChatWebUser
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 更新微信账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SignUp(UpdateUserStateInput input);

        Task Match(UpdateUserStateInput input);

    }
}
