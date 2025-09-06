using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Reports;
using MQKJ.BSMP.Reports.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BSMPControllerBase
    {
        private readonly IReportService _reportService;
        public HomeController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<ActionResult> Index()
        {
            // 新增如果是第三方公司，直接跳转到流水管理页面
            if (User.IsInRole("companyAdmin"))
            {
                return Redirect("/ChineseBabies/RunWaterRecord");
            }
            var response = await _reportService.GetHomeReport(new GetHomeReportRequest
            {

            });

            return View(response);
        }
    }
}
