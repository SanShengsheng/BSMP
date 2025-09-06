using System.Collections.Generic;
using MQKJ.BSMP.Roles.Dto;

namespace MQKJ.BSMP.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
