using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Essensoft.AspNetCore.Payment.WeChatPay;
using Essensoft.AspNetCore.Payment.WeChatPay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Authentication.JwtBearer;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Extensions;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.PlayerExtensions;
using MQKJ.BSMP.Players.WeChat;
using MQKJ.BSMP.TextAudios;
using MQKJ.BSMP.TextAudios.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.Web.Host.Models;
using MQKJ.BSMP.WeChat.Dtos;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;

namespace MQKJ.BSMP.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WechatController : BSMPControllerBase
    {
        private readonly IMiniappService _miniappService;
        private readonly IPlayerExtensionAppService _playerExtensionAppService;
        private readonly ITextAudiosAppService _textAudiosAppService;

        private readonly IWeChatPayAppService _weChatPayAppService;

        //private readonly IWeChatPayClient _client;

        private readonly IWeChatPlayerAppService _weChatPlayerAppService;

        public WechatController(IMiniappService miniappService
            , IPlayerExtensionAppService playerExtensionAppService
            , ITextAudiosAppService textAudiosAppService
            , IWeChatPayAppService weChatPayAppService
            , IWeChatPlayerAppService weChatPlayerAppService
            /*, IWeChatPayClient client*/)
        {
            _miniappService = miniappService;
            _playerExtensionAppService = playerExtensionAppService;
            _textAudiosAppService = textAudiosAppService;
            _weChatPayAppService = weChatPayAppService;

            _weChatPlayerAppService = weChatPlayerAppService;

            //_client = client;
        }

        [HttpPost]
        [Route("GetOpenId")]
        public ApiResponseModel<GetOpenIdOutput> GetOpenId(GetOpenIdInput input) =>
            this.ApiFunc(() => _miniappService.GetOpenId(input));


        /// <summary>
        /// 获取接待员
        /// </summary>
        [HttpGet]
        [Route("GetWaiters")]
        [ResponseCache(VaryByHeader = "Accept-Encoding", Location = ResponseCacheLocation.Any, Duration = 600)]
        public ApiResponseModel<List<DeveloperPlayersOutput>> GetWaiters(Guid? playerId) => this.ApiFunc(() => _miniappService.GetWaitersAsync(playerId));

        /// <summary>
        /// 分享到群
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShareToGroup")]
        public ApiResponseModel<RecoverStrengthOutput> ShareToGroup(RecoverStrengthInput input)
        {
            return this.ApiFunc(() => _playerExtensionAppService.RecoverStrength(input));
        }

        ///// <summary>
        ///// 获取短语语音播放列表
        ///// </summary>
        //[HttpGet]
        //[Route("GetTextAudios")]
        //public async Task<ApiResponseModel<PagedResultDto<TextAudiosListDto>>> GetTextAudios(GetTextAudiossInput input)
        //{
        //   return  this.ApiTaskFunc<PagedResultDto<TextAudiosListDto>>( (await  _textAudiosAppService.GetPaged(new GetTextAudiossInput() { MaxResultCount = 200 })));
        //    //() => _textAudiosAppService.GetPaged(new GetTextAudiossInput() { MaxResultCount = 200 })
        //}
        /// <summary>
        /// 获取短语语音播放列表
        /// </summary>
        [HttpGet]
        [Route("GetTextAudios")]
        [ResponseCache(VaryByHeader = "Accept-Encoding", Location = ResponseCacheLocation.Any, Duration = 3600)]
        public ApiResponseModel<PagedResultDto<TextAudiosListDto>> GetTextAudios(ESceneType scene, int maxResultCount)
        {
            return (this.ApiFunc<PagedResultDto<TextAudiosListDto>>(() => _textAudiosAppService.GetPaged(new GetTextAudiossInput() { Scene = scene, MaxResultCount = maxResultCount }).Result));
        }


        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WeChatPay")]
        public async Task<ApiResponseModel<MiniProgramPayOutput>> WeChatPay(SendPaymentRquestInput input)
        {
            return await this.ApiTaskFunc(_weChatPayAppService.Pay(input));
        }

        ///// <summary>
        ///// 公众号支付
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("TestPubPay")]
        //public async Task TestPubPay(WeChatPayPubPayViewModel weChatPayPubPayViewModel)
        //{
        //    var request = new WeChatPayUnifiedOrderRequest()
        //    {
        //        Body = weChatPayPubPayViewModel.Body,
        //        OutTradeNo = weChatPayPubPayViewModel.OutTradeNo,
        //        TotalFee = weChatPayPubPayViewModel.TotalFee,
        //        SpbillCreateIp = weChatPayPubPayViewModel.SpbillCreateIp,
        //        NotifyUrl = weChatPayPubPayViewModel.NotifyUrl,
        //        TradeType = weChatPayPubPayViewModel.TradeType,
        //        OpenId = weChatPayPubPayViewModel.OpenId
        //    };
        //    var response = await _client.ExecuteAsync(request);
        //    if (response.ReturnCode == "SUCCESS" && response.ResultCode == "SUCCESS")
        //    {
        //        var req = new WeChatPayH5CallPaymentRequest
        //        {
        //            Package = "prepay_id=" + response.PrepayId
        //        };
        //        var parameter = await _client.ExecuteAsync(req);
        //        // 将参数(parameter)给 公众号前端 让他在微信内H5调起支付(https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7&index=6)
        //    }
        //}

        ///// <summary>
        ///// 扫码支付
        ///// </summary>
        ///// <param name="viewModel"></param>
        ///// <returns></returns>
        //[HttpPost("QRCodePay")]
        //public async Task QRCodePay(WeChatPayQrCodePayViewModel viewModel)
        //{
        //    var request = new WeChatPayUnifiedOrderRequest
        //    {
        //        Body = viewModel.Body,
        //        OutTradeNo = viewModel.OutTradeNo,
        //        TotalFee = viewModel.TotalFee,
        //        SpbillCreateIp = viewModel.SpbillCreateIp,
        //        NotifyUrl = viewModel.NotifyUrl,
        //        TradeType = viewModel.TradeType
        //    };
        //    var response = await _client.ExecuteAsync(request);

        //}

        ///// <summary>
        ///// app支付
        ///// </summary>
        ///// <param name="viewModel"></param>
        ///// <returns></returns>
        //[HttpPost("AppPay")]
        //public async Task AppPay(WeChatPayAppPayViewModel viewModel)
        //{
        //    var request = new WeChatPayUnifiedOrderRequest
        //    {
        //        Body = viewModel.Body,
        //        OutTradeNo = viewModel.OutTradeNo,
        //        TotalFee = viewModel.TotalFee,
        //        SpbillCreateIp = viewModel.SpbillCreateIp,
        //        NotifyUrl = viewModel.NotifyUrl,
        //        TradeType = viewModel.TradeType
        //    };
        //    var response = await _client.ExecuteAsync(request);

        //    if (response.ReturnCode == "SUCCESS" && response.ResultCode == "SUCCESS")
        //    {
        //        var req = new WeChatPayAppCallPaymentRequest
        //        {
        //            PrepayId = response.PrepayId
        //        };
        //        var parameter = await _client.ExecuteAsync(req);
        //    }
        //}

        //[HttpPost("H5Pay")]
        //public async Task H5Pay(WeChatPayH5PayViewModel viewModel)
        //{
        //    var request = new WeChatPayUnifiedOrderRequest
        //    {
        //        Body = viewModel.Body,
        //        OutTradeNo = viewModel.OutTradeNo,
        //        TotalFee = viewModel.TotalFee,
        //        SpbillCreateIp = viewModel.SpbillCreateIp,
        //        NotifyUrl = viewModel.NotifyUrl,
        //        TradeType = viewModel.TradeType,
        //    };
        //    var response = await _client.ExecuteAsync(request);
        //    // mweb_url为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付,mweb_url的有效期为5分钟。
        //}

        //[HttpPost("MinProgramPay")]
        //public async Task MinProgramPay(WeChatPayLiteAppPayViewModel viewModel)
        //{
        //    var request = new WeChatPayUnifiedOrderRequest
        //    {
        //        Body = viewModel.Body,
        //        AppId = "",
        //        OutTradeNo = viewModel.OutTradeNo,
        //        TotalFee = viewModel.TotalFee,
        //        SpbillCreateIp = viewModel.SpbillCreateIp,
        //        NotifyUrl = viewModel.NotifyUrl,
        //        TradeType = viewModel.TradeType,
        //        OpenId = viewModel.OpenId
        //    };
        //    var response = await _client.ExecuteAsync(request);

        //    if (response.ReturnCode == "SUCCESS" && response.ResultCode == "SUCCESS")
        //    {
        //        var req = new WeChatPayLiteAppCallPaymentRequest
        //        {
        //            Package = "prepay_id=" + response.PrepayId
        //        };
        //        var parameter = await _client.ExecuteAsync(req);
        //        // 将参数(parameter)给 小程序前端 让他调起支付API(https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=7_7&index=5)
        //    }
        //}

        ///// <summary>
        ///// 查询订单
        ///// </summary>
        ///// <param name="viewModel"></param>
        ///// <returns></returns>
        //[HttpPost("OrderQuery")]
        //public async Task<IActionResult> OrderQuery(WeChatPayOrderQueryViewModel viewModel)
        //{
        //    var request = new WeChatPayOrderQueryRequest
        //    {
        //        TransactionId = viewModel.TransactionId,
        //        OutTradeNo = viewModel.OutTradeNo
        //    };

        //    var response = await _client.ExecuteAsync(request);
        //    ViewData["response"] = response.Body;
        //    return View();
        //}

        /// <summary>
        /// 公众号登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("VaildPubPlayer")]
        //[HttpPost("GetPlayerId")]
        //[AbpAuthorize]
        public async Task<ApiResponseModel<GetBabiesOutput>> VaildPubPlayer([FromBody]VaildPubPlayerInput input)
        {
            ////var playerId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.PlayerId);
            ////var temp = Guid.Empty;
            ////var isGuid = Guid.TryParse(playerId,out temp);
            ////input.PlayerId = temp;
            return await this.ApiTaskFunc(_weChatPlayerAppService.VaildPubPlayer(input));
        }


        [HttpPost("GetPlayerId")]
        public ApiResponseModel<string> VaildPubPlayer()
        {
            Func<string> playerId = new Func<string>(GetPlayerId);

            return this.ApiFunc(playerId);
        }

        private string GetPlayerId()
        {
            return AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.PlayerId);
        }

        [AbpAuthorize]
        /// <summary>
        /// 通过code获取accesstoken
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAccessTokenWithCode")]
        public ApiResponseModel<GetAccessTokenWithCodeOutput> GetAccessTokenWithCode([FromQuery]GetAccessTokenWithCodeDto input)
        {
            return this.ApiFunc(() => _weChatPlayerAppService.GetAccessTokenWithCode(input));
        }

        [AbpAuthorize]
        [HttpPost("LoginSignUpSystem")]
        public Task<ApiResponseModel<LoginSignUpSystemOutput>> LoginSignUpSystem([FromBody]LoginSignUpSystemInput input)
        {
            var userId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.UserId);
            input.UserId = long.Parse(userId);
            return this.ApiTaskFunc(_weChatPlayerAppService.LoginSignUpSystem(input));
        }

        [AbpAuthorize]
        [HttpPost("RegisterSignUpSystem")]
        public Task<ApiResponseModel<LoginSignUpSystemOutput>> RegisterSignUpSystem([FromBody]RegisterSignUpSystemInput input)
        {
            var userId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.UserId);
            input.UserId = long.Parse(userId);
            return this.ApiTaskFunc(_weChatPlayerAppService.RegisterSignUpSystem(input));
        }

    }
}