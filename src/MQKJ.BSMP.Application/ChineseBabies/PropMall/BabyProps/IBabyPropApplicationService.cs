
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


using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyProp应用层服务的接口方法
    ///</summary>
    public interface IBabyPropAppService : BsmpApplicationService<BabyProp, int, BabyPropEditDto, BabyPropEditDto, GetBabyPropsInput, BabyPropListDto>
    {
        /// <summary>
        /// 道具列表
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<GetBabyPropsOutput>> GetPage(GetBabyPropsInput input);

        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PostBuyPropOutput> PostBuyPropAsync(PostBuyPropInput input);

        void BuyFreeProps(int babyId, int familyId, Guid playerId);
        /// <summary>
        /// 获取家庭购买信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ICollection<GetFamilyPropBuyInfoOutput>> GetFamilyPropBuyInfo(GetFamilyPropBuyInfoInput input);
        /// <summary>
        /// 更新家庭资产和宝宝附加属性
        /// </summary>
        /// <param name="input"></param>
        /// <param name="prop"></param>
        /// <param name="price"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        //Task<BabyFamilyAsset> UpdateFamilyAssetAndBabyAward(PostBuyPropInput input, BabyProp prop, BabyPropPrice price, Family family);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BabyFamilyAsset> UpdateFamilyAssetAndBabyAward(UpdateFamilyAssetAndBabyAwardDto input);
    }
}
