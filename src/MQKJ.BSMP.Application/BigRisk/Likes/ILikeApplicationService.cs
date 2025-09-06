
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


using MQKJ.BSMP.Likes.Dtos;
using MQKJ.BSMP.Likes;

namespace MQKJ.BSMP.Likes
{
    /// <summary>
    /// Like应用层服务的接口方法
    ///</summary>
    public interface ILikeAppService : IApplicationService
    {
        /// <summary>
		/// 获取Like的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LikeListDto>> GetPaged(GetLikesInput input);


		/// <summary>
		/// 通过指定id获取LikeListDto信息
		/// </summary>
		Task<LikeListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLikeForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Like的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLikeInput input);

        Task<LikeListDto> GetLikeState(GetLikeStateInput input);


        /// <summary>
        /// 删除Like信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Like
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出Like为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
