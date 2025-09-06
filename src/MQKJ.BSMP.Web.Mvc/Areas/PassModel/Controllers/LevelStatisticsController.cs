using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.PassModelStatistics;
using MQKJ.BSMP.PassModelStatistics.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.PassModel.Controllers
{
    [Area("PassModel")]
    public class LevelStatisticsController : BSMPControllerBase 
    {
        private readonly PassModelStatisticsAppService _passModelStatisticsAppService;

        public LevelStatisticsController(PassModelStatisticsAppService passModelStatisticsAppService)
        {
            _passModelStatisticsAppService = passModelStatisticsAppService;
        }
        public async Task<ActionResult> Index(int? page,DateTime dateTime)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var result = await _passModelStatisticsAppService.GetLevelDistribution(new GetPassModelLevelDistributionDto()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                DateTime = dateTime
            });

            var dataList = new StaticPagedList<GetLevelDistributionOutput>(result.Items, pageNumber, pageSize, result.TotalCount);
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