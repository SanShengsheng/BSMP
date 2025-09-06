using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;

namespace MQKJ.BSMP.Web.Views.Shared.Components.SideBarNav
{
    public class SideBarNavViewComponent : BSMPViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;

        public SideBarNavViewComponent(
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
        }

        public async Task<IViewComponentResult> InvokeAsync(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier()),
                ActiveMenuItemName = activeMenu
            };
            model.MainMenu.Items = SetMenuDisplay(model.MainMenu.Items);

            return View(model);
        }
        /// <summary>
        ///  处理菜单显示/禁用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<UserMenuItem> SetMenuDisplay(IList<UserMenuItem> menus)
        {
            var response = menus;
            foreach (var item in menus)
            {
                //item.IsEnabled = false;
                var isHave = IsHasMenuItem(item, "RunWaterRecord");
                //System.Console.WriteLine($"item.DisplayName =>{item.DisplayName},item.Name=>{item.Name},IsHasMenuItem=>{isHave}");

                // 暂将第三方主播公司管理员角色设置为只可见 流水记录
                if (User.IsInRole("companyAdmin") )
                {
                    if (isHave)
                    {
                        item.IsVisible = true;
                    }
                    else
                    {
                        item.IsVisible = false;
                    }
                    //item.IsEnabled = true;
                }
                if (!item.Items.IsNullOrEmpty())
                {
                    SetMenuDisplay(item.Items);
                }

            }
            return menus;
        }
        /// <summary>
        /// 检查是否有指定菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="menuName"></param>
        /// <returns></returns>
        private bool IsHasMenuItem(UserMenuItem userMenuItem, string menuName)
        {
            if (userMenuItem.Items.Any(s => s.Name == menuName))
            {
                return true;
            }
            if (userMenuItem.Items.Any(s=>!s.Items.IsNullOrEmpty()))
            {
                return IsHasMenuItem(userMenuItem.Items.FirstOrDefault(s=>!s.Items.IsNullOrEmpty()), menuName);
            }
          
            return userMenuItem.Name==menuName;
        }
    }
}
