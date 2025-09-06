using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation;
using JCSoft.WX.Framework.Api;
using JCSoft.WX.Framework.Models.Miniapp.Requests;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.LoveCard.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCard.LoveCards.Dtos;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.LoveCards.Dtos;
using MQKJ.BSMP.MiniappServices.LoveCard.Models;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Products;
using MQKJ.BSMP.UnLocks;
using MQKJ.BSMP.UnLocks.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using System;
using System.Threading.Tasks;

namespace MQKJ.BSMP.MiniappServices.LoveCard
{
    public class LoveCardMiniappService : BSMPAppServiceBase, ILoveCardMiniappService
    {
        private readonly IApiClient _apiClient;
        private readonly Castle.Core.Logging.ILogger _logger;
        //private IDistributedCache _memoryCache;
        private readonly IRepository<Player, Guid> _playerRepository;
        private readonly IRepository<PlayerExtension> _playerExtensionRepository;
        private readonly ILoveCardFileAppService _loveCardFileAppService;
        private readonly ILoveCardAppService _loveCardAppService;
        private readonly IUnlockAppService _unlockAppService;
        private readonly IWeChatPayAppService _weChatPayAppService;
        private readonly IRepository<Product> _productRepository;

        public LoveCardMiniappService(IApiClient apiClient,
            //IDistributedCache memoryCache,
            IRepository<Player, Guid> playerRepository,
            IRepository<PlayerExtension> playerExtensionRepository,
            ILoveCardFileAppService loveCardFileAppService,
            ILoveCardAppService loveCardAppService,
            IUnlockAppService unlockAppService,
            IWeChatPayAppService weChatPayAppService,
            IRepository<Product> productRepository
            )
        {
            _apiClient = apiClient;
            _logger = Logger.CreateChildLogger(this.GetType().FullName);
            //_memoryCache = memoryCache;
            _playerRepository = playerRepository;
            _playerExtensionRepository = playerExtensionRepository;
            _loveCardFileAppService = loveCardFileAppService;
            _loveCardAppService = loveCardAppService;
            _unlockAppService = unlockAppService;
            _weChatPayAppService = weChatPayAppService;
            _productRepository = productRepository;
        }

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

        public Task<PagedResultDto<LoveCardListDto>> GetCardList(GetLoveCardsInput input)
        {
            return _loveCardAppService.GetPaged(input);
        }

