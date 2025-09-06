using Abp.AutoMapper;
using MQKJ.BSMP.Authentication.External;

namespace MQKJ.BSMP.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
