
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


using MQKJ.BSMP.LoveCards.Dtos;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.LoveCard.LoveCards.Dtos;

namespace MQKJ.BSMP.LoveCards
{
    /// <summary>
    /// LoveCard应用层服务的接口方法
    ///</summary>
    public interface ILoveCardAppService : IApplicationService
    {
        /// <summary>
		/// 获取LoveCard的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LoveCardListDto>> GetPaged(GetLoveCardsInput input);


		/// <summary>
		/// 通过指定id获取LoveCardListDto信息
		/// </summary>
		Task<LoveCardListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLoveCardForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LoveCard的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLoveCardInputbak input);


        /// <summary>
        /// 删除LoveCard信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LoveCard
        /// </summary>
        Task BatchDelete(List<Guid> input);

        /// <summary>
        /// 获取玩家自己的信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoveCardListDto> GetLoveCardInfo(GetLoveCardInfoInput input);

        /// <summary>
        /// 获取点赞和分享的列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetLikeAndSharePlayerListOutput>> GetLikeAndSharePlayerList(GetLikeAndSharePlayerListInput input);

        /// <summary>
        /// 保存或更新卡片信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreateOrUpdateLoveCardOutput> CreateOrUpdateLoveCard(CreateOrUpdateLoveCardInput input);

        Task<CreateOrUpdateLoveCardOutput> UpdateLoveCardOtherInfo(UpdateLoveCardOtherInfoInput input);
        Task<PagedResultDto<LoveCardListDto>> GetAllCardList(GetAllCardListInput input);

        /// <summary>
        /// 获取卡片信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoveCardListDto> GetLoveCardById(EntityDto<Guid> input);


        /// <summary>
        /// 导出LoveCard为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