        public async Task<UpLoadLoveCardFileOutput> UploadFile(UploadLoveCardFileDto input)
        {
            //保存图片
            return await _loveCardFileAppService.UploaCardFileAsync(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SaveCardOutput> SaveCard(SaveCardInput input)
        {

            var loveCardFilePath = string.Empty;

            //保存玩家其他信息
            var loveCard = await _loveCardAppService.CreateOrUpdateLoveCard(new CreateOrUpdateLoveCardInput()
            {
                PlayerId = input.PlayerId,
                BirthDate = input.BirthDate,
                Gender = input.playerGender,
                Domicile = input.Domicile,
                //WeChatAccount = input.WeChatAccount,
                Id = input.LoveCardId,
                Label = input.Label,
                StyleCode = input.StyleCode,
                Introduce = input.Introduce
            });

            if (loveCard == null)
            {
                return new SaveCardOutput();
            }
            else
            {
                var uploadFile = new UploadLoveCardFileDto(input.FormFile, input.PlayerId, loveCard.LoveCardId);

                if (uploadFile.FormFile != null)
                {
                    var file = await UploadFile(uploadFile);

                    loveCardFilePath = file.FilePath;
                }
                else
                {
                    var fileType = FileType.Image;

                    //if (input.WeChatAccount != null) //图片文件
                    //{
                        //fileType = FileType.Image;
                    //}
                    //else //录音文件
                    //{
                        //fileType = FileType.Audio;
                    //}

                    loveCardFilePath = await _loveCardFileAppService.GetLoveCardFileByCardId(new GetLoveCardFileByCardIdInput()
                    {
                        LoveCardId = input.LoveCardId.Value,
                        FileType = fileType
                    });
                }
            }

            return new SaveCardOutput()
            {
                FilePath = loveCardFilePath,
                LoveCardId = loveCard.LoveCardId,
                CardCode = loveCard.CardCode
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SaveCardOutput> SaveOtherInfo(SaveCardInput input)
        {

            var loveCardFilePath = string.Empty;
            var loveCard = new CreateOrUpdateLoveCardOutput();
            if (input.LoveCardId != null)
            {
                //var card = await _loveCardAppService.GetForEdit(new NullableIdDto<Guid>(input.LoveCardId));
                var loveCardModel = new UpdateLoveCardOtherInfoInput() {
                    PlayerId=input.PlayerId,LoveCardId=(Guid)input.LoveCardId,Introduce=input.Introduce
                };
                //保存玩家其他信息
                loveCard = await _loveCardAppService.UpdateLoveCardOtherInfo(loveCardModel);

                var uploadFile = new UploadLoveCardFileDto(input.FormFile, input.PlayerId, loveCard.LoveCardId);

                if (uploadFile.FormFile != null)
                {
                    var file = await UploadFile(uploadFile);

                    loveCardFilePath = file.FilePath;
                }
              
            }

            return new SaveCardOutput()
            {
                FilePath = loveCardFilePath,
                LoveCardId = loveCard.LoveCardId,
                CardCode = loveCard.CardCode
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SaveCardOutput> SaveOtherInfo2(SaveCardOtherInput input)
        {

            var loveCardFilePath = string.Empty;
            var loveCard = new CreateOrUpdateLoveCardOutput();
            if (input.LoveCardId != null)
            {
                //var card = await _loveCardAppService.GetForEdit(new NullableIdDto<Guid>(input.LoveCardId));
                var loveCardModel = new UpdateLoveCardOtherInfoInput()
                {
                    PlayerId = input.PlayerId,
                    LoveCardId = (Guid)input.LoveCardId,
                    Introduce = input.Introduce,
                    Profession=input.Profession,
                    Weight=input.Weight,
                    Height=input.Height,
                    WeChatAccount=input.WeChatAccount
                };
                //保存玩家其他信息
                loveCard = await _loveCardAppService.UpdateLoveCardOtherInfo(loveCardModel);

                var uploadFile = new UploadLoveCardFileDto(input.FormFile, input.PlayerId, loveCard.LoveCardId);

                if (uploadFile.FormFile != null)
                {
                    var file = await UploadFile(uploadFile);

                    loveCardFilePath = file.FilePath;
                }

            }

            return new SaveCardOutput()
            {
                FilePath = loveCardFilePath,
                LoveCardId = loveCard.LoveCardId,
                CardCode = loveCard.CardCode
            };
        }
        public async Task<CreateOrUpdateLoveCardOutput> CreateOrUpdateLoveCard(CreateOrUpdateLoveCardInput input)
        {
            return await _loveCardAppService.CreateOrUpdateLoveCard(input);
        }

        //public async Task<string> UnlockWeChatAccount(UnlockWeChatAccountInput input)
        //{
        //    await _unlockAppService.UnlockWeChatAccount(input);

        //    return "success";
        //}

        public async Task<MiniProgramPayOutput> UnlockWeChatAccount(UnlockWeChatAccountInput input)
        {
            var player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == input.ProductId);

            var output = await _weChatPayAppService.Pay(new SendPaymentRquestInput()
            {
                TenantId = player.TenantId,
                PlayerId = input.PlayerId,
                OpenId = player.OpenId,
                ClientType = input.ClientType,
                Totalfee = Convert.ToDecimal(product.Price),
                ProductId = input.ProductId
            });

            return output;
        }

        public async Task<GetUnLockResultOutput> GetUnLockResult(GetUnLockResultInput input)
        {
            var output = new GetUnLockResultOutput();

            try
            {
                var player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

                var order = _weChatPayAppService.QueryWechatPayResult(new QueryOrderStateInput()
                {
                    TenantId = player.TenantId,
                    TransactionId = input.TransactionId,
                    OutTradNo = input.OutTradeNo
                });
            }
            catch (Exception exp)
            {
                Logger.Error($"获取解锁结果失败：{exp}");
                output.IsSuccess = false;
            }

            output.IsSuccess = true;

            return output;
        }
    }
}