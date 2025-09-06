using MQKJ.BSMP.Common.WechatPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Common.WechatMerchants
{
    public class WechatMerchantCreator
    {
        private readonly BSMPDbContext _context;

        public WechatMerchantCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateMerchant();
        }

        private void CreateMerchant()
        {
            //if (!_context.WechatMerchants.Any(c => c.MchId == "1520917151"))
            //{
            //    var merchant = new WechatMerchant();
            //    merchant.MchId = "1520917151";
            //    merchant.MerchantState = MerchantState.Disconnected;
            //    merchant.PaymentType = Orders.PaymentType.MinProgram;
            //    merchant.NotifyUrl = "http://api.dev.mqsocial.com/api/baby/CoinRecharge/WechatPayNotify";
            //    merchant.Key = "oyhzF8v2DWMFedZQM0ERx7bCbkuAABnc";
            //    _context.WechatMerchants.Add(merchant);
            //}
        }
    }
}
