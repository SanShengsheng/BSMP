using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.UI;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.Authorization.Roles;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.Roles.Dto;
using Abp.Authorization.Roles;
using System;

namespace MQKJ.BSMP.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Role> _roleRepository;
        //private readonly IRolePermissionStore<Role> _roleStore;
        private readonly RoleStore _roleStore;
        //private readonly IPermissionDependency.;

        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager,
            IRepository<Role> roleRepository,
            RoleStore roleStore)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            //_permissionManager = permissionManager;
            _roleRepository = roleRepository;
            //_permissionRepository = permissionRepository;
            _roleStore = roleStore;
        }

        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task<RoleDto> Update(RoleDto input)
        {

            CheckUpdatePermission();
            //var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var role = await _roleRepository.GetAll().Include(p =>p.Permissions)
                .FirstOrDefaultAsync(r => r.Id == input.Id);
            var oldPermissions = await _roleManager.GetGrantedPermissionsAsync(role.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));
            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();
            //await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            // 源代码

            // myself
            //添加权限
            foreach (var item in grantedPermissions)
            {
                await _roleStore.AddPermissionAsync(role, new PermissionGrantInfo(item.Name, true));
            }
            //删除权限
            foreach (var item in oldPermissions)
            {
                var removePermission = grantedPermissions.FirstOrDefault(c => c.Name == item.Name);
                if (removePermission == null)
                {
                    await _roleStore.RemovePermissionAsync(role, new PermissionGrantInfo(item.Name, true));
                }
            }
            return MapToEntityDto(role);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions);
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
