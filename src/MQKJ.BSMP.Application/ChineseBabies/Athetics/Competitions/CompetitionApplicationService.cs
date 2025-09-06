using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.Asset;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.TestModels;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropPrices;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos;
using MQKJ.BSMP.Common.RunHorseInformations;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.Players;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions
{
    public class CompetitionApplicationService : BsmpApplicationServiceBase<Competition, Guid, CompetitionEditDto, CompetitionEditDto, GetCompetitionInput, CompetitionListDtos>, ICompetitionApplicationService
    {
        private List<BabyAttributeCode> codeList = new List<BabyAttributeCode>()
        {
            BabyAttributeCode.IntelligenceCode,
            BabyAttributeCode.PhysiqueCode,
            BabyAttributeCode.ImagineCode,
            BabyAttributeCode.WillPowerCode,
            BabyAttributeCode.EmotionQuotientCode,
            BabyAttributeCode.CharmCode
        };

        private List<string> dangradingDescriptions = new List<string>();

        private readonly IRepository<SeasonManagement> _seasonManagementRepository;

        private readonly IRepository<Family> _familyRepository;

        private readonly IRepository<Baby> _babyRepository;

        private readonly IRepository<FightRecord,Guid> _fightRecordRepository;

        private readonly IRepository<BuyFightCountRecord,Guid> _buyFightCountRecordRepository;

        private readonly IRepository<RunHorseInformation, Guid> _runInformationRepository;

        private readonly IRepository<SystemSetting> _systemSettingRepository;

        private readonly IRepository<AthleticsInformation,Guid> _athleticsinformationRepository;

        //private readonly IRepository<BabyAssetAddition, Guid> _babyAssetAdditionRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;

        private readonly IRepository<BabyAssetFeature, Guid> _babyAssetFeatureRepository;

        private readonly IRepository<BabyProp, int> _babyPropRepository;
        private readonly IRepository<BabyPropRecord,Guid> _babyPropRecordRepository;

        private readonly IRepository<AthleticsReward, int> _athleticsRewardRepository;

        private readonly IRepository<Information, Guid> _informationRepository;

        private readonly IRepository<AthleticsRewardRecord, Guid> _athleticsRewardRecordRepository;

        private readonly BabyPropAppService _babyPropAppService;


        private readonly IRepository<FamilyCoinDepositChangeRecord, Guid> _familyCoinDepositChangeRecordRepository;

        private const int winCoefficient = 100;

        //private BSMPDbContext _dbContext { get; set; }

        public CompetitionApplicationService(IRepository<Competition, Guid> repository,
            IRepository<SeasonManagement> seasonManagementRepository,
            IRepository<Family> familyRepository,
            IRepository<Baby> babyRepository,
            IRepository<FightRecord, Guid> fightRecordRepository,
            IRepository<BuyFightCountRecord, Guid> buyFightCountRecordRepository,
            IRepository<RunHorseInformation, Guid> runInformationRepository,
            IRepository<SystemSetting> systemSettingRepository,
            IRepository<AthleticsInformation, Guid> athleticsinformationRepository,
            //IRepository<BabyAssetAddition, Guid> babyAssetAdditionRepository,
            IRepository<BabyFamilyAsset, Guid> babyFamilyAssetRepository,
            IRepository<BabyAssetFeature, Guid> babyAssetFeatureRepository,
            IRepository<BabyProp, int> babyPropRepository,
            IRepository<BabyPropRecord, Guid> babyPropRecordRepository,
            IRepository<AthleticsReward, int> athleticsRewardRepository,
            IRepository<AthleticsRewardRecord, Guid> athleticsRewardRecordRepository,
            BabyPropAppService babyPropAppService,
            IRepository<FamilyCoinDepositChangeRecord, Guid> familyCoinDepositChangeRecordRepository,
            IRepository<Information, Guid> informationRepository

            //BSMPDbContext dbContext
            ) :base(repository)
        { 
            _seasonManagementRepository = seasonManagementRepository;

            _babyRepository = babyRepository;

            _familyRepository = familyRepository;

            _fightRecordRepository = fightRecordRepository;

            _buyFightCountRecordRepository = buyFightCountRecordRepository;

            _runInformationRepository = runInformationRepository;

            _athleticsinformationRepository = athleticsinformationRepository;

            _systemSettingRepository = systemSettingRepository;

            //_babyAssetAdditionRepository = babyAssetAdditionRepository;
            _babyFamilyAssetRepository = babyFamilyAssetRepository;

            _babyAssetFeatureRepository = babyAssetFeatureRepository;

            _babyPropRepository = babyPropRepository;

            _babyPropRecordRepository = babyPropRecordRepository;

            _athleticsRewardRepository = athleticsRewardRepository;

            _athleticsRewardRecordRepository = athleticsRewardRecordRepository;

            _babyPropAppService = babyPropAppService;

            _informationRepository = informationRepository;

            _familyCoinDepositChangeRecordRepository = familyCoinDepositChangeRecordRepository;

            //_dbContext = dbContext;
        }

        public Task<EndFightOutput> EndFight(EndFightInput input)
        {
            throw new NotImplementedException();
        }
        public async Task<EnterAtheticsOutput> EnterAthetics(EnterAtheticsInput input)
        {
            if (!input.BabyId.HasValue || !input.FamilyId.HasValue || !input.PlayerId.HasValue)
            {
                throw new Exception("参数有误");
            }

            var output = new EnterAtheticsOutput();

            var currentSeason = await GetSeasonInfo();

            var dangradingSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1006);
            var config = JsonConvert.DeserializeObject<DangradingSetting>(dangradingSetting.Value);

            var values = Array.ConvertAll(config.DangradingPoint.Split(','),int.Parse).ToList();
            values.Add(values.LastOrDefault() + 1);
            var dangradings = Enum.GetValues(typeof(DanGrading)).MapTo<List<DanGrading>>();
            dangradings.Remove(DanGrading.UnKnow);
            for (int i = 0; i < values.Count; i++)
            {
                var desc = string.Empty;
                if (i+1 == values.Count)
                {
                    desc = $"积分{values[i]}以上时获得【{EnumHelper.EnumHelper.GetDescription(dangradings[i])}】段位";
                }
                else
                {
                    if (i == 0)
                    {
                        desc = $"积分0-{values[i]}以上时获得【{EnumHelper.EnumHelper.GetDescription(dangradings[i])}】段位";
                    }
                    else
                    {
                        desc = $"积分{values[i-1]+1}-{values[i]}以上时获得【{EnumHelper.EnumHelper.GetDescription(dangradings[i])}】段位";
                    }
                    
                }
                dangradingDescriptions.Add(desc);
            }

            var competition = await _repository.GetAll().Include(c => c.Baby)
                .FirstOrDefaultAsync(a => a.BabyId == input.BabyId && a.SeasonManagementId == currentSeason.Id);

            //var competition = await _repository.GetAll().Include(c => c.Baby)
            //    .FirstOrDefaultAsync(a => a.BabyId == input.BabyId && a.FamilyId == input.FamilyId);

            if (competition == null)
            {
                competition = await _repository.InsertAsync(new Competition()
                {
                    BabyId = input.BabyId.Value,
                    FamilyId = input.FamilyId.Value,
                    FatherFightCount = currentSeason.FatherDefaultFightCount,
                    MotherFightCount = currentSeason.MotherDefaultFightCount,
                    SeasonManagementId = currentSeason.Id,
                    DanGrading = DanGrading.Bronze
                    //RankingNumber = currentCount + 1
                });

                var baby = await _babyRepository.GetAsync(input.BabyId.Value);
                competition.Baby = baby ?? throw new Exception("该宝宝不存在");
            }

            if (competition.DanGrading == DanGrading.UnKnow)
            {
                competition.DanGrading = DanGrading.Bronze;
                await _repository.UpdateAsync(competition);
            }

            competition.Baby.MapTo(output.Baby);
            competition.Baby.Age = Convert.ToInt32(competition.Baby.AgeDouble.ToString().Split('.')[0]);

            var family = await _familyRepository.GetAsync(input.FamilyId.Value);

            if (family == null)
            {
                throw new Exception("家庭不存在");
            }

            //今天是否获取了N次免费次数
            var isGetFightCount = _buyFightCountRecordRepository.GetAll()
                .Any(r => r.SourceType == SourceType.SystemPresentation &&
                     r.SeasonManagementId == currentSeason.Id &&
                     r.FamilyId == family.Id &&
                     //r.PurchaserId == input.PlayerId &&
                     r.CreationTime.Date == DateTime.Now.Date);
            if (!isGetFightCount)
            {
                competition.FatherFightCount = currentSeason.FatherDefaultFightCount;
                competition.MotherFightCount = currentSeason.MotherDefaultFightCount;
                //var giveFightCount = 0;
                //var isFather = input.PlayerId == family.FatherId ? true : false;
                //if (isFather)
                //{
                //    competition.FatherFightCount = currentSeason.FatherDefaultFightCount;
                //    giveFightCount = currentSeason.FatherDefaultFightCount;
                //}
                //else
                //{
                //    competition.MotherFightCount = currentSeason.MotherDefaultFightCount;
                //    giveFightCount = currentSeason.MotherDefaultFightCount;
                //}
                //await _repository.UpdateAsync(competition);

                //添加记录
                await addFightCountRecord(family.Id,input.BabyId.Value,currentSeason);
            }

            output.FightCount = input.PlayerId == family.FatherId ? competition.FatherFightCount : competition.MotherFightCount;
            var result = IsEquipmentProp(input.BabyId.Value,input.FamilyId.Value);
            output.IsHasHouse = result.IsHasHouse;
            output.IsHasCar = result.IsHasCar;
            output.IsHasServant = result.IsHasServant;
            output.IsHasButler = result.IsHasButler;
            output.IsHasSkip = result.IsHasSkip;
            output.SkinCode = result.SkinCode;
            //output.HouseCode = result.HouseCode;

            output.StartTime = currentSeason.StartTime;
            output.EndTime = currentSeason.EndTime;
            output.FailCount = competition.FailedCount;
            output.WinCount = competition.WiningCount;
            output.DanGrading = competition.DanGrading;
            output.DangradingDescriptions = dangradingDescriptions;
            return output;
        }

        private async Task addFightCountRecord(int familyId,int babyId,SeasonManagement seasonManagement)
        {
            await _buyFightCountRecordRepository.InsertAsync(new BuyFightCountRecord()
            {
                FamilyId = familyId,
                SeasonManagementId = seasonManagement.Id,
                SourceType = SourceType.SystemPresentation,
                //PurchaserId = input.PlayerId,
                FightCount = seasonManagement.FatherDefaultFightCount,
                BabyId = babyId
            });
            await _buyFightCountRecordRepository.InsertAsync(new BuyFightCountRecord()
            {
                FamilyId = familyId,
                SeasonManagementId = seasonManagement.Id,
                SourceType = SourceType.SystemPresentation,
                //PurchaserId = input.PlayerId,
                FightCount = seasonManagement.MotherDefaultFightCount,
                BabyId = babyId
            });
        }

        public EquipmentPropModel IsEquipmentProp(int babyId, int familyId)
        {
            var output = new EquipmentPropModel();
            var props = _babyFamilyAssetRepository.GetAll()
                .Include(o => o.BabyProp)
                .ThenInclude(t => t.BabyPropType)
                .Where(a => a.FamilyId == familyId &&
                a.IsEquipmenting == true &&
                (a.ExpiredDateTime > DateTime.UtcNow || a.ExpiredDateTime == null) 
                //&&
                //(
                //    a.BabyProp.BabyPropType.Name == "House" ||  //房子
                //    a.BabyProp.BabyPropType.Name == "Car" ||  //车子
                //    a.BabyProp.BabyPropType.Name == "Housekeeper" ||  //管家
                //    a.BabyProp.BabyPropType.Name == "Nanny" ||     //佣人
                //    a.BabyProp.BabyPropType.Name == "Skin"
                //)
                );
            output.IsHasHouse = props.Any(a => a.BabyProp.BabyPropType.Name == "House" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
            output.IsHasCar = props.Any(a => a.BabyProp.BabyPropType.Name == "Car" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
            output.IsHasServant = props.Any(a => a.BabyProp.BabyPropType.Name == "Housekeeper" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
            output.IsHasButler = props.Any(a => a.BabyProp.BabyPropType.Name == "Nanny" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
            output.IsHasSkip = props.Any(a => a.BabyProp.BabyPropType.Name == "Skin" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Baby && a.OwnId.Value == babyId);

            var houseAsset = props.FirstOrDefault(s => s.BabyProp.BabyPropType.Name == "House");
            var skinAsset = props.FirstOrDefault(a => a.BabyProp.BabyPropType.Name == "Skin" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Baby && a.OwnId.Value == babyId);
            output.HouseCode = houseAsset?.BabyProp?.Code == null ? 0 : houseAsset.BabyProp.Code;
            output.SkinCode = skinAsset?.BabyProp?.Code == null ? 0 : skinAsset.BabyProp.Code;
            return output;
        }

        public async Task<DanGrading> GetPlayerDanGride(int babyId, int familyId)
        {
            var danGrading = DanGrading.UnKnow;
            var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);
            if (currentSeason != null)
            {
                var competition = await _repository.FirstOrDefaultAsync(c => c.BabyId == babyId && c.FamilyId == familyId && c.SeasonManagementId == currentSeason.Id);
                if (competition != null)
                {
                    var dangradingSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1006);
                    var config = JsonConvert.DeserializeObject<DangradingSetting>(dangradingSetting.Value);
                    var dangradingValues = Array.ConvertAll((config.DangradingPoint.Split(',')), int.Parse);
                    var model = GetDanGrading(competition.GamePoint, dangradingValues);
                    danGrading = model.DanGrading;
                    if (competition.DanGrading != danGrading)
                    {
                        competition.DanGrading = danGrading;
                        await _repository.UpdateAsync(competition);
                    }
                }
            }

            return danGrading;
        }

        private List<EquipmentPropModel> IsEquipmentProps(List<int> babyIds,List<int> familyIds)
        {
            var output = new EquipmentPropModel();
            var props = _babyFamilyAssetRepository.GetAll()
                .Include(o => o.BabyProp)
                .ThenInclude(t => t.BabyPropType)
                .Where(a => familyIds.Contains(a.FamilyId) &&
                a.IsEquipmenting == true &&
                (a.ExpiredDateTime > DateTime.UtcNow || a.ExpiredDateTime == null))
                .GroupBy(c => c.FamilyId);

            var models = new List<EquipmentPropModel>();
            foreach (var item in props)
            {
                var model = new EquipmentPropModel();
                model.FamilyId = item.Key;
                model.IsHasHouse = item.Any(a => a.BabyProp.BabyPropType.Name == "House" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
                model.IsHasCar = item.Any(a => a.BabyProp.BabyPropType.Name == "Car" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
                model.IsHasServant = item.Any(a => a.BabyProp.BabyPropType.Name == "Housekeeper" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
                model.IsHasButler = item.Any(a => a.BabyProp.BabyPropType.Name == "Nanny" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Family);
                model.IsHasSkip = item.Any(a => a.BabyProp.BabyPropType.Name == "Skin" && a.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Baby && babyIds.Contains(a.OwnId.Value));
                var skinAsset = item.FirstOrDefault(s => s.BabyProp.BabyPropType.Name == "Skin");
                model.SkinCode = skinAsset?.BabyProp?.Code == null ? 0 : skinAsset.BabyProp.Code;
                models.Add(model);
            }
            return models;
        }
        /// <summary>
        /// 定时发奖与统计胜利次数  排名消息优先置顶 发奖消息第二
        /// </summary>
        //[UnitOfWork]
        public async Task TimingAwardPrize()
        {
            await Task.CompletedTask;

            Logger.Warn($"当前UTC时间是:{DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss")}");

            //var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);

            //var winCountSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1003);

            //var competitionEntitys = await _repository.GetAll()
            //    .Include(b => b.Baby)
            //    .Where(c => c.SeasonManagementId == currentSeason.Id)
            //    .OrderByDescending(p => p.GamePoint)
            //    .ThenBy(t => t.CreationTime)
            //    .ToListAsync();//需要tolist不然 for循环会出问题
            //// 1、发奖励到背包
            //var athleticsRewards = await _athleticsRewardRepository.GetAll().Select(c => new AthleticsRewardModel
            //{
            //    AthleticsRewardId = c.Id,
            //    RankingNumber = c.RankingNumber,
            //    RewardPropType = c.RewardPropType,
            //    BabyPropId = c.BabyPropId,
            //    BabyPropPriceId = c.BabyPropPriceId,
            //    CoinCount = c.CoinCount
            //}).ToListAsync();  //需要tolist不然 for循环会出问题

            //var familyIds = competitionEntitys.Select(x => x.FamilyId).Distinct();
            //var families = await _familyRepository.GetAll().Where(x => familyIds.Contains(x.Id)).ToListAsync();

            //// 2、添加发奖消息  1-100；
            //List<AthleticsInformation> informations = new List<AthleticsInformation>();
            //List<RunHorseInformation> runInformations = new List<RunHorseInformation>();
            //List<AthleticsRewardRecord> rewardRecords = new List<AthleticsRewardRecord>();
            //List<Family> tempFamilys = new List<Family>();
            //var i = 1;
            //foreach (var item in competitionEntitys)
            //{
            //    var rewardContent = string.Format(RANKING_INFO, item.Baby.Name, i);

            //    if (i <= currentSeason.RewardPlayerCount)
            //    {
            //        if (i == 1)
            //        {
            //            runInformations.Add(SetRunsInformations(currentSeason, rewardContent, 1, 60, PlayScene.AthleticsPage)); //第一名 有跑马灯消息(竞技场主页)
            //            runInformations.Add(SetRunsInformations(currentSeason, rewardContent, 1, 5 * 60, PlayScene.BabyPage)); //第一名 有跑马灯消息(宝宝主页)
            //        }

            //        //添加奖励消息
            //        var content = string.Format(REWARD_INFO, item.Baby.Name, i, i);
            //        informations.Add(SetAthleticsInformations(currentSeason.Id, item.FamilyId, content, AthleticsInformationType.PrizeInforamtion));
            //        //调接口发奖 添加发奖记录
            //        var family = _dbContext.Families.FirstOrDefault(f => f.Id == item.FamilyId);
            //        var rewardCoinCount = RewardPrizeToFamilyOrBaby(item, athleticsRewards, i, rewardRecords);
            //        if (rewardCoinCount != 0)
            //        {
            //            family.Deposit += rewardCoinCount;
            //            //_dbContext.Families.Update(family);
            //            //_dbContext.Entry(family).Property(d => d.Deposit).IsModified = true;
            //            families.FirstOrDefault(f => f.Id == item.FamilyId).Deposit += rewardCoinCount;
            //        }
            //        else
            //        {
            //            tempFamilys.Add(family);
            //        }

            //    }
            //    //添加排名消息
            //    informations.Add(SetAthleticsInformations(currentSeason.Id, item.FamilyId, rewardContent, AthleticsInformationType.RankingInforamtion));
            //    i++;
            //}
            //foreach (var item in tempFamilys)
            //{
            //    families.Remove(item);
            //}
            ////更新家庭
            //Parallel.ForEach(families, item =>
            //{
            //    _familyRepository.UpdateAsync(item);
            //});
            ////插入奖励记录
            //Parallel.ForEach(rewardRecords, item =>
            //{
            //    _athleticsRewardRecordRepository.Insert(item);
            //});
            ////插入竞技场消息
            //Parallel.ForEach(informations, item =>
            //{
            //    _athleticsinformationRepository.Insert(item);
            //});
            ////插入竞技场跑马灯消息
            //Parallel.ForEach(runInformations, item =>
            //{
            //    _runInformationRepository.Insert(item);
            //});



            //_dbContext.UpdateRange(families);
            //_dbContext.SaveChanges();
            //_dbContext.AthleticsRewardRecords.AddRange(rewardRecords);
            //_dbContext.AthleticsInformations.AddRange(informations);
            //_dbContext.RunHorseInformations.AddRange(runInformations);
            //_dbContext.SaveChanges();
        }

        private double RewardPrizeToFamilyOrBaby(Competition competition,List<AthleticsRewardModel> athleticsRewardModels, int rankingNumber, List<AthleticsRewardRecord> rewardRecords)
        {
            var rankingRewards = athleticsRewardModels.Where(x => x.RankingNumber == rankingNumber);
            var rewardCoinCount = 0.0;
            foreach (var item in rankingRewards)
            {
                if (item.RewardPropType == RewardPropType.CoinType)
                {
                    rewardCoinCount = item.CoinCount;
                    AddRewadRecord(item, competition, rewardRecords);
                }
                else if (item.RewardPropType == RewardPropType.Prodcut)
                {
                    //调接口
                    var result = _babyPropAppService.AwardPrizeToArenaPlayer(new AwardPrizeToArenaPlayerInput()
                    {
                        BabyId = competition.BabyId,
                        FamilyId = competition.FamilyId,
                        PriceId = item.BabyPropPriceId.Value,
                        PropId = item.BabyPropId.Value,
                    }).Result;
                    //添加发奖记录
                    AddRewadRecord(item, competition, rewardRecords);
                }
                else
                {
                    Logger.Warn($"未知类型:{item.RewardPropType}");
                }
            }

            return rewardCoinCount;
        }

        private void AddRewadRecord(AthleticsRewardModel model,Competition competition,List<AthleticsRewardRecord> rewardRecords)
        {
            AthleticsRewardRecord athleticsReward = new AthleticsRewardRecord();
            athleticsReward.Id = Guid.NewGuid();
            athleticsReward.BabyId = competition.BabyId;
            athleticsReward.CreationTime = DateTime.Now;
            athleticsReward.FamilyId = competition.Baby.FamilyId;
            athleticsReward.AthleticsRewardId = model.AthleticsRewardId;
            rewardRecords.Add(athleticsReward);
        }

        /// <summary>
        /// 设置跑马灯消息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private RunHorseInformation SetRunsInformations(SeasonManagement season,string content,int playCount,int interval,PlayScene playScene)
        {
            var information = new RunHorseInformation();
            information.Id = Guid.NewGuid();
            information.PlayCount = playCount;
            information.StartTime = season.StartTime.ToLocalTime();
            information.EndTime = season.EndTime.ToLocalTime();
            information.Content = content;
            information.Interval = interval;
            information.PlayScene = playScene;
            information.CreationTime = DateTime.Now;
            return information;
        }

        /// <summary>
        /// 设置竞技场消息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private AthleticsInformation SetAthleticsInformations(int seasonId,int familyId,string content, AthleticsInformationType type)
        {
            var information = new AthleticsInformation();
            information.Id = Guid.NewGuid();
            information.AthleticsInformationType = type;
            information.Content = content;
            information.FamilyId = familyId;
            information.CreationTime = DateTime.Now;
            information.SeasonManagementId = seasonId;
            return information;
        }

        public async Task<StartFightOutput> StartFight(StartFightInput input)
        {
            if (!input.BabyId.HasValue || !input.FamilyId.HasValue || !input.OtherBabyId.HasValue || !input.PlayerId.HasValue)
            {
                throw new Exception("参数有误");
            }

            if (input.BabyId == input.OtherBabyId)
            {
                throw new Exception("同一个宝宝不能对战");
            }

            var output = new StartFightOutput();

            var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);

            var competition = await _repository.GetAll().Include(f => f.Family)
               .FirstOrDefaultAsync(a => a.BabyId == input.BabyId && a.SeasonManagementId == currentSeason.Id);
            if (competition.Family != null && input.PlayerId == competition.Family.FatherId && competition.FatherFightCount <=0)
            {
                return new StartFightOutput()
                {
                    StartFightErrCode = StartFightErrCode.FightCountNotEnough
                };
            }
            if (competition.Family != null && input.PlayerId == competition.Family.MotherId && competition.MotherFightCount <= 0)
            {
                return new StartFightOutput()
                {
                    StartFightErrCode = StartFightErrCode.FightCountNotEnough
                };
            }
            var babies = _babyRepository.GetAll()
                .Include(f => f.Family)
                .ThenInclude(f => f.Father)
                .Include(m => m.Family.Mother)
                .Where(b => b.Id == input.BabyId || b.Id == input.OtherBabyId);

            if (babies.Count() < 2)
            {
                throw new Exception("获取宝宝信息异常");
            }
            var selfBaby = await babies.FirstOrDefaultAsync(b => b.Id == input.BabyId);

            var otherBaby = await babies.FirstOrDefaultAsync(b => b.Id == input.OtherBabyId);

            //获取随机属性编码
            var attributeCode = GetRandomAttribute(selfBaby);
            //var name = Enum.GetName(typeof(BabyAttributeCode),attributeCode);

            //获取对战结果
            var fightResult = await GetFightResult(selfBaby, otherBaby, attributeCode);

            //计算积分 更新成功/失败次数 更新积分
            var pointResult = await CalculatePoint(currentSeason, selfBaby, otherBaby, fightResult, input.PlayerId.Value);

            //添加对战记录
            await AddFightRecord(currentSeason, attributeCode, input, fightResult, pointResult);

            //添加消息
            await AddFightInformation(fightResult, selfBaby, otherBaby, pointResult, currentSeason, input.PlayerId.Value);

            output.BabyAttributeCode = attributeCode;

            output.FightResultEnum = fightResult.FightResultEnum;

            output.GamePoint = pointResult.PointChangeValue;

            output.WiningRatio = fightResult.WinRate;

            output.RewardCoin = pointResult.RewardCoin;

            output.PropAdditionRate = $"{fightResult.PropAdditionRate * 100}%";

            output.AttributeRate = $"{fightResult.AttributeRate * 100}%";

            output.DanGrading = pointResult.CurrentDanGrading;

            output.PrizeReward = pointResult.PrizeModel;

            //}

            return output;
        }

        //胜利
        private const string ATHLETICS_WIN_OFFENSIVE = "您的宝宝战胜了【{0}】";//进攻方
        private const string ATHLETICS_WIN_DEFENSE = "您的宝宝被【{0}】打败了";//防御方
        //失败
        private const string ATHLETICS_FAIL_OFFENSIVE = "您的宝宝挑战【{0}】失败了";//自己的另一半
        private const string ATHLETICS_FAIL_DEFENSE = "【{0}】宝宝挑战您的宝宝【{1}】失败了";//对方

        private const string RUN_HORSE_WIN_COUNT = "【{0}】获得了{1}次胜利";
        private const string WIN_COUNT = "获得了{1}次胜利";
        private const string RANKING_INFO = "{0}本次竞赛获得第{1}名";
        private const string REWARD_INFO = "{0}本期竞赛最终获得【第{1}名】,根据您排名发放【{2}等奖】奖";
        private const string DAN_GRADING_RUNHORSE_INFO = "恭喜{0}宝宝的段位从{1}升到{2}段位";
        private const string DAN_GRADING_UP_INFO = "恭喜您的宝宝段位从{0}升到{1}";
        private const string DAN_GRADING_DOWN_INFO = "您的宝宝段位从{0}降到{1}啦";

        /// <summary>
        /// 对战消息添加
        /// </summary>
        /// <param name="fightResult"></param>
        /// <param name="selfBaby"></param>
        /// <param name="otherBaby"></param>
        /// <param name="rankingAndPoint"></param>
        /// <param name="currentSeason"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        private async  Task AddFightInformation(FightResult fightResult,Baby selfBaby,Baby otherBaby,RankingAndPoint rankingAndPoint,SeasonManagement currentSeason,Guid playerId)
        {
            var winCount = await _fightRecordRepository.CountAsync(f => f.FightResultEnum == FightResultEnum.Win && f.SeasonManagementId == currentSeason.Id);

            var receiver = playerId == selfBaby.Family.FatherId ? selfBaby.Family.Mother : selfBaby.Family.Father;

            var sender = playerId == selfBaby.Family.FatherId ? selfBaby.Family.Father : selfBaby.Family.Mother;

            //var interval = int.MaxValue;

            var ofensiveMsg = fightResult.FightResultEnum == FightResultEnum.Win ? string.Format(ATHLETICS_WIN_OFFENSIVE,otherBaby.Name?? "未起名") : string.Format(ATHLETICS_FAIL_OFFENSIVE,otherBaby.Name ?? "未起名");
            var defenseMsg = fightResult.FightResultEnum == FightResultEnum.Win ? string.Format(ATHLETICS_WIN_DEFENSE, selfBaby.Name?? "未起名") : string.Format(ATHLETICS_FAIL_DEFENSE,selfBaby.Name ?? "未起名", otherBaby.Name ?? "未起名");

            //自己家庭的对战消息
            await AddInformation(currentSeason.Id, ofensiveMsg, selfBaby.FamilyId, AthleticsInformationType.ConfrontationInforamtion);
            //对方家庭的对战消息
            await AddInformation(currentSeason.Id, defenseMsg, otherBaby.FamilyId, AthleticsInformationType.ConfrontationInforamtion);

            //添加段位变化消息(自己的与对方的)
            await AddDanGradingInformations(currentSeason, selfBaby, rankingAndPoint);
            await AddDanGradingInformations(currentSeason, otherBaby, rankingAndPoint);

            //胜利次数消息(自己的与对方的)
            await AddWinCountInformations(currentSeason, selfBaby, rankingAndPoint.WinCount);
            await AddWinCountInformations(currentSeason, otherBaby, rankingAndPoint.OtherWinCount);
        }

        private async Task AddWinCountInformations(SeasonManagement currentSeason, Baby baby, int winCount)
        {
            var winCountSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1003);
            var winCounts = winCountSetting.Value.Split("##");
            var runHorsesCounts = winCounts[0].Split(',');//跑马灯消息
            var ahtleticsCounts = winCounts[1].Split(',');//竞技场消息
            if (runHorsesCounts.Contains(winCount.ToString())) //跑马灯消息
            {
                await AddRunInformation(string.Format(RUN_HORSE_WIN_COUNT, baby.Name, winCount), currentSeason, 1, 10, PlayScene.AthleticsPage);
            }
            else if (ahtleticsCounts.Contains(winCount.ToString()))//对战次数达标消息
            {
                //await AddInformation(currentSeason.Id,string.Format(string.Format(WIN_COUNT,winCount), selfBaby.Name), otherBaby.Family.Mother, selfBaby.FamilyId, AthleticsInformationType.WinCountInforamtion);
                await AddInformation(currentSeason.Id, string.Format(string.Format(WIN_COUNT, winCount), baby.Name), baby.FamilyId, AthleticsInformationType.WinCountInforamtion);
            }
        }

        /// <summary>
        /// 添加段位消息
        /// </summary>
        /// <param name="currentSeason"></param>
        /// <param name="baby"></param>
        /// <param name="rankingAndPoint"></param>
        /// <returns></returns>
        private async Task AddDanGradingInformations(SeasonManagement currentSeason, Baby baby,RankingAndPoint rankingAndPoint)
        {
            //段位消息
            var lastDanGradingDesc = EnumHelper.EnumHelper.GetDescription(rankingAndPoint.LastDanGrading);
            var currentDanGradingDesc = EnumHelper.EnumHelper.GetDescription(rankingAndPoint.CurrentDanGrading);
            if (lastDanGradingDesc == currentDanGradingDesc)
            {
                Logger.Debug("上次与本次段位相等，无需添加消息");
            }
            else
            {
                var athleticsUpDanGradingMsg = string.Format(DAN_GRADING_UP_INFO, lastDanGradingDesc, currentDanGradingDesc);//竞技场升段位消息
                var athleticsDownDanGradingMsg = string.Format(DAN_GRADING_DOWN_INFO, lastDanGradingDesc, currentDanGradingDesc);//竞技场降段位消息
                var runDanGradingMsg = string.Format(DAN_GRADING_RUNHORSE_INFO, baby.Name, lastDanGradingDesc, currentDanGradingDesc);//跑马灯消息
                if (rankingAndPoint.LastDanGrading > rankingAndPoint.CurrentDanGrading)//上次段位大于本次的段位
                {
                    //添加竞技场消息
                    await AddInformation(currentSeason.Id, athleticsDownDanGradingMsg, baby.FamilyId, AthleticsInformationType.DanGrading);
                }
                else if (rankingAndPoint.LastDanGrading == rankingAndPoint.CurrentDanGrading) //上次段位等于本次的段位
                {
                    Logger.Debug("上次与本次段位相等，无需添加消息");
                }
                else//上次段位小于本次的段位
                {
                    //添加竞技场消息
                    await AddInformation(currentSeason.Id, athleticsUpDanGradingMsg, baby.FamilyId, AthleticsInformationType.DanGrading);
                    //添加跑马灯消息
                    await AddRunInformation(runDanGradingMsg, currentSeason, -1, 3, PlayScene.AthleticsPage);
                    await AddRunInformation(runDanGradingMsg, currentSeason, -1, 3, PlayScene.BabyPage);
                }
            }
        }

        /// <summary>
        /// 添加跑马灯消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="season"></param>
        /// <param name="playCount"></param>
        /// <param name="interval"></param>
        /// <param name="playScene"></param>
        /// <returns></returns>
        private async Task AddRunInformation(string content,SeasonManagement season, int playCount, int interval, PlayScene playScene)
        {

            await _runInformationRepository.InsertAsync(new RunHorseInformation()
            {
                Content = content,
                StartTime = season.StartTime.ToLocalTime(),
                EndTime = season.EndTime.ToLocalTime(),
                PlayCount = playCount,
                Interval = interval,
                PlayScene = playScene,
            });
        }

        //private async Task SetRunHorseInformation(string content,Player sender,int familyId)
        //{
        //    await _athleticsinformationRepository.InsertAsync(new AthleticsInformation()
        //    {
        //        ReceiverId = sender.Id,
        //        Content = content,
        //        AthleticsInformationType = AthleticsInformationType.RunHorseInformation,
        //        FamilyId = familyId
        //    });
        //}

        /// <summary>
        /// 添加竞技场消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="receiver"></param>
        /// <param name="familyId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //private async Task AddInformation(int seasonId,string content, Player receiver, int familyId, AthleticsInformationType type)
        //{

        //    await _athleticsinformationRepository.InsertAsync(new AthleticsInformation()
        //    {
        //        AthleticsInformationType = type,
        //        Content = content,
        //        FamilyId = familyId,
        //        ReceiverId = receiver.Id
        //    });

        //    //var msg = new AthleticsInformation()
        //    //{
        //    //    Content = content,
        //    //    ReceiverId = receiver.Id,
        //    //    FamilyId = familyId
        //    //};
        //    //RedisHelper.LPush(receiver.Id.ToString(), msg);
        //    //await RedisHelper.ExpireAsync(receiver.Id.ToString(), interval);
        //}

        private async Task AddInformation(int seasonId, string content, int familyId, AthleticsInformationType type)
        {

            await _athleticsinformationRepository.InsertAsync(new AthleticsInformation()
            {
                AthleticsInformationType = type,
                Content = content,
                FamilyId = familyId,
                SeasonManagementId = seasonId
            });
        }

        /// <summary>
        /// 计算积分
        /// </summary>
        private async Task<RankingAndPoint> CalculatePoint(SeasonManagement currentSeason, Baby selfBaby, Baby OtherBaby, FightResult fightResult, Guid playerId)
        {

            var output = new RankingAndPoint();

            var competitionEntitys = await _repository.GetAll()
                .Where(a => (a.BabyId == selfBaby.Id || a.BabyId == OtherBaby.Id) && a.SeasonManagementId == currentSeason.Id)
                //.AsNoTracking()
                .ToListAsync();
            if (competitionEntitys.Count != 2)
            {
                throw new Exception("玩家没参赛记录");
            }
            var selfCompetition = competitionEntitys.SingleOrDefault(r => r.BabyId == selfBaby.Id);
            var otherCompetition = competitionEntitys.SingleOrDefault(r => r.BabyId == OtherBaby.Id);

            //获取自己与对方排名
            var resultRanking = await GetRankingNumber(currentSeason.Id, selfCompetition, otherCompetition);
            //获取自己的排名
            var selfNumber = resultRanking.SelfNumber;
            var otherNumber = resultRanking.OtherNumber;

            output.LastPoint = selfCompetition.GamePoint;
            output.LastDanGrading = selfCompetition.DanGrading;
            output.OtherLastDanGrading = otherCompetition.DanGrading;
            var pointChangeValue = 0;
            var diffValue = selfNumber - otherNumber;
            var dangradingSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1006);
            var config = JsonConvert.DeserializeObject<DangradingSetting>(dangradingSetting.Value);
            var rewardcoinCounts = config.CointReward.Split(',');
            var rewardCoin = 0;
            var fightResutStr = string.Empty;
            var fightObjStr = string.Empty;
            if (diffValue < 0 && fightResult.FightResultEnum == FightResultEnum.Win)  //胜利 排名比自己低 +1
            {
                fightResutStr = "赢";
                fightObjStr = "低";
                rewardCoin = int.Parse(rewardcoinCounts[2]);
                pointChangeValue = 1;
                selfCompetition.WiningCount += 1;
                otherCompetition.FailedCount += 1;
            }
            else if (diffValue < 0 && fightResult.FightResultEnum == FightResultEnum.Fail) //失败 排名比自己低 -排名差值
            {
                fightResutStr = "输";
                fightObjStr = "低";
                rewardCoin = int.Parse(rewardcoinCounts[3]);
                pointChangeValue = -diffValue < 5 ? -diffValue : 5; //大于5 则等于5
                selfCompetition.FailedCount += 1;
                otherCompetition.WiningCount += 1;
            }
            else if (diffValue > 0 && fightResult.FightResultEnum == FightResultEnum.Win)//胜利 排名比自己高 差值 * 2
            {
                fightResutStr = "赢";
                fightObjStr = "高";
                rewardCoin = int.Parse(rewardcoinCounts[0]);
                pointChangeValue = diffValue * 2 > 10 ? 10 : diffValue * 2;
                selfCompetition.WiningCount += 1;
                otherCompetition.FailedCount += 1;
            }
            else if (diffValue > 0 && fightResult.FightResultEnum == FightResultEnum.Fail)//失败 排名比自己高 -1
            {
                fightResutStr = "输";
                fightObjStr = "高";
                rewardCoin = int.Parse(rewardcoinCounts[1]);
                pointChangeValue = 1;
                selfCompetition.FailedCount += 1;
                otherCompetition.WiningCount += 1;
            }
            else
            {
                Logger.Debug("不处理");
            }
            var tempValue = pointChangeValue > 10 ? 10 : pointChangeValue;
            //更新积分
            if (fightResult.FightResultEnum == FightResultEnum.Win) //赢
            {
                selfCompetition.GamePoint += tempValue;
                //otherCompetition.GamePoint -= pointChangeValue;
            }
            else if (fightResult.FightResultEnum == FightResultEnum.Fail)//输
            {
                selfCompetition.GamePoint -= tempValue;

                //otherCompetition.GamePoint += pointChangeValue;
            }
            else
            {
                Logger.Debug("无需计算");
            }

            if (selfCompetition.GamePoint <= 0)
            {
                selfCompetition.GamePoint = 0;
            }

            //更新次数
            if (playerId == selfBaby.Family.FatherId)
            {
                selfCompetition.FatherFightCount -= 1;
                if (selfCompetition.FatherFightCount <= 0)
                {
                    selfCompetition.FatherFightCount = 0;
                }
            }
            else if (playerId == selfBaby.Family.MotherId)
            {
                selfCompetition.MotherFightCount -= 1;
                if (selfCompetition.MotherFightCount <= 0)
                {
                    selfCompetition.MotherFightCount = 0;
                }
            }
            if (selfBaby.Family != null)
            {
                selfBaby.Family.Deposit += rewardCoin;
                //添加金币记录
                await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
                {
                    Amount = rewardCoin,
                    BabyId = selfBaby.Id,
                    FamilyId = selfBaby.FamilyId,
                    GetWay = CoinGetWay.PropToCoin,
                    CurrentFamilyCoinDeposit = selfBaby.Family.Deposit
                });
                //添加家庭消息
                var parentName = playerId == selfBaby.Family.FatherId ? "爸爸" : "妈妈";
                var content = string.Format(REWARD_COIN_MSG, selfBaby.Name, parentName, fightResutStr,fightObjStr, rewardCoin);
                await AddFamilyInformations(content, selfBaby, playerId);
            }
            var dangradingValues = Array.ConvertAll((config.DangradingPoint.Split(',')), int.Parse);
            var propRewards = config.DangradingReward.Split(',');
            var selfModel = GetDanGrading(selfCompetition.GamePoint, dangradingValues, propRewards);
            selfCompetition.DanGrading = selfModel.DanGrading;
            var otherModel = GetDanGrading(otherCompetition.GamePoint, dangradingValues);
            otherCompetition.DanGrading = otherModel.DanGrading;

            if (selfCompetition.DanGrading == DanGrading.UnKnow)
            {
                selfCompetition.DanGrading = DanGrading.Bronze;
            }
            if (otherCompetition.DanGrading == DanGrading.UnKnow)
            {
                otherCompetition.DanGrading = DanGrading.Bronze;
            }

            await _repository.UpdateAsync(selfCompetition);
            //更新对方的
            await _repository.UpdateAsync(otherCompetition);

            PrizeRewardModel prizeModel = null;
            var isFightRecord = _fightRecordRepository.GetAll()
                .Any(c => c.InitiatorBabyId == selfBaby.Id && c.LastDangrading == selfCompetition.DanGrading);

            if (output.LastDanGrading < selfCompetition.DanGrading && !isFightRecord)//之前的段位是否小于当前的
            {
                //发奖
                prizeModel = await PrizeReward(selfBaby, selfModel, playerId);
            }

            output.CurrentPoint = selfCompetition.GamePoint;
            output.PointChangeValue = tempValue;
            output.CurrentDanGrading = selfCompetition.DanGrading;
            output.OtherCurrentDanGrading = otherCompetition.DanGrading;
            output.WinCount = selfCompetition.WiningCount;
            output.OtherWinCount = otherCompetition.WiningCount;
            output.RewardCoin = rewardCoin;
            output.PrizeModel = prizeModel;

            return output;
        }

        private const string REWARD_PROP_MSG = "宝宝的竞技场段位首次达到{0}段位，获得{1}奖励。";
        private const string REWARD_PROP_COIN_MSG = "宝宝的竞技场段位首次达到{0}段位，获得{1}奖励。因您的家庭已拥有{1}，{1}道具已折算成{3}金币";
        private const string REWARD_COIN_MSG = "{0}的{1}在竞技场打{2}了{3}于自己宝宝排名的对手，获得{4}金币";
        private async Task<PrizeRewardModel> PrizeReward(Baby baby,GetDangradingModel model,Guid playerId)
        {
            PrizeRewardModel prizeModel = null;

            if (model.PropCode != 0)
            {
                var propCodeStr = model.PropCode.ToString();
                var prop = await _babyPropRepository
                        .GetAll().Include(p => p.Prices)
                        .Include(p => p.BabyPropType)
                        .Include(p => p.BabyPropPropertyAward)
                        .FirstOrDefaultAsync(p => p.Code == model.PropCode);
                if (prop == null)
                {
                    throw new Exception("数据错误，请稍后重试！");
                }
                var dangradingStr = EnumHelper.EnumHelper.GetDescription(model.DanGrading);
                prizeModel = prop.MapTo<PrizeRewardModel>();
                var propCount = await _babyFamilyAssetRepository.GetAllIncluding()
                    .CountAsync(s => s.BabyPropId == prop.Id && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null) && s.FamilyId == baby.FamilyId);
                var propPrice = prop.Prices.FirstOrDefault(p => p.Validity == model.ExpireTime);
                //道具是否折金币
                if (prop.MaxPurchasesNumber <= propCount)//已拥有该道具 如果有 折金币
                {
                    if (baby.Family != null && propPrice != null)
                    {
                        await ConvertPropToCoin(propPrice, baby, prop, dangradingStr, playerId);
                        prizeModel.PrizeCoinCount = propPrice.PropValue;
                    }
                }
                else
                {
                    await _babyPropAppService.UpdateFamilyAssetAndBabyAward(new UpdateFamilyAssetAndBabyAwardDto()
                    {
                        Family = baby.Family.MapTo<UpdateFamilyAssetAndBabyAwardFamily>(),
                        Prop = prop.MapTo<BuyBabyPropDto>(),
                        PropPrice = propPrice.MapTo<BabyPropPriceDto>(),
                        BuyProp = new PostBuyPropInput() { BabyId = baby.Id, FamilyId = baby.FamilyId, PlayerGuid = playerId, PriceId = propPrice.Id, PropId = prop.Id }
                    });
                    //家庭消息
                    string content = string.Format(REWARD_PROP_MSG, dangradingStr, prop.Title);
                    await AddFamilyInformations(content, baby, playerId);
                }
            }

            return prizeModel;
        }

        private async Task ConvertPropToCoin(BabyPropPrice propPrice,Baby baby,BabyProp prop,string dangradingStr,Guid playerId)
        {
            //添加金币记录
            await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
            {
                Amount = propPrice.PropValue,
                BabyId = baby.Id,
                FamilyId = baby.FamilyId,
                GetWay = CoinGetWay.PropToCoin,
                GoodsId = prop.Code.ToString(),
                CostType = CoinCostType.DangradingUpgradeReward,
                CurrentFamilyCoinDeposit = baby.Family.Deposit
            });

            var content = string.Format(REWARD_PROP_COIN_MSG, dangradingStr, prop.Title, prop.Title, propPrice.Price);
            //添加家庭消息(自己与对方的)
            await AddFamilyInformations(content, baby, playerId);
            var family = baby.Family;
            family.Deposit += propPrice.PropValue;
            await _familyRepository.UpdateAsync(family);
        }

        private async Task AddFamilyInformations(string content,Baby baby,Guid playerId)
        {
            //家庭消息 自己
            await _informationRepository.InsertAsync(new Information()
            {
                Content = content,
                SenderId = playerId,
                ReceiverId = playerId == baby.Family.FatherId ? baby.Family.MotherId : baby.Family.FatherId,
                State = InformationState.Create,
                Type = InformationType.Event,
                FamilyId = baby.FamilyId
            });
            //家庭消息 对方
            await _informationRepository.InsertAsync(new Information()
            {
                Content = content,
                SenderId = playerId,
                ReceiverId = playerId,
                State = InformationState.Create,
                Type = InformationType.Event,
                FamilyId = baby.FamilyId
            });
        }

        public class GetDangradingModel
        {
            public DanGrading DanGrading { get; set; }

            public int PropCode { get; set; }

            public int ExpireTime { get; set; }
        }

        /// <summary>
        ///获取段位
        /// </summary>
        /// <param name="gamePoint"></param>
        /// <returns></returns>
        private GetDangradingModel GetDanGrading(int gamePoint,int[] dangradValues,string[] propRewards = null)
        {
            GetDangradingModel model = new GetDangradingModel();

            if (dangradValues.Count() < 4)
            {
                throw new Exception("段位数据错误");
            }
            if (gamePoint >= 0 && gamePoint <= dangradValues[0])
            {
                model.DanGrading = DanGrading.Bronze;
            }
            else if (gamePoint >= dangradValues[0] && gamePoint <= dangradValues[1])
            {
                model.DanGrading = DanGrading.Silver;
                model.PropCode = propRewards == null ? 0 : int.Parse(propRewards[0].Split('#')[0]);
                model.ExpireTime = propRewards == null ? 0 : int.Parse(propRewards[0].Split('#')[1]);
            }
            else if (gamePoint >= dangradValues[1] && gamePoint <= dangradValues[2])
            {
                model.DanGrading = DanGrading.Gold;
                model.PropCode = propRewards == null ? 0 : int.Parse(propRewards[1].Split('#')[0]);
                model.ExpireTime = propRewards == null ? 0 : int.Parse(propRewards[1].Split('#')[1]);
            }
            else if (gamePoint >= dangradValues[2] && gamePoint <= dangradValues[3])
            {
                model.DanGrading = DanGrading.Diamond;
                model.PropCode = propRewards == null ? 0 : int.Parse(propRewards[2].Split('#')[0]);
                model.ExpireTime = propRewards == null ? 0 : int.Parse(propRewards[2].Split('#')[1]);
            }
            else if (gamePoint >= dangradValues[3])
            {
                model.DanGrading = DanGrading.King;
                model.PropCode = propRewards == null ? 0 : int.Parse(propRewards[3].Split('#')[0]);
                model.ExpireTime = propRewards == null ? 0 : int.Parse(propRewards[3].Split('#')[1]);
            }
            else
            {
                model.DanGrading = DanGrading.UnKnow;
            }

            return model;
        }

        public class RankingNumber
        {
            public int SelfNumber { get; set; }
            public int OtherNumber { get; set; }
        }

        private async Task<RankingNumber> GetRankingNumber(int seasonId,Competition selfCompetition,Competition otherCompetition)
        {
            var query = GetRankings(seasonId);
            var competitions = await query.ToListAsync();

            var result = new RankingNumber();

            var selfIndex = competitions.FindIndex(c => c.Id == selfCompetition.Id);
            var otherIndex = competitions.FindIndex(c => c.Id == otherCompetition.Id);
            if (selfIndex < 0 || otherIndex < 0)
            {
                throw new Exception("数据或参数错误，请稍后再试");
            }

            result.SelfNumber = selfIndex + 1;

            result.OtherNumber = otherIndex + 1;

            return result;
        }


        public class GetRankingsModel
        {
            public Guid CompetitionId { get; set; }

            public Baby Baby { get; set; }

            public int GamePoint { get; set; }

            public DateTime CreationTime { get; set; }

            public int SelfNumber { get; set; }
        }

        /// <summary>
        /// 获取本赛季的排名
        /// </summary>
        private IQueryable<Competition> GetRankings(int seasonId, int? selfGamePoint = null)
        {
            var tenantId = AbpSession.TenantId ?? 295;
            var query = _repository.GetAll()
                .Include(b => b.Baby)
                .Include(f => f.Family)
                .ThenInclude(f => f.Father)
                .Where(c => c.SeasonManagementId == seasonId && c.Family.IsDeleted == false &&
                      c.Family.Father.TenantId == tenantId && c.Baby.State == BabyState.UnderAge)
                .OrderByDescending(c => c.GamePoint)
                .ThenBy(c => c.CreationTime);
            //var query = _repository.GetAll()
            //  .Include(c => c.Baby)
            //  .Where(c => c.SeasonManagementId == seasonId)
            //  .WhereIf(selfGamePoint != null,c => c.GamePoint >= selfGamePoint)
            //  .Select(c => new GetRankingListOutput { Baby = c.Baby.MapTo<GetRankingListBaby>(), GamePoint = c.GamePoint, CreationTime = c.CreationTime, Id = c.Id,FamilyId = c.FamilyId,DanGrading = c.DanGrading })
            //  .OrderByDescending(c => c.GamePoint)
            //  .ThenBy(c => c.CreationTime);

            return query;
        }

        /// <summary>
        /// 填加对战记录
        /// </summary>
        /// <param name="currentSeason"></param>
        /// <param name="attributeCode"></param>
        /// <param name="input"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task AddFightRecord(SeasonManagement currentSeason, BabyAttributeCode attributeCode, StartFightInput input,FightResult result,RankingAndPoint rankingAndPoint)
        {
            //int? winnerId = null;
            //int? failderId = null;
            //if (result.FightResultEnum == FightResultEnum.Win)
            //{
            //    winnerId = input.BabyId;
            //    failderId = input.OtherBabyId;
            //}
            //else
            //{
            //    winnerId = input.OtherBabyId;
            //    failderId = input.BabyId;
            //}

            //发起人的记录
            await _fightRecordRepository.InsertAsync(new FightRecord()
            {
                SeasonManagementId = currentSeason.Id,
                InitiatorBabyId = input.BabyId.Value,
                OtherBabyId = input.OtherBabyId.Value,
                InitiatorId = input.PlayerId.Value,
                GamePoint = rankingAndPoint.PointChangeValue,
                BabyAttributeCode = attributeCode,
                CurrentPoint = rankingAndPoint.CurrentPoint,
                LastTimePoint = rankingAndPoint.LastPoint,
                FightResultEnum = result.FightResultEnum,
                PropAdditionRate = result.PropAdditionRate,
                AttributeRate = result.AttributeRate,
                WinningRatio = result.WinRate,
                RandomNumber = result.RandomNumber,
                LastDangrading = rankingAndPoint.CurrentDanGrading
            });

        }

        /// <summary>
        /// 获取随机属性
        /// 
        /// </summary>
        private BabyAttributeCode GetRandomAttribute(Baby baby)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var number = random.Next(0, codeList.Count * 3);
            return codeList[number / 3];
        }

        /// <summary>
        /// 计算胜率
        /// </summary>
        private double CalculateWinRate(int selfAttributeValue,int otherAttributeValue)
        {
            var attributeRate = 0.0;

            if (selfAttributeValue == 0 && otherAttributeValue == 0) //都等于0 按1/2计算
            {
                attributeRate = Math.Round(((winCoefficient / 2) / winCoefficient) * 1.0,2);

            }else if (otherAttributeValue == 0)//对方的等于0 按1计算
            {
                attributeRate = winCoefficient / winCoefficient;
            }
            else
            {
                attributeRate = Math.Round(selfAttributeValue * 1.0 / (otherAttributeValue + selfAttributeValue), 2); //自己的 除以 (自己的 + 对方的)
            }

            return attributeRate;
        }

        /// <summary>
        /// 获取对战结果
        /// </summary>
        /// <param name="selfBaby"></param>
        /// <param name="otherBaby"></param>
        /// <param name="attributeCode"></param>
        private async Task<FightResult> GetFightResult(Baby selfBaby,Baby otherBaby, BabyAttributeCode attributeCode)
        {
            var attributeRate = 0.0;
            switch (attributeCode)
            {
                case BabyAttributeCode.UnKnow:
                    break;
                case BabyAttributeCode.IntelligenceCode:
                    attributeRate = CalculateWinRate(selfBaby.Intelligence, otherBaby.Intelligence);
                    break;
                case BabyAttributeCode.PhysiqueCode:
                    attributeRate = CalculateWinRate(selfBaby.Physique, otherBaby.Physique);
                    break;
                case BabyAttributeCode.ImagineCode:
                    attributeRate = CalculateWinRate(selfBaby.Imagine, otherBaby.Imagine);
                    break;
                case BabyAttributeCode.WillPowerCode:
                    attributeRate = CalculateWinRate(selfBaby.WillPower, otherBaby.WillPower);
                    break;
                case BabyAttributeCode.EmotionQuotientCode:
                    attributeRate = CalculateWinRate(selfBaby.EmotionQuotient, otherBaby.EmotionQuotient);
                    break;
                case BabyAttributeCode.CharmCode:
                    attributeRate = CalculateWinRate(selfBaby.Charm, otherBaby.Charm);
                    break;
                default:
                    break;
            }

            //计算道具加成
            var babyAssetRatesQuery = _babyAssetFeatureRepository.GetAll()
                .Where(a => a.BabyId == selfBaby.Id || a.BabyId == otherBaby.Id)
                .Select(x => new { x.AssetFeatureProperty, x.BabyId });
            
            var selfAssetRate = 0.0;
            var otherAssetRate = 0.0;
            var selfAssetFeature = await babyAssetRatesQuery.FirstOrDefaultAsync(a => a.BabyId == selfBaby.Id);
            var otherAssetFeature = await babyAssetRatesQuery.FirstOrDefaultAsync(a => a.BabyId == otherBaby.Id);
            if (selfAssetFeature != null)
            {
                
                var selfFeatureValues = JsonConvert.DeserializeObject<List<PropAdditionModel>>(selfAssetFeature.AssetFeatureProperty)/*["IncreaseArenaFightWinProbability"]*/;

                var selfadditionModel = selfFeatureValues.FirstOrDefault(x => x.Name == "IncreaseArenaFightWinProbability");

                if (selfadditionModel != null)
                {
                    selfAssetRate = Math.Round((double.Parse(selfadditionModel.Value)),2);
                }
            }
            if (otherAssetFeature != null)
            {
                var otherFeatureValues = JsonConvert.DeserializeObject<List<PropAdditionModel>>(otherAssetFeature.AssetFeatureProperty)/*["IncreaseArenaFightWinProbability"]*/;
                var otherAdditionModel = otherFeatureValues.FirstOrDefault(x => x.Name == "IncreaseArenaFightWinProbability");
                if (otherAdditionModel != null)
                {
                    otherAssetRate = Math.Round((double.Parse(otherAdditionModel.Value)),2);
                }
            }
            //两者胜利率相差(有可能是负数)
            var propRatioDiff = selfAssetRate - otherAssetRate;
            Logger.Warn($"属性加成是：{attributeRate},自己的道具加成是：{selfAssetRate},对方的道具加成是：{otherAssetRate}");

            //总加成(属性 + 道具)
            var totalRate = attributeRate + propRatioDiff;
            if (totalRate < 0)
            {
                totalRate = 0;
            }
            if (totalRate > 1)
            {
                totalRate = 1;
            }

            var winnerBabyId = 0;
            var fightResultEnum = FightResultEnum.Intialize;
            //生成随机数 如果随机数大于 胜率 则赢 否则输
            var random = new Random();
            var randomNumber = random.Next(0, winCoefficient) * 0.01 ;
            if (totalRate >= randomNumber)
            {
                winnerBabyId = selfBaby.Id;
                fightResultEnum = FightResultEnum.Win;

            }
            else
            {
                winnerBabyId = otherBaby.Id;
                fightResultEnum = FightResultEnum.Fail;
            }

            return new FightResult()
            {
                RandomNumber = randomNumber,
                WinRate = totalRate,
                WinnerBabyId = winnerBabyId,
                FightResultEnum = fightResultEnum,
                PropAdditionRate = selfAssetRate,
                AttributeRate = attributeRate
            };
        }

        internal override IQueryable<Competition> GetQuery(GetCompetitionInput model)
        {
            var query = _repository.GetAll()
                .OrderByDescending(t => t.GamePoint)
                ;

            return query;
        }

        /// <summary>
        /// 当前的排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RankingModel> GetRankingList(GetRankingListInput input)
        {
            if (!input.BabyId.HasValue)
            {
                throw new Exception("参数有误");
            }

           var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);

            var query = GetRankings(currentSeason.Id);

            //获取排行榜前RankingShowPlayerCount名
            var rankingQuery = query
              .Take(currentSeason.RankingShowPlayerCount);

            var count = await rankingQuery.CountAsync();

            var entityList = await rankingQuery
                    .AsNoTracking()
                    .ToListAsync();

            var output = new RankingModel();
            var selfTemp = new Competition();
            var isHasSelf = entityList.Any(c => c.BabyId == input.BabyId);
            if (isHasSelf)
            {
                selfTemp = entityList.FirstOrDefault(c => c.BabyId == input.BabyId);
                output.SelfRankingInfo = selfTemp.MapTo<GetRankingListOutput>();
                output.SelfRankingInfo.SelfNumber = entityList.FindIndex(x => x.BabyId == input.BabyId) + 1;
            }
            else
            {
                selfTemp = await query.FirstOrDefaultAsync(c => c.BabyId == input.BabyId);

                if (selfTemp == null)
                {
                    throw new Exception("该宝宝未参加竞技场");
                }

                var overSelfList = await query.Where(c => c.GamePoint >= selfTemp.GamePoint).ToListAsync();

                output.SelfRankingInfo = selfTemp.MapTo<GetRankingListOutput>();
                output.SelfRankingInfo.SelfNumber = overSelfList.FindIndex(x => x.BabyId == input.BabyId) + 1;
            }

            var entityListDtos = entityList.MapTo<List<GetRankingListOutput>>();

            output.RankingListDto = new PagedResultDto<GetRankingListOutput>(count, entityListDtos);

            return output;
        }

        /// <summary>
        /// 某一期的排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<RankingModel> GetLatestRankingList(GetRankingListInput input)
        //{
        //    var output = new RankingModel();
        //    var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);
        //    if (currentSeason == null)
        //    {
        //        throw new Exception("当前赛季不存在");
        //    }
        //    var lastSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.SeasonNumber == currentSeason.SeasonNumber - 1);
        //    if (lastSeason == null)
        //    {
        //        throw new Exception("未找到上个赛季");
        //    }
        //    var query = GetRankings(lastSeason.Id);
        //    var rankingQuery = query
        //      .Take(lastSeason.RankingShowPlayerCount);

        //    var count = await rankingQuery.CountAsync();

        //    var entityList = await rankingQuery
        //            .AsNoTracking()
        //            .ToListAsync();
        //    if (input.BabyId.HasValue)
        //    {

        //        var isHasSelf = entityList.Any(c => c.BabyId == input.BabyId);
        //        if (isHasSelf)
        //        {
        //            output.SelfRankingInfo = entityList.FirstOrDefault(c => c.BabyId == input.BabyId);
        //            output.SelfRankingInfo.SelfNumber = entityList.FindIndex(x => x.BabyId == input.BabyId) + 1;
        //        }
        //        else
        //        {
        //            var selfInfo = await _repository.GetAll()
        //             .Where(c => c.SeasonManagementId == currentSeason.Id && c.BabyId == input.BabyId)
        //             .Select(c => new GetRankingListOutput { Baby = c.Baby.MapTo<GetRankingListBaby>(), GamePoint = c.GamePoint, CreationTime = c.CreationTime, CompetitionId = c.Id })
        //             .SingleOrDefaultAsync();

        //            if (selfInfo == null)
        //            {
        //                throw new Exception("该宝宝未参加竞技场");
        //            }

        //            var overSelfList = await query.Where(c => c.GamePoint >= selfInfo.GamePoint).ToListAsync();

        //            output.SelfRankingInfo = overSelfList.SingleOrDefault(c => c.BabyId == input.BabyId);
        //            output.SelfRankingInfo.SelfNumber = overSelfList.FindIndex(x => x.BabyId == input.BabyId) + 1;
        //        }
        //    }

        //    var entityListDtos = entityList.MapTo<List<GetRankingListOutput>>();

        //    output.RankingListDto = new PagedResultDto<GetRankingListOutput>(count, entityListDtos);

        //    return output;
        //}

        public async Task<PagedResultDto<GetRankingListOutput>> GetFightList(GetRankingListInput input)
        {
            if (!input.BabyId.HasValue)
            {
                throw new Exception("参数有误");
            }
            var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);

            var entityListDtos = new List<GetRankingListOutput>();

            var canShowFrontCount = currentSeason.CanPKCount / 2;
            var query =  GetRankings(currentSeason.Id);
            var allRankingList = await query.ToListAsync();

            //获取自己的排名信息
            var selfTemp = allRankingList.FirstOrDefault(c => c.BabyId == input.BabyId);
            if (selfTemp == null)
            {
                throw new Exception("数据参数错误，请稍后重试");
            }
            var selfInfo = selfTemp.MapTo<GetRankingListOutput>();
            selfInfo.SelfNumber = allRankingList.FindIndex(c => c.BabyId == input.BabyId) + 1;
            //获取后canShowFrontCount名
            var backList = allRankingList.Skip(selfInfo.SelfNumber).Take(canShowFrontCount);
            //获取前canShowFrontCount名
            var tempList = allRankingList.OrderBy(c => c.GamePoint).ThenByDescending(c => c.CreationTime).ToList();
            var tempIndex = tempList.FindIndex(c => c.BabyId == input.BabyId);
            var frontList = tempList.Skip(tempIndex + 1).Take(canShowFrontCount);
            var tempFrontList = frontList.OrderByDescending(c => c.GamePoint).ThenBy(c => c.CreationTime);
            entityListDtos.AddRange(tempFrontList.MapTo<List<GetRankingListOutput>>());
            //自己
            entityListDtos.Add(selfInfo);
            entityListDtos.AddRange(backList.MapTo<List<GetRankingListOutput>>());

            var babyIds = entityListDtos.Select(x => x.BabyId).ToList();
            var familyIds = entityListDtos.Select(x => x.FamilyId).ToList();
            var result = IsEquipmentProps(babyIds,familyIds);

            foreach (var item in entityListDtos)
            {
                var model = result.FirstOrDefault(c => c.FamilyId == item.FamilyId);
                if (model != null)
                {
                    item.Baby.IsHasButler = model.IsHasButler;
                    item.Baby.IsHasHouse = model.IsHasHouse;
                    item.Baby.IsHasServant = model.IsHasServant;
                    item.Baby.IsHasCar = model.IsHasCar;
                    item.Baby.IsHasSkip = model.IsHasSkip;
                    item.Baby.SkinCode = model.SkinCode;
                }
            }
            return new PagedResultDto<GetRankingListOutput>(currentSeason.CanPKCount, entityListDtos);
        }

        public async Task<GetAthleticsInfoOutput> GetAthleticsInfo()
        {
            var output = new GetAthleticsInfoOutput();

            var currentSeason = await GetSeasonInfo();

            currentSeason.MapTo(output);
            
            return output;
        }

        private async Task<SeasonManagement> GetSeasonInfo()
        {
            var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);

            if (currentSeason == null)
            {
                var systemConfig = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1002);

                var configValue = JsonConvert.DeserializeObject<SeasonManagement>(systemConfig.Value);
                configValue.IsCurrent = true;
                configValue.SeasonNumber = 1;
                configValue.StartTime = configValue.StartTime.ToUniversalTime();
                configValue.EndTime = DateTime.MaxValue/*configValue.EndTime.ToUniversalTime()*/;

                currentSeason = await _seasonManagementRepository.InsertAsync(configValue);
            }


            //var utcNowTime = DateTime.UtcNow;
            //if (utcNowTime >= currentSeason.EndTime.AddSeconds(-1))
            //{

            //    currentSeason.IsCurrent = false;
            //    await _seasonManagementRepository.UpdateAsync(currentSeason);

            //    var systemConfig = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1002);

            //    var configValue = JsonConvert.DeserializeObject<SeasonManagement>(systemConfig.Value);
            //    configValue.IsCurrent = true;
            //    configValue.SeasonNumber = currentSeason.SeasonNumber + 1;
            //    configValue.StartTime = configValue.StartTime.ToUniversalTime();
            //    configValue.EndTime = GetMaxTime()/*configValue.EndTime.ToUniversalTime()*/;

            //    currentSeason = await _seasonManagementRepository.InsertAsync(configValue);

            //    //清除上次的定时任务
            //    RecurringJob.RemoveIfExists("TimingAwardPrize");

            //    //定时任务
            //    var localTime = currentSeason.EndTime.ToLocalTime();
            //    var hour = localTime.Hour;
            //    var minute = localTime.Minute;
            //    RecurringJob.AddOrUpdate<CompetitionApplicationService>("TimingAwardPrize", (s) => s.TimingAwardPrize(), $"{minute} {hour} * * *");
            //}
            return currentSeason;
        }

        /// <summary>
        /// 获取最大时间
        /// </summary>
        /// <returns></returns>
        private DateTime GetMaxTime()
        {
            long ticksNumber = Int64.MaxValue;

            DateTime validTime = DateTime.Now.AddYears(999);

            if (ticksNumber >= DateTime.MinValue.Ticks && ticksNumber <= DateTime.MaxValue.Ticks)
            {
                validTime = new DateTime(ticksNumber);
            }
            else
            {
                throw new ArgumentOutOfRangeException("时间小于或超出范围");
            }
            return validTime;
        }

        //[UnitOfWork]
        public async Task<BuyFightCountOutput> BuyFightCount(BuyFightCountInput input)
        {
            var output = new BuyFightCountOutput();

            if (!input.BabyId.HasValue || !input.FamilyId.HasValue || !input.PlayerId.HasValue)
            {
                throw new Exception("参数有误");
            }

            if (input.Count == 0)
            {
                throw new Exception("购买次数不能为0");
            }

            var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);

            var coinCount = currentSeason.Price * input.Count;

            var family = await _familyRepository.FirstOrDefaultAsync(f => f.Id == input.FamilyId);

            if (family.Deposit < coinCount)
            {
                output.ErrorCode = BuyFightCountErrorCode.NotEnough;
            }
            else
            {
                var entity = await _repository.FirstOrDefaultAsync(c => c.BabyId == input.BabyId && c.SeasonManagementId == currentSeason.Id);

                if (entity == null)
                {
                    output.ErrorCode = BuyFightCountErrorCode.Fail;
                }
                else
                {
                    var totalCount = input.PlayerId == family.FatherId ? entity.FatherFightCount : entity.MotherFightCount;

                    if (totalCount > currentSeason.MaxFightCount)
                    {
                        output.ErrorCode = BuyFightCountErrorCode.BuyFightCountLimit;
                    }
                    else
                    {
                        family.Deposit -= coinCount;

                        await _familyRepository.UpdateAsync(family);

                        if (input.PlayerId == family.FatherId)
                        {
                            entity.FatherFightCount += input.Count;
                        }
                        else
                        {
                            entity.MotherFightCount += input.Count;
                        }

                        await _repository.UpdateAsync(entity);

                        await _buyFightCountRecordRepository.InsertAsync(new BuyFightCountRecord()
                        {
                            PurchaserId = input.PlayerId,
                            SeasonManagementId = currentSeason.Id,
                            SourceType = SourceType.Exchange,
                            FightCount = input.Count,
                            FamilyId = input.FamilyId,
                            CoinCount = input.Count,
                            BabyId = input.BabyId.Value
                        });

                        await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
                        {
                            Amount = coinCount,
                            BabyId = input.BabyId,
                            FamilyId = entity.FamilyId,
                            GetWay = CoinGetWay.PropToCoin,
                            //StakeholderId = input.PlayerGuid,
                            CurrentFamilyCoinDeposit = family.Deposit
                        });

                        output.CoinCount = family.Deposit;
                    }
                }
            }
            return output;
        }

        //public async Task<AthleticsInformation[]> GetAthleticsInformations(GetAthleticsInformationsInput input)
        //{
        //    //var informations = await RedisHelper.LRangeAsync<AthleticsInformation>(input.PlayerId, 0, 10);

        //    //return informations;

        //    var informations = await
        //}

        public async Task<PagedResultDto<GetAthleticsInformationsListDtos>> GetAthleticsInformations(GetAthleticsInformationsInput input)
        {
            var season = await _seasonManagementRepository.SingleAsync(s => s.IsCurrent == true);

            var query = _athleticsinformationRepository.GetAll()
                .Include(p => p.Receiver)
                .Where(s => s.SeasonManagementId == season.Id)
                .WhereIf(input.Type.HasValue, c => c.AthleticsInformationType == input.Type)
                .WhereIf(input.PlayerId.HasValue, c => c.ReceiverId == input.PlayerId)
                .WhereIf(input.FamilyId.HasValue, c => c.FamilyId == input.FamilyId);

            var count = await query.CountAsync();

            var entityList = await query
                    .AsNoTracking()
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetAthleticsInformationsListDtos>>();

            return new PagedResultDto<GetAthleticsInformationsListDtos>(count, entityListDtos);
        }

        //public async Task<PagedResultDto<InformationListDto>> GetAthleticsInformations(GetInformationsInput input)
        //{
        //    //var informations = await RedisHelper.LRangeAsync(AthleticsInformationRedisKey.AthleticsInformation, 0, 10);

        //    //return informations;

        //    var query = _informationRepository.GetAll()
        //        .Include(p => p.Receiver)
        //        .WhereIf(input.InformationType.HasValue, c => c.Type == input.InformationType)
        //        .WhereIf(input.PlayerId.HasValue, c => c.ReceiverId == input.PlayerId)
        //        .WhereIf(input.PlayerId.HasValue, c => c.FamilyId == input.FamilyId);

        //    var count = await query.CountAsync();

        //    var entityList = await query
        //            .AsNoTracking()
        //            .OrderBy(input.Sorting)
        //            .PageBy(input)
        //            .ToListAsync();

        //    var entityListDtos = entityList.MapTo<List<InformationListDto>>();

        //    return new PagedResultDto<InformationListDto>(count, entityListDtos);
        //}

        public BabyAttributeCode TestRandomAttribute(int babyId)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var number = random.Next(0, codeList.Count * 3);
            return codeList[number / 3];
        }

        public async Task<TestFightResultOutput> TestFightResult(TestFightResultInput input)
        {
            var selfBaby = await _babyRepository.GetAsync(input.SelfBabyId);

            var otherBaby = await _babyRepository.GetAsync(input.OtherBabyId);

            var ratio = 0.0;
            switch (input.AttributeCode)
            {
                case BabyAttributeCode.UnKnow:
                    break;
                case BabyAttributeCode.IntelligenceCode:
                    ratio = CalculateWinRate(selfBaby.Intelligence, otherBaby.Intelligence);
                    break;
                case BabyAttributeCode.PhysiqueCode:
                    ratio = CalculateWinRate(selfBaby.Physique, otherBaby.Physique);
                    break;
                case BabyAttributeCode.ImagineCode:
                    ratio = CalculateWinRate(selfBaby.Imagine, otherBaby.Imagine);
                    break;
                case BabyAttributeCode.WillPowerCode:
                    ratio = CalculateWinRate(selfBaby.WillPower, otherBaby.WillPower);
                    break;
                case BabyAttributeCode.EmotionQuotientCode:
                    ratio = CalculateWinRate(selfBaby.EmotionQuotient, otherBaby.EmotionQuotient);
                    break;
                case BabyAttributeCode.CharmCode:
                    ratio = CalculateWinRate(selfBaby.Charm, otherBaby.Charm);
                    break;
                default:
                    break;
            }
            var winnerBabyId = 0;
            var fightResultEnum = FightResultEnum.Intialize;
            var random = new Random();
            var randomNumber = 0.00;
            if (ratio > 0 && ratio <= 10000)
            {
                randomNumber = random.Next(1, 10000);
                if (ratio >= randomNumber)
                {
                    winnerBabyId = selfBaby.Id;
                    fightResultEnum = FightResultEnum.Win;

                }
                else
                {
                    winnerBabyId = otherBaby.Id;
                    fightResultEnum = FightResultEnum.Fail;
                }
            }

            return new TestFightResultOutput()
            {
                Result = fightResultEnum == FightResultEnum.Win ? "发起方赢了" + "胜率是：" + ratio + "%" + "随机数：" + randomNumber : "发起方输了" + "胜率是：" + ratio + "%" + "随机数：" + randomNumber,
                WinRatio = ratio
            };
        }


        public void RemoveAthleticsInformations(string playerId)
        {

        }
    }
}
