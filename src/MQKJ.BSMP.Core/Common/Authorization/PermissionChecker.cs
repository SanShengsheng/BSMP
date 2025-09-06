using Abp.Authorization;
using MQKJ.BSMP.Authorization.Roles;
using MQKJ.BSMP.Authorization.Users;

namespace MQKJ.BSMP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
