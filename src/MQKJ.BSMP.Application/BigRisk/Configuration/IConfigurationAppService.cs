using System.Threading.Tasks;
using MQKJ.BSMP.Configuration.Dto;

namespace MQKJ.BSMP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
