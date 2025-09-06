using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Common.Authorization.Users.Authorization;

namespace MQKJ.BSMP.Authorization
{
    public class BSMPAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var userManagmentPermission = context.CreatePermission(UserPermissions.UserManagment, L("UserManagment"));
            userManagmentPermission.CreateChildPermission(UserPermissions.QuerySubsidy, L("UserManagment.QuerySubsidy"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}
