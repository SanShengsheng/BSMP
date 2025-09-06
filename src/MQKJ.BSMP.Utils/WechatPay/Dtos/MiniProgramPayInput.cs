﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class MiniProgramPayInput
    {
        public string AppId { get; set; }

        public string MchId { get; set; }

        public string Key { get; set; }

        /// <summary>
        ///     小程序用户OpenId
        /// </summary>
        public string OpenId { get; set; }

        public string NotifyUrl { get; set; }

        /// <summary>
        ///     商品描述。
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     商品名称明细列表
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        ///     终端设备号(门店号或收银设备ID)
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        ///     附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        ///     商户系统内部的订单号,32个字符内、可包含字母 为空则自动生成
        /// </summary>
        public string OutTradeNo { get; set; }

        public string TradeType { get; set; } = "JSAPI";

        public string SignType { get; set; } = "MD5";

        /// <summary>
        ///     符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string FeeType { get; set; }

        /// <summary>
        ///     订单总金额，单位为元
        /// </summary>
        public decimal TotalFee { get; set; }

        /// <summary>
        ///     用户端实际ip
        /// </summary>
        public string SpbillCreateIp { get; set; }


        /// <summary>
        ///     订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。
        /// </summary>
        public string TimeStart { get; set; }

        /// <summary>
        ///     订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010
        /// </summary>
        public string TimeExpire { get; set; }

        /// <summary>
        ///     商品标记，代金券或立减优惠功能的参数，说明详见代金券或立减优惠
        /// </summary>
        public string GoodsTag { get; set; }

        /// <summary>
        ///     no_credit--指定不能使用信用卡支付
        /// </summary>
        public string LimitPay { get; set; }
    }
}
