
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


using MQKJ.BSMP.Feedbacks.Dtos;
using MQKJ.BSMP.Feedbacks;

namespace MQKJ.BSMP.Feedbacks
{
    /// <summary>
    /// Feedback应用层服务的接口方法
    ///</summary>
    public interface IFeedbackAppService : IApplicationService
    {
        /// <summary>
		/// 获取Feedback的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<FeedbackListDto>> GetPaged(GetFeedbacksInput input);


		/// <summary>
		/// 通过指定id获取FeedbackListDto信息
		/// </summary>
		Task<FeedbackListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFeedbackForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Feedback的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateFeedbackInput input);


        /// <summary>
        /// 删除Feedback信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Feedback
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出Feedback为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
