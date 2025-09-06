using Abp;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using JCSoft.WX.Framework.Models;
using Microsoft.Extensions.Logging;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Professions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Orders
{
    public class OrderSuccessEventHandler: AbpServiceBase,
        IAsyncEventHandler<OrderSuccessEventData>,
        ITransientDependency
    {
        private readonly IProfessionAppService _professionAppService;
        private readonly ICoinRechargeAppService _coinRechargeAppService;
        private readonly IChangeProfessionCostAppService _changeProfessionCostAppService;

        public OrderSuccessEventHandler(IProfessionAppService professionAppService,
            ICoinRechargeAppService coinRechargeAppService,
            IChangeProfessionCostAppService changeProfessionCostAppService)
        {
            _professionAppService = professionAppService;
            _coinRechargeAppService = coinRechargeAppService;
            _changeProfessionCostAppService = changeProfessionCostAppService;
        }
        public async Task HandleEventAsync(OrderSuccessEventData eventData)
        {
            Logger.Debug($"OrderSuccessEventHandler>HandleEventAsync=>{eventData.Order?.OrderNumber}");
            if (eventData.Order != null)
            {
                if (eventData.Order.GoodsType == GoodsType.ChangeProfession && 
                    eventData.Order.FamilyId.HasValue)
                {
                    var product = _changeProfessionCostAppService.Get(eventData.Order.ProductId.Value);
                    if(product != null)
                    {
                        if (eventData.Order.State != OrderState.Paid)
                        {
                            await _professionAppService.QueryChangePorfessionResult(new QueryChangeResultInput
                            {
                                FamilyId = eventData.Order.FamilyId.Value,
                                OutTradeNo = eventData.Order.OrderNumber,
                                PlayerId = eventData.Order.PlayerId.Value,

                            });
                        }
                    }
                    
                }
                Logger.Debug($"OrderSuccessEventHandler,eventData.Order.GoodsType:{eventData.Order.GoodsType} ,eventData.Order.FamilyId.HasValue:{eventData.Order.FamilyId.HasValue} ");            
                //if (eventData.Order.GoodsType == GoodsType.RechargeCoin &&
                //    eventData.Order.FamilyId.HasValue)
                //{
                //    var product = await _changeProfessionCostAppService.Get(eventData.Order.ProductId);
                //    Logger.Debug($"OrderSuccessEventHandler,product:{product?.Id} ");
                //    if (product != null)
                //    {
                //        await _coinRechargeAppService.GetCoinRechargeResult(new UpdateOrderStateInput
                //        {
                //            OutTradeNo = eventData.Order.OrderNumber,
                //            Id = eventData.Order.ProductId,
                //            FamilyId = eventData.Order.FamilyId.Value,
                //            PlayerId = eventData.Order.PlayerId,
                //        });
                //    }
                //}
            }
        }
    }
}
