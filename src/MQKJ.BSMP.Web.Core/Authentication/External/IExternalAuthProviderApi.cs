using System.Threading.Tasks;

namespace MQKJ.BSMP.Authentication.External
{
    public interface IExternalAuthProviderApi
    {
        ExternalLoginProviderInfo ProviderInfo { get; }

        Task<bool> IsValidUser(string userId, string accessCode);

        Task<ExternalAuthUserInfo> GetUserInfoAsync(string accessCode);

        void Initialize(ExternalLoginProviderInfo providerInfo);
    }
}
