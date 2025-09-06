using System.Collections.Generic;
using MQKJ.BSMP.Roles.Dto;
using MQKJ.BSMP.Users.Dto;

namespace MQKJ.BSMP.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
