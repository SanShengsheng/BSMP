using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.GamblingModelStatistics;
using MQKJ.BSMP.GamblingModelStatistics.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.GamblingModel.Controllers
{
    [Area("GamblingModel")]
    public class ErrorQuestionStatisticsController : BSMPControllerBase
    {
        private readonly IGamblingModelAppService _gamblingModelAppService;
        public ErrorQuestionStatisticsController(IGamblingModelAppService gamblingModelAppService)
        {
            _gamblingModelAppService = gamblingModelAppService;
        }
        public IActionResult Index(int? page,DateTime dateTime)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var result = _gamblingModelAppService.GetErrQuestionDistribution(new GetErrQuestionDistributionDto()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                DateTime = dateTime
            });

            var dataList = new StaticPagedList<GetErrQuestionDistributionOutput>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", dataList);
            }
            else
            {
                return View(dataList);
            }
        }
    }
}