
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


using MQKJ.BSMP.LoveCardOptions.Dtos;
using MQKJ.BSMP.LoveCardOptions;

namespace MQKJ.BSMP.LoveCardOptions
{
    /// <summary>
    /// LoveCardOption应用层服务的接口方法
    ///</summary>
    public interface ILoveCardOptionAppService : IApplicationService
    {
        /// <summary>
		/// 获取LoveCardOption的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LoveCardOptionListDto>> GetPaged(GetLoveCardOptionsInput input);


		/// <summary>
		/// 通过指定id获取LoveCardOptionListDto信息
		/// </summary>
		Task<LoveCardOptionListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLoveCardOptionForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LoveCardOption的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLoveCardOptionInput input);


        /// <summary>
        /// 删除LoveCardOption信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LoveCardOption
        /// </summary>
        Task BatchDelete(List<Guid> input);

        //Task OptionCardState(OptionCardStateDto input);


        /// <summary>
        /// 导出LoveCardOption为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
