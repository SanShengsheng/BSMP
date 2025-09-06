using Abp.AspNetCore.Mvc.Authorization;
using Abp.Json;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Authorization;
using MQKJ.BSMP.Pay;
using MQKJ.BSMP.WeChatPay;
using System;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 云支付
    /// </summary>
    public class CloudPayController : BabyBaseController
    {
        private readonly IWeChatPayAppService _weChatPayAppService;
        private readonly IYunZhangHuApplicationService _yunzhanghuApplicationService;

        public CloudPayController(IWeChatPayAppService weChatPayAppService
            , IYunZhangHuApplicationService aliPayApplicationService
            )
        {
            _weChatPayAppService = weChatPayAppService;
            _yunzhanghuApplicationService = aliPayApplicationService;
        }


        /// <summary>
        /// 云服务第三方付款到支付宝回调
        /// </summary>
        /// <returns></returns>
        [HttpPost("AliPayNotify")]
        public async Task<IActionResult> AliPayNotify()
        {
            var data = Request.Form["data"];
            var response = await _yunzhanghuApplicationService.PostUpdateWithdrawApplyRecordAsync(data);
            return Content(response.ToString());
        }

    }
}