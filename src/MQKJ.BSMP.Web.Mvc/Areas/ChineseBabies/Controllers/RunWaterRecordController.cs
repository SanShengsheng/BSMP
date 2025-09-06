using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Web.Areas.ChineseBabies.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [Area("ChineseBabies")]
    public class RunWaterRecordController : BSMPControllerBase
    {
        private readonly IMqAgentAppService _mqAgentAppService;


        private readonly ICoinRechargeAppService _coinRechargeAppService;


        public RunWaterRecordController(IMqAgentAppService mqAgentAppService, ICoinRechargeAppService coinRechargeAppService)
        {
            _mqAgentAppService = mqAgentAppService;

            _coinRechargeAppService = coinRechargeAppService;

        }

        //[AbpMvcAuthorize(ChinesePermissions.GetAllRunWaterRecords)]
        public async Task<IActionResult> Index(int? page, DateTime? startTime, DateTime? endTime, double? amount, string orderNo, string rechargerName, string businesserName, string profiterName, IncomeTypeEnum runWaterType,string company)
        {
            var coinRechargeRecords = await _coinRechargeAppService.PageSearch(new GetCoinRechargesInput()
            {
                MaxResultCount = 10,
                SkipCount = 0
            });

            var pageNumber = page ?? 1;
            var pageSize = 10;
            var runwaterRecords = await _mqAgentAppService.GetAllRunWaterRecords(new GetAllRunWaterRecordInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                Amount = amount,
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                OrderNo = orderNo,
                RechargerName = rechargerName,
                BusinesserName = businesserName,
                ProfiterName = profiterName,
                RunWaterType = runWaterType,
                Company = company
            });

            var model = new RunWaterRecordModel();

            model.CoinRechargeListDtos = coinRechargeRecords.Items.ToList();
            model.BrokerTotalIncome = runwaterRecords.BrokerTotalIncome;
            model.TotalAnchorIncome = runwaterRecords.TotalAnchorIncome;
            model.TotalPayment = runwaterRecords.TotalPayment;
            model.MoneyDetailedListDtos = new StaticPagedList<GetAllRunWaterRecordsListDtoWaterRecordModl>(runwaterRecords.WaterRecords.Items, pageNumber, pageSize, runwaterRecords.WaterRecords.TotalCount);
            model.RoyaltyRate = runwaterRecords.RoyaltyRate;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model);
            }
            else
            {
                return View(model);
            }
        }

        //[AbpMvcAuthorize(ChinesePermissions.ExportRunWaterRecordToExcel)]
        public IActionResult ExportToExcel(DateTime? startTime, DateTime? endTime, double? amount, string orderNo, string rechargerName, string businesserName, string profiterName)
        {
            var filePath = _mqAgentAppService.ExportIncomeReocrdToExcel(new GetAllRunWaterRecordInput()
            {
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                Amount = amount,
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                OrderNo = orderNo,
                RechargerName = rechargerName,
                BusinesserName = businesserName,
                ProfiterName = profiterName
            });


            return Content(filePath);
        }
    }
}