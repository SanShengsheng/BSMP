using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.LoveCard.LoveCards.Dtos;
using MQKJ.BSMP.LoveCards.Dtos;
using MQKJ.BSMP.MiniappServices.LoveCard;
using MQKJ.BSMP.MiniappServices.LoveCard.Models;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.UnLocks.Dtos;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoveCardController : BSMPControllerBase
    {
        //private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoveCardMiniappService _loveCardMiniappService;
        private readonly IWeChatPayAppService  _weChatPayAppService;
        public LoveCardController(
            ILoveCardMiniappService loveCardMiniappService,
            IWeChatPayAppService weChatPayAppService
            )
        {
            //_hostingEnvironment = hostingEnvironment;
            _loveCardMiniappService = loveCardMiniappService;
            _weChatPayAppService = weChatPayAppService;
        }
        /// <summary>
        /// 保存卡片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SaveCard")]
        public async Task<ApiResponseModel<SaveCardOutput>> SaveCard([FromForm]SaveCardInput input)
        {
            if (Request.ContentType != "application/x-www-form-urlencoded")
            {
                var formFile = Request.Form.Files.First();

                input.FormFile = formFile;
            }

            var response = await _loveCardMiniappService.SaveCard(input);

            return   this.ApiFunc<SaveCardOutput>(() => response);
        }

        /// <summary>
        /// 保存其他信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SaveOtherInfo")]
        public async Task<ApiResponseModel<SaveCardOutput>> SaveOtherInfo([FromForm]SaveCardInput input)
        {
            if (Request.ContentType != "application/x-www-form-urlencoded"&& Request.HasFormContentType)
            {
                var formFile = Request.Form.Files.First();

                input.FormFile = formFile;
            }
           
            var response = await _loveCardMiniappService.SaveOtherInfo(input);

            return this.ApiFunc<SaveCardOutput>(() => response);
        }

        /// <summary>
        /// 保存其他信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SaveOtherInfo2")]
        public async Task<ApiResponseModel<SaveCardOutput>> SaveOtherInfo2([FromForm]SaveCardOtherInput input)
        {
            if (Request.ContentType != "application/x-www-form-urlencoded" && Request.HasFormContentType)
            {
                var formFile = Request.Form.Files.First();

                input.FormFile = formFile;
            }

            var response = await _loveCardMiniappService.SaveOtherInfo2(input);

            return this.ApiFunc<SaveCardOutput>(() => response);
        }

        [HttpGet("GetCardList")]
        public async Task<ApiResponseModel<PagedResultDto<LoveCardListDto>>> GetCardList([FromQuery]GetLoveCardsInput input)
        {
            var response = await _loveCardMiniappService.GetCardList(input);

            return this.ApiFunc< PagedResultDto<LoveCardListDto>>(() => response);
        }

        [HttpPost("CreateOrUpdateLoveCard")]
        public async Task<ApiResponseModel<CreateOrUpdateLoveCardOutput>> CreateOrUpdateLoveCard([FromBody]CreateOrUpdateLoveCardInput input)
        {
            var response = await _loveCardMiniappService.CreateOrUpdateLoveCard(input);

            return this.ApiFunc<CreateOrUpdateLoveCardOutput>(() => response);

        }

        [HttpPost("UnLockWeChatAccount")]
        public async Task<ApiResponseModel> UnLockWeChatAccount([FromBody]UnlockWeChatAccountInput input)
        {
            return await this.ApiTaskFunc(_loveCardMiniappService.UnlockWeChatAccount(input));
        }
        //public async Task<ApiResponseModel> UnLockWeChatAccount([FromBody]UnlockWeChatAccountInput input)
        //{
        //    var response = await _loveCardMiniappService.UnlockWeChatAccount(input);

        //    return this.ApiFunc<string>(() => response);
        //}

        [HttpPost("SendPay")]
        public async Task<ApiResponseModel<SendPaymentRquestOutput>> SendPay([FromBody]SendPaymentRquestInput input)
        {
            input.TerminalIp = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(input.TerminalIp))
            {
                input.TerminalIp = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            var response = await _weChatPayAppService.SendPaymentRquest(input);

            return this.ApiFunc<SendPaymentRquestOutput>(() => response);
        }

        [HttpPost("PayNotify")]
        public async Task<string> PayNotify()
        {
            Logger.Warn("收到支付通知了");

            var response = await _weChatPayAppService.GetPayNotify();

            return response;
        }

        /// <summary>
        /// 获取解锁结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetUnLockResult")]
        public async Task<ApiResponseModel<GetUnLockResultOutput>> GetUnLockResult([FromQuery]GetUnLockResultInput input)
        {
            return await this.ApiTaskFunc(_loveCardMiniappService.GetUnLockResult(input));
        }

    }
}