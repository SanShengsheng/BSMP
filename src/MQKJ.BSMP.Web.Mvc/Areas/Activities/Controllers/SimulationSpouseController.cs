using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.WeChatWebUsers.Dtos;
using MQKJ.BSMP.Controllers;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.Activities.Controllers
{
    [Area("Activities")]
    public class SimulationSpouseController : BSMPControllerBase
    {
        private readonly IWeChatWebUserAppService _weChatWebUserAppService;

        public SimulationSpouseController(IWeChatWebUserAppService weChatWebUserAppService)
        {
            _weChatWebUserAppService = weChatWebUserAppService;
        }

        public async Task<IActionResult> Index(int? page,string nickName)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var result = await _weChatWebUserAppService.GetPaged(new GetWeChatWebUsersInput()
            {
                NickName = nickName,
                SkipCount = (pageNumber - 1) * pageSize
            });

            var list = new StaticPagedList<WeChatWebUserListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }
            else
            {
                return View(list);
            }
        }

        public async Task<IActionResult> SignUp(Guid id,string wechatAccount)
        {
            var content = string.Empty;

            try
            {
                await _weChatWebUserAppService.SignUp(new UpdateUserStateInput()
                {
                    Id = id,
                    WechatAccount = wechatAccount
                });
            }
            catch (Exception exp)
            {
                content = exp.Message;
            }

            return Content(content);
        }

        public async Task<IActionResult> Match(Guid id, string wechatAccount)
        {
            var content = string.Empty;

            try
            {
                await _weChatWebUserAppService.Match(new UpdateUserStateInput()
                {
                    Id = id,
                    WechatAccount = wechatAccount
                });
            }
            catch (Exception exp)
            {
                content = exp.Message;
            }

            return Content(content);
        }
    }
}