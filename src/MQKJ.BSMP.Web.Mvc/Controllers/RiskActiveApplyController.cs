using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ActiveApply;
using MQKJ.BSMP.ActiveApply.Dtos;
using MQKJ.BSMP.Controllers;
using System.Threading.Tasks;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class RiskActiveApplyController : BSMPControllerBase
    {
        protected IRiskActiveApplyAppService _riskActiveApplyAppService;
        protected int PageSize = 10;
        public RiskActiveApplyController(IRiskActiveApplyAppService riskActiveApplyAppService)
        {
            _riskActiveApplyAppService = riskActiveApplyAppService;
        }
        // GET: RiskActiveApply
        public async Task<ActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var input = new GetRiskActiveApplysInput()
            {
                SkipCount = (pageNumber - 1) * PageSize,
            };
            var data = await _riskActiveApplyAppService.GetPaged(input);
            var result = new StaticPagedList<RiskActiveApplyListDto>(data.Items, pageNumber, PageSize, data.TotalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", result);
            }
            else
            {
                return View(result);
            }
        }

        // GET: RiskActiveApply/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RiskActiveApply/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskActiveApply/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RiskActiveApply/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RiskActiveApply/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RiskActiveApply/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RiskActiveApply/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}