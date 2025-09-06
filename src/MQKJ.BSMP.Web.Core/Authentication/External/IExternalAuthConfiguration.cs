using System.Collections.Generic;

namespace MQKJ.BSMP.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
