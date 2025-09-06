
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
using MQKJ.BSMP.TextAudios.Dtos;

namespace MQKJ.BSMP.TextAudios
{
    /// <summary>
    /// TextAudios应用层服务的接口方法
    ///</summary>
    public interface ITextAudiosAppService : IApplicationService
    {
        /// <summary>
		/// 获取TextAudios的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<TextAudiosListDto>> GetPaged(GetTextAudiossInput input);


		/// <summary>
		/// 通过指定id获取TextAudiosListDto信息
		/// </summary>
		Task<TextAudiosListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetTextAudiosForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改TextAudios的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateTextAudiosInput input);


        /// <summary>
        /// 删除TextAudios信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除TextAudios
        /// </summary>
        Task BatchDelete(List<int> input);


		/// <summary>
        /// 导出TextAudios为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
