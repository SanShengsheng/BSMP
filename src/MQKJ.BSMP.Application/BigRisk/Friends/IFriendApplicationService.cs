
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


using MQKJ.BSMP.Friends.Dtos;
using MQKJ.BSMP.Friends;
using Microsoft.AspNetCore.Mvc;

namespace MQKJ.BSMP.Friends
{
    /// <summary>
    /// Friend应用层服务的接口方法
    ///</summary>
    public interface IFriendAppService : IApplicationService
    {
        /// <summary>
		/// 获取Friend的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<FriendListDto>> GetPagedFriends(GetFriendsInput input);


		/// <summary>
		/// 通过指定id获取FriendListDto信息
		/// </summary>
		Task<FriendListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFriendForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Friend的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateFriendInput input);


        /// <summary>
        /// 删除Friend信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Friend
        /// </summary>
        Task BatchDelete(List<Guid> input);

        /// <summary>
        /// 跟新催促状态(催促好友，已读催促信息)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateUrgeState(EntityDto<Guid> input);

        /// <summary>
        /// 更新心的数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateHeartCount(EntityDto<Guid> input);

        /// <summary>
        /// 更新关卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateBarrier(EntityDto<Guid> input);

        /// <summary>
        /// 导出Friend为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

        Task<int> GetUrgeCount(GetUrgeCountDto input);

        Task<PagedResultDto<GetAllFriendListOutput>> GetAllFriendList(GetAllFriendListDto input);

        Task UpdateFloor(UpdateFloorDto input);
    }
}
