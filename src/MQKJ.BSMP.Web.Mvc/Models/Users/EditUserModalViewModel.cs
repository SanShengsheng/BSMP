using System.Collections.Generic;
using System.Linq;
using MQKJ.BSMP.Common.Companies;
using MQKJ.BSMP.Roles.Dto;
using MQKJ.BSMP.Users.Dto;

namespace MQKJ.BSMP.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
        public IReadOnlyList<Company> Companies { get; set; }
        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
