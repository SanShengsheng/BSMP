using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MQKJ.BSMP.Configuration.Dto;

namespace MQKJ.BSMP.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BSMPAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
