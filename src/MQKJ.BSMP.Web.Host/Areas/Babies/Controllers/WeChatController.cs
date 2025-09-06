using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.WeChatPay;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 微信
    /// </summary>
    public class WeChatController : BabyBaseController
    {
        private readonly IWeChatPayAppService _weChatPayAppService;

        public WeChatController(IWeChatPayAppService weChatPayAppService)
        {
            _weChatPayAppService = weChatPayAppService;
        }

        /// <summary>
        /// 微信小程序支付
        /// </summary>
        /// <returns></returns>
        [HttpPost("MiniProgramPay")]
        public async Task MiniProgramPay()
        {
            await Task.CompletedTask;
        }


        /// <summary>
        /// H5支付
        /// </summary>
        /// <returns></returns>
        [HttpPost("H5Pay")]
        public async Task H5Pay()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 微信公众号支付
        /// </summary>
        /// <returns></returns>
        [HttpPost("PubPay")]
        public async Task PubPay()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <returns></returns>
        [HttpPost("PayNotify")]
        public async Task PayNotify()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <returns></returns>
        [HttpPost("EnterprisePay")]
        public async Task EnterprisePay()
        {
            await Task.CompletedTask;
        }
    }
}