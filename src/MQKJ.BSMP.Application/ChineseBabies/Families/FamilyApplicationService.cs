using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Json;
using Abp.Linq.Extensions;
using Abp.UI;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.Families.Model;
using MQKJ.BSMP.ChineseBabies.HostDtos.FamilyDto;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.QCloud;
using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Models.CMQ.Requests;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.BabySystemSetting;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using Abp.Domain.Uow;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Family应用层服务的接口实现方法  
    ///</summary>
    public class FamilyAppService : BsmpApplicationServiceBase<Family, int, FamilyEditDto, FamilyEditDto, GetFamilysInput, FamilyListDto>, IFamilyAppService
    {
        //reposinorys
        private readonly IRepository<Family, int> _entityRepository;
        private readonly IRepository<Baby, int> _babyRepository;
        private readonly IRepository<Profession, int> _professionRepository;
        private readonly IRepository<PlayerProfession, int> _playerProfessionRepository;
        private readonly IRepository<BabyGrowUpRecord, Guid> _babyGrowUpRecordRepository;
        private readonly IRepository<Player, Guid> _playerRepository;
        private readonly IRepository<Information, Guid> _informationRepository;
        private readonly IRepository<SystemSetting> _systemSettingRepository;
        private readonly IRepository<BuyFightCountRecord, Guid> _buyProductRecordRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;
        private readonly ICompetitionApplicationService _competitionService;
        private readonly IRepository<DismissFamilyRecord, Guid> _dismissFamilyRecordRepository;
        //private readonly IRepository<SeasonManagement> _seasonManagementRepository;
        private readonly IWeChatPayAppService _weChatPayAppService;
        private readonly IQCloudApiClient _qcouldApiClient;
        private readonly QcloudConfig _config;
        private IOptions<QueryOrderConfig> _queryOrderOption;

        private readonly IHostingEnvironment _hostingEnvironment;

        private const string FAMILYFILE = "家庭数据表.xlsx";
        //services
        private readonly IEventGroupAppService _eventGroupAppService;
        private readonly IBabyFamilyAssetAppService _babyFamilyAssetAppService;
        private readonly IBabyPropAppService _babyPropAppService;
        private readonly IRepository<FamilyWorshipRecord> _familyWorshipRecordRepository;
        //auto run 
        //private static bool IsAutoRun = false;
        public FamilyAppService(
            IRepository<Family, int> entityRepository,
         IRepository<Baby, int> babyRepository,
         IRepository<Profession, int> professionRepository,
         IRepository<PlayerProfession, int> playerProfessionRepository,
         IRepository<BabyGrowUpRecord, Guid> babyGrowUpRecordRepository,
           IRepository<Player, Guid> playerRepository,
           IRepository<Information, Guid> informationRepository,
           IEventGroupAppService eventGroupAppService,
           IRepository<SystemSetting> systemSettingRepository,
           IRepository<BuyFightCountRecord, Guid> buyProductRecordRepository,
           IRepository<BabyFamilyAsset, Guid> babyFamilyAssetRepository,
           IHostingEnvironment hostingEnvironment,
           IBabyFamilyAssetAppService babyFamilyAssetAppService,
           ICompetitionApplicationService competitionService,
           IRepository<DismissFamilyRecord, Guid> dismissFamilyRecordRepository,
           IWeChatPayAppService weChatPayAppService,
           IQCloudApiClient qcloudApiClient,
           IOptions<QcloudConfig> configOptions,
           IOptions<QueryOrderConfig> querOrderOption,
           IBabyPropAppService babyPropAppService,
           IRepository<FamilyWorshipRecord> familyWorshipRecordRepository
            ) : base(entityRepository)
        {
            _entityRepository = entityRepository;
            _babyRepository = babyRepository;
            _professionRepository = professionRepository;
            _playerProfessionRepository = playerProfessionRepository;
            _babyGrowUpRecordRepository = babyGrowUpRecordRepository;
            _playerRepository = playerRepository;
            _eventGroupAppService = eventGroupAppService;
            _informationRepository = informationRepository;
            _systemSettingRepository = systemSettingRepository;

            _buyProductRecordRepository = buyProductRecordRepository;
            _familyWorshipRecordRepository = familyWorshipRecordRepository;
            _hostingEnvironment = hostingEnvironment;
            _babyPropAppService = babyPropAppService;
            _babyFamilyAssetRepository = babyFamilyAssetRepository;
            _babyFamilyAssetAppService = babyFamilyAssetAppService;
            _competitionService = competitionService;
            _dismissFamilyRecordRepository = dismissFamilyRecordRepository;
            _weChatPayAppService = weChatPayAppService;
            _qcouldApiClient = qcloudApiClient;
            _config = configOptions?.Value;
            _queryOrderOption = querOrderOption;
        }

        public async Task<CreateFamilyOutput> CreateFamily(CreateFamilyInput input)
        {
            var response = new CreateFamilyOutput();
            try
            {
                //TODO：判断是否创建已经创建过家庭
                var family = await _entityRepository.GetAllIncluding(s => s.Babies).FirstOrDefaultAsync(s => (s.MotherId == input.PlayerGuid && s.FatherId == input.InviterPlayerGuid) || (s.MotherId == input.InviterPlayerGuid && s.FatherId == input.PlayerGuid));
                if (family != null && family.Babies != null && family.Babies.Count(s => s.IsDeleted == false) > 0)
                {
                    throw new UserFriendlyException((int)BSMPErrorCodes.AlreadyCreateFamily, "邀请人和被邀请人已组建过家庭，不允许重复组建！PS：请到我的宝宝里，点击养育第x胎按钮，来养育新的宝宝！");
                }
                if (family == null)
                {
                    #region //创建家庭
                    // 获取默认职业
                    var iniDeposit = 150000;// professionDefault.Sum(s => s.Salary);
                    var iniHappiness = 100;// professionDefault.Sum(s => s.SatisfactionDegree);
                    // 创建家庭
                    family = new Family()
                    {
                        MotherId = input.InviterFamilyIdentity == FamilyIdentity.Mom ? input.InviterPlayerGuid : input.PlayerGuid,
                        FatherId = input.InviterFamilyIdentity == FamilyIdentity.Dad ? input.InviterPlayerGuid : input.PlayerGuid,
                        Deposit = iniDeposit,
                        Happiness = iniHappiness,
                        Type = 1,
                    };
                    var familyId = await _entityRepository.InsertAndGetIdAsync(family);
                    family.Id = familyId;
                    response.FamilyId = familyId;
                    #endregion

                    #region  //创建父母职业
                    var professionDefault = await _professionRepository.GetAllListAsync(s => s.IsDefault == true);
                    //创建父母职业
                    var fatherProfessionId = professionDefault.FirstOrDefault(s => s.Gender == Gender.Male).Id;
                    var motherProfessionId = professionDefault.FirstOrDefault(s => s.Gender == Gender.Female).Id;
                    //创建父亲的职业
                    await _playerProfessionRepository.InsertAsync(new PlayerProfession
                    {
                        FamilyId = familyId,
                        PlayerId = family.FatherId,
                        ProfessionId = fatherProfessionId,
                        IsCurrent = true
                    });
                    //创建母亲的职业
                    await _playerProfessionRepository.InsertAsync(new PlayerProfession
                    {
                        FamilyId = familyId,
                        PlayerId = family.MotherId,
                        ProfessionId = motherProfessionId,
                        IsCurrent = true,
                    });
                    #endregion
                }
                if (family.Babies == null || family.Babies.Count() == 0)
                {
                    #region // 创建宝宝
                    //创建宝宝
                    var baby = IniBaby(family.Id);
                    var babyId = await _babyRepository.InsertAndGetIdAsync(baby);
                    _babyPropAppService.BuyFreeProps(babyId, family.Id, input.PlayerGuid);
                    response.BabyId = babyId;
                    #endregion

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFamilyOutput> GetFamily(GetFamilyInput input)
        {
            var response = new GetFamilyOutput();
            try
            {
                var family = await _entityRepository
                    .GetAll()
                    .Where(f => f.Id == input.FamilyId)
                    //.Include(f => f.Babies)
                    .Include(f => f.Father)
                    .Include(f => f.Mother)
                    .FirstOrDefaultAsync();
                if (family != null)
                {
                    response.Happiness = family.Happiness;
                    response.HappinessTitle = family.HappinessTitle;
                    response.Deposit = family.Deposit;
                    response.Level = family.Level;//暂时先用此字段
                    response.BabyInfo = new GetFamilyOutputBabyInfo() { AdultBabiesCount = await _babyRepository.CountAsync(s => s.FamilyId == input.FamilyId && s.State == BabyState.Adult) };
                    var professions = await _playerProfessionRepository.GetAll()
                        .Include(p => p.Profession)
                            .ThenInclude(p => p.Reward)
                        .Where(p => p.FamilyId == family.Id && p.IsCurrent)
                        .ToDictionaryAsync(k => k.PlayerId);
                    //dad
                    var dadProfession = professions[family.FatherId]?.Profession;
                    var dadProfessionPropertyAddition = ObjectMapper.Map<PropertyAddition>(dadProfession?.Reward);
                    response.Dad = new Parent
                    {
                        Id = family.FatherId,
                        HeadPicture = family.Father.HeadUrl,
                        Name = family.Father.NickName,
                        ProfessionName = dadProfession.Name,
                        Salary = dadProfession.Salary,
                        ProfessionId = dadProfession.Id,
                        ProfessionPropertyAddition = dadProfessionPropertyAddition
                    };
                    //mom
                    var momProfession = professions[family.MotherId]?.Profession;
                    var momProfessionPropertyAddition = ObjectMapper.Map<PropertyAddition>(momProfession.Reward);
                    response.Mom = new Parent
                    {
                        Id = family.Mother.Id,
                        HeadPicture = family.Mother.HeadUrl,
                        Name = family.Mother.NickName,
                        ProfessionName = momProfession.Name,
                        Salary = momProfession.Salary,
                        ProfessionId = momProfession.Id,
                        ProfessionPropertyAddition = momProfessionPropertyAddition
                    };
                    //系统设置
                    // 系统设置
                    if (input.PlayerGuid != null)
                    {
                        var player = _playerRepository.FirstOrDefault(s => s.Id == input.PlayerGuid);
                        var isIos = player.DeviceSystem.Contains("iOS");
                        var systemSettings = await _systemSettingRepository.GetAllListAsync(s => s.GroupName == "payment");
                        var iOSIsEnabled = isIos ? systemSettings.Any(s => s.Code == 3 && s.Value == "true") : true;
                        var playerDeviceSystem = !iOSIsEnabled ? "暂不支持" : "充值";
                        var systemSetting = new BabySystemSetting()
                        {
                            Payment = new SystemSettingPayment()
                            {
                                IsShow = true,
                                IsEnable = isIos ? systemSettings.Any(s => s.Code == 3 && s.Value == "true") : true,
                                Title = playerDeviceSystem,
                                Energy = new Energy()
                                {
                                    IsShow = isIos ? systemSettings.Any(s => s.Code == 2 && s.Value == "true") : true,
                                    IsEnable = isIos ? systemSettings.Any(s => s.Code == 4 && s.Value == "true") : true,
                                    Message = "十分抱歉，由于相关规范，此功能暂不开放。",
                                    Title = "暂不支持",
                                },
                                GoldCoin = new GoldCoin()
                                {
                                    IsShow = isIos ? systemSettings.Any(s => s.Code == 1 && s.Value == "true") : true,
                                    IsEnable = isIos ? systemSettings.Any(s => s.Code == 3 && s.Value == "true") : true,
                                    Message = "十分抱歉，由于相关规范，此功能暂不开放。",
                                    Title = "暂不支持",
                                }
                            }
                        };
                        response.SystemSetting = systemSetting;
                    }

                }
                else
                {
                    throw new BSMPException(0, "", "家庭不存在！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 获取家庭信息
        /// version 2.0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFamilyOutput2> GetFamily2(GetFamilyInput input)
        {
            var response = new GetFamilyOutput2();
            try
            {
                var family = await _entityRepository
                    .GetAll()
                    .Where(f => f.Id == input.FamilyId)
                    //.Include(f => f.Babies)
                    .Include(f => f.Father)
                    .Include(f => f.Mother)
                    .FirstOrDefaultAsync();
                if (family != null)
                {
                    response.Happiness = family.Happiness;
                    response.HappinessTitle = family.HappinessTitle;
                    response.Deposit = family.Deposit;
                    response.Level = family.Level;//暂时先用此字段
                    response.BabyInfo = new GetFamilyOutputBabyInfo2() { AdultBabiesCount = await _babyRepository.CountAsync(s => s.FamilyId == input.FamilyId && s.State == BabyState.Adult) };

                    //系统设置
                    // 系统设置
                    if (input.PlayerGuid != null)
                    {
                        var player = _playerRepository.FirstOrDefault(s => s.Id == input.PlayerGuid);
                        var isIos = player.DeviceSystem.Contains("iOS");
                        var systemSettings = await _systemSettingRepository.GetAllListAsync(s => s.GroupName == "payment");
                        var iOSIsEnabled = isIos ? systemSettings.Any(s => s.Code == 3 && s.Value == "true") : true;
                        var playerDeviceSystem = !iOSIsEnabled ? "暂不支持" : "充值";
                        var systemSetting = new BabySystemSetting()
                        {
                            Payment = new SystemSettingPayment()
                            {
                                IsShow = true,
                                IsEnable = isIos ? systemSettings.Any(s => s.Code == 3 && s.Value == "true") : true,
                                Title = playerDeviceSystem,
                                Energy = new Energy()
                                {
                                    IsShow = isIos ? systemSettings.Any(s => s.Code == 2 && s.Value == "true") : true,
                                    IsEnable = isIos ? systemSettings.Any(s => s.Code == 4 && s.Value == "true") : true,
                                    Message = "十分抱歉，由于相关规范，此功能暂不开放。",
                                    Title = "暂不支持",
                                },
                                GoldCoin = new GoldCoin()
                                {
                                    IsShow = isIos ? systemSettings.Any(s => s.Code == 1 && s.Value == "true") : true,
                                    IsEnable = isIos ? systemSettings.Any(s => s.Code == 3 && s.Value == "true") : true,
                                    Message = "十分抱歉，由于相关规范，此功能暂不开放。",
                                    Title = "暂不支持",
                                }
                            }
                        };
                        response.SystemSetting = systemSetting;
                    }

                }
                else
                {
                    throw new BSMPException(0, "", "家庭不存在！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 获取家庭基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBabisFamilyInfoOutput> GetBasicFamilyInfo(GetBasicFamilyInput input)
        {
            var output = new GetBabisFamilyInfoOutput();
            var family = await _entityRepository.GetAll().Include(s => s.Father).Include(s => s.Mother).Include(s => s.Babies).FirstOrDefaultAsync(s => s.Id == input.FamilyId);
            output.Family = family;
            return output;
        }
        /// <summary>
        /// 生宝宝（二胎）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BirthNewBabyOutput> BirthNewBaby(BirthNewBabyInput input)
        {
            var response = new BirthNewBabyOutput();
            try
            {
                var family = await _entityRepository.GetAll()
                    .FirstOrDefaultAsync(f => f.Id == input.FamilyId && (f.MotherId == input.PlayerGuid || f.FatherId == input.PlayerGuid));
                if (family != null)
                {
                    //TODO：检测上一个宝宝是否已经成人
                    var lastBaby = await _babyRepository.GetAll()
                        .Where(b => b.FamilyId == input.FamilyId)
                        .OrderByDescending(b => b.BirthOrder)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
                    if (lastBaby?.State == BabyState.Adult)
                    {
                        //二胎家庭声望值清零
                        family.Prestiges = 0;
                        //创建宝宝
                        var baby = IniBaby(family.Id, lastBaby.BirthOrder + 1);
                        var babyIni = baby;
                        //将父母职业 对孩子的属性加成 加到当前宝宝
                        var dict = _playerProfessionRepository.GetAll()
                            .Include(p => p.Profession)
                                .ThenInclude(p => p.Reward)
                            .Where(p => p.FamilyId == family.Id && p.IsCurrent)
                            .ToDictionary(p => p.PlayerId, p => p.Profession);
                        var fatherPlayerProfession = dict[family.FatherId];
                        var motherPlayerProfession = dict[family.MotherId];
                        // 父/母亲的职业 加成
                        baby = InheritParentsProfessionAddition(baby, fatherPlayerProfession.Reward);
                        baby = InheritParentsProfessionAddition(baby, motherPlayerProfession.Reward);
                        baby = await _babyRepository.InsertAsync(baby);
                        var babyId = baby.Id;
                        // 记录父母职业加成记录
                        var inheritMotherProfessionAdditionRecord = InsertBabyGrowUpRecordWhenBirthAsync(baby, motherPlayerProfession.Reward, family.MotherId, TriggerType.InheritParentsProfessionAddition);
                        var inheritFatherProfessionAdditionRecord = InsertBabyGrowUpRecordWhenBirthAsync(baby, fatherPlayerProfession.Reward, family.FatherId, TriggerType.InheritParentsProfessionAddition);
                        // 出生属性记录
                        var birthGrowUpRecord = _babyGrowUpRecordRepository.InsertAsync(new BabyGrowUpRecord()
                        {
                            BabyId = babyId,
                            CreationTime = DateTime.Now,
                            Charm = baby.Charm,
                            Energy = baby.Energy,
                            EmotionQuotient = baby.EmotionQuotient,
                            Healthy = baby.Healthy,
                            Imagine = baby.Imagine,
                            Intelligence = baby.Intelligence,
                            Physique = baby.Physique,
                            WillPower = baby.WillPower,
                            PlayerId = input.PlayerGuid,
                            TriggerType = TriggerType.BirthOwn,
                        });
                        // 设定宝宝的家庭装备的特性加成
                        var assetFeatureAddition = await _babyFamilyAssetAppService.ReCalculateAssetFeatureAddition(new ReCalculateAssetFeatureAdditionInput
                        {
                            IsInheritRequest = true,
                            AssetId = null,
                            BabyId = babyId,
                            FamilyId = input.FamilyId,
                            PropId = null,
                        });
                        // 设定宝宝的家庭装备【属性】加成
                        await _babyFamilyAssetAppService.InheritFamilyAssetPropertyAddion(new InheritFamilyAssetPropertyAddionInput()
                        {
                            BabyId = babyId,
                            FamilyId = input.FamilyId,
                            Baby = baby,
                        });
                        response.BabyId = babyId;
                    }
                    else
                    {
                        //未成年
                        throw new UserFriendlyException((int)BSMPErrorCodes.LastBabyNotYetAdult, "存在尚未成年的宝宝！");
                    }
                }
                else
                {
                    throw new UserFriendlyException((int)BSMPErrorCodes.DontExsitFamily, "家庭不存在或用户不是家庭中的一员！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        internal override IQueryable<Family> GetQuery(GetFamilysInput model)
        {
            return _entityRepository.GetAll();
        }


        /// <summary>
        /// 创建宝宝
        /// </summary>
        /// <returns></returns>
        private Baby IniBaby(int familyId, int birthOrder = 1)
        {
            //创建宝宝
            var _random = new Random();
            string[] hospitals = new string[] { "圣玛丽亚", "圣马力诺", "玛利亚", "康辉", "默奇", "中美", "惠林顿" };
            var baby = new Baby
            {

                Intelligence = _random.Next(1, 11),
                Physique = _random.Next(1, 11),
                Imagine = _random.Next(1, 11),
                WillPower = _random.Next(1, 11),
                EmotionQuotient = _random.Next(1, 11),
                Charm = _random.Next(1, 11),
            };

            var list = new List<int>
            {
                baby.Intelligence,
                baby.Physique,
                baby.Imagine,
                baby.WillPower,
                baby.EmotionQuotient,
                baby.Charm
            };

            if (list.Count(v => v >= 10) > 2 || list.Sum() > 40)
            {
                return IniBaby(familyId, birthOrder);
            }
            //基本信息
            baby.FamilyId = familyId;
            baby.State = BabyState.UnderAge;
            baby.BirthLength = _random.NextDouble(46, 58);
            baby.BirthWeight = _random.NextDouble(5, 9);
            baby.BirthHospital = hospitals[_random.Next(0, 7)];
            baby.Potential = 5000;
            baby.Gender = (Gender)_random.Next(1, 3);
            baby.BirthOrder = birthOrder;
            baby.Healthy = 100;
            baby.Energy = 100;
            var eventGroup = _eventGroupAppService.GetInitGroup();
            baby.GroupId = eventGroup?.Value.Id;
            return baby;
        }

        public async Task<GetFamilyInfoOutput> GetFamilyInfo(GetFamilyInfoInput input)
        {
            //var entity = await _repository.GetAllIncluding(f => f.Father, m => m.Mother).FirstOrDefaultAsync(f => f.Id == input.FamilyId);

            var output = new GetFamilyInfoOutput();

            var entity = await _repository.GetAllIncluding(f => f.Father, b => b.Babies).FirstOrDefaultAsync(f => f.Id == input.FamilyId);

            var baby = entity.Babies.FirstOrDefault(f => f.State == BabyState.UnderAge);

            output.Baby = baby.MapTo(output.Baby);

            output.ChargeAmount = entity.ChargeAmount;

            output.Deposit = entity.Deposit;

            output.Father.HeadUrl = entity.Father.HeadUrl;

            return output;

        }
        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFamilyStateOutput> GetFamilyState(GetFamilyStateInput input)
        {
            var response = new GetFamilyStateOutput();
            try
            {
                var family = await _entityRepository.GetAllIncluding(s => s.Babies).FirstOrDefaultAsync(s => (s.MotherId == input.MotherId && s.FatherId == input.FatherId));
                var familyCreated = new GetFamilyStateOutputFamilyStateInfo();
                response.isCreatedFamily = family != null;
                if (family != null)
                {

                    if (family.Babies != null)
                    {
                        familyCreated.FamilyId = familyCreated.FamilyId;
                        familyCreated.BabyCount = family.Babies.Count;
                    }
                    response.FamilyStateInfo = familyCreated;
                    if (family.Babies.Count > 0)
                    {
                        var nowBaby = new GetFamilyStateOutputFamilyStateInfoNowBaby();
                        var baby = family.Babies.LastOrDefault();
                        nowBaby.State = baby.State;
                        nowBaby.BabyId = baby.Id;
                        response.FamilyStateInfo.LastBaby = nowBaby;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AbpException() { Source = ex.Message };
            }
            return response;
        }

        public async Task<PagedResultDto<GetFamiliesWithPlayerIdOutput>> GetFamiliesWithPlayerId(GetFamiliesWithPlayerIdInput input)
        {
            var query = _repository.GetAllIncluding(f => f.Father, m => m.Mother, b => b.Babies)
                .Where(p => p.FatherId == input.PlayerId || p.MotherId == input.PlayerId)
                .WhereIf(input.BabyAge.HasValue, a => a.Babies.Any(b => b.Age >= input.BabyAge && b.State == BabyState.UnderAge))
                .WhereIf(!string.IsNullOrEmpty(input.BabyName), n => n.Babies.Any(b => b.Name == input.BabyName))
                .WhereIf(input.MinDeposit.HasValue && input.MaxDeposit.HasValue, d => d.Deposit > input.MinDeposit && d.Deposit < input.MaxDeposit)
                .WhereIf(input.MaxDeposit.HasValue && !input.MinDeposit.HasValue, d => d.Deposit < input.MaxDeposit)
                .WhereIf(!input.MaxDeposit.HasValue && input.MinDeposit.HasValue, d => d.Deposit > input.MinDeposit)
                .WhereIf(input.AddOnState.HasValue, s => s.AddOnStatus == input.AddOnState);

            var count = query.Count();

            var entityList = await query
                    .PageBy(input)
                    .ToListAsync();

            var familyIds = query.Select(x => x.Id);

            var playerProfession = _playerProfessionRepository.GetAll().Include(p => p.Profession).Where(p => familyIds.Contains(p.FamilyId) && p.IsCurrent == true);

            List<GetFamiliesWithPlayerIdOutput> entityListDtos = new List<GetFamiliesWithPlayerIdOutput>();

            foreach (var item in entityList)
            {
                GetFamiliesWithPlayerIdOutput output = new GetFamiliesWithPlayerIdOutput();

                if (input.PlayerId == item.FatherId)
                {
                    item.Mother.MapTo(output.Other);
                }
                else
                {
                    item.Father.MapTo(output.Other);
                }

                output.MonthlyIncome = await playerProfession.Where(p => p.FamilyId == item.Id).Select(c => c.Profession.Salary).SumAsync();

                var baby = item.Babies.FirstOrDefault(b => b.State == BabyState.UnderAge);

                baby.MapTo(output.Baby);

                output.ChargeAmount = item.ChargeAmount;
                output.Deposit = item.Deposit;

                output.AddOnStatus = item.AddOnStatus;

                output.FamilyId = item.Id;

                output.Remark = item.Remark;

                if (output.Deposit <= 300000)
                {
                    output.Level = "贫困";
                }
                else if (output.Deposit >= 800000 && output.Deposit <= 3000000)
                {
                    output.Level = "小康";
                }
                else if (output.Deposit >= 4500000)
                {
                    output.Level = "富豪";
                }
                else
                {
                    output.Level = "空挡";
                }

                entityListDtos.Add(output);
            }

            return new PagedResultDto<GetFamiliesWithPlayerIdOutput>(count, entityListDtos);

        }

        public async Task<PagedResultDto<AgentFamilyOutput>> GetAgentFamilies(AgentFamilyInput input)
        {
            var query = _repository.GetAllIncluding(f => f.Babies, f => f.Father, f => f.Mother)
                .WhereIf(input.PlayerId.HasValue, f => f.FatherId == input.PlayerId || f.MotherId == input.PlayerId)
                .WhereIf(input.MinAge.HasValue, f => f.Babies.Any(b => b.Age >= input.MinAge && b.State == BabyState.UnderAge))
                .WhereIf(input.MaxAge.HasValue, f => f.Babies.Any(b => b.Age <= input.MaxAge && b.State == BabyState.UnderAge))
                .WhereIf(!input.NickName.IsNullOrEmpty(),
                    f => f.FatherId == input.PlayerId ?
                        f.Mother.NickName.Contains(input.NickName) :
                        f.Father.NickName.Contains(input.NickName))
                .WhereIf(input.Status.HasValue, f => f.AddOnStatus == input.Status)
                .WhereIf(input.MinMoney.HasValue, f => f.Deposit >= input.MinMoney)
                .WhereIf(input.MaxMoney.HasValue, f => f.Deposit <= input.MaxMoney);

            if (!input.Status.HasValue)
            {
                query = query.Where(f => f.AddOnStatus != AddOnStatus.Hide);
            }

            if (input.PageSize > 0)
            {
                input.MaxResultCount = input.PageSize;
            }
            if (input.PageIndex > 0)
            {
                input.SkipCount = (input.PageIndex - 1) * input.PageSize;
            }

            var count = await query.CountAsync();

            var result = query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToList()
                    .Select(s => new AgentFamilyOutput
                    {
                        AddOnStatus = s.AddOnStatus,
                        BabyName = s.Baby?.Name,
                        Level = s.FamilyLevel.GetDescription(),
                        Note = s.AddOnNote,
                        OtherHeader = input.PlayerId != s.FatherId ? s.Father?.HeadUrl : s.Mother?.HeadUrl,
                        OtherName = input.PlayerId != s.FatherId ? s.Father?.NickName : s.Mother?.NickName,
                        RealAmount = s.ChargeAmount,
                        VirtualAmount = s.VirtualRecharge,
                        Status = s.AddOnStatus.GetDescription(),
                        Id = s.Id,
                        Deposit = s.Deposit,
                        BabyAge = s.Baby?.Age,
                        BabyId = s.Baby?.Id,
                        AgeString = s.Baby?.AgeString
                    })
                    .ToList();

            return new PagedResultDto<AgentFamilyOutput>
            {
                Items = result,
                TotalCount = count
            };
        }


        public async Task<PagedResultDto<GetAllFamilysListDto>> GetAllFamilys(GetAllFamilysInput input)
        {
            var query = SearchFamilies(input);
            var count = query.Count();


            var entityList = await query
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetAllFamilysListDto>>();

            return new PagedResultDto<GetAllFamilysListDto>(count, entityListDtos);

        }

        public IQueryable<Family> SearchFamilies(GetAllFamilysInput input)
        {
            var maxRecharge = 0;
            var minRecharge = 0;

            if (input.RechargeRange != RechargeRange.All)
            {
                if (input.RechargeRange == RechargeRange.FirstRange)
                {
                    maxRecharge = 100;
                }
                else if (input.RechargeRange == RechargeRange.SecondRange)
                {
                    minRecharge = 100;
                    maxRecharge = 500;
                }
                else if (input.RechargeRange == RechargeRange.ThirdRange)
                {
                    minRecharge = 500;

                    maxRecharge = 1000;
                }
                else if (input.RechargeRange == RechargeRange.FourthRange)
                {
                    minRecharge = 1000;
                    maxRecharge = 999999999;
                }
            }

            //var teantId = AbpSession.TenantId ?? 295;  //养娃
            //   var TenantIds = input.TenantIds.Split(',').ToList();
            var query = _repository.GetAllIncluding(f => f.Father, m => m.Mother, b => b.Babies)
                .Where(f => f.CreationTime >= input.StartTime && f.CreationTime <= input.EndTime && input.TenantIds.Contains(f.Father.TenantId) && input.TenantIds.Contains(f.Mother.TenantId))
                .WhereIf(!string.IsNullOrEmpty(input.BabyName), b => b.Babies.Any(baby => baby.Name.Contains(input.BabyName)))
                .WhereIf(!string.IsNullOrEmpty(input.FatherName), f => f.Father.NickName.Contains(input.FatherName))
                .WhereIf(!string.IsNullOrEmpty(input.MotherName), f => f.Mother.NickName.Contains(input.MotherName))
                .WhereIf(input.FamilyLevel != FamilyLevel.All, l => l.FamilyLevel == input.FamilyLevel)
                .WhereIf(maxRecharge != 0 && minRecharge != 0, f => f.ChargeAmount > minRecharge && f.ChargeAmount < maxRecharge);
            //.WhereIf(input.RechargeRange != RechargeRange.All, f => f.ChargeAmount == 0);
            if (input.Orders != null && input.Orders.Length > 0)
            {
                var desType = input.Orders.FirstOrDefault(f => f.FieldType == FamilyModelFieldType.Deposit);
                var rcgType = input.Orders.FirstOrDefault(f => f.FieldType == FamilyModelFieldType.Recharge);
                if (desType != null && rcgType != null)
                {
                    if (desType.OrderType == OrderType.ASC && rcgType.OrderType == OrderType.ASC)
                    {
                        query = query.OrderBy(s => s.Deposit).ThenBy(s => s.ChargeAmount);
                    }
                    else if (desType.OrderType == OrderType.DESC && rcgType.OrderType == OrderType.DESC)
                    {
                        query = query.OrderByDescending(s => s.Deposit).ThenByDescending(s => s.ChargeAmount);
                    }
                    else if (desType.OrderType == OrderType.ASC && rcgType.OrderType == OrderType.DESC)
                    {
                        query = query.OrderBy(s => s.Deposit).ThenByDescending(s => s.ChargeAmount);
                    }
                    else if (desType.OrderType == OrderType.DESC && rcgType.OrderType == OrderType.ASC)
                    {
                        query = query.OrderByDescending(s => s.Deposit).ThenBy(s => s.ChargeAmount);
                    }
                }
                else if (desType != null)
                {
                    if (desType.OrderType == OrderType.ASC)
                    {
                        query = query.OrderBy(s => s.Deposit);
                    }
                    else
                    {
                        query = query.OrderByDescending(s => s.Deposit);
                    }
                }
                else if (rcgType != null)
                {
                    if (rcgType.OrderType == OrderType.ASC)
                    {
                        query = query.OrderBy(s => s.ChargeAmount);
                    }
                    else
                    {
                        query = query.OrderByDescending(s => s.ChargeAmount);
                    }
                }

            }

            return query;
        }

        public async Task UpdateAgentState(UpdateAgentStateInput input)
        {
            var find = await _repository.FirstOrDefaultAsync(f => f.Id == input.FamilyId);
            if (find == null)
            {
                throw new AbpException("未找到对应家庭");
            }

            if (input.Status.HasValue)
            {
                find.AddOnStatus = input.Status.Value;
                if (input.Status.Value == AddOnStatus.Hide)
                {
                    find.IsShow = !input.Status.HasValue;
                }
            }

            if (!input.Note.IsNullOrEmpty())
            {
                find.AddOnNote = input.Note;
            }
            await _repository.UpdateAsync(find);
        }

        public async Task<RemarkFamilyOutput> RemarkFamily(RemarkFamilyInput input)
        {
            var family = await _repository.GetAsync(input.FamilyId);

            family.Remark = input.Remark;

            await _repository.UpdateAsync(family);

            return new RemarkFamilyOutput()
            {
                IsSuccess = true
            };
        }
        /// <summary>
        /// 定时更新家庭级别
        /// </summary>
        public Task<CrontabUpdateFamilyLevelOutput> CrontabUpdateFamilyLevel(CrontabUpdateFamilyLevelInput input)
        {
            //if (IsAutoRun&&!input.IsReset)
            //{
            //    return null;
            //}
            if (input == null)
            {
                input = new CrontabUpdateFamilyLevelInput()
                {
                    CronExpression = "0 21,08,12,18 * * * "
                };
            }
            //RecurringJob.AddOrUpdate<FamilyAppService>((s) => s.UpdateFamilyLevel(), input.CronExpression, TimeZoneInfo.Local, "crontab_update_family_level_900001");
            RecurringJob.AddOrUpdate<FamilyAppService>((s) => s.UpdateFamilyLevel(), input.CronExpression);
            //IsAutoRun = true;
            return Task.FromResult(new CrontabUpdateFamilyLevelOutput());
        }
        public void UpdateFamilyLevel()
        {
            _entityRepository.GetAllList().ForEach(async (s) =>
            {
                if (s.Deposit <= 600000)
                {
                    s.Level = FamilyLevel.Poor;
                }
                else if (s.Deposit <= 3000000)
                {
                    s.Level = FamilyLevel.WellOff;
                }
                else
                {
                    s.Level = FamilyLevel.Rich;
                }
                await _entityRepository.UpdateAsync(s);
            });
        }

        private const string DISMISSFAMILYMSG = "您与{0}于{1}组建家庭，共养育了{2}个儿女";
        /// <summary>
        /// 获取家庭其他宝宝列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFamilyElseBabiesByPageOutput> GetFamilyElseBabiesByPage(GetFamilyElseBabiesByPageInput input)
        {
            var response = new GetFamilyElseBabiesByPageOutput();
            var familyBabies = new List<GetFamilyElseBabiesByPageOutputBaby>();
            try
            {
                Family family = null;
                var record = await _dismissFamilyRecordRepository
                    .GetAll()
                    .Include(r => r.Family).ThenInclude(b => b.Babies)
                    .Include(r => r.Family).ThenInclude(b => b.Father)
                    .Include(r => r.Family).ThenInclude(b => b.Mother)
                    .FirstOrDefaultAsync(c => c.FamilyId == input.FamilyId && c.FamilyState == FamilyState.Dismissing);
                if (record != null)
                {
                    if (record.ExpireTime >= DateTime.UtcNow)
                    {
                        response.InitiatorId = record.InitiatorId;
                        response.ExpireTime = record.ExpireTime;
                    }
                    else
                    {

                        record.FamilyState = FamilyState.Normal;
                        if (record.Family != null)
                        {
                            family = record.Family;
                            family.FamilyState = FamilyState.Normal;
                            await _dismissFamilyRecordRepository.UpdateAsync(record);

                            var otherId = input.PlayerGuid == family.FatherId ? family.MotherId : family.FatherId;
                            var selfMsg = input.PlayerGuid == family.FatherId ? "妈妈" : "爸爸";
                            var otherMsg = input.PlayerGuid == family.FatherId ? "爸爸" : "妈妈";
                            var babyName = family.Babies.LastOrDefault().Name;
                            if (babyName == null)
                            {
                                babyName = "未起名";
                            }
                            var dismissFamilyConfigStr = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1004);
                            var dismissFamilyConfig = JsonConvert.DeserializeObject<DismissFamilyConfig>(dismissFamilyConfigStr.Value);
                            //插入系统消息(自己)
                            await _informationRepository.InsertAsync(new Information()
                            {
                                Content = string.Format(AUTO_CANCEL_SELF_MESSAGE, babyName, selfMsg, dismissFamilyConfig.Validate),
                                FamilyId = family.Id,
                                Type = InformationType.System,
                                SenderId = input.PlayerGuid,
                                ReceiverId = input.PlayerGuid,
                                SystemInformationType = SystemInformationType.BigBag
                            });
                            await _informationRepository.InsertAsync(new Information()
                            {
                                Content = string.Format(AUTO_CANCEL_OTHER_MESSAGE, dismissFamilyConfig.Validate, otherMsg),
                                FamilyId = family.Id,
                                Type = InformationType.System,
                                SystemInformationType = SystemInformationType.BigBag,
                                SenderId = input.PlayerGuid,
                                ReceiverId = otherId
                            });
                        }
                    }
                }
                if (family == null)
                {
                    family = await _entityRepository.GetAllIncluding(b => b.Babies, f => f.Father, m => m.Mother)
                      .FirstOrDefaultAsync(s => s.Id == input.FamilyId);
                }
                var babies = family.Babies
                    .Where(s => s.Id != input.BabyId)
                      .OrderByDescending(s => s.Id).ToList();

                if (input.PageSize > 0)
                {
                    input.MaxResultCount = input.PageSize;
                }
                if (input.PageIndex > 0)
                {
                    input.SkipCount = (input.PageIndex - 1) * input.PageSize;
                }
                var result = babies.AsQueryable().PageBy(input).ToList();
                var babyCount = babies.Count;

                if (result != null && result.Any())
                {
                    foreach (var item in result)
                    {
                        var baby = ObjectMapper.Map<GetFamilyElseBabiesByPageOutputBaby>(item);
                        baby.BabyProperty = ObjectMapper.Map<GetFamilyElseBabiesByPageOutputBabyBabyProperty>(item);
                        var babyEntity = await _babyRepository.GetAllIncluding(s => s.BabyEnding).FirstOrDefaultAsync(s => s.Id == item.Id);
                        baby.StroyEnding = ObjectMapper.Map<GetFamilyElseBabiesByPageOutputBabyBabyStory>(babyEntity.BabyEnding); ;
                        familyBabies.Add(baby);
                    }
                }
                response.Babies = new PagedResultDto<GetFamilyElseBabiesByPageOutputBaby>(babyCount, familyBabies);

                var creationTime = family.CreationTime;
                var babiesCount = family.Babies.Count;
                var otherPlayer = family.FatherId == input.PlayerGuid ? family.Mother : family.Father;
                response.Content = string.Format(DISMISSFAMILYMSG, otherPlayer.NickName, $"{creationTime.Year}年{creationTime.Month}月{creationTime.Day}日", babiesCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 继承父母的职业对宝宝的属性加成
        /// </summary>
        /// <returns></returns>
        private Baby InheritParentsProfessionAddition(Baby baby, Reward reward)
        {
            //var baby =  _babyRepository.Get(babyId);
            if (baby != null)
            {
                baby.Charm += reward.Charm;
                baby.Energy += reward.Energy;
                baby.EmotionQuotient += reward.EmotionQuotient;
                baby.Healthy += reward.Healthy;
                baby.Imagine += reward.Imagine;
                baby.Intelligence += reward.Intelligence;
                baby.Physique += reward.Physique;
                baby.WillPower += reward.WillPower;
                return baby;
            }
            return null;
        }
        /// <summary>
        /// 增加宝宝成长记录（出生时）
        /// </summary>
        /// <returns></returns>
        private async Task InsertBabyGrowUpRecordWhenBirthAsync(Baby baby, Reward reward, Guid playerGuid, TriggerType triggerType)
        {
            //更新宝宝成长记录
            await _babyGrowUpRecordRepository.InsertAsync(new BabyGrowUpRecord()
            {
                BabyId = baby.Id,
                CreationTime = DateTime.Now,
                Charm = reward.Charm,
                Energy = reward.Energy,
                EmotionQuotient = reward.EmotionQuotient,
                Healthy = reward.Healthy,
                Imagine = reward.Imagine,
                Intelligence = reward.Intelligence,
                Physique = reward.Physique,
                WillPower = reward.WillPower,
                PlayerId = playerGuid,
                TriggerType = triggerType,
            });
        }

        /// <summary>
        /// 获取父亲/母亲详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetParentDetailOutput> GetParentDetail(GetParentDetailInput input)
        {
            var response = new GetParentDetailOutput();
            try
            {
                var family = await _entityRepository
                  .GetAll()
                  .Where(f => f.Id == input.FamilyId && (f.FatherId == input.PlayerGuid || f.MotherId == input.PlayerGuid))
                  .Include(f => f.Father)
                  .Include(f => f.Mother)
                  .FirstOrDefaultAsync();
                if (family != null)
                {
                    var professionList = await _playerProfessionRepository.GetAll()
                    .Include(p => p.Profession)
                        .ThenInclude(p => p.Reward)
                    .Where(p => p.FamilyId == family.Id && p.IsCurrent)
                    .ToListAsync();
                    var professions = professionList
                    .DistinctBy(s => s.PlayerId)
                    .ToDictionary(k => k.PlayerId);
                    if (input.Kinship == "dad")
                    {
                        var dadProfession = professions[family.FatherId]?.Profession;
                        var dadProfessionPropertyAddition = ObjectMapper.Map<GetParentDetailOutputBasicInfoPropertyAddition>(dadProfession?.Reward);
                        response = new GetParentDetailOutput()
                        {
                            BasicInfo = new GetParentDetailOutputBasicInfo()
                            {
                                Id = family.FatherId,
                                HeadPicture = family.Father.HeadUrl,
                                Name = family.Father.NickName,
                            },
                            ProfessionInfo = new GetParentDetailOutputProfessionInfo()
                            {
                                ProfessionName = dadProfession.Name,
                                Salary = dadProfession.Salary,
                                ProfessionId = dadProfession.Id,
                                ProfessionPropertyAddition = dadProfessionPropertyAddition
                            }
                        };
                    }
                    else if (input.Kinship == "mom")
                    {
                        var momProfession = professions[family.MotherId]?.Profession;
                        var momProfessionPropertyAddition = ObjectMapper.Map<GetParentDetailOutputBasicInfoPropertyAddition>(momProfession.Reward);
                        response = new GetParentDetailOutput()
                        {
                            BasicInfo = new GetParentDetailOutputBasicInfo()
                            {
                                Id = family.MotherId,
                                HeadPicture = family.Mother.HeadUrl,
                                Name = family.Mother.NickName,
                            },
                            ProfessionInfo = new GetParentDetailOutputProfessionInfo()
                            {
                                ProfessionName = momProfession.Name,
                                Salary = momProfession.Salary,
                                ProfessionId = momProfession.Id,
                                ProfessionPropertyAddition = momProfessionPropertyAddition
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.InnerException.ToString());
                throw new UserFriendlyException("请求出错", ex.Message);
            }
            return response;
        }

        public async Task<GetOtherFamilyInfoOutput> GetOtherFamilyInfo(GetOtherFamilyInfoInput input)
        {
            int currentBabyId = -1;
            var _isWorshipedlist = !string.IsNullOrWhiteSpace(input.NowBabyId) && int.TryParse(input.NowBabyId, out currentBabyId);
            if (!input.BabyId.HasValue)
            {
                throw new Exception("参数不能为空");
            }

            var otherFamily = await _entityRepository
                .GetAllIncluding(f => f.Father, m => m.Mother, b => b.Babies)
                .FirstOrDefaultAsync(f => f.Id == input.FamilyId);

            var output = otherFamily.MapTo<GetOtherFamilyInfoOutput>();
            //添加声望值，膜拜功能
            if (_isWorshipedlist)
            {
                var _familyWorshipRecordRepo = IocManager.Instance.Resolve<IRepository<Prestiges.FamilyWorshipRecord>>();
                var _countToday = _familyWorshipRecordRepo.GetAll().Count(f => f.ToBabyId == otherFamily.LatestBaby.Id && f.CreationTime.Date == System.DateTime.Now.Date);
                var _setRepo = IocManager.Instance.Resolve<IRepository<SystemSetting>>();
                if (!int.TryParse(_setRepo.FirstOrDefault(w => w.Name.Equals("WorshipedTimesMax"))?.Value, out int _worshipedTimesMax)) throw new Abp.UI.UserFriendlyException("系统参数[WorshipedTimesMax]配置格式错误");
                if (!int.TryParse(_setRepo.FirstOrDefault(w => w.Name.Equals("ToWorshipTimesMax"))?.Value, out int _toWorshipTimesMax)) throw new Abp.UI.UserFriendlyException("系统参数[ToWorshipTimesMax]配置格式错误");
                var _currenBaby = _babyRepository.FirstOrDefault(s => s.Id == currentBabyId);
                output.TimesToday = _countToday;
                output.Maxlimit = _worshipedTimesMax;
                output.IsLimited = _toWorshipTimesMax == _familyWorshipRecordRepo.Count(s => s.FromBabyId == _currenBaby.Id && s.CreationTime.Date == DateTime.Now.Date); //|| _countToday == _worshipedTimesMax
                output.IsWorshiped = _familyWorshipRecordRepo.GetAll().Any(s => s.ToBabyId == input.BabyId && s.FromBabyId == _currenBaby.Id && s.CreationTime.Date == System.DateTime.Now.Date);
                output.ToWorshipMax = _toWorshipTimesMax;

            }
            //var houseAndSkinAssets =  _babyFamilyAssetRepository.GetAllIncluding()
            //       .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropType)
            //       .Where(s => s.FamilyId == otherFamily.Id && (s.BabyProp.BabyPropType.Name == "House"|| s.BabyProp.BabyPropType.Name== "Skin") && s.IsEquipmenting && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null)).ToList();
            //var houseAsset = houseAndSkinAssets.FirstOrDefault(h => h.BabyProp.BabyPropType.Name == "House");
            //var skinAsset = houseAndSkinAssets.FirstOrDefault(s => s.BabyProp.BabyPropType.Name == "Skin");

            var result = _competitionService.IsEquipmentProp(input.BabyId.Value, input.FamilyId);
            output.Level = result.HouseCode;
            output.IsHasButler = result.IsHasButler;
            output.IsHasCar = result.IsHasCar;
            output.IsHasHouse = result.IsHasHouse;
            output.IsHasServant = result.IsHasServant;
            output.IsHasSkip = result.IsHasSkip;
            var currentBaby = otherFamily.Babies.FirstOrDefault(b => b.State == BabyState.UnderAge);

            currentBaby.MapTo(output.LatestBaby);

            output.LatestBaby.DanGrading = await _competitionService.GetPlayerDanGride(input.BabyId.Value, input.FamilyId);
            output.LatestBaby.Skin = result.SkinCode;
            return output;
        }


        private const string SYSTEM_MESSAGE_FORMAT = "";
        public async Task UpgradeVersionGiveCoin()
        {
            var families = await _repository.GetAll().ToListAsync();

            var content = string.Format(SYSTEM_MESSAGE_FORMAT);

            families.ForEach(async family =>
            {
                await _informationRepository.InsertAsync(new Information()
                {
                    Content = content,
                    FamilyId = family.Id,
                    Type = InformationType.System,
                    SenderId = family.FatherId
                });
            });
        }

        //private void AddInformations()
        //{
        //    _informationRepository.InsertAsync(new Information()
        //    {
        //        ReceiverId = 
        //    });
        //}
        private const string DISMISS_SELF_MESSAGE = "您已发起解散家庭，等待孩子{0}处理。";
        private const string DISMISS_OTHER_MESSAGE = "孩子{0}发起解散家庭，等待您的处理。";
        private const string CANCEL_SELF_MESSAGE = "您已取消解散家庭。";
        private const string CANCEL_OTHER_MESSAGE = "孩子{0}已取消解散家庭。";
        private const string AUTO_CANCEL_SELF_MESSAGE = "{0}的{1}在{2}小时内没有处理您解散家庭的请求，系统已自动取消了解散家庭请求。";
        private const string AUTO_CANCEL_OTHER_MESSAGE = "您在{0}小时内没有处理孩子{1}的解散家庭请求，系统已自动取消了解散家庭的请求。";
        private const string REFUSE_SELF_MESSAGE = "您已拒绝解散家庭。";
        private const string REFUSE_OTHER_MESSAGE = "孩子{0}已拒绝解散家庭。";
        public async Task<RequestDismissFamilyOutput> RequestDismissFamily(RequestDismissFamilyInput input)
        {

            var output = new RequestDismissFamilyOutput();
            var family = await _entityRepository.FirstOrDefaultAsync(f => f.Id == input.FamilyId);
            if (family == null)
            {
                throw new Exception("家庭不存在");
            }
            var dismissFamilyConfigStr = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1004);
            var dismissFamilyConfig = JsonConvert.DeserializeObject<DismissFamilyConfig>(dismissFamilyConfigStr.Value);
            var isRecord = _dismissFamilyRecordRepository.GetAll()
                .Any(c => c.FamilyId == input.FamilyId && c.DismissFamilyType == DismissFamilyType.AgreementDismiss && c.CreationTime.Date == DateTime.Now.Date);
            if (isRecord && input.DismissType == DismissFamilyType.AgreementDismiss)
            {
                throw new Exception("每天只能发起一次解散家庭");
            }

            switch (family.FamilyState)
            {
                case FamilyState.Normal:
                    {
                        if (input.DismissType == DismissFamilyType.ForceDismiss)
                        {
                            var player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerGuid);
                            //1、创建订单
                            var payResult = await _weChatPayAppService.Pay(new SendPaymentRquestInput()
                            {
                                TenantId = player.TenantId,
                                OpenId = player.OpenId,
                                PlayerId = input.PlayerGuid,
                                ClientType = ClientType.MinProgram,
                                Totalfee = dismissFamilyConfig.PayRMB,
                                GoodsType = GoodsType.DismissFamily,
                                FamilyId = input.FamilyId,
                            });
                            output.PayOutput = payResult;
                        }
                        else
                        {
                            family.FamilyState = FamilyState.Dismissing;
                            await _entityRepository.UpdateAsync(family);
                            //3、添加记录
                            var expireDate = DateTime.UtcNow.AddHours(dismissFamilyConfig.Validate);
                            await AddDismissFamilyRecord(new DismissFamilyModel()
                            {
                                FamilyId = input.FamilyId,
                                InitiatorId = input.PlayerGuid,
                                DismissFamilyType = DismissFamilyType.AgreementDismiss,
                                FamilyState = FamilyState.Dismissing,
                                ExpireTime = expireDate
                            });
                            //弹板消息
                            await _informationRepository.InsertAsync(new Information()
                            {
                                Content = expireDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                SenderId = input.PlayerGuid,
                                ReceiverId = input.PlayerGuid == family.FatherId ? family.MotherId : family.FatherId,
                                State = InformationState.Create,
                                SystemInformationType = SystemInformationType.DismissFamily,
                                Type = InformationType.DismissFamily,
                                NoticeType = NoticeType.Popout,
                                FamilyId = input.FamilyId
                            });
                            var self = input.PlayerGuid == family.FatherId ? "爸爸" : "妈妈";
                            var other = input.PlayerGuid == family.FatherId ? "妈妈" : "爸爸";
                            //对方
                            await _informationRepository.InsertAsync(new Information()
                            {
                                Content = string.Format(DISMISS_OTHER_MESSAGE, self),
                                SenderId = input.PlayerGuid,
                                ReceiverId = input.PlayerGuid == family.FatherId ? family.MotherId : family.FatherId,
                                State = InformationState.Create,
                                Type = InformationType.Event,
                                FamilyId = input.FamilyId
                            });
                            //自己
                            await _informationRepository.InsertAsync(new Information()
                            {
                                Content = string.Format(DISMISS_SELF_MESSAGE, other),
                                SenderId = input.PlayerGuid,
                                ReceiverId = input.PlayerGuid,
                                State = InformationState.Create,
                                Type = InformationType.Event,
                                FamilyId = input.FamilyId
                            });
                        }
                    }
                    break;
                case FamilyState.Dismissed:
                    throw new Exception("该家庭已经解散");
                case FamilyState.Dismissing:
                    throw new Exception("已发起解散家庭，不要重复发起");
                default:
                    break;
            }

            return output;
        }

        public async Task<ConfirmDismissFamilyOutput> ConfirmDismissFamily(ConfirmDismissFamilyInput input)
        {
            var family = await _entityRepository.FirstOrDefaultAsync(input.FamilyId).CheckNull("家庭不存在，数据错误");
            if (family.FamilyState == FamilyState.CancelDismiss)
            {
                throw new Exception("对方与取消解散家庭");
            }
            var initatorId = input.PlayerGuid == family.FatherId ? family.MotherId : family.FatherId;
            var record = await _dismissFamilyRecordRepository.GetAll()
                .FirstOrDefaultAsync(r => r.InitiatorId == initatorId && r.FamilyId == input.FamilyId && r.FamilyState == FamilyState.Dismissing);
            if (record == null)
            {
                throw new Exception("解散家庭失败，请稍后重试");
            }

            family.FamilyState = FamilyState.Dismissed;
            family.IsDeleted = record != null;
            await _entityRepository.UpdateAsync(family);
            Logger.Warn($"家庭Id为{family.Id}的家庭已被{input.PlayerGuid}确认解散");
            record.FamilyState = FamilyState.Dismissed;
            await _dismissFamilyRecordRepository.UpdateAsync(record);
            //var content = "已成功解散家庭";
            //await _informationRepository.InsertAsync(new Information()
            //{
            //    Content = content,
            //    SenderId = input.PlayerGuid,
            //    ReceiverId = input.PlayerGuid == family.FatherId ? family.MotherId : family.FatherId,
            //    State = InformationState.Create,
            //    SystemInformationType = SystemInformationType.DismissFamilySuccess,
            //    Type = InformationType.DismissFamily,
            //    NoticeType = NoticeType.Popout,
            //    FamilyId = input.FamilyId
            //});
            return new ConfirmDismissFamilyOutput() { IsSuccess = true };
        }

        private async Task AddDismissFamilyRecord(DismissFamilyModel model)
        {
            await _dismissFamilyRecordRepository.InsertAsync(new DismissFamilyRecord()
            {
                InitiatorId = model.InitiatorId,
                FamilyId = model.FamilyId,
                FamilyState = model.FamilyState,
                DismissFamilyType = model.DismissFamilyType,
                OrderId = model.OrderId,
                ExpireTime = model.ExpireTime
            });
        }

        public async Task ForceDismissFamilySuccess(UpdateOrderStateInput input)
        {
            var family = await _entityRepository.FirstOrDefaultAsync(input.FamilyId).CheckNull("家庭数据错误，请稍后重试!");
            family.IsDeleted = family != null;
            family.FamilyState = FamilyState.Dismissed;
            await _entityRepository.UpdateAsync(family);

            await AddDismissFamilyRecord(new DismissFamilyModel()
            {
                DismissFamilyType = DismissFamilyType.ForceDismiss,
                FamilyState = FamilyState.Dismissed,
                OrderId = input.OrderId,
                FamilyId = input.FamilyId,
                InitiatorId = input.PlayerId.HasValue ? input.PlayerId.Value : Guid.Empty
            });

            //var records = _dismissFamilyRecordRepository.GetAll()
            //    .Where(r => r.FamilyId == input.FamilyId)
            //    .CheckNull("记录不存在");
            //var record = (await records.FirstOrDefaultAsync(r => r.FamilyState == FamilyState.Dismissing))
            //    .CheckNull("未找到解散中的家庭，请稍后重试");
            //await _informationRepository.InsertAsync(new Information()
            //{
            //    Content = "已成功解散家庭",
            //    SenderId = input.PlayerId,
            //    ReceiverId = input.PlayerId == family.FatherId ? family.MotherId : family.FatherId,
            //    State = InformationState.Create,
            //    SystemInformationType = SystemInformationType.DismissFamilySuccess,
            //    Type = InformationType.DismissFamily,
            //    NoticeType = NoticeType.Popout,
            //    FamilyId = input.FamilyId
            //});
        }


        public async Task<CancelDismissFamilyOutput> CanceAndRefuselDismissFamily(CancelDismissFamilyInput input)
        {
            var family = await _entityRepository.FirstOrDefaultAsync(input.FamilyId);
            if (family == null)
            {
                throw new Exception("该家庭已不存在");
            }
            if (family.FamilyState == FamilyState.Normal)
            {
                throw new Exception("家庭状态正常，操作有误");
            }
            var self = input.PlayerGuid == family.FatherId ? "爸爸" : "妈妈";
            var record = await _dismissFamilyRecordRepository.GetAll()
                .FirstOrDefaultAsync(r => r.FamilyId == input.FamilyId && r.FamilyState == FamilyState.Dismissing)
                .CheckNull("家庭未找到，请稍后重试");
            var selfContent = string.Empty;
            var otherContent = string.Empty;
            if (record.InitiatorId == input.PlayerGuid) //取消
            {
                selfContent = CANCEL_SELF_MESSAGE;
                otherContent = string.Format(CANCEL_OTHER_MESSAGE, self);
                record.FamilyState = FamilyState.CancelDismiss;
                family.FamilyState = FamilyState.Normal;
            }
            else //拒绝
            {
                selfContent = REFUSE_SELF_MESSAGE;
                otherContent = string.Format(REFUSE_OTHER_MESSAGE, self);
                if (family.FamilyState == FamilyState.CancelDismiss)
                {
                    throw new Exception("对方与取消解散家庭");
                }
                record.FamilyState = FamilyState.RefuseDismiss;
                family.FamilyState = FamilyState.Normal;
            }
            await _dismissFamilyRecordRepository.UpdateAsync(record);
            await _entityRepository.UpdateAsync(family);

            //对方
            await _informationRepository.InsertAsync(new Information()
            {
                Content = otherContent,
                SenderId = input.PlayerGuid,
                ReceiverId = input.PlayerGuid == family.FatherId ? family.MotherId : family.FatherId,
                State = InformationState.Create,
                Type = InformationType.Event,
                FamilyId = input.FamilyId
            });
            //自己
            await _informationRepository.InsertAsync(new Information()
            {
                Content = selfContent,
                SenderId = input.PlayerGuid,
                ReceiverId = input.PlayerGuid,
                State = InformationState.Create,
                Type = InformationType.Event,
                FamilyId = input.FamilyId
            });

            return new CancelDismissFamilyOutput() { IsSuccess = true };
        }

        //public async Task<string> DismissFamilyPayNotify()
        //{
        //    var result = String.Empty;
        //    var msg = String.Empty;
        //    try
        //    {
        //        var wechatPayNotifyResponse = await _weChatPayAppService.WechatPayNotify();

        //        if (wechatPayNotifyResponse != null)
        //        {
        //            if (!wechatPayNotifyResponse.IsReturnSuccess || !wechatPayNotifyResponse.IsResultSuccess)
        //            {
        //                throw new Exception($"返回代码或业务代码返回错误");
        //            }

        //            //推送到队列
        //            var request = new SendMessageRequest(_config)
        //            {
        //                Timestamp = DateTime.UtcNow.GetTimestamp(),
        //                Nonce = GetNonce(),
        //                QueueName = _config.MQ_WechatPay,
        //                MsgBody = wechatPayNotifyResponse.OutTradeNo,
        //                DelaySeconds = 0
        //            };

        //            var response = await _qcouldApiClient.Execute<SendMessageRequest, SendMessageResponse>(request);

        //            Logger.Debug($"解散家庭增加队列结果：{response.ToJsonString()}");
        //            result = "SUCCESS";
        //            msg = "OK";
        //        }
        //        else
        //        {
        //            result = "FAIL";
        //            msg = "解析失败";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "FAIL";
        //        msg = ex.Message;
        //        Logger.Error($"接收微信通知出错,错误信息:{ex.Message}", ex);
        //    }

        //    Logger.Warn($"解散家庭通知结果:{result}, 消息:{msg}");
        //    return WeChatPayHelper.GetReturnXml(result, msg);
        //}

        public string ExportFamiliesToExcel(GetAllFamilysInput input)
        {
            var query = SearchFamilies(input);
            var fileName = FAMILYFILE;
            FileInfo file = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, fileName));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("提现记录表");//创建sheet
                worksheet.Cells[1, 1].Value = "家庭Id";
                worksheet.Cells[1, 2].Value = "创建时间";
                worksheet.Cells[1, 3].Value = "宝爸昵称";
                worksheet.Cells[1, 4].Value = "宝妈昵称";
                worksheet.Cells[1, 5].Value = "宝宝昵称";
                worksheet.Cells[1, 6].Value = "存款";
                worksheet.Cells[1, 7].Value = "充值金额";
                //worksheet.Cells.Style.ShrinkToFit = true;
                //worksheet.Cells.AutoFitColumns();

                //样式
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int r = 1;
                foreach (var item in query)
                {
                    worksheet.Cells[r + 1, 1].Value = item.Id.ToString();
                    worksheet.Cells[r + 1, 2].Value = item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[r + 1, 3].Value = item.Father == null ? "无" : item.Father.NickName;
                    worksheet.Cells[r + 1, 4].Value = item.Mother == null ? "无" : item.Mother.NickName;
                    worksheet.Cells[r + 1, 5].Value = item.Babies.LastOrDefault().Name;
                    worksheet.Cells[r + 1, 6].Value = item.Deposit;
                    worksheet.Cells[r + 1, 7].Value = item.ChargeAmount;
                    r++;
                }

                package.Save();

                return fileName;
            }
        }
    }
}


