
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


using MQKJ.BSMP.PlayerLabels.Dtos;
using MQKJ.BSMP.PlayerLabels;

namespace MQKJ.BSMP.PlayerLabels
{
    /// <summary>
    /// PlayerLabel应用层服务的接口方法
    ///</summary>
    public interface IPlayerLabelAppService : IApplicationService
    {
        /// <summary>
		/// 获取PlayerLabel的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PlayerLabelListDto>> GetPaged(GetPlayerLabelsInput input);


		/// <summary>
		/// 通过指定id获取PlayerLabelListDto信息
		/// </summary>
		Task<PlayerLabelListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPlayerLabelForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改PlayerLabel的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdatePlayerLabelInput input);


        /// <summary>
        /// 删除PlayerLabel信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除PlayerLabel
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出PlayerLabel为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
