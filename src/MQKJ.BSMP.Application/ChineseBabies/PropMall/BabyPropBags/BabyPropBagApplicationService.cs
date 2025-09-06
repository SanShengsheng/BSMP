
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
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.ChineseBabies.PropMall.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall.DomainService;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Asset;

using MQKJ.BSMP.Utils.Extensions;
using Abp.Domain.Uow;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropBags;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropPrices;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using Abp.Json;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.QCloud.Models.CMQ.Requests;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using MQKJ.BSMP.QCloud;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.QCloud.Configs;
using System.Threading;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// BabyPropBag应用层服务的接口实现方法  
    ///</summary>
    public class BabyPropBagAppService : BSMPAppServiceBase, IBabyPropBagAppService
    {
        private readonly IRepository<BabyPropBag, Guid> _entityRepository;
        private readonly IRepository<BabyPropRecord, Guid> _babyPropRecordRepository;
        private readonly IBabyPropBagManager _entityManager;
        private readonly IRepository<BabyPropBagAndBabyProp, Guid> _babyPropBagAndBabyPropRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;
        private readonly IRepository<FamilyCoinDepositChangeRecord, Guid> _familyCoinDepositChangeRecordRepository;
        private readonly IRepository<Family, int> _familyRepository;
        private readonly IRepository<BabyAssetAward, Guid> _babyAssetAwardRepository;
        private readonly IRepository<Player, Guid> _playerRepository;
        private IBabyPropAppService _babyPropAppService;
        private readonly IWeChatPayAppService _weChatPayAppService;
        private readonly IQCloudApiClient _qcouldApiClient;
        private readonly QcloudConfig _config;
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Information, Guid> _inforRepository;
        private readonly IRepository<Baby> _babyRepository;
        private IOptions<QueryOrderConfig> _queryOrderOption;



        //private const int Max_Try_Query_Number = 5; //最大的尝试查询的次数
        /// <summary>
        /// 构造函数 
        ///</summary>
        public BabyPropBagAppService(
        IRepository<BabyPropBag, Guid> entityRepository
        , IBabyPropBagManager entityManager
            , IRepository<BabyPropRecord, Guid> babyPropRecordRepository
            , IRepository<BabyPropBagAndBabyProp, Guid> babyPropBagAndBabyPropRepository
         , IRepository<BabyFamilyAsset, Guid> babyFamilyAssetRepository
        , IRepository<FamilyCoinDepositChangeRecord, Guid> familyCoinDepositChangeRecordRepository
        , IRepository<Family, int> familyRepository
        , IRepository<BabyAssetAward, Guid> babyAssetAwardRepository,
        IRepository<Information, Guid> inforRepository
            , IRepository<Player, Guid> playerRepository
            , IBabyPropAppService babyPropAppService
            , IWeChatPayAppService weChatPayAppService
            ,IQCloudApiClient qcloudApiClient
            ,IOptions<QcloudConfig> configOptions
            , IRepository<Order, Guid> orderRepository
            , IRepository<Baby> babyRepository
            ,IOptions<QueryOrderConfig> querOrderOption
        )
        {
            _entityRepository = entityRepository;
            _babyPropRecordRepository = babyPropRecordRepository;
            _entityManager = entityManager;
            _babyPropBagAndBabyPropRepository = babyPropBagAndBabyPropRepository;
            _babyFamilyAssetRepository = babyFamilyAssetRepository;
            //_babyPropRecordRepository = babyPropRecordRepository;
            _familyCoinDepositChangeRecordRepository = familyCoinDepositChangeRecordRepository;
            _familyRepository = familyRepository;
            _babyAssetAwardRepository = babyAssetAwardRepository;
            _babyPropAppService = babyPropAppService;
            _weChatPayAppService = weChatPayAppService;
            _playerRepository = playerRepository;

            _qcouldApiClient = qcloudApiClient;
            _config = configOptions?.Value;

            _orderRepository = orderRepository;
            _inforRepository = inforRepository;
            _babyRepository = babyRepository;

            _queryOrderOption = querOrderOption;
        }
        /// <summary>
        /// 获取最新大礼包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPropBagLastestOutput> GetPropBagLastest(GetPropBagLastestInput input)
        {
            var response = new GetPropBagLastestOutput();
            // 查询当前家庭购买的大礼包序列
            var propBagHistory = await _babyPropRecordRepository.GetAllIncluding(s => s.BabyPropBag).Where(s => s.FamilyId == input.FamilyId && s.BabyPropBagId != null).OrderByDescending(s => s.BabyPropBag.Order).FirstOrDefaultAsync();
            // 根据 order 排序获取
            var propBag = await _entityRepository.GetAllIncluding()
                .Where(s => s.Gender == input.ParentIden)
                .WhereIf(propBagHistory != null, s => s.Order == propBagHistory.BabyPropBag.Order + 1)
                .OrderBy(s => s.Order)
                .FirstOrDefaultAsync();
            if (propBag != null)
            {
                // 获取大礼包中所有道具
                var props = new List<GetPropBagLastestOutputChild>();
                await _babyPropBagAndBabyPropRepository.GetAllIncluding(s=>s.BabyPropPrice)
                   .Include(s => s.BabyProp)
                   .Where(s => s.BabyPropBagId == propBag.Id)
                   .ForEachAsync(s =>
                   {
                       props.Add(new GetPropBagLastestOutputChild
                       {
                           Title = s.BabyProp.Title,
                           Validity = s.BabyPropPrice.Validity,
                           Count = s.Count,
                       });
                   });
                response.Props = null;// props;
            }
            response.BasicInfo = ObjectMapper.Map<GetPropBagLastestOutputBasicInfo>(propBag);
            return response;
        }
        /// <summary>
        /// 购买道具大礼包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<PostBuyPropBagOutput> PostBuyPropBag(PostBuyPropBagInput input)
        {
            var response = new PostBuyPropBagOutput() { };

            var isBoughtBag = _babyPropRecordRepository.GetAll()
                .Any(c => c.BabyPropBagId == input.propBagId && c.FamilyId == input.FamilyId);

            if (isBoughtBag)
            {
                throw new Exception("您已购买过大礼包，同类型的大礼包只能购买一次哦");
            }


            // 获取家庭
            var family = (await _familyRepository.GetAllIncluding(s => s.Babies).FirstOrDefaultAsync(s => s.Id == input.FamilyId)).CheckNull($"当前家庭不存在！{input.FamilyId}");
            //feature#IV1QZ 道具模块消息通知
            var isFather = family.FatherId == input.PlayerGuid;
            List<string>  notifies =new List<string>();
            List<string> notifies_coin = new List<string>();
            // 获取大礼包
            var propBags = _babyPropBagAndBabyPropRepository.GetAllIncluding(s => s.BabyPropBag)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropPropertyAward)
                .Include(s => s.BabyPropPrice)
                .Where(s => s.BabyPropBagId == input.propBagId);
            var props = propBags.Select(s => s.BabyProp)
               .Include(s => s.BabyPropPropertyAward);
            var propBag = (await propBags.FirstOrDefaultAsync())?.BabyPropBag;

            // 金币购买
            if (input.CurrencyType == CurrencyType.Coin)
            {
                if (family.Deposit < propBag.PriceGoldCoin)
                {
                    throw new Exception("金币不足，请充值后再购买");
                }
                // 循环将大礼包道具发放到家庭资产
                await props.ForEachAsync(s =>
                {
                    var price = propBags.FirstOrDefault(d => d.BabyPropId == s.Id)?.BabyPropPrice;
                    var output = BuyPropFromPropBag(s, input, family, price, notifies, notifies_coin).Result;
                    family = output.Family;//为了及时更新家庭的资产情况
                    response.Messages.Add(output.Message);
                });
                // 扣掉金币消耗
                // 增加金币
                family.Deposit -= propBag.PriceGoldCoin;
                family = await _familyRepository.UpdateAsync(family);

                // 金币消耗记录
                await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
                {
                    Amount = propBag.PriceGoldCoin,
                    BabyId = input.BabyId,
                    FamilyId = input.FamilyId,
                    //StakeholderId = input.PlayerGuid,
                    CostType = CoinCostType.BuyPropBag,
                    CurrentFamilyCoinDeposit = family.Deposit
                });
                //添加消息
                await AddInformations(isFather, propBag, notifies, notifies_coin, family);

                //添加购买记录
                await AddBoughtBigBagRecord(input);
            }
            else if (input.CurrencyType == CurrencyType.RMB)
            {
                var player = _playerRepository.FirstOrDefault(p => p.Id == input.PlayerGuid).CheckNull("不存在的玩家");
                var payResult = await _weChatPayAppService.Pay(new SendPaymentRquestInput()
                {
                    TenantId = player.TenantId,
                    OpenId = player.OpenId,
                    PlayerId = input.PlayerGuid,
                    ClientType = ClientType.MinProgram,
                    Totalfee = propBag.PriceRMB,
                    GoodsType = GoodsType.BuyBigGiftBag,
                    FamilyId = input.FamilyId,
                    PropBagId = input.propBagId
                });

                response.PayOutput = payResult;
            }
            return response;
        }

        private static Random _random = new Random();
        private static long GetNonce()
        {
            return _random.Next(int.MaxValue);
        }

        private async Task AddInformations(bool isFather, BabyPropBag propBag, List<string> notifies, List<string> notifies_coin, Family family)
        {
            //添加消息
            //TODO: 确定消息类型      
            if (notifies_coin != null && notifies_coin.Count > 0)
            {
                await _inforRepository.InsertAsync(new Information
                {
                    Content = $"孩子的{(isFather ? "爸爸" : "妈妈")}购买了{propBag.Title},{(string.IsNullOrWhiteSpace(string.Join(',', notifies)) ? "" : ("获得了" + string.Join(',', notifies)))} {(string.Join(',', notifies_coin))}",
                    FamilyId = family.Id,
                    State = InformationState.Create,
                    Type = InformationType.System,
                    ReceiverId = family.MotherId,
                    SenderId = family.FatherId,
                    SystemInformationType = SystemInformationType.BigBag,
                    BabyEventId = null,
                    Remark = null
                });
                await _inforRepository.InsertAsync(new Information
                {
                    Content = $"孩子的{(isFather ? "爸爸" : "妈妈")}购买了{propBag.Title},{(string.IsNullOrWhiteSpace(string.Join(',', notifies)) ? "" : ("获得了" + string.Join(',', notifies)))} {(string.Join(',', notifies_coin))}",
                    FamilyId = family.Id,
                    State = InformationState.Create,
                    Type = InformationType.System,
                    ReceiverId = family.FatherId,
                    SenderId = family.MotherId,
                    SystemInformationType = SystemInformationType.BigBag,
                    BabyEventId = null,
                    Remark = null
                });
            }
            else
            {
                await _inforRepository.InsertAsync(new Information
                {
                    Content = $"孩子的{(isFather ? "爸爸" : "妈妈")}购买了{propBag.Title},获得了{string.Join(',', notifies)} {(string.Join(',', notifies_coin))}",
                    FamilyId = family.Id,
                    State = InformationState.Create,
                    Type = InformationType.System,
                    SystemInformationType = SystemInformationType.BigBag,
                    ReceiverId = family.FatherId,
                    SenderId = family.MotherId,

                });
                await _inforRepository.InsertAsync(new Information
                {
                    Content = $"孩子的{(isFather ? "爸爸" : "妈妈")}购买了{propBag.Title},获得了{string.Join(',', notifies)} {(string.Join(',', notifies_coin))}",
                    FamilyId = family.Id,
                    State = InformationState.Create,
                    Type = InformationType.System,
                    SystemInformationType = SystemInformationType.BigBag,
                    ReceiverId = family.MotherId,
                    SenderId = family.FatherId,

                });
            }
        }
        [UnitOfWork]
        public virtual async Task AddBoughtRecordAndInformations(UpdateOrderStateInput input)
        {
            // 获取家庭
            var family = (await _familyRepository.GetAllIncluding(s => s.Babies).FirstOrDefaultAsync(s => s.Id == input.FamilyId)).CheckNull($"当前家庭不存在！{input.FamilyId}");
            //feature#IV1QZ 道具模块消息通知
            var isFather = family.FatherId == input.PlayerId;
            List<string> notifies = new List<string>();
            List<string> notifies_coin = new List<string>();
            // 获取大礼包
            var propBags = await _babyPropBagAndBabyPropRepository.GetAllIncluding(s => s.BabyPropBag)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropPropertyAward)
                .Include(s => s.BabyPropPrice)
                .Where(s => s.BabyPropBagId == input.PropBagId).ToListAsync();
            var props = propBags.Select(s => s.BabyProp).ToList();
            var propBag = (propBags.FirstOrDefault())?.BabyPropBag;
            if (propBag != null)
            {
                family.ChargeAmount += double.Parse(Math.Round(propBag.PriceRMB,2).ToString());
                await _familyRepository.UpdateAsync(family);
            }

            var baby = _babyRepository.GetAll().
                LastOrDefault(b => b.FamilyId == input.FamilyId && b.State == BabyState.UnderAge)
                .CheckNull("数据错误，请稍后重试");


            var postBuyPropBag = new PostBuyPropBagInput()
            {
                BabyId = baby.Id,
                FamilyId = baby.FamilyId,
                PlayerGuid = input.PlayerId.Value
            };
            // 循环将大礼包道具发放到家庭资产
            props.ForEach(s =>
            {
                var price = propBags.FirstOrDefault(d => d.BabyPropId == s.Id);
                if (price == null)
                {
                    throw new Exception("数据错误，请稍后重试");
                }
                //var output = await BuyPropFromPropBag(s, postBuyPropBag, family, price, notifies, notifies_coin);
                //if (output != null)
                //{
                //    family = output.Family;//为了及时更新家庭的资产情况
                //}
               //var result = BuyPropFromPropBag(s, postBuyPropBag, family, price, notifies, notifies_coin).Result;
               var result = BuyPropFromPropBag(s, postBuyPropBag, family, price.BabyPropPrice, notifies, notifies_coin).Result;
                family = result.Family;
            });


            await AddBoughtBigBagRecord(new PostBuyPropBagInput()
            {
                BabyId = baby.Id,
                CurrencyType = CurrencyType.RMB,
                FamilyId = input.FamilyId,
                PlayerGuid = input.PlayerId.Value,
                propBagId = input.PropBagId.Value,
                OrderId = input.OrderId.Value
            });

            //await _babyPropRecordRepository.InsertAsync(new BabyPropRecord()
            //{
            //    BabyPropBagId = input.PropBagId.Value,
            //    FamilyId = input.FamilyId,
            //    PropSource = PropSource.PresentByArena,
            //    PurchaserId = input.PlayerId,
            //    BabyId = baby.Id,
            //    OrderId = input.OrderId.Value
            //}); 
            await AddInformations(isFather, propBag, notifies, notifies_coin, family);
        }

        public async Task AddBoughtBigBagRecord(PostBuyPropBagInput input)
        {
            // 添加购买记录
            await _babyPropRecordRepository.InsertAsync(new BabyPropRecord()
            {
                BabyPropBagId = input.propBagId,
                FamilyId = input.FamilyId,
                PropSource = PropSource.PresentByArena,
                PurchaserId = input.PlayerGuid,
                BabyId = input.BabyId,
                OrderId = input.OrderId
            });
        }

        //public async Task<UpdateOrderStateOutput> GetBoughtBigBagPayResult(UpdateOrderStateInput input)
        //{
        //    var output = new UpdateOrderStateOutput();
        //    Logger.Debug($"1>订单编号（OutTradeNo）：{input.ToJsonString()}");

        //    var querOrderOption = _queryOrderOption.Value;
        //    var i = 0;
        //    while (i++ < querOrderOption.Max_Try_QueryOrder_Number)
        //    {
        //        try
        //        {
        //            //Thread.Sleep(2000);
        //            await Task.Delay(2000);
        //            var order = _orderRepository.GetAll()
        //                .AsNoTracking()
        //                .FirstOrDefault(o => o.OrderNumber == input.OutTradeNo).CheckNull("订单未找到");

        //            //.Where(o => o.OrderNumber == input.OutTradeNo)
        //            //.SingleOrDefaultAsync())
        //            //.CheckNull($"未找到对应订单,请核对订单号");
        //            Logger.Debug($"查询了{i}次,订单状态是：{order.State},订单号：{order.OrderNumber},订单id：{order.Id}");
        //            //订单如果已支付，直接返回是否成功
        //            if (order.State == OrderState.Paid)
        //            {
        //                output.IsSuccess = order.State == OrderState.Paid ? true : false;
        //                return output;
        //            }
        //        }
        //        catch (Exception exp)
        //        {

        //            Logger.Error($"获取支付结果失败:{exp}");
        //        }
        //    }
        //    Logger.Debug($"查询多次没有更新订单,再去微信上查询, {input.ToJsonString()}");

        //    return output;
        //}

        //public async Task<string> BoughtBigBagPayNotify()
        //{
        //    var result = String.Empty;
        //    var msg = String.Empty;

        //    try
        //    {
        //        var wechatPayNotifyResponse = await _weChatPayAppService.WechatPayNotify();

        //        if (wechatPayNotifyResponse != null)
        //        {
        //            if (!wechatPayNotifyResponse.IsReturnSuccess || !wechatPayNotifyResponse.IsResultSuccess)
        //            {
        //                throw new Exception($"返回代码或业务代码返回错误");
        //            }

        //            //推送到队列
        //            var request = new SendMessageRequest(_config)
        //            {
        //                Timestamp = DateTime.UtcNow.GetTimestamp(),
        //                Nonce = GetNonce(),
        //                QueueName = _config.MQ_WechatPay,
        //                MsgBody = wechatPayNotifyResponse.OutTradeNo,
        //                DelaySeconds = 0
        //            };

        //            var response = await _qcouldApiClient.Execute<SendMessageRequest, SendMessageResponse>(request);

        //            Logger.Debug($"购买大礼包增加队列结果：{response.ToJsonString()}");
        //            result = "SUCCESS";
        //            msg = "OK";
        //        }
        //        else
        //        {
        //            result = "FAIL";
        //            msg = "解析失败";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "FAIL";
        //        msg = ex.Message;
        //        Logger.Error($"接收微信通知出错,错误信息:{ex.Message}", ex);
        //    }

        //    Logger.Warn($"购买大礼包通知结果:{result}, 消息:{msg}");
        //    return WeChatPayHelper.GetReturnXml(result, msg);
        //}

        //[UnitOfWork]
        //public virtual async Task<BuyPropFromPropBagOutput> BuyPropFromPropBag(BabyProp prop, PostBuyPropBagInput input, Family family, BabyPropPrice price)
        //{
        //    var output = new BuyPropFromPropBagOutput() { };
        //    var message = $"{prop.Title}购买成功！";
        //    //var price = prop.Prices.FirstOrDefault(s => s.id ==);

        //    //判断是否已经买了该道具且没有到期，且超过最大持有数量
        //    var propCount = await _babyFamilyAssetRepository.GetAllIncluding().CountAsync(s => s.BabyPropId == prop.Id && s.FamilyId == input.FamilyId && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null));
        //    if (prop.MaxPurchasesNumber <= propCount)
        //    {
        //        // 如果已经有该道具了，则折成金币发放
        //        //var family = (await _familyRepository.GetAsync(input.FamilyId)).CheckNull($"当前家庭不存在！{input.FamilyId}");
        //        // 增加金币
        //        family.Deposit += price.PropValue;
        //        family = await _familyRepository.UpdateAsync(family);
        //        message = $"已经有{prop.Title}了，系统自动折成金币发放，金币数量为：{price.PropValue}";
        //        //// 增加对于金币消耗的记录
        //        await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
        //        {
        //            Amount = price.Price,
        //            BabyId = input.BabyId,
        //            FamilyId = input.FamilyId,
        //            GetWay = CoinGetWay.PropToCoin,
        //            //StakeholderId = input.PlayerGuid,
        //            CurrentFamilyCoinDeposit = family.Deposit
        //        });
        //    }
        //    else
        //    {
        //        // 将道具增加到家庭资产中
        //        await _babyPropAppService.UpdateFamilyAssetAndBabyAward(new PostBuyPropInput()
        //        {
        //            BabyId = input.BabyId,
        //            FamilyId = input.FamilyId,
        //            PlayerGuid = input.PlayerGuid,
        //            PropId = prop.Id,
        //            PriceId = price.Id
        //        }, prop, price, family);
        //    }

        //    output.Message = message;
        //    output.Family = family;
        //    return output;
        //}
        Func<double, string> ToValidString = (double validity) => validity == -1 ? "永久" : $"{((int)validity / 3600).ToString()}小时";
        [UnitOfWork]
        private async Task<BuyPropFromPropBagOutput> BuyPropFromPropBag(BabyProp prop, PostBuyPropBagInput input, Family family, BabyPropPrice price,List<string> msgs,List<string> msgs_coin)
        {
            var output = new BuyPropFromPropBagOutput() {   };
            var message = $"{prop.Title}购买成功！";
            //var price = prop.Prices.FirstOrDefault(s => s.id ==);
            //孩子爸爸或妈妈购买了XX大礼包，获得X个XXX(4小时)道具、X个XXX（永久）道具。
            //判断是否已经买了该道具且没有到期，且超过最大持有数量
            var propCount = await _babyFamilyAssetRepository
                .CountAsync(s => s.BabyPropId == prop.Id && s.FamilyId == input.FamilyId && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null));
                //.Where()).ToListAsync();
            if (prop.MaxPurchasesNumber <= propCount)
            {
                // 如果已经有该道具了，则折成金币发放
                //var family = (await _familyRepository.GetAsync(input.FamilyId)).CheckNull($"当前家庭不存在！{input.FamilyId}");
                // 增加金币
                family.Deposit += price.PropValue;
                family = await _familyRepository.UpdateAsync(family); 
                 message = $"已经有{prop.Title}了，系统自动折成金币发放，金币数量为：{price.PropValue}";
                msgs_coin.Add($"因您已拥有{prop.Title}（{ToValidString(price.Validity)}）道具，现给您折算{price.PropValue}金币");

                //// 增加对于金币消耗的记录
                await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
                {
                    Amount = price.Price,
                    BabyId = input.BabyId,
                    FamilyId = input.FamilyId,
                    GetWay = CoinGetWay.PropToCoin,
                    //StakeholderId = input.PlayerGuid,
                    CurrentFamilyCoinDeposit = family.Deposit
                });
            }
            else
            {
               
                msgs.Add($"{prop.Title}({ToValidString(price.Validity)})");
                // 将道具增加到家庭资产中
                await _babyPropAppService.UpdateFamilyAssetAndBabyAward(new UpdateFamilyAssetAndBabyAwardDto()
                {
                    BuyProp = new PostBuyPropInput() {
                        BabyId = input.BabyId,
                        FamilyId = input.FamilyId,
                        PlayerGuid = input.PlayerGuid,
                        PropId = prop.Id,
                        PriceId = price.Id
                    },
                    Family = family.MapTo<UpdateFamilyAssetAndBabyAwardFamily>(),
                    Prop = prop.MapTo<BuyBabyPropDto>(),
                    PropPrice = price.MapTo<BabyPropPriceDto>()
                });
            }

            output.Message = message;
            output.Family = family;
            return output;
        }

     
    }
}


