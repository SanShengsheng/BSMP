using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using MQKJ.BSMP.Authentication.External;
using MQKJ.BSMP.Authentication.JwtBearer;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.Models.TokenAuth;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Extensions;
using Abp.Json;

namespace MQKJ.BSMP.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : BSMPControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IMqAgentAppService _mqAgentAppService;
        //private readonly IPlayerAppService _playerAppService;

        public TokenAuthController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager,
            IMqAgentAppService mqAgentAppService
            //IPlayerAppService playerAppService
            )
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;

            _mqAgentAppService = mqAgentAppService;

            //_playerAppService = playerAppService;
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                GetTenancyNameOrNull()
            );

            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = loginResult.User.Id
            };
        }


        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);
            //Logger.Info($"用户模型:{Newtonsoft.Json.JsonConvert.SerializeObject(externalUser)}");
            //Logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(new UserLoginInfo(model.AuthProvider, externalUser.ProviderKey, model.AuthProvider) ) + GetTenancyNameOrNull());
            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
            //Logger.Debug(loginResult.Result.ToString());
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        //Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.ProviderKey,
                Authorization.Users.User.CreateRandomPassword(),
                true,
                externalUser.HeadUrl
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            //默认的providerkey要求同一个登录器下的同一用唯一 但是微信小程序里只有openid能做到用户唯一 openid又不能放到网络传输，因此需要修改下默认的方式
            //if (userInfo.ProviderKey != model.ProviderKey)
            //{
            //    throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            //}
            Logger.Info($"GetExternalUserInfo：{userInfo.ToJsonString()}");
            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            //var agent = 
            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity, Guid playerId, int? agentId, string unionId, string openId)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            //var agent = 
            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            var playerIdStr = playerId == Guid.Empty ? "" : playerId.ToString();
            var agentIdStr = string.Empty;
            if (!agentId.HasValue || agentId == 0)
            {
                agentIdStr = "";
            }
            else
            {
                agentIdStr = agentId.ToString();
            }
            claims.AddRange(new[]
            {
                new Claim(ExternalJwtRegisteredClaimNames.Sub, agentIdStr),
                new Claim(ExternalJwtRegisteredClaimNames.Jti, playerIdStr),
                new Claim(ExternalJwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ExternalJwtRegisteredClaimNames.PlayerId, playerIdStr),
                new Claim(ExternalJwtRegisteredClaimNames.AgentId, agentIdStr),
                new Claim(ExternalJwtRegisteredClaimNames.UnionId, unionId),
                new Claim(ExternalJwtRegisteredClaimNames.OpenId, openId),
                new Claim(ExternalJwtRegisteredClaimNames.UserId, identity.GetUserId().ToString())
            });
            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<ExternalAuthenticateResultModel> WeChatAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = new ExternalAuthUserInfo();

            try
            {
                externalUser = await GetExternalUserInfo(model);
            }
            catch (Exception exp)
            {
                throw new UserFriendlyException(exp.Message, exp);
            }

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, externalUser.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        //var playerId = Guid.Empty;
                        //var agent = await _mqAgentAppService.GetAgentWithOpenId(new Common.MqAgents.Agents.Dtos.GetAgentWithOpenIdInput()
                        //{
                        //    OpenId = externalUser.ProviderKey
                        //});

                        //if (agent != null)
                        //{
                        //    if (agent.PlayerId == Guid.Empty || agent.PlayerId == null)
                        //    {
                        //        var output = await _playerAppService.GetPlayerWithUnionId(new BigRisk.Players.Dtos.GetPlayerWithUnionIdInput
                        //        {
                        //            UnionId = externalUser.Surname
                        //        });
                        //        if (output != null)
                        //        {
                        //            playerId = output.PlayerId;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        playerId = agent.PlayerId;
                        //    }
                        //}

                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity, externalUser.PlayerId, externalUser.AgentId, externalUser.Surname, externalUser.ProviderKey));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        // Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, externalUser.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }
                        //var playerId = Guid.Empty;
                        //var agent = await _mqAgentAppService.GetAgentWithOpenId(new Common.MqAgents.Agents.Dtos.GetAgentWithOpenIdInput()
                        //{
                        //    OpenId = externalUser.ProviderKey
                        //});

                        //if (agent == null || agent.PlayerId == null)
                        //{
                        //    var output = await _playerAppService.GetPlayerWithUnionId(new BigRisk.Players.Dtos.GetPlayerWithUnionIdInput
                        //    {
                        //        UnionId = externalUser.Surname
                        //    });
                        //    if (output != null)
                        //    {
                        //        playerId = output.PlayerId;
                        //    }
                        //}
                        //else
                        //{
                        //    playerId = agent.PlayerId;
                        //}
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity, externalUser.PlayerId, externalUser.AgentId, externalUser.Surname, externalUser.ProviderKey)),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ReGetToken()
        {
            var tempAgentId = 0;

            var openId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.OpenId);

            var agent = await _mqAgentAppService.GetAgentWithOpenId(new Common.MqAgents.Agents.Dtos.GetAgentWithOpenIdInput()
            {
                OpenId = openId
            });

            if (agent == null)
            {
                tempAgentId = 0;
            }
            else
            {
                tempAgentId = agent.AgentId;
            }

            var tempPlayerId = Guid.Empty;

            var playerId = Guid.TryParse(AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.PlayerId), out tempPlayerId);

            var unionId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.UnionId);

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(WechatMiniProgramAuthProviderApi.ProviderNmae, openId, WechatMiniProgramAuthProviderApi.ProviderNmae), GetTenancyNameOrNull());

            if (loginResult.Result == AbpLoginResultType.Success)
            {
                return new ExternalAuthenticateResultModel
                {
                    AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity, tempPlayerId, tempAgentId, unionId, openId)),
                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                };
            }
            throw new AbpAuthorizationException("授权异常，请稍后重试");
        }
    }
}
