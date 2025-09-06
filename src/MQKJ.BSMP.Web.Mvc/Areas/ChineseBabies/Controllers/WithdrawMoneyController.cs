using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.AliPay;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Authorization;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.EnterprisePayments.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Pay;
using System;
using System.Threading.Tasks;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [Area("ChineseBabies")]
    public class WithdrawMoneyController : BSMPControllerBase
    {

        private IEnterpirsePaymentRecordAppService _enterpirsePaymentRecordAppService;

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IYunZhangHuApplicationService _yunZhangHuApplicationService;
        public WithdrawMoneyController(IEnterpirsePaymentRecordAppService enterpirsePaymentRecordAppService, IHostingEnvironment hostingEnvironment,
            IYunZhangHuApplicationService yunZhangHuApplicationService
            )
        {
            _enterpirsePaymentRecordAppService = enterpirsePaymentRecordAppService;

            _hostingEnvironment = hostingEnvironment;
            _yunZhangHuApplicationService = yunZhangHuApplicationService;
        }

        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.GetEnterpirsePaymentRecordPags)]
        public async Task<IActionResult> Index(int? page, string userName, WithdrawDepositState withdrawMoneyState, DateTime? startTime, DateTime? endTime, WithdrawMoneyType withdrawMoneyType, string orderNumber)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var result = await _enterpirsePaymentRecordAppService.GetPaged(new GetEnterpirsePaymentRecordsInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                UserName = userName,
                WithdrawDepositState = startTime == null ? WithdrawDepositState.All : withdrawMoneyState,
                OrderNumber = orderNumber,
                WithdrawMoneyType = withdrawMoneyType
            });

            //ViewBag.jqueryUnobtrusive = jqueryUnobtrusive;

            var records = new StaticPagedList<EnterpirsePaymentRecordListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", records);
            }
            else
            {
                return View(records);
            }
        }

        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent)]
        public async Task<IActionResult> WithDrawMoney(Guid? id)
        {
            var result = new WithDrawMoneyForAgentOutput();

            try
            {
                var ipStr = HttpContext.Connection.RemoteIpAddress.ToString();
                if (id.HasValue)
                {
                    result = await _enterpirsePaymentRecordAppService.WithDrawMoneyForAgent(new WithDrawMoneyForAgentInput()
                    {
                        Id = id.Value,
                        IpStr = ipStr
                    });
                }
            }
            catch (Exception exp)
            {

                result.ErrorMessage = exp.Message;
            }

            return Content(result.ToJsonString());
        }
        /// <summary>
        /// 云平台提现审核通过，下单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent)]
        public async Task<IActionResult> WithDrawMoneyByCloudPlatform(Guid? id, WithdrawMoneyType? requestPlatform)
        {
            var result = new OrderPayOutput();
            if (id.HasValue)
            {
                result = await _yunZhangHuApplicationService.PostOrderAsync(new PostOrderAsyncInput()
                {
                    ApplyId = (Guid)id,
                    RequestPlatform = requestPlatform
                });
            }
            return Content(result.ToJsonString());
        }

        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.RefuseAuditeWithDrawMoney)]
        public async Task<IActionResult> RefuseWithDrawMoney(Guid? id)
        {
            var result = string.Empty;
            try
            {
                await _enterpirsePaymentRecordAppService.RefuseAuditeWithDrawMoney(new RefuseAuditeWithDrawMoneyInput()
                {
                    Id = id.Value
                });
                result = "拒绝成功";
            }
            catch (Exception exp)
            {

                result = exp.Message;
            }

            return Content(result);

        }

        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.ExportExcel)]
        public IActionResult ExportToExcel(string userName, WithdrawDepositState withdrawMoneyState, DateTime? startTime, DateTime? endTime, WithdrawMoneyType withdrawMoneyType, string orderNumber)
        {
            var filePath = _enterpirsePaymentRecordAppService.GetToExcel(new GetEnterpirsePaymentRecordsInput()
            {
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                UserName = userName,
                OrderNumber = orderNumber,
                WithdrawMoneyType = withdrawMoneyType,
                WithdrawDepositState = startTime == null ? WithdrawDepositState.All : withdrawMoneyState,
            });

            return Content(filePath);
            //string path = Path.Combine(_hostingEnvironment.WebRootPath, ENTERPRISEPAYMENTRECORDFILENAM);

            //return Content(path);
            //return File(ENTERPRISEPAYMENTRECORDFILENAM, "application/vnd.ms-excel;charset=utf-8", ENTERPRISEPAYMENTRECORDFILENAM);

        }


        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.ManualPass)]
        /// <summary>
        /// 手动通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ManualPass(Guid? id)
        {
            string result = string.Empty;
            try
            {
                var response = await _enterpirsePaymentRecordAppService.UpdateWithdrawRecord(id.Value);

                result = response.ToString();
            }
            catch (Exception exp)
            {
                result = exp.Message;
            }

            return Content(result);
        }
        /// <summary>
        /// 云平台提现审核通过，下单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent)]
        [HttpPost]
        public async Task<JsonResult> PostOrderAsync([FromBody]PostOrderAsyncInput input)
        {
            Console.WriteLine($"input=>{input.ToJsonString()}");
            var result = await _yunZhangHuApplicationService.PostOrderAsync(input);
            return Json(result);
        }
        /// <summary>
        /// 云平台提现审核通过，下单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent)]
        [HttpPost]
        public async Task<JsonResult> PostSetOrderFailAsync([FromBody]PostSetOrderFailAsyncInput input)
        {
            var result = await _yunZhangHuApplicationService.PostSetOrderFailAsync(input);
            return Json(result);
        }
    }
}