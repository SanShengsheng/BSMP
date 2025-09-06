
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


using MQKJ.BSMP.Orders.Dtos;
using MQKJ.BSMP.Orders;
using System.IO;

namespace MQKJ.BSMP.Orders
{
    /// <summary>
    /// Order应用层服务的接口方法
    ///</summary>
    public interface IOrderAppService : IApplicationService
    {
        /// <summary>
		/// 获取Order的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrderListDto>> GetPaged(GetOrdersInput input);


		/// <summary>
		/// 通过指定id获取OrderListDto信息
		/// </summary>
		Task<OrderListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetOrderForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Order的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateOrderInput input);


        /// <summary>
        /// 删除Order信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Order
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task CreateOrder(CreateOrderInput input);

        /// <summary>
        /// 查询订单号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderEditDto> QueryOrder(QueryOrderInput input);

        Task UpdateOrderState(WechatPayUpdateOrderStateInput input);
        Task UpdateBabyOrderState(WechatPayUpdateOrderStateInput input);

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetToExcel(GetOrdersInput input);
        /// <summary>
        /// 导出Order为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
