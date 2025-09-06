using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.LoveCards.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class CardManageController : BSMPControllerBase
    {
        private readonly ILoveCardAppService _loveCardAppService;

        public CardManageController( ILoveCardAppService loveCardAppService)
        {
            _loveCardAppService = loveCardAppService;
        }

        public async Task<IActionResult> Index(int? page, DateTime? startTime, DateTime? endTime)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

           var result = await _loveCardAppService.GetAllCardList(new GetAllCardListInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,

                StartTime = startTime ?? DateTime.Now.AddDays(-7),

                EndTime = endTime ?? DateTime.Now,
            });

            var lovecardList = new StaticPagedList<LoveCardListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", lovecardList);
            }
            else
            {
                return View(lovecardList);
            }
        }
    }
}