using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation;
using JCSoft.WX.Framework.Api;
using JCSoft.WX.Framework.Models;
using JCSoft.WX.Framework.Models.ApiRequests;
using JCSoft.WX.Framework.Models.Miniapp.Requests;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.MiniappServices.BigRisk.Models;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.Utils.Tools.HttpRequestTool;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.MiniappServices
{
    public class MiniappService : BSMPAppServiceBase, IMiniappService
    {
        private readonly IApiClient _apiClient;
        private readonly Castle.Core.Logging.ILogger _logger;
        //private IDistributedCache _memoryCache;
        private readonly IRepository<Player, Guid> _playerRepository;
        private readonly IRepository<PlayerExtension> _playerExtensionRepository;

        private readonly IRepository<Tenant> _tenantRepository;

        public MiniappService(IApiClient apiClient,
            //IDistributedCache memoryCache,
            IRepository<Player, Guid> playerRepository,
            IRepository<PlayerExtension> playerExtensionRepository,
            IRepository<Tenant> tenantRepository
            )
        {
            _apiClient = apiClient;
            _logger = Logger.CreateChildLogger(this.GetType().FullName);
            //_memoryCache = memoryCache;
            _playerRepository = playerRepository;
            _playerExtensionRepository = playerExtensionRepository;

            _tenantRepository = tenantRepository;
        }

        public List<BSMPTenantDto> GetTenants()
        {

            var lst = _tenantRepository.GetAll().Select(t => new BSMPTenantDto { Id = t.Id, Name = t.Name }).ToList();

            return lst;
        }

        public Tenant GetTenant(int id)
        {
            return _tenantRepository.Get(id);
        }

        /// <summary>
        /// 小程序获取openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetOpenIdOutput GetOpenId(GetOpenIdInput input)
        {
            var request = new SnsJscodeToSessionRequest
            {
                AppId = input.AppId,
                AppSecret = input.AppSecret,
                Code = input.Code
            };

            var response = _apiClient.Execute(request);
            if (response.IsError)
            {
                Logger.Error($"请求微信api出错，错误信息：{response}");
                throw new AbpValidationException($"获取OpenId发生错误，错误信息：{response}");
            }

            return new GetOpenIdOutput
            {
                OpenId = response.OpenId,
                SessionKey = response.SessionKey,
                Unionid = response.Unionid
            };
        }

        /// <summary>
        /// 获取公众号openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetOfficeAccountOpenIdOutput GetOfficeAccountOpenId(GetOpenIdInput input)
        {
            var request = new AccessTokenCodeRequest
            {
                AppId = input.AppId,
                AppSecret = input.AppSecret,
                Code = input.Code
            };

            var response = _apiClient.Execute(request);

            if (response.IsError)
            {
                Logger.Error($"获取公众号openid出错,错误信息:{response}");

                return new GetOfficeAccountOpenIdOutput()
                {
                    ErrCode = response.ErrorCode.ToString(),
                    ErrMsg = response.ErrorMessage
                };
            }


            return new GetOfficeAccountOpenIdOutput
            {
                AccessToken = response.Access_Token,
                ExpiresTime = response.Expires_in.ToString(),
                OpenId = response.OpenId,
                RefreshToken = response.Refresh_token,
                Scope = response.Scope
            };
        }

        public GetOfficeAccountUnionIdOutput GetOfficeAccountUnionId(GetOfficeAccountUnionIdInput input)
        {
            var request = new SnsUserInfoRequest
            {
                AccessToken = input.AccessToken,
                Lang = Language.CN,
                OAuthToken = input.AccessToken,
                OpenId = input.OpenId
            };

            var response = _apiClient.Execute(request);

            if (response.IsError)
            {
                Logger.Error($"获取用户unionId出错,错误信息:{response}");

                return null;
            }


            return new GetOfficeAccountUnionIdOutput
            {
                City = response.City,
                Country = response.Country,
                Gender = response.Sex.ToInt32(1),
                HeadUrl = response.HeadImageUrl,
                NickName = response.NickName,
                OpenId = response.OpenId,
                Privilege = response.Privilege?.ToArray(),
                Province = response.Province,
                UnionId = response.UnionId
            };
            //var response = WeChatPayApi.GetOfficeAccountUnionId(new GetOfficeAccountUnionIdInput()
            //{
            //    AccessToken = input.AccessToken,
            //    OpenId = input.OpenId,
            //    Lang = input.Lang
            //});

            //return response;
        }

        /// <summary>
        /// 获取accesstoken
        /// </summary>
        /// <param name="input"></param>
        public GetAccessTokenWithCodeOutput GetWebAccessTokenWithCode(GetAccessTokenWithCodeInput input)
        {
            var response = WeChatPayApi.GetWechatWebAccessToken(new GetAccessTokenWithCodeInput()
            {
                AppId = input.AppId,
                Secret = input.Secret,
                Code = input.Code,
                GrantType = input.GrantType
            });

            return response;
        }

        /// <summary>
        /// 刷新accesstoken
        /// </summary>
        /// <param name="input"></param>
        public RefreshAccessTokenOutput RefreshAccessToken(RefreshAccessTokenInput input)
        {
            var response = WeChatPayApi.RefreshAccessToken(new RefreshAccessTokenInput()
            {
                AppId = input.AppId,
                RefreshToken = input.RefreshToken,
                GrantType = input.GrantType
            });

            return response;
        }

        /// <summary>
        /// 检测accesstoken
        /// </summary>
        /// <param name="input"></param>
        public CheckAccessTokenOutput CheckAccessToken(CheckAccessTokenInput input)
        {
            var response = WeChatPayApi.CheckAccessToken(new CheckAccessTokenInput()
            {
                AccessToken = input.AccessToken,
                OpenId = input.OpenId
            });

            return response;
        }

        public GetWechatWebUserInfoOutput GetWechatUserInfo(GetWechatWebUserInfoInput input)
        {
            var response = WeChatPayApi.GetWechatWebUserInfo(new GetWechatWebUserInfoInput()
            {
                AccessToken = input.AccessToken,
                OpenId = input.OpenId,
                Lang = input.Lang
            });

            return response;
        }

        //public Task GetPayInfo(GetPayInfoInput input)
        //{
        //    _playerRepository.FirstOrDefaultAsync(x => x.id == input.)
        //}

        //public void CreateTempalateInfo(CreateTemplateInfoInputDto input)
        //{
        //   _memoryCache.SetStringAsync(input.PlayerId, input.TemplateId,new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow=new TimeSpan(5000)    });
        //}

        /// <summary>
        /// 获取接待员
        /// </summary>
        public List<DeveloperPlayersOutput> GetWaitersAsync(Guid? playerId)
        {
            var players = new object();
            var tenant = GetCurrentTenantAsync().Result;
            AddWaiter(playerId);//将当前用户添加到接待员
            if (playerId == null)
            {
                players = _playerRepository.GetAllList(a => a.IsDeveloper && !a.IsDeleted && a.TenantId == tenant.Id).Select(s => new { s.NickName, s.Id }).ToList();
            }
            else
            {
                players = _playerRepository.GetAllList(a => a.IsDeveloper && !a.IsDeleted && a.Id != playerId && a.TenantId == tenant.Id).Select(s => new { s.NickName, s.Id }).ToList();
            }
            return players.MapTo<List<DeveloperPlayersOutput>>();
        }

        /// <summary>
        /// 添加接待员
        /// </summary>
        private bool AddWaiter(Guid? playerId)
        {
            if (playerId == null) { return false; }
            var tenant = GetCurrentTenantAsync().Result;
            var player = _playerRepository.FirstOrDefault(s => s.Id == playerId && s.TenantId == tenant.Id);
            if (!player.IsDeveloper)
            {
                player.IsDeveloper = true;
                _playerRepository.Update(player);
            }
            return true;
        }


    }
}