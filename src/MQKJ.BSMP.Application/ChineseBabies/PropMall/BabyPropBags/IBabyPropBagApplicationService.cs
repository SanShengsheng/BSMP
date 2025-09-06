
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


using MQKJ.BSMP.ChineseBabies.PropMall.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// BabyPropBag应用层服务的接口方法
    ///</summary>
    public interface IBabyPropBagAppService : IApplicationService
    {
        /// <summary>
        /// 获取最新的大礼包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPropBagLastestOutput> GetPropBagLastest(GetPropBagLastestInput input);

        /// <summary>
        ///  购买道具大礼包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PostBuyPropBagOutput> PostBuyPropBag(PostBuyPropBagInput input);

        Task AddBoughtBigBagRecord(PostBuyPropBagInput input);

        /// <summary>
        /// 获取充值结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<UpdateOrderStateOutput> GetBoughtBigBagPayResult(UpdateOrderStateInput input);

        /// <summary>
        /// 购买大礼包回调
        /// </summary>
        /// <returns></returns>
        //Task<string> BoughtBigBagPayNotify();

        /// <summary>
        /// 购买大礼包成功后处理的逻辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddBoughtRecordAndInformations(UpdateOrderStateInput input);
    }
}
