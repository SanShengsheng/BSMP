using Abp;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.Asset;
using MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Common.MqAgents.Agents;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner;
using MQKJ.BSMP.Common.RunHorseInformations;
using MQKJ.BSMP.Common.SensitiveWords;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.BabySystemSetting;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Baby应用层服务的接口实现方法  
    ///</summary>
    public class BabyAppService : BsmpApplicationServiceBase<Baby, int, BabyEditDto, BabyEditDto, GetBabysInput, BabyListDto>, IBabyAppService
    {
        private readonly IRepository<Baby, int> _entityRepository;
        private readonly IRepository<Family, int> _familyRepository;
        private readonly IRepository<Player, Guid> _playerRepository;
        private readonly IRepository<EventGroup, int> _eventGroupRepository;
        private readonly IRepository<SensitiveWord, int> _sensitiveWordRepository;
        private readonly IRepository<RunHorseInformation, Guid> _runInformationRepository;
        private readonly IRepository<CoinRechargeRecord, Guid> _coinRechargeRecordRepository;
        private readonly IRepository<BabyEnding, int> _babyEndingRepository;
        private readonly IRepository<MqAgent> _agentRepository;
        private readonly IRepository<BabyEventRecord, Guid> _babyEventRecordRepository;
        private readonly IRepository<SystemSetting> _systemSettingRepository;
        //private readonly IDistributedCache _redisCache;
        private readonly IRepository<BabyAssetAward, Guid> _babyAssetAwardRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;
        private readonly IRepository<BabyPropPrice, int> _babyPropPriceRepository;
        //private readonly IRepository<Competition, Guid> _competitionRepository;
        //private readonly IRepository<SeasonManagement> _seasonManagementRepository;
        private readonly IRepository<PlayerProfession, int> _playerProfessionRepository;
        private readonly IRepository<BabyAssetFeatureRecord, Guid> _babyAssetFeatureRecordRepository;
        private readonly IRepository<BabyGrowUpRecord, Guid> _babyGrowUpRecordRepository;
        private readonly IRepository<Information, Guid> _informationRepository;
        private readonly IRepository<DismissFamilyRecord, Guid> _dismissFamilyRecordRepository;

        // services
        private readonly ICompetitionApplicationService _competitionService;
        private readonly IBabyAssetFeatureApplicationService _babyAssetFeatureApplicationService;

        public BabyAppService(IRepository<Baby, int> repository,
            IRepository<Family, int> familyRepository,
            IRepository<Player, Guid> playerRepository,
            IRepository<EventGroup, int> eventGroupRepository,
            IRepository<SensitiveWord, int> sensitiveWordRepository,
            IRepository<RunHorseInformation, Guid> runInformationRepository,
            IRepository<CoinRechargeRecord, Guid> coinRechargeRecordRepository,
            IRepository<MqAgent> agentRepository,
            IRepository<BabyEnding, int> babyEndingRepository,
            IRepository<BabyEventRecord, Guid> babyEventRecordRepository,
            IRepository<SystemSetting> systemSettingRepository,
            IRepository<BabyAssetAward, Guid> babyAssetAwardRepository,
            IRepository<BabyFamilyAsset, Guid> babyFamilyAssetRepository,
            IRepository<BabyPropPrice, int> babyPropPriceRepository,
            IRepository<PlayerProfession, int> playerProfessionRepository,
            //IRepository<Competition, Guid> competitionRepository,
            //IRepository<SeasonManagement> seasonManagementRepository,
            IRepository<BabyAssetFeatureRecord, Guid> babyAssetFeatureRecordRepository,
            IRepository<BabyGrowUpRecord, Guid> babyGrowUpRecordRepository,
            IRepository<Information, Guid> informationRepository,
            IRepository<DismissFamilyRecord, Guid> dismissFamilyRecordRepository,
            //services
            IBabyAssetFeatureApplicationService babyAssetFeatureApplicationService,
        CompetitionApplicationService competitionService
            ) : base(repository)
        {
            _entityRepository = repository;
            _familyRepository = familyRepository;
            _playerRepository = playerRepository;
            _eventGroupRepository = eventGroupRepository;
            _sensitiveWordRepository = sensitiveWordRepository;
            _runInformationRepository = runInformationRepository;
            _coinRechargeRecordRepository = coinRechargeRecordRepository;
            _babyEndingRepository = babyEndingRepository;
            _agentRepository = agentRepository;
            _babyEventRecordRepository = babyEventRecordRepository;
            _systemSettingRepository = systemSettingRepository;
            //_redisCache = redisCache;
            _babyAssetAwardRepository = babyAssetAwardRepository;
            _babyFamilyAssetRepository = babyFamilyAssetRepository;
            _babyPropPriceRepository = babyPropPriceRepository;
            //_competitionRepository = competitionRepository;
            _playerProfessionRepository = playerProfessionRepository;
            _babyAssetFeatureRecordRepository = babyAssetFeatureRecordRepository;
            _babyGrowUpRecordRepository = babyGrowUpRecordRepository;
            _informationRepository = informationRepository;
            _dismissFamilyRecordRepository = dismissFamilyRecordRepository;
            //_seasonManagementRepository = seasonManagementRepository;
            _competitionService = competitionService;
            _babyAssetFeatureApplicationService = babyAssetFeatureApplicationService;
        }

        public async Task<BabyBirthRecordOutput> BabyBirthRecord(BabyBirthRecordInput input)
        {
            var response = new BabyBirthRecordOutput();
            try
            {
                //基本信息
                var baby = await _entityRepository.GetAsync(input.BabyId);
                //出生属性级别
                var attribut = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1007 && s.Name == "AttributeDetermine");

                if (baby != null)
                {
                    response.BabyBirthInfo = ObjectMapper.Map<BabyBirthInfo>(baby);
                    //出生属性
                    response.BabyBirthProperty = ObjectMapper.Map<BabyBirthRecordOutputBabyProperty>(baby);
                    //属性级别
                    string[] strArray = ObjectMapper.Map<BabyAttributDetermine>(attribut).Value.Split(",");
                    response.Levels = Array.ConvertAll<string, int>(strArray, s => int.Parse(s));
                }
                else
                {
                    throw new BSMPException(0, "", "宝宝不存在！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public Task<BabyGrowUpOutput> BabyGrowUp(BabyGrowUpInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 宝宝排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BabyRankListOutput> BabyRankList(BabyRankListInput input)
        {
            var response = new BabyRankListOutput();
            try
            {
                var baby = await _entityRepository.GetAllIncluding(b => b.Family)
                    .FirstOrDefaultAsync(b => b.Id == input.BabyId);
                var families = await _familyRepository.GetAllListAsync();
                var babySort = families.IndexOf(baby.Family);
                var top10Family = families.OrderByDescending(s => s.Deposit).Take(10).ToList();
                var top10FamilyIds = top10Family.Select(s => s.Id).ToList();
                var result = _entityRepository.GetAllIncluding(f => f.Family).Where(f => top10FamilyIds.Contains(f.FamilyId)).Take(9).ToList().Select((s, sort) => new BabyRankDto
                {
                    BabyName = s.Name,
                    BabyRank = sort,
                    Deposit = s.Family.Deposit,
                    GrowthTotal = 1000,
                    Potential = 5000
                });
                var babyRank = new BabyRankDto
                {
                    BabyName = baby.Name,
                    BabyRank = babySort,
                    Deposit = baby.Family.Deposit,
                    GrowthTotal = 1000,
                    Potential = 5000
                };
                result.ToList().Add(babyRank);//将当前宝宝加入到队列最后
                response.BabyRankDtos = result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// 宝宝排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BabyRankingList_V2Output> BabyRankList_V2(BabyRankingList_V2Input input)
        {
            var output = new BabyRankingList_V2Output();

            if (!input.BabyId.HasValue)
            {
                throw new Exception("参数不能为空");
            }

            var count = 100;
            var tenantId = AbpSession.TenantId ?? 295;
            var babies = _entityRepository
                .GetAll()
                .Include(f => f.Family)
                .ThenInclude(f => f.Father)
                .Where(f => f.State == BabyState.UnderAge && f.Family.IsDeleted == false && f.Family.Father.TenantId == tenantId)
                .Select(x => new BabyRankingModel()
                {
                    PropertyDto = new BabyPropertyDto()
                    {
                        Intelligence = x.Intelligence,
                        Charm = x.Charm,
                        EmotionQuotient = x.EmotionQuotient,
                        Physique = x.Physique,
                        WillPower = x.WillPower,
                        Imagine = x.Imagine,
                    },
                    TotalValue = x.Intelligence + x.Charm + x.EmotionQuotient + x.Physique + x.WillPower + x.Imagine,
                    Age = x.Age,
                    AgeDouble = x.AgeDouble,
                    AgeString = x.AgeString,
                    FamilyId = x.FamilyId,
                    BabyId = x.Id,
                    BabyName = x.Name
                })
                .OrderByDescending(x => x.TotalValue)
                .ThenBy(c => c.CreationTime);

            //var count = await babies.CountAsync();

            var totalList = await babies.AsNoTracking()
                .ToListAsync();

            var entityListDtos = totalList.Take(count);
            var i = 1;
            foreach (var item in totalList)
            {
                item.RankingNumber = i++;
            }

            if (input.PageSize <= 0)
            {
                input.PageSize = input.MaxResultCount;
            }
            if (input.PageIndex <= 0)
            {
                input.PageIndex = input.SkipCount;
            }
            var resultDtos = totalList.Take(count)/*Skip(input.PageIndex).Take(input.PageSize)*/.ToList();

            output.selfInfo = totalList.FirstOrDefault(x => x.BabyId == input.BabyId.Value).CheckNull("该宝宝不存在");
            output.selfInfo.RankingNumber = totalList.FindIndex(x => x.BabyId == input.BabyId.Value) + 1;
            output.pagedResultDto = new PagedResultDto<BabyRankingModel>(count, resultDtos);

            return output;
        }

        public class PropPriceModel
        {
            public int FamilyId { get; set; }
            public int BabyId { get; set; }

            public double PropPrice { get; set; }
        }

        public async Task<GetAssetRankingListOutput> GetAssetRankingList(GetAssetRankingListInput input)
        {
            var output = new GetAssetRankingListOutput();

            if (!input.BabyId.HasValue)
            {
                throw new Exception("参数不能为空!");
            }

            var count = 100;

            var tenantId = AbpSession.TenantId ?? 295;
            var babies = await _repository.GetAll()
                .Include(b => b.Family)
                .ThenInclude(f => f.Father)
                .Where(b => b.State == BabyState.UnderAge && b.Family.IsDeleted == false && b.Family.Father.TenantId == tenantId)
                .Select(x => new GetAssetRankingListModel()
                {
                    Asset = x.Family.Deposit,
                    BabyName = x.Name,
                    BabyId = x.Id,
                    FamilyId = x.FamilyId
                })
                .ToListAsync();


            //var totalList = await babies.AsNoTracking()
            //    .ToListAsync();

            //var count = totalList.Count;

            //var enumerables = totalList.Take(count);

            //var familyIds = enumerables.Select(x => x.FamilyId);

            //var babyIds = enumerables.Select(x => x.BabyId);

            //获取宝宝的道具   资产需要加当前道具的价值
            var babyAssets = _babyFamilyAssetRepository.GetAll()
                .Include(p => p.BabyProp)
                .Include(p => p.BabyPropPrice)
                .Where(c => c.ExpiredDateTime > DateTime.UtcNow || c.ExpiredDateTime == null)
                .Select(x => new PropPriceModel()
                {
                    BabyId = x.OwnId == null ? 0 : x.OwnId.Value,
                    FamilyId = x.FamilyId,
                    PropPrice = x.BabyPropPrice.PropValue
                });

            foreach (var item in babyAssets)
            {
                var baby = babies.FirstOrDefault(x => x.FamilyId == item.FamilyId);
                if (baby != null && item.BabyId == baby.BabyId)
                {
                    baby.Asset += item.PropPrice;
                }
            }

            var entityListDtos = babies.OrderByDescending(c => c.Asset).ThenBy(c => c.CreationTime).ToList();

            //for (int i = 0; i < entityListDtos.Count; i++)
            //{
            //    entityListDtos[i].EquipmentProp = IsEquipmentProp(entityListDtos[i].BabyId, entityListDtos[i].FamilyId);

            //    entityListDtos[i].Asset += babyAssets.Where(x => x.FamilyId == entityListDtos[i].FamilyId).Sum(p => p.PropPrice);
            //    if (i == 0)
            //    {
            //        entityListDtos[i].RankingNumber = 1;
            //    }
            //    if (i > 0 && entityListDtos[i - 1].Asset > entityListDtos[i].Asset)//前一个大于后一个的资产
            //    {
            //        entityListDtos[i].RankingNumber = entityListDtos[i - 1].RankingNumber + 1;
            //    }
            //    else if (i > 0 && entityListDtos[i - 1].Asset < entityListDtos[i].Asset)
            //    {
            //        var entity = entityListDtos[i - 1];
            //        entityListDtos[i].RankingNumber = entity.RankingNumber;//把上个家庭的排名赋值给当前家庭的排名
            //        entityListDtos[i - 1] = entityListDtos[i];//把当前的家庭放到它的上个家庭的位置
            //        entityListDtos[i] = entity;//把上个家庭的位置放到当前位置
            //        entityListDtos[i].RankingNumber = entity.RankingNumber + 1;//更新排名
            //    }
            //}

            if (input.PageSize <= 0)
            {
                input.PageSize = input.MaxResultCount;
            }
            if (input.PageIndex <= 0)
            {
                input.PageIndex = input.SkipCount;
            }
            var resultDtos = entityListDtos.Take(count)/*Skip(input.PageIndex).Take(input.PageSize)*/.ToList();

            output.SelfInfo = entityListDtos.FirstOrDefault(x => x.BabyId == input.BabyId.Value);

            output.SelfInfo.RankingNumber = entityListDtos.FindIndex(x => x.BabyId == input.BabyId.Value) + 1;

            output.pagedResultDto = new PagedResultDto<GetAssetRankingListModel>(count, resultDtos);

            return output;

        }

        /// <summary>
        /// 宝宝列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<GetBabiesOutput> GetBabies(GetBabiesInput input)
        {
            var response = new GetBabiesOutput() { Babies = new List<GetBabiesDto>() };
            try
            {
                // 获取每个家庭最新的宝宝的编号
                var familyBabyMaxId = await _entityRepository.GetAllIncluding(s => s.Family)
                    .Where(s => (s.Family.MotherId == input.PlayerGuid || s.Family.FatherId == input.PlayerGuid))
                    .AsNoTracking()
                    .GroupBy(s => s.FamilyId)
                    .Select(s => s.Max(x => x.Id))
                    .ToListAsync();

                var result = await _entityRepository.GetAllIncluding()
                    .Include(s => s.Family).ThenInclude(s => s.Father)
                    .Include(s => s.Family).ThenInclude(s => s.Mother)
                    .Where(s => familyBabyMaxId.Contains(s.Id))
                    .WhereIf(input.Gender != 0, s => s.Gender == input.Gender)
                    .WhereIf(input.Age != -1 && input.Age != null, s => s.AgeDouble >= input.Age && s.AgeDouble < input.Age + 1)
                    .WhereIf(!String.IsNullOrEmpty(input.Keyword),
                            s => (s.Name != null && s.Name.Contains(input.Keyword)) ||
                                 (s.Family.Mother.NickName.Contains(input.Keyword) ||
                                 s.Family.Father.NickName.Contains(input.Keyword)))
                    .WhereIf(input.State != 0, s => s.State == input.State)
                    .AsNoTracking()
                    .ToListAsync();


                if (result != null && result.Any())
                {
                    foreach (var item in result)
                    {
                        var baby = ObjectMapper.Map<GetBabiesOutputBaby>(item);

                        var familyItem = new GetBabiesOutputFamily()
                        {
                            FamilyId = item.FamilyId,
                            //IsHasUnderAgeBaby = item.Family.Babies.Any(s => s.State == BabyState.UnderAge),
                            Dad = new GetBabiesOutputParent()
                            {
                                Name = item.Family.Father.NickName,
                                HeadPicture = item.Family.Father.HeadUrl,
                                PlayerGuid = item.Family.FatherId
                            },
                            Mom = new GetBabiesOutputParent()
                            {
                                Name = item.Family.Mother.NickName,
                                HeadPicture = item.Family.Mother.HeadUrl,
                                PlayerGuid = item.Family.MotherId
                            }
                        };
                        var getBabiesDto = new GetBabiesDto()
                        {
                            Baby = baby,
                            Family = familyItem
                        };
                        response.Babies.Add(getBabiesDto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 宝宝列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBabiesByPageOutput> GetBabiesByPage(GetBabiesInput input)
        {
            var response = new GetBabiesByPageOutput() { };
            var babies = new List<GetBabiesDto>();

            // var records = _dismissFamilyRecordRepository.GetAll()
            //     .Include(f => f.Family)
            //     .Where(r => r.Family.FamilyState == FamilyState.Dismissing && r.ExpireTime <= DateTime.UtcNow);
            // await records.ForEachAsync(async item =>
            //{
            //    var family = item.Family;
            //    family.FamilyState = FamilyState.Normal;
            //    await _familyRepository.UpdateAsync(family);
            //});

            // 获取每个家庭最新的宝宝的编号
            var familyBabyMaxId = await _entityRepository.GetAllIncluding(s => s.Family)
                .Where(s => !s.Family.IsDeleted)
                .Where(s => (s.Family.MotherId == input.PlayerGuid || s.Family.FatherId == input.PlayerGuid)
                && !s.Family.Father.IsDeleted && !s.Family.Mother.IsDeleted)
                .GroupBy(s => s.FamilyId)
                .AsNoTracking()
                .Select(s => s.Max(x => x.Id))
                .ToListAsync();

            //foreach (var item in records)
            //{
            //    familyBabyMaxId.Remove(item.FamilyId);
            //}
            var isAgent = await _agentRepository.GetAll()
                    .Where(a => a.PlayerId == input.PlayerGuid && a.State == AgentState.Audited)
                    .AnyAsync();
            var query = _entityRepository.GetAllIncluding()
                  .Include(s => s.Family).ThenInclude(s => s.Father)
                  .Include(s => s.Family).ThenInclude(s => s.Mother)
                  .Where(s => familyBabyMaxId.Contains(s.Id) && !s.Family.IsDeleted)
                  .WhereIf(input.Gender != 0, s => s.Gender == input.Gender)
                  .WhereIf(input.Age != -1 && input.Age != null, s => s.AgeDouble >= input.Age && s.AgeDouble < input.Age + 1)
                  .WhereIf(!String.IsNullOrEmpty(input.Keyword),
                          s => (s.Name != null && s.Name.Contains(input.Keyword)) ||
                               (s.Family.Mother.NickName.Contains(input.Keyword) ||
                               s.Family.Father.NickName.Contains(input.Keyword)))
                  .WhereIf(input.State != 0, s => s.State == input.State)
                  .WhereIf(isAgent, s => s.Family.AddOnStatus != AddOnStatus.Hide)
                  .OrderByDescending(f => f.Family.ChargeAmount)
                  .AsNoTracking();
            var babyCount = await query.AsQueryable().CountAsync();
            if (input.PageSize > 0)
            {
                input.MaxResultCount = input.PageSize;
            }
            if (input.PageIndex > 0)
            {
                input.SkipCount = (input.PageIndex - 1) * input.PageSize;
            }
            var result = await query.AsQueryable().PageBy(input).ToListAsync();
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    var baby = ObjectMapper.Map<GetBabiesOutputBaby>(item);

                    var familyItem = new GetBabiesOutputFamily()
                    {
                        FamilyId = item.FamilyId,
                        //IsHasUnderAgeBaby = item.Family.Babies.Any(s => s.State == BabyState.UnderAge),
                        Dad = new GetBabiesOutputParent()
                        {
                            Name = item.Family.Father.NickName,
                            HeadPicture = item.Family.Father.HeadUrl,
                            PlayerGuid = item.Family.FatherId
                        },
                        Mom = new GetBabiesOutputParent()
                        {
                            Name = item.Family.Mother.NickName,
                            HeadPicture = item.Family.Mother.HeadUrl,
                            PlayerGuid = item.Family.MotherId
                        }
                    };
                    var getBabiesDto = new GetBabiesDto()
                    {
                        Baby = baby,
                        Family = familyItem
                    };
                    babies.Add(getBabiesDto);
                }
            }
            response.Babies = new PagedResultDto<GetBabiesDto>(babyCount, babies);
            return response;
        }
        /// <summary>
        /// 宝宝信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBabyBasicInfoOutput> GetBabyBasicInfo(GetBabyBasicInfoInput input)
        {
            var response = new GetBabyBasicInfoOutput();
            try
            {
                // Stopwatch watch = new Stopwatch();

                // watch.Start();
                //基本信息
                var baby = (await _entityRepository.GetAll()
                    .Where(b => b.Id == input.BabyId)
                    .FirstOrDefaultAsync()).CheckNull($"宝宝不能为空，宝宝ID：{input.BabyId}");

                // watch.Stop();
                //Logger.Debug($"获取宝宝用时：{watch.ElapsedMilliseconds}");
                // watch.Restart();
                var family = await _familyRepository.GetAll()
                        .Where(f => f.Id == baby.FamilyId)
                        .Include(f => f.Mother)
                        .Include(f => f.Father)
                        .FirstOrDefaultAsync();
                // watch.Stop();
                //Logger.Debug($"获取家庭用时：{watch.ElapsedMilliseconds}");
                //var family = await _familyRepository.GetAsync(baby.FamilyId);
                //var parents = await _playerRepository.GetAllListAsync(s => s.Id == family.FatherId || s.Id == family.MotherId);
                var father = family.Father;
                var mother = family.Mother;
                var player = father.Id == input.PlayerGuid ? father : mother;

                // watch.Restart();
                var renameCostCoinSys = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1001 && s.Name == "RENAME_BABY_CONSUME_COIN");
                var renameCostCoin = Convert.ToInt32(renameCostCoinSys.Value);
                //var houseAsset = await _babyFamilyAssetRepository.GetAllIncluding()
                //    .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropType)
                //    .FirstOrDefaultAsync(s => s.FamilyId == family.Id && s.BabyProp.BabyPropType.Name == "House" && s.IsEquipmenting && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null));

                var equimentProp = _competitionService.IsEquipmentProp(input.BabyId, family.Id);
                if (baby != null)
                {
                    response.Family = new GetBabyBasicInfoOutputFamily()
                    {
                        FamilyId = baby.FamilyId,
                        Deposit = family.Deposit,
                        Level = equimentProp.HouseCode,// family.Level,
                        DadHeadPicture = father.HeadUrl,
                        MomHeadPicture = mother.HeadUrl,
                        DadGuid = father.Id,
                        MomGuid = mother.Id,
                        DadName = father.NickName,
                        MomName = mother.NickName,
                        //HappinessTitle = family.HappinessTitle
                    };
                    ////获取段位
                    //var danGrading = DanGrading.UnKnow;
                    //var currentSeason = await _seasonManagementRepository.FirstOrDefaultAsync(a => a.IsCurrent == true);
                    //if (currentSeason != null)
                    //{
                    //    var competition = await _competitionRepository.FirstOrDefaultAsync(c => c.BabyId == input.BabyId && c.FamilyId == baby.FamilyId && c.SeasonManagementId == currentSeason.Id);
                    //    if (competition != null)
                    //    {
                    //        danGrading = competition.DanGrading;
                    //    }
                    //}
                    // 出生属性
                    var babyProperty = ObjectMapper.Map<GetBabyBasicInfoOutputBabyProperty>(baby);
                    response.Baby = ObjectMapper.Map<GetBabyBasicInfoOutputBaby>(baby);
                    response.Baby.BabyProperty = babyProperty;
                    response.Baby.Age = Convert.ToInt32(baby.AgeDouble.ToString().Split('.')[0]);
                    response.Baby.AgeString = baby.AgeString;
                    response.Baby.HealthyTitle = baby.HealthyTitle;
                    response.Baby.ChangeCost = renameCostCoin;
                    response.Baby.DanGrading = await _competitionService.GetPlayerDanGride(baby.Id, baby.FamilyId);
                    //添加 baby 皮肤代码
                    //var _propResult =_competitionService.IsEquipmentProp(input.BabyId, family.Id);//IsEquipmentProp(input.BabyId, family.Id);
                    response.EquipmentProp = equimentProp;
                    response.Baby.Skin = equimentProp.SkinCode;
                    // 系统设置
                    if (input.PlayerGuid != null)
                    {
                        var isIos = player.DeviceSystem.Contains("iOS");
                        var systemSettings = await _systemSettingRepository
                            .GetAll()
                            .Where(s => s.GroupName == "payment")
                            .WhereIf(isIos, s => s.Name.Contains("iOS"))
                            .WhereIf(!isIos, s => s.Name.Contains("andriod"))
                            .ToDictionaryAsync(k => k.Code, k => k.Value == "true");
                        var buyGoldCoinIsEnabledCode = isIos ? 1 : 5;
                        var buyGoldCoinIconIsShowCode = isIos ? 3 : 6;
                        // Console.WriteLine("================isIos:"+isIos);
                        // Console.WriteLine("================"+systemSettings.ContainsKey(buyGoldCoinIsEnabledCode));
                       var systemSetting = new BabySystemSetting()
                        {
                            Payment = new SystemSettingPayment()
                            {
                                IsShow = true,
                                IsEnable = true,
                                Message = ((systemSettings.ContainsKey(buyGoldCoinIsEnabledCode) ? systemSettings[buyGoldCoinIsEnabledCode] : false)) ? "程序异常，请联系客服协助解决！" : "此功能正在进行维护，如有疑问，请联系客服！",
                                Title = "暂不支持",
                                Energy = new Energy()
                                {
                                    IsShow = true,//systemSettings.ContainsKey(2) ? systemSettings[2] : false,
                                    IsEnable = true,//systemSettings.ContainsKey(4) ? systemSettings[4] : false,
                                    Message = "程序异常，请联系客服协助解决！",
                                    Title = "暂不支持",
                                },
                                GoldCoin = new GoldCoin()
                                {
                                    // 此问题正在调试，如有疑问，请联系客服
                                    IsShow = systemSettings.ContainsKey(buyGoldCoinIsEnabledCode) ? systemSettings[buyGoldCoinIsEnabledCode] : false,
                                    IsEnable = systemSettings.ContainsKey(buyGoldCoinIconIsShowCode) ? systemSettings[buyGoldCoinIconIsShowCode] : false,
                                    Message = ((systemSettings.ContainsKey(buyGoldCoinIsEnabledCode) ? systemSettings[buyGoldCoinIsEnabledCode] : false)) ? "程序异常，请联系客服协助解决！" : "此功能正在进行维护，如有疑问，请联系客服！",
                                    Title = "暂不支持",
                                }
                            }
                        };
                        response.SystemSetting = systemSetting;
                    }

                }
                // watch.Stop();
                //Logger.Debug($"赋值用时：{watch.ElapsedMilliseconds}");
                BackgroundJob.Enqueue<IAutoRunnerJob>(job => job.StopRunnerJob(new StopRunnerRequest
                {
                    BabyId = baby.Id,
                    FamilyId = family.Id,
                    PlayerId = input.PlayerGuid == null ? Guid.Empty : input.PlayerGuid.Value,
                    Reason = $"{player.NickName}上线，机器人自动暂停"
                }));

                //var agent = await _agentRepository.FirstOrDefaultAsync(a => a.PlayerId.Value == father.Id || a.PlayerId.Value == mother.Id);

                //if (agent != null)
                //{
                //    family.AddOnStatus = AddOnStatus.NotRunning;
                //    await _familyRepository.UpdateAsync(family);
                //}
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("获取宝宝信息（GetBabyBasicInfo）出错啦", ex.Message + ex.InnerException);
                throw;
            }
            return response;
        }

        /// <summary>
        /// 取名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GiveBabyNameOutput> GiveBabyName(GiveBabyNameInput input)
        {
            var response = new GiveBabyNameOutput();
            try
            {
                //检查是否有敏感词
                var sensitiveWordCheck = await _sensitiveWordRepository.CountAsync(s => s.Content.Contains(input.NewName));
                if (sensitiveWordCheck == 0)
                {
                    var baby = await _entityRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
                    var isFirstGiveName = false;
                    if (baby != null)
                    {
                        var renameCostCoinSys = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 1001 && s.Name == "RENAME_BABY_CONSUME_COIN");
                        var renameCostCoin = Convert.ToInt32(renameCostCoinSys.Value);
                        if (baby.Name == null)
                        {
                            if (input.PlayerGuid == baby.Family.MotherId)
                            {
                                baby.Name = input.NewName;
                                isFirstGiveName = true;
                            }
                            else
                            {
                                throw new UserFriendlyException((int)BSMPErrorCodes.FirstGiveNameOnlyAllowByFather, "取名只有宝宝的母亲可以哦");
                            }
                        }
                        else
                        {
                            //判断金币是否够，暂定100
                            if (baby.Family.Deposit < renameCostCoin)
                            {
                                throw new AbpException("金币不够");
                            }
                            if (input.PlayerGuid == baby.Family.MotherId)
                            {
                                baby.Name = input.NewName;
                            }
                            else
                            {
                                throw new UserFriendlyException((int)BSMPErrorCodes.FirstGiveNameOnlyAllowByFather, "只有宝宝的母亲可以改名哦");
                            }
                        }
                        await _entityRepository.UpdateAsync(baby);
                        if (!isFirstGiveName)
                        {
                            //减去金币
                            baby.Family.Deposit -= renameCostCoin;
                            await _familyRepository.UpdateAsync(baby.Family);
                            //增加花费金币的记录
                            await _coinRechargeRecordRepository.InsertAsync(new CoinRechargeRecord()
                            {
                                FamilyId = baby.FamilyId,
                                RechargeCount = -renameCostCoin,
                                RechargerId = input.PlayerGuid,
                            });
                        }
                        if (isFirstGiveName)
                        {
                            #region 添加消息
                            var genderName = baby.Gender == Gender.Male ? '男' : '女';
                            await _runInformationRepository.InsertAsync(new RunHorseInformation()
                            {
                                Content = $"{genderName}宝宝{baby.Name}出生了",
                                PlayScene = PlayScene.BabyPage,
                            });
                            //await _informationRepository.InsertAsync(new Information()
                            //{
                            //    Content = $"{genderName}宝宝{baby.Name}出生了",
                            //    FamilyId = baby.FamilyId,
                            //    Type = InformationType.Barrage,
                            //    State = InformationState.Create
                            //});
                            #endregion
                        }

                    }
                    else
                    {
                        throw new UserFriendlyException("宝宝不存在！");
                    }
                }
                else
                {
                    throw new UserFriendlyException((int)BSMPErrorCodes.ContainSensitiveWord, "名字中包含敏感词");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        internal override IQueryable<Baby> GetQuery(GetBabysInput model)
        {
            return _repository.GetAll();
        }
        public IQueryable<Baby> GetAll()
        {
            return _repository.GetAll();
        }
        /// <summary>
        /// 查看出生动画
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LookBirthMovieOutput> LookBirthMovie(LookBirthMovieInput input)
        {
            var response = new LookBirthMovieOutput();
            try
            {
                var baby = await _entityRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
                if (baby != null)
                {
                    if (input.PlayerGuid == baby.Family.FatherId)
                    {
                        baby.IsWatchBirthMovieFather = true;
                    }
                    else if (input.PlayerGuid == baby.Family.MotherId)
                    {
                        baby.IsLoadBirthMovieMother = true;
                    }
                }
                var output = await _entityRepository.UpdateAsync(baby);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 结局（已成人宝宝）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAudltBabyInfoOutput> GetAudltBabyInfo(GetAudltBabyInfoInput input)
        {
            var response = new GetAudltBabyInfoOutput();
            var baby = await _repository.GetAllIncluding(s => s.BabyEnding).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            if (baby?.BabyEnding == null)
            {
                var babyEnding = await GetEnding(baby);
                if (babyEnding == null)
                {
                    response.StroyEnding = new GetAudltBabyInfoOutputBabyStory()
                    {
                        Description = "暂未找到合适的职业",
                        Name = "无业游民",
                        Image = "0.jpg"
                    };
                }
                else
                {
                    baby.BabyEndingId = babyEnding?.Id;
                    baby.BabyEnding = babyEnding;
                    await _repository.UpdateAsync(baby);
                    var story = ObjectMapper.Map<GetAudltBabyInfoOutputBabyStory>(baby.BabyEnding);
                    response.StroyEnding = story;
                }
            }
            else
            {
                var story = ObjectMapper.Map<GetAudltBabyInfoOutputBabyStory>(baby.BabyEnding);
                response.StroyEnding = story;
            }
            return response;
        }
        /// <summary>
        /// 获取结局
        /// </summary>
        /// <param name="babyEndings"></param>
        /// <param name="StudyGroupCountDtos"></param>
        /// <returns></returns>
        private async Task<BabyEnding> GetEnding(Baby baby)
        {
            var babyEndings = await _babyEndingRepository.GetAllListAsync(s => s.MaxProperty >= baby.PropertyTotal && s.MinProperty < baby.PropertyTotal && (s.Gender != Gender.All ? (baby.Gender == s.Gender) : true));
            var studyGroupList = await _babyEventRecordRepository.GetAllIncluding(s => s.Event).Where(s => s.BabyId == baby.Id && s.Event.Type == IncidentType.Study).GroupBy(s => s.Event.StudyType).Select(s => new StudyGroupCountDto { StudyType = s.Key.Value, Count = s.Count() }).OrderByDescending(s => s.Count).ToListAsync();
            //取学习次数最大的几个结局随机选择
            var studyGroup = studyGroupList.FirstOrDefault();
            var studyMaxTypes = studyGroupList.Where(s => s.Count == studyGroup.Count).ToList();
            if (studyMaxTypes.Count > 1)
            {
                var ran = new Random();
                var ranIndex = ran.Next(0, studyMaxTypes.Count);
                studyGroup = studyMaxTypes[ranIndex];
            }

            var babyEnding = babyEndings
                .WhereIf(studyGroup != null, s => s.StudyType == studyGroup.StudyType && s.StudyMax >= studyGroup.Count && s.StudyMin < studyGroup.Count)
                .FirstOrDefault();
            if (babyEnding != null)
            {
                return babyEnding;
            }
            else if (babyEndings.Count > 0)
            {
                //随机选一个
                var ran = new Random();
                var ranIndex = ran.Next(0, babyEndings.Count);
                return babyEndings[ranIndex];
            }
            return null;
        }
        public async Task<Baby> GetFirstBaby()
        {
            var baby = await _repository
                .GetAll()
                .Include(b => b.Family)
                .FirstOrDefaultAsync(b => true);

            return baby;
        }
        /// <summary>
        /// 重新计算宝宝属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ReCaclBabyPropertyOutput> ReCaclBabyProperty(ReCaclBabyPropertyInput input)
        {
            var response = new ReCaclBabyPropertyOutput();
            //for (int i = 0; i < input.BabyIds.Length; i++)
            //{
            var professionAddition = new BabySixBasicProperty();
            var familyAssetAddition = new BabySixBasicProperty();
            var eventAward = new BabySixBasicProperty();
            var birthOwn = new BabySixBasicProperty();

            var babyId = input.BabyId;
            var baby = await _entityRepository.GetAsync(babyId);
            // 父母职业属性加成
            //var professionAward = await _babyGrowUpRecordRepository
            //    .GetAll()
            //    .WhereIf(baby.BirthOrder > 1, s => s.TriggerType == TriggerType.InheritParentsProfessionAddition)
            //     .WhereIf(baby.BirthOrder <= 1, s => s.TriggerType == TriggerType.ParentsProfessionAddition)
            //    .LastOrDefaultAsync(a => a.BabyId == babyId);
            //if (professionAward != null)
            //{
            //    professionAddition = ObjectMapper.Map<BabySixBasicProperty>(professionAward);
            //}
            //else
            //{
            var professionAwards = _playerProfessionRepository.GetAllIncluding()
               .Include(s => s.Profession).ThenInclude(s => s.Reward)
               .Where(s => s.FamilyId == baby.FamilyId).Select(s => s.Profession.Reward).ForEachAsync(s =>
               {
                   professionAddition.Charm += s.Charm;
                   professionAddition.EmotionQuotient += s.EmotionQuotient;
                   professionAddition.Imagine += s.Imagine;
                   professionAddition.Intelligence += s.Intelligence;
                   professionAddition.Physique += s.Physique;
                   professionAddition.WillPower += s.WillPower;
               });
            //}
            // 家庭装备固定属性奖励 ,可继承类型的道具
            var babyPropertyAssetAwardAsync = _babyFamilyAssetRepository.GetAllIncluding()
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropPropertyAward)
                .Where(s => (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null) && s.OwnId == babyId).Select(s => s.BabyProp.BabyPropPropertyAward).ForEachAsync(s =>
                               {
                                   familyAssetAddition.Charm += s.Charm;
                                   familyAssetAddition.EmotionQuotient += s.EmotionQuotient;
                                   familyAssetAddition.Imagine += s.Imagine;
                                   familyAssetAddition.Intelligence += s.Intelligence;
                                   familyAssetAddition.Physique += s.Physique;
                                   familyAssetAddition.WillPower += s.WillPower;
                               });
            // 获取家庭所有装备
            //var assets = _babyFamilyAssetRepository.GetAllIncluding()
            //    .Where(asset => asset.FamilyId == baby.FamilyId);
            var babyAssetFeatureRecords = await _babyAssetFeatureRecordRepository.GetAllListAsync(afr => afr.FamilyId == baby.FamilyId && afr.BabyId == babyId);
            // 父母在宝宝事件选择的选项的奖励
            await _babyEventRecordRepository.GetAllIncluding(s => s.Event)
                .Include(s => s.Option).ThenInclude(s => s.Reward)
                .Include(s => s.Option).ThenInclude(s => s.Consume)
                .Where(s => s.BabyId == babyId && s.State == EventRecordState.Handled).ForEachAsync(s =>
               {

                   // 计算事件对于宝宝属性的各项值
                   var isGrowUpEvent = s.Event.Type == IncidentType.Growup;
                   var eventType = isGrowUpEvent ? EventAdditionType.GrowUp : EventAdditionType.Study;//TODO: 此处还需进一步完善
                   var assetFeature = _babyAssetFeatureApplicationService.GetAssetFeature(new AssetFeatureWorkInput() { FamilyId = baby.FamilyId, BabyId = babyId, EventType = eventType }).Result;
                   // 奖励属性加成
                   var propertyAddtion = assetFeature == null ? 0 : assetFeature.PropertyAddtion;
                   // 计算各个属性加成
                   var reward = s.Option.Reward;
                   var consume = s.Option.Consume;

                   var rewardLast = new
                   {
                       Charm = reward.Charm + Convert.ToInt32(reward.Charm * propertyAddtion),
                       EmotionQuotient = reward.EmotionQuotient + Convert.ToInt32(reward.EmotionQuotient * propertyAddtion),
                       Imagine = reward.Imagine + Convert.ToInt32(reward.Imagine * propertyAddtion),
                       Intelligence = reward.Intelligence + Convert.ToInt32(reward.Intelligence * propertyAddtion),
                       Physique = reward.Physique + Convert.ToInt32(reward.Physique * propertyAddtion),
                       WillPower = reward.WillPower + Convert.ToInt32(reward.WillPower * propertyAddtion),
                       reward.Energy,
                       reward.Healthy,
                   };
                   // 宝宝属性
                   eventAward.Charm = eventAward.Charm + rewardLast.Charm + consume.Charm;
                   eventAward.EmotionQuotient = eventAward.EmotionQuotient + rewardLast.EmotionQuotient + consume.EmotionQuotient;
                   eventAward.Imagine = eventAward.Imagine + rewardLast.Imagine + consume.Imagine;
                   eventAward.Intelligence = eventAward.Intelligence + rewardLast.Intelligence + consume.Intelligence;
                   eventAward.Physique = eventAward.Physique + rewardLast.Physique + consume.Physique;
                   eventAward.WillPower = eventAward.WillPower + rewardLast.WillPower + consume.WillPower;
               });
            // 出生属性 各维属性
            var birthOwnProperty = await _babyGrowUpRecordRepository.FirstOrDefaultAsync(a => a.BabyId == babyId && a.TriggerType == TriggerType.BirthOwn);
            if (birthOwnProperty != null)
            {
                birthOwn = ObjectMapper.Map<BabySixBasicProperty>(birthOwnProperty);
            }

            // 计算总值
            var babySixBasicProperty = new BabySixBasicProperty
            {
                Charm = birthOwn.Charm + eventAward.Charm + familyAssetAddition.Charm + professionAddition.Charm,
                EmotionQuotient = birthOwn.EmotionQuotient + eventAward.EmotionQuotient + familyAssetAddition.EmotionQuotient + professionAddition.EmotionQuotient,
                Imagine = birthOwn.Imagine + eventAward.Imagine + familyAssetAddition.Imagine + professionAddition.Imagine,
                Intelligence = birthOwn.Intelligence + eventAward.Intelligence + familyAssetAddition.Intelligence + professionAddition.Intelligence,
                Physique = birthOwn.Physique + eventAward.Physique + familyAssetAddition.Physique + professionAddition.Physique,
                WillPower = birthOwn.WillPower + eventAward.WillPower + familyAssetAddition.WillPower + professionAddition.WillPower,
            };
            response.BabySixProperty = new ReCaclBabyPropertyOutputBabySixProperty()
            {
                BabyId = babyId,
                BabySixBasicProperty = babySixBasicProperty,
                BirthOwn = birthOwn,
                EventAward = eventAward,
                FamilyAssetAddition = familyAssetAddition,
                ProfessionAddition = professionAddition,
                CurrentProperty = ObjectMapper.Map<BabySixBasicProperty>(baby),
                TotalOffset = baby.PropertyTotal - babySixBasicProperty.Total,
            };
            //}
            return response;
        }
        /// <summary>
        ///  重新设置计算错误的宝宝属性，如果请求参数有宝宝编号，则计算该宝宝，否则计算2019-04-06到2019-04-15号期间的所有未成年宝宝
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FixBabyPropertyErrorOutput> FixBabyPropertyError(FixBabyPropertyErrorInput input)
        {
            var response = new FixBabyPropertyErrorOutput();
            if (input.BabyId.HasValue)
            {
                var baby = await _entityRepository.GetAsync(input.BabyId.Value);
                var newProperty = await ReCaclBabyProperty(new ReCaclBabyPropertyInput(baby.Id));
                await UpdateBabySixBasicProperty(baby, newProperty.BabySixProperty.BabySixBasicProperty);
                response.BabyIds.Add(input.BabyId.Value);
            }
            else
            {
                await _entityRepository.GetAll()
                     .Where(s => s.State == BabyState.UnderAge && s.LastModificationTime > input.StartDateTime && s.LastModificationTime < input.EndDateTime).ForEachAsync(s =>
                         {
                             var newProperty = ReCaclBabyProperty(new ReCaclBabyPropertyInput(s.Id)).Result;

                             var updateAsync = UpdateBabySixBasicProperty(s, newProperty.BabySixProperty.BabySixBasicProperty);
                             response.BabyIds.Add(s.Id);

                         });
            }

            return response;
        }
        /// <summary>
        /// 更新宝宝六维基本属性
        /// </summary>
        /// <param name="baby"></param>
        /// <param name="babySixBasicProperty"></param>
        /// <returns></returns>
        private async Task UpdateBabySixBasicProperty(Baby baby, BabySixBasicProperty babySixBasicProperty)
        {
            // 更新宝宝属性
            baby.Charm = babySixBasicProperty.Charm;
            baby.Intelligence = babySixBasicProperty.Intelligence;
            baby.Imagine = babySixBasicProperty.Imagine;
            baby.Physique = babySixBasicProperty.Physique;
            baby.WillPower = babySixBasicProperty.WillPower;
            baby.EmotionQuotient = babySixBasicProperty.EmotionQuotient;
            await _entityRepository.UpdateAsync(baby);
            // 新增宝宝属性变更记录
            var insertGrowUpRecordAsync = _babyGrowUpRecordRepository.InsertAsync(new BabyGrowUpRecord()
            {
                BabyId = baby.Id,
                Charm = babySixBasicProperty.Charm,
                Intelligence = babySixBasicProperty.Intelligence,
                Imagine = babySixBasicProperty.Imagine,
                Physique = babySixBasicProperty.Physique,
                WillPower = babySixBasicProperty.WillPower,
                EmotionQuotient = babySixBasicProperty.EmotionQuotient,
                TriggerType = TriggerType.ReCaculate
            });
            var family = await _familyRepository.GetAsync(baby.FamilyId);
            // 发送系统消息
            await _informationRepository.InsertAsync(new Information()
            {
                Content = $"非常抱歉，宝宝属性的计算出现了一点小问题，系统进行了重新计算，如有疑问，请联系客服！",
                FamilyId = baby.FamilyId,
                State = InformationState.Create,
                Type = InformationType.System,
                ReceiverId = family.FatherId,
                SystemInformationType = SystemInformationType.Default,
                NoticeType = NoticeType.Default,
            });
            //await _informationRepository.InsertAsync(new Information()
            //{
            //    Content = $"非常抱歉，宝宝属性的计算出现了一点小问题，系统进行了重新计算，如有疑问，请联系客服！",
            //    FamilyId = baby.FamilyId,
            //    State = InformationState.Create,
            //    Type = InformationType.System,
            //    ReceiverId = family.MotherId,
            //    SystemInformationType = SystemInformationType.Default,
            //    NoticeType = NoticeType.Default,
            //});
        }
    }
}


