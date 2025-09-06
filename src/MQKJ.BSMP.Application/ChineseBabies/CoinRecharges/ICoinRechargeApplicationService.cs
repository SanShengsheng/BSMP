
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
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.ChineseBabies.CoinRechargeRecords.Dtos;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Alipay;
using MQKJ.BSMP.Pay.Alipay.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// CoinRecharge应用层服务的接口方法
    ///</summary>
    public interface ICoinRechargeAppService : BsmpApplicationService<CoinRecharge, int, CoinRechargeEditDto, CoinRechargeEditDto, GetCoinRechargesInput, CoinRechargeListDto>
    {
        /// <summary>
        /// 获取充值金币列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PagedResultDto<CoinRechargeListDto>> GetCoinRecharges(GetCoinRechargesInput model);

        /// <summary>
        /// 购买金币
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MiniProgramPayOutput> BuyCoins(BuyCoinsInput input);
        Task<PagedResultDto<CoinRechargeListDto>> GetVirtualCoins(GetCoinRechargesInput input);

        /// <summary>
        /// 获取充值结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UpdateOrderStateOutput> GetCoinRechargeResult(UpdateOrderStateInput input);


        /// <summary>
        /// 更新充值后的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task UpdateRechargeData(UpdateRechargeDataInput input);

        /// <summary>
        /// 虚拟充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UpdateOrderStateOutput> VirtualRecharge(VirtualRechargeInput input);
        /// <summary>
        /// 开启/关闭 虚拟充值权限
        /// </summary>
        /// <param name="playerid">玩家ID</param>
        /// <returns></returns>
        Task<ToggleVtlRhgPermOutput> ToggleVtlRhgPerm(string playerid);
        /// <summary>
        /// 重新计算可提现金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task RecalBalance(ReCalAgentIncomeRequest request);

        /// <summary>
        /// 补充金币 | 补充金币跟订单跟流水
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SupplementCoinRechargeOutput> SupplementCoinRecharge(SupplementCoinRechargeInput input);

        /// <summary>
        /// 充值金币
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task RechargeCoin(RechargeCoinRequest request);
        Task<string> WechatPayNotify();
        Task<UpdateOrderStateOutput> QueryOrderState(UpdateOrderStateInput input);

        /// <summary>
        /// 重新计算代理收益
        /// </summary>
        /// <returns></returns>
        Task ReCalAgentIncome(ReCalAgentIncomeRequest request);

        /// <summary>
        /// 为家庭补充金币
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SupplementCoinForFamily(SupplementCoinForFamilyInput input);

        /// <summary>
        /// 补充金币
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task V2_SupplementCoinForFamily(SupplementCoinForFamilyInput input);

        /// <summary>
        /// 补贴流水
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SubsidyMoneyOutput> SubsidyMoney(SubsidyMoneyInput input);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        Task<bool> SendMessageValideCode();
        /// <summary>
        /// 接收支付宝异步通知
        /// </summary>
        /// <returns></returns>
         Task<AliPayNotifyOutput> Receive_Notify(AliPayNotifyRsultAsyncDto input);
    }
}
