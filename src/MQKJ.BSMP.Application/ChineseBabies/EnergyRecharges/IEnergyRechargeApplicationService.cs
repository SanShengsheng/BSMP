
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
using MQKJ.BSMP.ChineseBabies.EnergyRecharges.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// EnergyRecharge应用层服务的接口方法
    ///</summary>
    public interface IEnergyRechargeAppService : BsmpApplicationService<EnergyRecharge,int,EnergyRechargeEditDto,EnergyRechargeEditDto,GetEnergyRechargesInput,EnergyRechargeListDto>
    {
        /// <summary>
        /// 购买精力
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BuyEnergyOutput> BuyEnergy(BuyEnergyInput input);

        /// <summary>
        /// 自动购买精力
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BuyEnergyOutput> AutoBuyEnergy(BuyEnergyInput input);
    }
}
