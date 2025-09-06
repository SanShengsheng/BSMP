using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Common.Companies;
using MQKJ.BSMP.Roles.Dto;
using MQKJ.BSMP.Users.Dto;

namespace MQKJ.BSMP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task ModifyPassword(ModifyPasswordInput input);
        Task<List<Company>> GetAllUserCompanies();

        Task<bool> ModifyPasswordForTest(ModifyPasswordInput input);
    }
}
