using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Json;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class PayController : BSMPControllerBase
    {
        //private readonly IWeChatPayAppService _weChatPayAppService;

        //public PayController(IWeChatPayAppService weChatPayAppService)
        //{
        //    _weChatPayAppService = weChatPayAppService;
        //}
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> SendPay([FromBody]SendPaymentRquestInput input)
        //{
        //    input.TerminalIp = Request.Headers["X-Forwarded-For"].FirstOrDefault();
        //    if (string.IsNullOrEmpty(input.TerminalIp))
        //    {
        //        input.TerminalIp = HttpContext.Connection.RemoteIpAddress.ToString();
        //    }
        //    var response = await _weChatPayAppService.SendPaymentRquest(input);

        //    return Content(response.ToJsonString());

        //}
    }
}