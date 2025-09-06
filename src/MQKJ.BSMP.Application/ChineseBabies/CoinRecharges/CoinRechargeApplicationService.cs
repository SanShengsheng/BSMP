using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Extensions;
using Abp.Json;
using Abp.Linq.Extensions;
using Abp.UI;
using Abp.Web.Models;
using JCSoft.WX.Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.Alipay;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.ChineseBabies.Authorization;
using MQKJ.BSMP.ChineseBabies.CoinRechargeRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Migrations;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Pay.Alipay.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.QCloud;
using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Models.CMQ.Requests;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.Utils.JpushTool.Dtos;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// CoinRecharge应用层服务的接口实现方法
    ///</summary>
    //[AbpAuthorize]
    public class CoinRechargeAppService :
        BsmpApplicationServiceBase<CoinRecharge, int, CoinRechargeEditDto, CoinRechargeEditDto, GetCoinRechargesInput, CoinRechargeListDto>,
        ICoinRechargeAppService
    {
        private readonly IWeChatPayAppService _weChatPayAppService;

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IFamilyAppService _familyAppService;

        //private readonly ICoinRechargeRecordAppService _coinRechargeRecordAppService;

        private readonly IRepository<CoinRechargeRecord, Guid> _coinRechargeRecordRepository;

        private readonly IRepository<Order, Guid> _orderRepository;

        private readonly IRepository<Family, int> _familyRepository;

        private readonly IRepository<Information, Guid> _informationAppService;

        private readonly IRepository<MqAgent, int> _agentRepository;

        private readonly IRepository<IncomeRecord, Guid> _incomeRecordRepository;
        private readonly IRepository<EnterpirsePaymentRecord, Guid> _enterpirsePayRepository;

        private readonly IRepository<SupplementCoinRecord, Guid> _supplementCoinRecordRepository;
        private readonly IBabyPropBagAppService _propBagAppService;

        private JpushMessageConfig _jpushMessageConfig;

        private readonly IQCloudApiClient _qcouldApiClient;
        private readonly QcloudConfig _config;
        public IEventBus EventBus { get; set; }
        private readonly IRepository<FamilyCoinDepositChangeRecord, Guid> _familyCoinDeposiCRepository;

        private readonly IRepository<SystemSetting> _systemSettingRepository;

        private IOptions<QueryOrderConfig> _queryOrderOption;

        private readonly UserManager _userManager;

        private readonly IAlipayAppService _alipayAppService;
        /// <summary>
        /// 构造函数
        ///</summary>
        public CoinRechargeAppService(
        IRepository<CoinRecharge, int> entityRepository
            , IWeChatPayAppService weChatPayAppService
            , IRepository<Player, Guid> playerRepository
            , IFamilyAppService familyAppService
            , IRepository<Family, int> familyRepository
            //, ICoinRechargeRecordAppService coinRechargeRecordAppService
            , IRepository<CoinRechargeRecord, Guid> coinRechargeRecordRepository
            , IRepository<Order, Guid> orderRepository
            , IRepository<Information, Guid> informationAppService
            , IRepository<MqAgent, int> agentRepository
            , IOptions<JpushMessageConfig> jpushMessageOption
            , IRepository<IncomeRecord, Guid> incomeRecordRepository,
        IRepository<EnterpirsePaymentRecord, Guid> enterpirsePayRepository,
        IQCloudApiClient qcloudApiClient,
        IOptions<QcloudConfig> configOptions,
        IRepository<SupplementCoinRecord, Guid> supplementCoinRecordRepository,
        IBabyPropBagAppService propBagAppService,
        IRepository<FamilyCoinDepositChangeRecord, Guid> familyCoinDeposiCRepository,
         IRepository<SystemSetting> systemSettingRepository,
        IOptions<QueryOrderConfig> querOrderOption,
        UserManager userManager,
        IAlipayAppService alipayAppservice
            ) : base(entityRepository)
        {
            _weChatPayAppService = weChatPayAppService;

            _playerRepository = playerRepository;

            _orderRepository = orderRepository;

            //_coinRechargeRecordAppService = coinRechargeRecordAppService;
            _coinRechargeRecordRepository = coinRechargeRecordRepository;

            _familyRepository = familyRepository;

            _informationAppService = informationAppService;

            EventBus = NullEventBus.Instance;

            _agentRepository = agentRepository;

            _incomeRecordRepository = incomeRecordRepository;
            _enterpirsePayRepository = enterpirsePayRepository;
            _qcouldApiClient = qcloudApiClient;
            _config = configOptions?.Value;

            _supplementCoinRecordRepository = supplementCoinRecordRepository;

            _propBagAppService = propBagAppService;
            _familyCoinDeposiCRepository = familyCoinDeposiCRepository;

            _queryOrderOption = querOrderOption;

            _familyAppService = familyAppService;
            _systemSettingRepository = systemSettingRepository;

            _jpushMessageConfig = jpushMessageOption.Value;
            _userManager = userManager;
            _alipayAppService = alipayAppservice;
        }

        internal override IQueryable<CoinRecharge> GetQuery(GetCoinRechargesInput model)
        {


            return _repository.GetAll();
        }

        public async Task<PagedResultDto<CoinRechargeListDto>> GetCoinRecharges(GetCoinRechargesInput model)
        {
            var rechargeRecordIds = _coinRechargeRecordRepository.GetAll()
                .Where(r => r.RechargerId == model.PlayerId && r.FamilyId == model.FamilyId)
                .Select(r => r.CoinRechargeId);

            var query = _repository.GetAll()
                .Where(c => c.IsShow)
                .OrderBy(s => s.RechargeLevel);

            var count = query.Count();

            var entityList = await query
                    .PageBy(model)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<CoinRechargeListDto>>();

            if (entityListDtos.Count > 0)
            {
                foreach (var item in entityListDtos)
                {
                    if (!rechargeRecordIds.Contains(item.Id))
                    {
                        item.IsFirstCharge = true;
                    }
                }
            }

            return new PagedResultDto<CoinRechargeListDto>(count, entityListDtos);
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<MiniProgramPayOutput> BuyCoins(BuyCoinsInput input)
        {
            var output = new MiniProgramPayOutput();

            var entity = await _repository.FirstOrDefaultAsync(c => c.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("你要充值的数据没有");
            }
            else
            {
                var player = await _playerRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(p => p.Id == input.PlayerId);

                //if (player.DeviceModel.ToLower().Contains("iphone"))
                //{
                //    throw new UserFriendlyException("暂不支持iOS客户支付，请联系客服");
                //}

                if (player == null)
                {
                    throw new UserFriendlyException("用户不存在");
                }
                else
                {
                    //if (input.IsVirtualRecharge)
                    //{
                    //    var result = GetCoinRechargeResult(new UpdateOrderStateInput()
                    //    {
                    //        FamilyId = input.FamilyId,
                    //        Id = input.Id,
                    //        isVirtualRecharge = input.IsVirtualRecharge,
                    //        PlayerId = input.PlayerId,
                    //    });
                    //}

                    //var openId = input.ClientType == ClientType.MinProgram ? player.OpenId : player.ChineseBabyPubOpenId;
                    var payResult = new MiniProgramPayOutput();
                    if (input.ClientType == ClientType.MinProgram)
                    {
                        payResult = await _weChatPayAppService.Pay(new SendPaymentRquestInput()
                        {
                            TenantId = player.TenantId,
                            OpenId = player.OpenId,
                            PlayerId = input.PlayerId,
                            ClientType = input.ClientType,
                            Body = input.Body,
                            Attach = input.Attach,
                            Totalfee = entity.MoneyAmount,
                            GoodsType = GoodsType.RechargeCoin,
                            FamilyId = input.FamilyId,
                            Code = input.Code,
                            ProductId = input.Id
                        });
                    }
                    else if (input.ClientType == ClientType.PublicAccount)
                    {
                        if (player.ChineseBabyPubOpenId == null)
                        {
                            throw new AbpException("公众号OpenId为null！");
                        }

                        payResult = await _weChatPayAppService.H5Pay(new SendPaymentRquestInput()
                        {
                            TenantId = player.TenantId,
                            OpenId = player.ChineseBabyPubOpenId,
                            PlayerId = input.PlayerId,
                            ClientType = input.ClientType,
                            Body = input.Body,
                            Attach = input.Attach,
                            Totalfee = entity.MoneyAmount,
                            GoodsType = GoodsType.RechargeCoin,
                            FamilyId = input.FamilyId,
                            Code = input.Code,
                            ProductId = input.Id
                        });
                    }
                    else if (input.ClientType == ClientType.H5AliPay)
                    {
                        payResult.FormTableString = await _alipayAppService.Pay(new AliPayH5Input()
                        {
                            FamilyId = input.FamilyId,
                            GoodsType = GoodsType.RechargeCoin,
                            PlayerId = input.PlayerId,
                            ProductId = input.Id,
                            Totalfee = entity.MoneyAmount,
                            ProductAmount = entity.CoinCount
                        });
                    }
                    else
                    {
                        throw new Exception("未知支付方法");
                    }
                    output = payResult;
                }
            }
            return output;
        }
        private const string MY_FAMILY_MESSAGE_FORMAT = "孩子的{0}花费了{1}元,获得金币{2}";
        private const string SYSTEM_MESSAGE_FORMAT = "{0}的{1}花费了¥{2}元,获得金币{3}";
        private const string RUNWATERMESSAGE = "{0}的{1}{2}充值";

        //private const int Max_Try_Query_Number = 5; //最大的尝试查询的次数

        public async Task<UpdateOrderStateOutput> GetCoinRechargeResult(UpdateOrderStateInput input)
        {
            var output = new UpdateOrderStateOutput();
            Logger.Debug($"1>订单编号（OutTradeNo）：{input.ToJsonString()}");

            await CheckGetCoinRechargeResultRequest(input);
            //预留1秒时间
            var querOrderOption = _queryOrderOption.Value;
            var i = 0;
            while (i++ < querOrderOption.Max_Try_QueryOrder_Number)
            {
                try
                {
                    //Thread.Sleep(1000);
                    await Task.Delay(2000);
                    var order = _orderRepository.GetAll()
                .Where(o => o.OrderNumber == input.OutTradeNo)
                .AsNoTracking()
                .FirstOrDefault()
                .CheckNull($"未找到对应订单,请核对订单号");

                    //订单如果已支付，直接返回是否成功
                    if (order.State == OrderState.Paid)
                    {
                        output.IsSuccess = order.State == OrderState.Paid ? true : false;
                        return output;
                    }
                }
                catch (Exception exp)
                {
                    Logger.Error($"获取支付结果失败:{exp}");
                }
            }
            Logger.Debug($"查询多次没有更新订单,再去微信上查询, {input.ToJsonString()}");

            return output;
            //return await BuyCoins(input);
        }

        private async Task<UpdateOrderStateOutput> BuyCoins(UpdateOrderStateInput input)
        {
            var output = new UpdateOrderStateOutput();
            var order = (await _orderRepository.GetAll()
               .Include(c => c.Player)
               .Where(o => o.OrderNumber == input.OutTradeNo)
               .FirstOrDefaultAsync())
               .CheckNull($"未找到对应订单,请核对订单号");

            input.PlayerId = order.PlayerId;
            input.FamilyId = order.FamilyId.Value;
            input.PropBagId = order.PropBagId;
            input.OrderId = order.Id;

            if (order.State != OrderState.UnPaid)
            {
                output.IsSuccess = order.State == OrderState.Paid ? true : false;
                return output;
            }

            //去查询微信中的状态
            var wechatPayResult = (_weChatPayAppService.QueryWechatPayResult(new QueryOrderStateInput
            {
                TenantId = order.Player.TenantId,
                OutTradNo = input.OutTradeNo
            })).CheckNull($"查询订单出错，请查看日志");

            var orderPayment = Math.Round(order.Payment, 2) * 100;
            if (wechatPayResult.TotalFee != orderPayment)
            {
                throw new Exception($"订单金额与支付结果不一致，请检查，订单号：{order.OrderNumber}, 订单金额：{order.Payment}, 支付金额:{wechatPayResult.TotalFee / 100}");
            }

            //获取成功后 更新订单状态
            bool payResult = await UpdateWechatOrderState(order, wechatPayResult);
            // 支付后发放奖励
            await PaidGrantAward(input, order);

            //代理的话 计算代理金额
            await CalculationEarning(new CalIncomeRequest(order, input.FamilyId));

            output.IsSuccess = payResult;

            return output;
        }
        /// <summary>
        /// 发放奖励（支付后）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task PaidGrantAward(UpdateOrderStateInput input, Order order)
        {
            switch (order.GoodsType)
            {
                case GoodsType.RechargeCoin:
                    {
                        await RechargeCoin(new RechargeCoinRequest
                        {
                            FamilyId = order.FamilyId.Value,
                            IsVirtual = false,
                            OrderId = order.Id,
                            PlayerId = order.Player.Id,
                            RechargeId = order.ProductId.Value
                        });
                    }
                    break;
                case GoodsType.ChangeProfession:
                    break;
                case GoodsType.UnLockWeChatAccount:
                    break;
                case GoodsType.BuyBigGiftBag:
                    {
                        await _propBagAppService.AddBoughtRecordAndInformations(input);
                    }
                    break;
                case GoodsType.DismissFamily:
                    {
                        await _familyAppService.ForceDismissFamilySuccess(input);
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 更新【微信】订单状态
        /// </summary>
        /// <param name="order"></param>
        /// <param name="wechatPayResult"></param>
        /// <returns></returns>
        private async Task<bool> UpdateWechatOrderState(Order order, OrderQueryOutput wechatPayResult)
        {
            var payResult = wechatPayResult.IsSuccess();
            order.State = payResult ? OrderState.Paid : OrderState.Failed;
            order.PaymentTime = DateTime.ParseExact(wechatPayResult.TimeEnd, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            order.PaymentData = $"支付回调查询结果：{wechatPayResult.DetailResult}";
            order.SettlementTotalFee = wechatPayResult.SettlementTotalFee / 100;
            order.TransactionId = wechatPayResult.TransactionId;
            order.LastModificationTime = DateTime.Now;

            await _orderRepository.UpdateAsync(order);
            return payResult;
        }

        private Task CheckGetCoinRechargeResultRequest(UpdateOrderStateInput input)
        {
            if (input.OutTradeNo.IsNullOrEmpty())
            {
                throw new AbpException("订单号不能同时为空");
            }

            return Task.CompletedTask;
        }

        private async Task CalculationEarning(CalIncomeRequest input)
        {
            var order = input.Order;
            var family = (await _familyRepository.GetAll()
                .Include(m => m.Father)
                .Include(m => m.Mother)
                .Include(m => m.Babies)
                .FirstOrDefaultAsync(f => f.Id == input.FamilyId))
                .CheckNull($"未找到家庭， 家庭ID：{input.FamilyId}");


            var other = order.PlayerId == family.FatherId ? family.Mother : family.Father;
            var player = order.PlayerId == family.FatherId ? family.Father : family.Mother;
            var parent = order.PlayerId == family.FatherId ? "爸爸" : "妈妈";
            var otherAgent = await _agentRepository.GetAll()
                //.Where(a => a.State == AgentState.Audited)
                .Include(b => b.UpperLevelMqAgent)
                .FirstOrDefaultAsync(o => o.PlayerId == other.Id);
            var playerAgent = await _agentRepository.GetAll()
                //.Where(a => a.State == AgentState.Audited)
                .Include(b => b.UpperLevelMqAgent)
                .FirstOrDefaultAsync(o => o.PlayerId == player.Id);

            if (otherAgent == null && playerAgent == null)
            {
                Logger.Warn($"家庭编号:{input.FamilyId}, 两个人都不是代理，不计算收益");
                return;
            }

            if (otherAgent != null && playerAgent != null)
            {
                //Logger.Warn($"家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber},两个人都是代理，不计算收益");
                //if (otherAgent.Level == AgentLevel.FirstPromoterLevel && playerAgent.Level == AgentLevel.FirstPromoterLevel)
                //{
                //    Logger.Warn($"双方都是一级推广，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                //    await AddFirstIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName,order,0,0);
                //    await AddFirstIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order,0,0);
                //}
                //else if (otherAgent.Level == AgentLevel.FirstAgentLevel && playerAgent.Level == AgentLevel.FirstAgentLevel)
                //{
                //    Logger.Warn($"双方都是一级代理，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                //    await AddFirstIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order,0,0);
                //    await AddFirstIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //}
                //else if (otherAgent.Level == AgentLevel.SecondLevel && playerAgent.Level == AgentLevel.SecondLevel)
                //{
                //    Logger.Warn($"双方都是二级代理，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                //    await AddFirstIncomeRecords(otherAgent.UpperLevelMqAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddFirstIncomeRecords(otherAgent.UpperLevelMqAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddSecondIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddSecondIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //}
                //else if ((otherAgent.Level == AgentLevel.FirstAgentLevel || otherAgent.Level == AgentLevel.FirstPromoterLevel) && playerAgent.Level == AgentLevel.SecondLevel)
                //{
                //    Logger.Warn($"有一方是二级，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                //    await AddFirstIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddFirstIncomeRecords(playerAgent.UpperLevelMqAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddSecondIncomeRecords(playerAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //}
                //else if ((playerAgent.Level == AgentLevel.FirstAgentLevel || playerAgent.Level == AgentLevel.FirstPromoterLevel) && otherAgent.Level == AgentLevel.SecondLevel)
                //{
                //    Logger.Warn($"有一方是二级，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                //    await AddFirstIncomeRecords(playerAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddFirstIncomeRecords(otherAgent.UpperLevelMqAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //    await AddSecondIncomeRecords(otherAgent, family.Baby?.Name, parent, player.NickName, order, 0, 0);
                //}
                //else
                //{
                //    Logger.Error($"双方身份异常，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                //}

                //双方都是代理的时候 各自添加为0的记录即可，然后返回
                await AddAngentZoreIncome(otherAgent, order, family?.Baby?.Name, parent, player.NickName);
                await AddAngentZoreIncome(playerAgent, order, family?.Baby?.Name, parent, player.NickName);

                return;
            }


            //计算代理的收益
            var agente = otherAgent ?? playerAgent;

            if (agente.Level == AgentLevel.FirstAgentLevel)
            {
                Logger.Warn($"代理是一级代理，不计算收益，家庭编号:{input.FamilyId}, 订单编号：{order.OrderNumber}");
                return;
            }


            await UpdateAgentOrPromoterIncome(agente, order, family.Baby?.Name, parent, player.NickName);

        }

        /// <summary>
        /// 添加代理0收益
        /// </summary>
        /// <returns></returns>
        private async Task AddAngentZoreIncome(MqAgent agent, Order order, string babyName, string parent, string nickname)
        {
            await AddFirstIncomeRecords(agent, babyName, parent, nickname, order, 0, 0);
            Logger.Warn($"开始插入{agent.NickName}的流水");
            if (agent.Level == AgentLevel.SecondLevel && agent.UpperLevelMqAgent != null)
            {
                Logger.Warn($"开始插入{agent.UpperLevelMqAgent.NickName}的二级流水");
                await AddSecondIncomeRecords(agent, babyName, parent, nickname, order, 0, 0);
            }
            else
            {
                Logger.Warn($"代理{agent.NickName}的上级代理不存在他的上级id是{agent.Id}");
            }
        }

        private async Task UpdateAgentOrPromoterIncome(MqAgent mqAgent, Order order, string babyName, string parent, string nickName)
        {
            var moneyAmount = order.Payment;
            Logger.Warn($"开始计算收益，{babyName}, {nickName}, {moneyAmount}");
            var realIncome = 0.0;
            var currentRatio = 0.0;
            if (mqAgent.Level == AgentLevel.FirstPromoterLevel)
            {
                realIncome = moneyAmount * (mqAgent.PromoterWithdrawalRatio / 100.0);
                currentRatio = mqAgent.PromoterWithdrawalRatio;
            }
            else if (mqAgent.Level == AgentLevel.SecondLevel)
            {
                realIncome = (moneyAmount * (mqAgent.AgentWithdrawalRatio / 100.0)).ToFixed(2);
                currentRatio = mqAgent.AgentWithdrawalRatio;
            }
            else
            {
                throw new Exception("未知身份");
            }
            var total = mqAgent.Balance + realIncome;
            //var withdrawMoneyState = total > 5000 ? WithdrawMoneyState.Intialization : WithdrawMoneyState.EnteredBalance;

            if (mqAgent.Level == AgentLevel.FirstPromoterLevel)
            {
                await AddFirstIncomeRecords(mqAgent, babyName, parent, nickName, order, realIncome, currentRatio);
                await UpdateAgentBalance(mqAgent, WithdrawMoneyState.Intialization, realIncome, order);
            }
            else if (mqAgent.Level == AgentLevel.SecondLevel)
            {

                await AddFirstIncomeRecords(mqAgent, babyName, parent, nickName, order, realIncome, currentRatio);
                await UpdateAgentBalance(mqAgent, WithdrawMoneyState.Intialization, realIncome, order);

                if (mqAgent.UpperLevelMqAgent != null)
                {

                    //上级的比例减去二级的比例就是上级的比例
                    var currentUpRatio = mqAgent.UpperLevelMqAgent.AgentWithdrawalRatio - mqAgent.AgentWithdrawalRatio;

                    var currentUpRealIncome = moneyAmount * (currentUpRatio / 100.0);
                    //#region 2019-02-13 gtp
                    //currentUpRatio = mqAgent.UpperLevelMqAgent.AgentWithdrawalRatio / 100.0;
                    //#endregion

                    currentUpRealIncome = currentUpRealIncome.ToFixed(2);
                    //upAgentRealIncome = moneyAmount * (mqAgent.UpperLevelMqAgent.AgentWithdrawalRatio / 100.0);
                    //var upAgentRealIncome = (moneyAmount * currentUpRatio).ToFixed(2);

                    await AddSecondIncomeRecords(mqAgent, babyName, parent, nickName, order, currentUpRealIncome, currentUpRatio);
                    //var upTotal = mqAgent.UpperLevelMqAgent.Balance + upAgentRealIncome;
                    //var upAgentWithdrawMoneyState = upTotal >= 5000 ? WithdrawMoneyState.Intialization : WithdrawMoneyState.EnteredBalance;
                    await UpdateAgentBalance(mqAgent.UpperLevelMqAgent, WithdrawMoneyState.Intialization, currentUpRealIncome, order);
                }
            }
            else
            {
                Logger.Debug("无需计算收益");
            }
        }

        private async Task AddSecondIncomeRecords(MqAgent mqAgent, string babyName, string parent, string nickName, Order order, double realIncome, double currentRatio)
        {
            var moneyAmount = order.Payment;

            await _incomeRecordRepository.InsertAsync(new IncomeRecord()
            {
                MqAgentId = mqAgent.UpperLevelMqAgentId.Value,
                Income = moneyAmount,
                WithdrawMoneyState = WithdrawMoneyState.Intialization,
                Description = string.Format(RUNWATERMESSAGE, babyName, parent, nickName),
                RealIncome = realIncome,
                OrderId = order.Id,
                RunWaterRecordType = RunWaterRecordType.Second,
                SecondAgentId = mqAgent.Id,
                CreationTime = order.PaymentTime.HasValue ? order.PaymentTime.Value : order.CreationTime,
                CurrentEarningRatio = currentRatio,
                IncomeTypeEnum = order.GoodsType == GoodsType.Subsidy ? IncomeTypeEnum.Subsidy : IncomeTypeEnum.Recharge,
                CompanyId = mqAgent?.CompanyId
            });
        }
        private async Task AddFirstIncomeRecords(MqAgent mqAgent, string babyName, string parent, string nickName, Order order, double realIncome, double currentRatio)
        {
            var moneyAmount = order.Payment;
            await _incomeRecordRepository.InsertAsync(new IncomeRecord()
            {
                MqAgentId = mqAgent.Id,
                Income = moneyAmount,
                WithdrawMoneyState = WithdrawMoneyState.Intialization,
                Description = string.Format(RUNWATERMESSAGE, babyName, parent, nickName),
                RealIncome = realIncome,
                OrderId = order.Id,
                RunWaterRecordType = RunWaterRecordType.First,
                CreationTime = order.PaymentTime.HasValue ? order.PaymentTime.Value : order.CreationTime,
                CurrentEarningRatio = currentRatio,
                IncomeTypeEnum = order.GoodsType == GoodsType.Subsidy ? IncomeTypeEnum.Subsidy : IncomeTypeEnum.Recharge,
                CompanyId = mqAgent?.CompanyId
            });
        }

        private async Task UpdateAgentBalance(MqAgent mqAgent, WithdrawMoneyState withdrawMoneyState, double realIncome, Order order)
        {

            mqAgent.Balance = (mqAgent.Balance + realIncome).ToFixed(2);
            mqAgent.TotalBalance = (mqAgent.TotalBalance + realIncome).ToFixed(2);
            if (order.GoodsType == GoodsType.Subsidy)
            {
                mqAgent.TotalSubsidyAmount = mqAgent.TotalSubsidyAmount + order.Payment;
            }
            await _agentRepository.UpdateAsync(mqAgent);
        }

        /// <summary>
        /// 虚拟充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UpdateOrderStateOutput> VirtualRecharge(VirtualRechargeInput input)
        {
            var output = new UpdateOrderStateOutput();
            await CheckPlayerAgentPermission(input);
            await CheckVirtualRecharge(input);
            //进行充值
            await RechargeCoin(new RechargeCoinRequest
            {
                FamilyId = input.FamilyId,
                IsVirtual = true,
                PlayerId = input.PlayerId,
                RechargeId = input.Id
            });

            return output;
        }

        private async Task CheckVirtualRecharge(VirtualRechargeInput input)
        {
            var player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);
            if (player.IsDeveloper)
                return;

            var buyRecords = await _coinRechargeRecordRepository
                .GetAll()
                .Include(c => c.CoinRecharge)
                .Where(c => c.CoinRechargeId == input.Id &&
                    c.RechargerId == input.PlayerId &&
                    c.FamilyId == input.FamilyId)
                .ToListAsync();

            var moeny_6 = buyRecords.Where(c => c.CoinRecharge.MoneyAmount >= 6 && c.CoinRecharge.MoneyAmount < 12);


            if (moeny_6.Count() >= 3)
            {
                throw new AbpException($"目前一个家庭只能进行三次6元虚拟购买");
            }

            var moeny_12 = buyRecords.Where(c => c.CoinRecharge.MoneyAmount >= 12 && c.CoinRecharge.MoneyAmount < 13);



            if (moeny_12.Count() >= 1)
            {
                throw new AbpException($"目前一个家庭只能进行一次12元虚拟购买");
            }
        }

        private async Task CheckPlayerAgentPermission(VirtualRechargeInput input)
        {
            var agent = await _agentRepository.FirstOrDefaultAsync(p => p.PlayerId == input.PlayerId);
            if (agent == null)
            {
                throw new AbpException($"您不是代理,无法使用此功能");
            }

            if (agent.State != AgentState.Audited)
            {
                Logger.Error($"代理未审核，input:{JsonConvert.SerializeObject(input)}，agent:{JsonConvert.SerializeObject(agent)}");
                throw new AbpException($"您的代理权限正在审核中,请耐心等待");
            }

            //if (agent.Level == AgentLevel.FirstAgentLevel)
            //{
            //    throw new AbpException($"一级代理无法购买金币");
            //}
        }

        public async Task<PagedResultDto<CoinRechargeListDto>> GetVirtualCoins(GetCoinRechargesInput input)
        {
            var rechargeRecordIds = _coinRechargeRecordRepository.GetAll()
                .Where(r => r.RechargerId == input.PlayerId && r.FamilyId == input.FamilyId)
                .Select(r => r.CoinRechargeId);

            var isAdmin = input.PlayerId.HasValue ? CheckIsAdmin(input.PlayerId.Value) : false;

            IQueryable<CoinRecharge> query = _repository
                .GetAll()
                .Where(c => c.IsShow)
                .OrderBy(r => r.CoinCount);

            if (!isAdmin)
            {
                query = query.Take(2);
            }

            var count = query.Count();

            var entityList = await query
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<CoinRechargeListDto>>();

            if (entityListDtos.Count > 0)
            {
                foreach (var item in entityListDtos)
                {
                    if (!rechargeRecordIds.Contains(item.Id))
                    {
                        item.IsFirstCharge = true;
                    }
                }
            }

            return new PagedResultDto<CoinRechargeListDto>(count, entityListDtos);
        }

        private bool CheckIsAdmin(Guid playerId)
        {
            return _playerRepository.GetAll()
                .Where(p => p.Id == playerId && p.IsDeveloper)
                .Any();
        }

        public async Task<SupplementCoinRechargeOutput> SupplementCoinRecharge(SupplementCoinRechargeInput input)
        {
            var output = new SupplementCoinRechargeOutput();

            if (string.IsNullOrEmpty(input.OrdeNum)) //充值金币就OK
            {
                if (input.FamilyId.HasValue)
                {
                    var family = await _familyRepository.GetAsync(input.FamilyId.Value);

                    family.Deposit += input.CoinCount;

                    await _familyRepository.UpdateAsync(family);

                    await _coinRechargeRecordRepository.InsertAsync(new CoinRechargeRecord()
                    {
                        FamilyId = input.FamilyId.Value,
                        RechargeCount = input.CoinCount,
                        SourceType = SourceType.SystemPresentation
                    });
                }
            }
            else //订单号不为空 插入一条订单 并且如果有代理 插入流水 更新代理的收益
            {

            }

            return output;
        }

        /// <summary>
        /// 充值金币
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task RechargeCoin(RechargeCoinRequest request)
        {
            Logger.Debug($"新的充值金币接口");
            var coin = _repository.Get(request.RechargeId)
                .CheckNull($"充值选项不正确,充值ID:{request.RechargeId}");

            var isFirst = await _coinRechargeRecordRepository
                .GetAll().AnyAsync(r => r.FamilyId == request.FamilyId &&
                    r.CoinRechargeId == coin.Id &&
                    r.RechargerId == request.PlayerId);


            var coinCount = !isFirst ? coin.CoinCount * 2 : coin.CoinCount;
            var family = (await _familyRepository.GetAll()
                .Where(f => f.Id == request.FamilyId)
                .Include(f => f.Babies)
                .Include(f => f.Father)
                .Include(f => f.Mother)
                .FirstOrDefaultAsync()).CheckNull($"未找到相应家庭,ID:{request.FamilyId}");

            var money = ((double)coin.MoneyAmount).ToFixed(2);
            if (request.IsVirtual)
            {
                family.VirtualRecharge += money;
            }
            else
            {
                family.ChargeAmount += money;
            }
            //添加获得金币记录
            await _familyCoinDeposiCRepository.InsertAsync(new FamilyCoinDepositChangeRecord
            {
                Amount = coinCount,
                CurrentFamilyCoinDeposit = family.Deposit,
                GetWay = CoinGetWay.virtualRecharge,
                FamilyId = family.Id,
                StakeholderId = request.PlayerId
            });
            family.Deposit += coinCount;
            await _familyRepository.UpdateAsync(family);
            await AddCoinRechargeRecord(request.FamilyId, request.PlayerId, coinCount, coin.RechargeLevel, request.OrderId, request.RechargeId, request.IsVirtual, SourceType.Recharge);

            var receiverId = request.PlayerId == family.FatherId ? family.MotherId : family.FatherId;
            var parent = request.PlayerId == family.FatherId ? "爸爸" : "妈妈";
            var content = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, money, coinCount);
            var other = request.PlayerId == family.FatherId ? family.Mother : family.Father;
            var sender = request.PlayerId == family.FatherId ? family.Father : family.Mother;

            //发给对方
            await AddFamilyInformation(content, request.FamilyId, receiverId, request.PlayerId, content);
            //发给自己
            await AddFamilyInformation(content, request.FamilyId, request.PlayerId, request.PlayerId, content);
            //发给系统
            if (family.Baby != null)
            {
                var systemContent = String.Format(SYSTEM_MESSAGE_FORMAT, family.Baby.Name, parent, money, coinCount);
                await AddSystemInformation(systemContent, request.FamilyId, request.PlayerId);
            }


        }

        private Task AddSystemInformation(string content, int familyId, Guid? senderId)
        {
            return _informationAppService.InsertAsync(new Information()
            {
                Content = content,
                FamilyId = familyId,
                Type = InformationType.System,
                SenderId = senderId
            });
        }

        private Task AddFamilyInformation(string content, int familyId, Guid receiverId, Guid senderId, string remark)
        {
            return _informationAppService.InsertAsync(new Information()
            {
                Content = content,
                FamilyId = familyId,
                ReceiverId = receiverId,
                Type = InformationType.Event,
                SenderId = senderId,
                NoticeType = NoticeType.Popout,
                State = InformationState.Create,
                SystemInformationType = SystemInformationType.Recharge,
                Remark = remark
            });
        }

        /// <summary>
        /// 添加充值记录
        /// </summary>
        /// <param name="familyId"></param>
        /// <param name="playerId"></param>
        /// <param name="rechargeCount"></param>
        /// <param name="level"></param>
        /// <param name="orderId"></param>
        /// <param name="coinId"></param>
        /// <param name="isVirtual"></param>
        /// <returns></returns>
        private Task AddCoinRechargeRecord(int familyId, Guid playerId, int rechargeCount, RechargeLevel level, Guid? orderId, int coinId, bool isVirtual, SourceType sourceType)
        {
            return _coinRechargeRecordRepository.InsertAsync(new CoinRechargeRecord()
            {
                SourceType = sourceType,
                FamilyId = familyId,
                RechargerId = playerId,
                RechargeCount = rechargeCount,
                RechargeLevel = level,
                OrderId = orderId,
                CoinRechargeId = coinId,
                IsVirtualRecharge = isVirtual
            });
        }

        private static Random _random = new Random();
        private static long GetNonce()
        {
            return _random.Next(int.MaxValue);
        }

        public async Task<string> WechatPayNotify()
        {
            var result = String.Empty;
            var msg = String.Empty;

            try
            {
                var wechatPayNotifyResponse = await _weChatPayAppService.WechatPayNotify();

                if (wechatPayNotifyResponse != null)
                {
                    if (!wechatPayNotifyResponse.IsReturnSuccess || !wechatPayNotifyResponse.IsResultSuccess)
                    {
                        throw new Exception($"返回代码或业务代码返回错误");
                    }

                    //推送到队列
                    var request = new SendMessageRequest(_config)
                    {
                        Timestamp = DateTime.UtcNow.GetTimestamp(),
                        Nonce = GetNonce(),
                        QueueName = _config.MQ_WechatPay,
                        MsgBody = wechatPayNotifyResponse.OutTradeNo,
                        DelaySeconds = 0
                    };

                    var response = await _qcouldApiClient.Execute<SendMessageRequest, SendMessageResponse>(request);

                    Logger.Debug($"增加队列结果：{response.ToJsonString()}");
                    result = "SUCCESS";
                    msg = "OK";
                    //去掉购买金币
                    //var buycoin = await BuyCoins(new UpdateOrderStateInput
                    //{
                    //    OutTradeNo = wechatPayNotifyResponse.OutTradeNo,
                    //    TransactionId = wechatPayNotifyResponse.TransactionId
                    //});


                    //Logger.Debug($"1>订单编号（{wechatPayNotifyResponse.OutTradeNo}）：{buycoin.ToJsonString()}");

                    //if (buycoin.IsSuccess)
                    //{
                    //    result = "SUCCESS";
                    //    msg = "OK";
                    //}
                    //else
                    //{
                    //    result = "FAIL";
                    //    msg = "购买金币失败";
                    //}
                }
                else
                {
                    result = "FAIL";
                    msg = "解析失败";
                }
            }
            catch (Exception ex)
            {
                result = "FAIL";
                msg = ex.Message;
                Logger.Error($"接收微信通知出错,错误信息:{ex.Message}", ex);
            }

            Logger.Warn($"通知结果:{result}, 消息:{msg}");
            return WeChatPayHelper.GetReturnXml(result, msg);
        }

        public async Task ReCalAgentIncome(ReCalAgentIncomeRequest request)
        {
            //删除所有没有订单号的流水


            IEnumerable<int> familyIds = null;

            if (request.AgentId.HasValue)
            {
                var agent = await _agentRepository.FirstOrDefaultAsync(a => a.Id == request.AgentId.Value);
                agent.Balance = 0;
                agent.TotalBalance = 0;
                await _agentRepository.UpdateAsync(agent);

                familyIds = await _familyRepository.GetAll()
                    .Where(f => f.FatherId == agent.PlayerId || f.MotherId == agent.PlayerId)
                    .Select(f => f.Id)
                    .ToListAsync();

                await _incomeRecordRepository.DeleteAsync(i => i.MqAgentId == agent.Id && i.IncomeTypeEnum != IncomeTypeEnum.Subsidy);//不删掉补贴流水，因为这些流水没有订单，删掉之后会无法恢复这些数据
            }
            else
            {
                await _incomeRecordRepository.DeleteAsync(i => true);

                var agents = _agentRepository.GetAll()
                    .Where(a => /*a.State == AgentState.Audited &&*/
                        a.TotalBalance > 0);

                foreach (var agent in agents)
                {
                    agent.Balance = 0;
                    agent.TotalBalance = 0;
                    await _agentRepository.UpdateAsync(agent);
                }
            }

            var orders = await _orderRepository.GetAll()
                .Include(o => o.Family)
                .Where(o => o.State == OrderState.Paid &&
                    o.GoodsType == GoodsType.RechargeCoin &&
                    o.FamilyId.HasValue &&
                    o.Family != null)
                .WhereIf(familyIds != null, o => familyIds.Contains(o.FamilyId.Value))
                .ToListAsync();

            foreach (var order in orders)
            {
                if (order.Family != null)
                {
                    await CalculationEarning(new CalIncomeRequest(order, order.FamilyId.Value));
                }

            }

            await RecalBalance(request);
        }

        public async Task RecalBalance(ReCalAgentIncomeRequest request)
        {
            var agents = await _agentRepository.GetAll()
                //.Where(a => a.State == AgentState.Audited)
                .WhereIf(request.AgentId.HasValue, a => a.Id == request.AgentId.Value)
                .ToListAsync();

            if (!agents.IsNullOrEmpty())
            {
                foreach (var agent in agents)
                {
                    var getmoney = _enterpirsePayRepository.GetAll()
                        .Where(a => a.AgentId == agent.Id && a.State == WithdrawDepositState.Success)
                        .Sum(a => a.Amount);

                    var total = _incomeRecordRepository.GetAll()
                        .Where(a => a.MqAgentId == agent.Id)
                        .Sum(a => a.RealIncome);


                    if (getmoney <= 0)
                        getmoney = 0;

                    if (agent.TotalBalance != total)
                    {
                        agent.TotalBalance = total;
                    }

                    agent.Balance = agent.TotalBalance - getmoney - agent.LockedBalance;
                    await _agentRepository.UpdateAsync(agent);
                }
            }

        }
        public async Task<UpdateOrderStateOutput> QueryOrderState(UpdateOrderStateInput input)
        {
            var result = new UpdateOrderStateOutput();
            var order = await _orderRepository.FirstOrDefaultAsync(o => o.OrderNumber == input.OutTradeNo);
            //var tenant = await GetCurrentTenantAsync();
            var query = _weChatPayAppService.QueryWechatPayResult(new QueryOrderStateInput
            {
                OutTradNo = input.OutTradeNo,
                TenantId = order.TenantId
            });

            Logger.Debug($"订单号：{input.OutTradeNo}, 结果：{query.ToJsonString()}");
            if (query != null)
            {
                result.IsSuccess = (query.IsSuccess() && query.TradeState == "SUCCESS");

                if (!result.IsSuccess)
                {


                    if (order.CreationTime.AddHours(3) <= DateTime.Now)
                    {
                        Logger.Warn($"订单超过时间未支付，更改订单状态，订单号：{input.OutTradeNo}");
                        order.State = OrderState.Failed;
                        await _orderRepository.UpdateAsync(order);
                    }
                }
                else
                {
                    Logger.Warn($"订单已支付，补充值，订单号：{input.OutTradeNo}");
                    await BuyCoins(input);
                }
            }

            return result;
        }

        public async Task SupplementCoinForFamily(SupplementCoinForFamilyInput input)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(o => (o.OrderNumber == input.OrderNumber || o.TransactionId == input.OrderNumber)
                && o.FamilyId == input.FamilyId);

            if (order == null)
            {
                throw new Exception("未找到订单号");
            }
            if (order.State == OrderState.Paid)
            {
                var systemContent = string.Empty;

                var coinRecharge = await _repository.GetAsync(order.ProductId.Value);

                if (coinRecharge == null)
                {
                    throw new Exception("未找到对应的金币档次");
                }

                var coinRechargeRecord = await _coinRechargeRecordRepository.FirstOrDefaultAsync(c => c.FamilyId == input.FamilyId && c.OrderId == order.Id && c.CoinRechargeId == coinRecharge.Id);

                if (coinRechargeRecord == null)
                {
                    throw new Exception("未找到充值记录");
                }

                if (coinRecharge.CoinCount * 2 == coinRechargeRecord.RechargeCount)
                {
                    throw new Exception("该充值记录是正常的，无需补充");
                }

                var family = await _familyRepository.FirstOrDefaultAsync(f => f.Id == input.FamilyId);

                if (family == null)
                {
                    throw new Exception("未找到该家庭");
                }

                family.Deposit += coinRecharge.CoinCount;

                await _familyRepository.UpdateAsync(family);

                //添加充值记录
                await AddCoinRechargeRecord(family.Id, order.PlayerId.Value, coinRecharge.CoinCount, coinRecharge.RechargeLevel, order.Id, coinRecharge.Id, false, SourceType.SupplementRecharge);

                var other = order.PlayerId == family.FatherId ? family.Mother : family.Father;
                var player = order.PlayerId == family.FatherId ? family.Father : family.Mother;
                var parent = order.PlayerId == family.FatherId ? "爸爸" : "妈妈";
                systemContent = $"您在{coinRechargeRecord.CreationTime}充值的金币由于系统原因未翻倍，系统为您补充金币{coinRecharge.CoinCount}";
                //添加消息记录
                await AddFamilyInformation(systemContent, family.Id, order.PlayerId.Value, order.PlayerId.Value, "系统补充金币");

            }
            else
            {
                throw new Exception("该订单未支付无法充值金币");
            }
        }

        [AbpAuthorize(ChinesePermissions.SupplementCoinRecharge)]
        public async Task V2_SupplementCoinForFamily(SupplementCoinForFamilyInput input)
        {
            var orderId = Guid.Empty;

            Order order = null;

            Family family = null;

            if (!string.IsNullOrEmpty(input.OrderNumber))
            {
                order = await _orderRepository
                    .GetAll()
                    .Include(f => f.Family)
                    .FirstOrDefaultAsync(o => (o.OrderNumber == input.OrderNumber || o.TransactionId == input.OrderNumber) && o.FamilyId == input.FamilyId);

                family = order == null ? null : order.Family;
            }
            else
            {
                family = await _familyRepository.GetAsync(input.FamilyId);
            }

            if (family == null)
            {
                throw new UserFriendlyException("未查询到该家庭");
            }

            family.Deposit += input.CoinCount;

            await _familyRepository.UpdateAsync(family);

            await _supplementCoinRecordRepository.InsertAsync(new SupplementCoinRecord()
            {
                CoinCount = input.CoinCount,
                FamilyId = family.Id,
                OrderId = order == null ? Guid.Empty : order.Id
            });
            //添加消息记录
            var systemContent = $"系统为您补充金币{input.CoinCount}";
            var playerId = order == null ? null : order.PlayerId;
            await AddSystemInformation(systemContent, family.Id, playerId);
        }
        /// <summary>
        /// 开启/关闭 虚拟充值权限
        /// </summary>
        /// <param name="playerid">玩家ID</param>
        /// <returns></returns>
        [AbpAuthorize(MqAgentPermissions.ToggleVtlRhgPerm)]
        [DontWrapResult]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ToggleVtlRhgPermOutput> ToggleVtlRhgPerm(string playerid)
        {
            var _result = new ToggleVtlRhgPermOutput();
            try
            {
                using (var _unitOfWorkHandler = UnitOfWorkManager.Begin())
                {
                    if (string.IsNullOrWhiteSpace(playerid))
                    {
                        _result.Message = "玩家标识不能为空";
                        return _result;
                    }
                    var singplayer = await _playerRepository.FirstOrDefaultAsync(new Guid(playerid));
                    singplayer.IsDeveloper = !singplayer.IsDeveloper;
                    UnitOfWorkManager.Current.SaveChanges();
                    _unitOfWorkHandler.Complete();
                    _result.Success = true;
                    return _result;
                }
            }
            catch (Exception e)
            {
                Logger.Error("ToggleVtlRhgPerm", e);
                _result.Success = false;
                _result.Message = "保存数据库异常";
                return _result;
            }
        }


        //public async Task SupplementAgentWaterRecord(SupplementAgentWaterRecordInput input)
        //{
        //    var agent = await _agentRepository.GetAsync(input.AgentId);

        //    var order = await _orderRepository.FirstOrDefaultAsync(o => o.OrderNumber == input.OrderNo);

        //    if (order == null)
        //    {
        //        throw new Exception("订单不存在");
        //    }



        //    var family = _familyRepository.GetAsync(order.FamilyId.Value);

        //    if (family == null)
        //    {
        //        throw new Exception("不存在该家庭");
        //    }

        //    var realIncome = (order.Payment * agent.AgentWithdrawalRatio / 100.0).ToFixed(2);

        //    if (agent.Level == AgentLevel.SecondLevel) // 补充流水
        //    {
        //        var incomeRecord = _incomeRecordRepository.FirstOrDefaultAsync(a => a.MqAgentId == agent.Id && a.OrderId == order.Id);

        //        if (incomeRecord != null)
        //        {
        //            throw new Exception("该流水已存在");
        //        }
        //    }
        //    else
        //    {

        //    }

        //    await _incomeRecordRepository.InsertAsync(new IncomeRecord()
        //    {
        //        MqAgentId = agent.Id,
        //        Income = order.Payment,
        //        WithdrawMoneyState = WithdrawMoneyState.EnteredBalance,
        //        Description = string.Format(RUNWATERMESSAGE, babyName, parent, nickName),
        //        RealIncome = realIncome,
        //        OrderId = order.Id,
        //        RunWaterRecordType = RunWaterRecordType.Second,
        //        //SecondAgentId = mqAgent.Id,
        //        CreationTime = order.PaymentTime.HasValue ? order.PaymentTime.Value : order.CreationTime,
        //        CurrentEarningRatio = agent.AgentWithdrawalRatio
        //    });
        //}

        /// <summary>
        /// 补贴金额
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SubsidyMoneyOutput> SubsidyMoney(SubsidyMoneyInput input)
        {
            var response = new SubsidyMoneyOutput();
            var family = await _familyRepository.GetAllIncluding(s => s.Babies, f => f.Father, m => m.Mother).FirstOrDefaultAsync(s => s.Id == input.FamilyId);
            if (family == null)
            {
                throw new UserFriendlyException("未查询到该家庭！");
            }

            // 校验验证码
            var bindTelSystemSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Name == "Subsidy_SMSCode");
            if (bindTelSystemSetting != null)
            {
                var msgId = RedisHelper.Get(bindTelSystemSetting.Value);
                //验证码验证
                var isValid = await ShortMessageCode.CodeIsValide(new JpushMessageInput()
                {
                    PhoneNumber = bindTelSystemSetting.Value,
                    AppKey = _jpushMessageConfig.AppKey,
                    MasterSecret = _jpushMessageConfig.MasterSecret,
                    SendMessageUrl = _jpushMessageConfig.SendMessageUrl,
                    MessageId = msgId.ToString(),
                    Code = input.SMSCode
                });

                if (!isValid)
                {
                    throw new UserFriendlyException("验证码错误！");
                }
            }
            // 如果双方都为代理，则返回
            var fatherAgent = await _agentRepository.GetAll()
              .Include(b => b.UpperLevelMqAgent)
              .FirstOrDefaultAsync(o => o.PlayerId == family.FatherId);
            var motherAgent = await _agentRepository.GetAll()
                .Include(b => b.UpperLevelMqAgent)
                .FirstOrDefaultAsync(o => o.PlayerId == family.MotherId);
            if (fatherAgent != null && motherAgent != null)
            {
                throw new UserFriendlyException("不允许双代理代理家庭进行该操作！");
            }
            else if (fatherAgent == null && motherAgent == null)
            {
                throw new UserFriendlyException("不允许无代理家庭进行该操作！");
            }
            var baby = family.LatestBaby;
            // 创建订单
            var agent = fatherAgent ?? motherAgent;
            var user = input.Draww == 0 ? family.Father : family.Mother;// agent == fatherAgent ? family.Mother : family.Father;
            var order = await _orderRepository.InsertAsync(new Order()
            {
                Id = Guid.NewGuid(),
                //CreationTime = input.SubsidyDate,
                IsDeleted = false,
                FamilyId = family.Id,
                BussinessState = BussinessState.Temporary,
                GoodsType = GoodsType.Subsidy,
                CompanyId = agent?.CompanyId,
                IsWithdrawCash = false,
                OrderNumber = "",
                Payment = input.SubsidyMoneyAmount,
                PaymentData = "Subsidy NO",
                PaymentTime = input.SubsidyDate,
                PlayerId = user.Id,
                State = OrderState.UnKnow,
            });
            await UpdateAgentOrPromoterIncome(fatherAgent ?? motherAgent, order, baby.Name, fatherAgent == null ? "爸爸" : "妈妈", user.NickName);
            family.TotalSubsidyAmount += input.SubsidyMoneyAmount;
            //_familyRepository.Update(family);
            return response;
        }


        //[RemoteService=false]
        public async Task<bool> SendMessageValideCode()
        {
            var bindTelSystemSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Name == "Subsidy_SMSCode");
            if (bindTelSystemSetting != null)
            {
                //调用接口发送验证码
                var msgId = await ShortMessageCode.SendMessageCode(new JpushMessageInput()
                {
                    PhoneNumber = bindTelSystemSetting.Value,
                    SendMessageUrl = _jpushMessageConfig.SendMessageUrl,
                    AppKey = _jpushMessageConfig.AppKey,
                    MasterSecret = _jpushMessageConfig.MasterSecret,
                    TempId = _jpushMessageConfig.TempId
                });

                if (msgId != null)
                {
                    return RedisHelper.Set(bindTelSystemSetting.Value, msgId, 5 * 60);
                }
            }

            return false;
        }
        /// <summary>
        /// 接收通知
        /// </summary>
        /// <returns></returns>
        public async Task<AliPayNotifyOutput> Receive_Notify(AliPayNotifyRsultAsyncDto input)
        {
            var output = new AliPayNotifyOutput();
            // 校验
            var signValideResult = await _alipayAppService.NotifyResultCheckSign(input);
            if (signValideResult != null && signValideResult.Order?.State == OrderState.Paid)
            {
                output.State = true;
            }
            else if (signValideResult.Status && signValideResult.Order != null && signValideResult.Order.FamilyId != null)
            {
                var family = signValideResult.Order.Family;
                var order = signValideResult.Order;
                var notify = signValideResult.Notify;
                //
                await UpdateAliPayOrderState(order, notify);
                // 支付后发放奖励
                await PaidGrantAward(new UpdateOrderStateInput() { FamilyId = family.Id, OutTradeNo = notify.out_trade_no, OrderId = order.Id }, signValideResult.Order);
                // 计算流水
                await CalculationEarning(new CalIncomeRequest(signValideResult.Order, family.Id));
                output.State = true;
            }
            return output;
        }

        /// <summary>
        /// 更新【支付宝】订单状态
        /// </summary>
        /// <param name="order"></param>
        /// <param name="wechatPayResult"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAliPayOrderState(Order order, AliPayNotifyRsultAsyncDto payNotify)
        {
            var payResult = payNotify.IsSuccess();
            order.State = payResult ? OrderState.Paid : OrderState.Failed;
            order.PaymentTime = payNotify.gmt_payment;
            order.PaymentData = payNotify.ToJsonString();
            order.SettlementTotalFee = (double)payNotify.receipt_amount;
            order.TransactionId = payNotify.trade_no;
            order.LastModificationTime = DateTime.Now;

            await _orderRepository.UpdateAsync(order);
            return payResult;
        }

    }

}