using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : BSMPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
