using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Users;
using MQKJ.BSMP.Web.Models.Users;
using MQKJ.BSMP.Users.Dto;
using Abp.Json;
using System;
using System.Linq;
namespace MQKJ.BSMP.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : BSMPControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<ActionResult> Index()
        {
            var users = (await _userAppService.GetAll(new PagedResultRequestDto {MaxResultCount = int.MaxValue})).Items; // Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Users = users,
                Roles = roles
            };
            return View(model);
        }

        public async Task<ActionResult> EditUserModal(long userId)
        {
            var companyList =await _userAppService.GetAllUserCompanies();
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            if (user.CompanyId != null) {
                var rmentity =companyList.Where(s => s.Id == user.CompanyId).FirstOrDefault();
                companyList.Remove(rmentity);
                companyList.Insert(0, rmentity);
                companyList.Add(new Common.Companies.Company { Name="无",Id=-1 });
            }
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles,
                Companies = companyList.ToList(),
            };
            return View("_EditUserModal", model);
        }

        public async Task<ActionResult> ModifyPassword(long userId,string oldPassword,string newPassword)
        {
            var result = string.Empty;

            try
            {
                await _userAppService.ModifyPassword(new ModifyPasswordInput()
                {
                    UserId = userId,
                    OldPassword = oldPassword,
                    NewPassword = newPassword
                });
            }
            catch (Exception exp)
            {

                result = exp.Message;
            }

            return Content(result);
        }
    }
}
