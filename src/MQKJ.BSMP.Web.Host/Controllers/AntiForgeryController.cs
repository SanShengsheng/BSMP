using Microsoft.AspNetCore.Antiforgery;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Host.Controllers
{
    public class AntiForgeryController : BSMPControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
