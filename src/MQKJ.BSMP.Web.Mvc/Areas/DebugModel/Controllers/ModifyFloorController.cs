using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.Friends.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.DebugModel.Controllers
{
    [Area("DebugModel")]
    public class ModifyFloorController : BSMPControllerBase
    {
        private IFriendAppService _friendAppService;

        public ModifyFloorController(IFriendAppService friendAppService)
        {
            _friendAppService = friendAppService;
        }

        //public IActionResult Index(int? page,string inviterName,string inviteeName)
        public IActionResult Index()
        {
            //var pageNumber = page ?? 1;
            //var pageSize = 10;
            //var result = _friendAppService.GetAllFriendList(new GetAllFriendListDto()
            //{
            //    SkipCount = (pageNumber - 1) * pageSize,
            //    InviterName = inviterName,
            //    InviteeName = inviterName

            //});

            //var gameTasks = new StaticPagedList<GetAllFriendListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_List", gameTasks);
            //}
            //else
            //{
            //    return View(gameTasks);
            //}

            return View();
        }
    }
}