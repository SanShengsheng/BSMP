using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.ChineseBabies.ChangeProfessionCosts.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Informations.Events;
using MQKJ.BSMP.ChineseBabies.PlayerProfessions.Dtos;
using MQKJ.BSMP.ChineseBabies.Professions.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Products;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Profession应用层服务的接口实现方法
    ///</summary>
    //[AbpAuthorize]
    public class ProfessionAppService : BsmpApplicationServiceBase<Profession, int, ProfessionEditDto, ProfessionEditDto, GetProfessionsInput, ProfessionListDto>, IProfessionAppService
    {
        //private readonly IFamilyAppService _familyAppService;

        //private readonly IPlayerProfessionAppService _playerProfessionAppService;

        private readonly IRepository<PlayerProfession> _playerProfessionRepository;

        private readonly IRepository<Reward> _rewardRepository;

        private readonly IChangeProfessionCostAppService _changeProfessionCostAppService;
        private readonly IRepository<ChangeProfessionCost> _changeProfessionCostRepository;

        private readonly IWeChatPayAppService _weChatPayAppService;

        private readonly IRepository<Product> _productRepository;

        private readonly IBabyGrowUpRecordAppService _babyGrowUpRecordAppService;

        private readonly IBabyAppService _babyAppService;

        //private IDistributedCache _redisMemoryCache;

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<Family> _familyRepository;

        private readonly IRepository<Baby> _babyRepository;

        private readonly IInformationAppService _informationAppService;
        private readonly IRepository<FamilyCoinDepositChangeRecord,Guid> _familyCoinDCRepository;

        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 构造函数
        ///</summary>
        public ProfessionAppService(IRepository<Profession, int> entityRepository
            //, IFamilyAppService familyAppService
            //, IPlayerProfessionAppService playerProfessionAppService
            , IRepository<Reward> rewardRepository
            , IChangeProfessionCostAppService changeProfessionCostAppService,
            IRepository<ChangeProfessionCost> changeProfessionCostRepository
            , IWeChatPayAppService weChatPayAppService
            , IRepository<Product> productRepository
            , IInformationAppService informationAppService
            , IBabyGrowUpRecordAppService babyGrowUpRecordAppService
            , BabyAppService babyAppService
            //, IDistributedCache redisMemoryCache
            , IRepository<Player, Guid> playerRepository
            , IRepository<Family> familyRepository
            , IRepository<Baby> babyRepository
            , IRepository<PlayerProfession> playerProfessionRepository,
            IRepository<FamilyCoinDepositChangeRecord,Guid> familyCoinDCRepository
            ) : base(entityRepository)
        {
            //_familyAppService = familyAppService;

            //_playerProfessionAppService = playerProfessionAppService;
            _playerProfessionRepository = playerProfessionRepository;

            _rewardRepository = rewardRepository;

            _changeProfessionCostAppService = changeProfessionCostAppService;

            _weChatPayAppService = weChatPayAppService;

            _productRepository = productRepository;

            _babyGrowUpRecordAppService = babyGrowUpRecordAppService;

            _babyAppService = babyAppService;

            //_redisMemoryCache = redisMemoryCache;

            _playerRepository = playerRepository;

            _familyRepository = familyRepository;

            _babyRepository = babyRepository;
            EventBus = NullEventBus.Instance;
            _changeProfessionCostRepository = changeProfessionCostRepository;

            _informationAppService = informationAppService;
            _familyCoinDCRepository = familyCoinDCRepository;
        }

        internal override IQueryable<Profession> GetQuery(GetProfessionsInput model)
        {
            var changedProfessionIds = _playerProfessionRepository.GetAll()
                .Where(p => p.FamilyId == model.FamilyId && p.PlayerId == model.PlayerId)
                .Select(x => x.ProfessionId);

            var query = _repository.GetAll()
                .Include(p => p.Reward)
                .Include(c => c.Costs)
                .Where(x => x.Gender == model.Gender && changedProfessionIds.Contains(x.Id));

            return query;
        }

        public async Task<PagedResultDto<ProfessionListDto>> GetProfessions(GetProfessionsInput input)
        {
            //var changedProfessionIds = await _playerProfessionAppService.GetChangedProfessions(new GetChangedProfessionsInput()
            //{
            //    FamilyId = input.FamilyId,
            //    PlayerId = input.PlayerId
            //});
            var changedProfessionIds = _playerProfessionRepository.GetAll()
                .Where(p => p.FamilyId == input.FamilyId && p.PlayerId == input.PlayerId)
                .Select(x => x.ProfessionId);

            var query = _repository.GetAll()
                .Include(p => p.Reward)
                .Include(c => c.Costs)
                .Where(x => x.Gender == input.Gender);

            var count = query.Count();

            var entityList = await query
                    .PageBy(input)
                    .ToListAsync();

            var currentProfessionId = _playerProfessionRepository.FirstOrDefaultAsync(p => p.FamilyId == input.FamilyId && p.PlayerId == input.PlayerId && p.IsCurrent == true).Result.ProfessionId;

            var entityListDtos = entityList.MapTo<List<ProfessionListDto>>();

            if (changedProfessionIds.Count() > 0)
            {
                foreach (var item in entityListDtos)
                {

                    if (changedProfessionIds.Contains(item.Id) || item.IsDefault)
                    {
                        item.IsUnLock = true;
                    }

                    if (item.Id == currentProfessionId)
                    {
                        item.IsCurrent = true;
                    }
                }
            }

            return new PagedResultDto<ProfessionListDto>(count, entityListDtos);
        }

        public async Task<MiniProgramPayOutput> ChangeProfession(ChangeProfessionInput input)
        {
            var output = new MiniProgramPayOutput();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                //var professonChageList = await _changeProfessionCostRepository.GetAllListAsync();

                var changeProfession = await _changeProfessionCostRepository.GetAll()
                    .FirstOrDefaultAsync(c => c.CostType == input.CostType && c.ProfessionId == input.ProfessionId);

                if (changeProfession == null)
                {
                    throw new UserFriendlyException("未找到相关职业");
                }
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                var hasProfession = _playerProfessionRepository
                    .GetAll()
                    .Any(m => m.ProfessionId == input.ProfessionId && m.PlayerId == input.PlayerId && m.FamilyId == input.FamilyId);

                //stopwatch.Stop();

                //Console.WriteLine($"查询职业记录某一条耗时{stopwatch.Elapsed.TotalMilliseconds}毫秒");

                if (hasProfession)
                {
                    Stopwatch stopwatch1 = new Stopwatch();
                    stopwatch1.Start();
                    //获取父母双方的职业
                    var parentProfessions = _playerProfessionRepository
                        .GetAllIncluding(f => f.Family, p => p.Profession)
                        .Where(f => f.FamilyId == input.FamilyId && f.IsCurrent == true);

                    //获取自己的职业
                    var myPlayerProfession = parentProfessions.FirstOrDefault(s => s.PlayerId == input.PlayerId);
                    stopwatch1.Stop();
                    Console.WriteLine($"查询职业记录双方父母职业耗时{stopwatch.Elapsed.TotalMilliseconds}毫秒");
                    //把原来的职业更新为不是当前的
                    myPlayerProfession.IsCurrent = false;
                    await _playerProfessionRepository.UpdateAsync(myPlayerProfession);

                    var toProfession = await _playerProfessionRepository
                        .FirstOrDefaultAsync(p => p.PlayerId == input.PlayerId &&
                            p.ProfessionId == input.ProfessionId &&
                            p.FamilyId == input.FamilyId);

                    toProfession.IsCurrent = true;
                    await _playerProfessionRepository.UpdateAsync(toProfession);


                    //获取对方的
                    //var otherPlayerProfession = parentProfessions.FirstOrDefault(s => s.PlayerId != input.PlayerId);

                    //获取自己要转职的职业
                    //var selfCurrentProfession = _repository.Get(input.ProfessionId);

                    //var happiness = CalculateHappinessDegree(myPlayerProfession.Family.Happiness, otherPlayerProfession.Profession.SatisfactionDegree, myPlayerProfession.Profession.SatisfactionDegree, selfCurrentProfession.SatisfactionDegree);

                    //myPlayerProfession.Family.Happiness = happiness;

                    //myPlayerProfession.Family.Happiness = happiness >= 100 ? 100 : happiness;

                    //myPlayerProfession.Family.Happiness = happiness <= 0 ? 1 : happiness;

                    //await _familyRepository.UpdateAsync(myPlayerProfession.Family);

                    //await _playerProfessionRepository.InsertAsync(new PlayerProfession
                    //{
                    //    CreationTime = DateTime.UtcNow,
                    //    FamilyId = myPlayerProfession.Family.Id,
                    //    IsCurrent = true,
                    //    LastModificationTime = DateTime.UtcNow,
                    //    PlayerId = input.PlayerId,
                    //    ProfessionId = input.ProfessionId,
                    //    IsVirtualRecharge = input.IsVirtualChange
                    //});

                    output.ChangeResult = ChangeResult.Success;
                    stopwatch.Stop();
                    Console.WriteLine($"转换已有职业花费{stopwatch.Elapsed.TotalMilliseconds}毫秒");
                }
                else
                {
                    var family = await _familyRepository.GetAll().Include(b => b.Babies).FirstOrDefaultAsync(x => x.Id == input.FamilyId);

                    var player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

                    if (input.CostType == CostType.Coin)
                    {
                        if (family.Deposit >= Convert.ToDouble(changeProfession.Cost))
                        {
                            await AddPlayerNewProfession(family, new AddPlayerNewProfessionRequest()
                            {
                                IsVirtual = false,
                                PayAmount = Convert.ToDouble(changeProfession.Cost),
                                PlayerId = input.PlayerId,
                                ProfessionId = input.ProfessionId,
                                CostType = input.CostType
                            });
                            output.ChangeResult = ChangeResult.Unlock;
                        }
                        else
                        {
                            output.ChangeResult = ChangeResult.NotEnough;
                        }
                    }
                    else
                    {
                        if (input.IsVirtualChange)
                        {
                            await AddPlayerNewProfession(family, new AddPlayerNewProfessionRequest()
                            {
                                PlayerId = input.PlayerId,
                                ProfessionId = input.ProfessionId,
                                PayAmount = Convert.ToDouble(changeProfession.Cost),
                                IsVirtual = true,
                                CostType = input.CostType
                            });

                            output.ChangeResult = ChangeResult.Success;
                        }
                        else
                        {
                            output = await _weChatPayAppService.Pay(new SendPaymentRquestInput()
                            {
                                TenantId = player.TenantId,
                                OpenId = player.OpenId,
                                PlayerId = input.PlayerId,
                                ClientType = input.ClientType,
                                Code = input.Code,
                                Body = input.Body,
                                Attach = input.Attach,
                                Totalfee = changeProfession.Cost,
                                ProductId = changeProfession.Id,
                                FamilyId = input.FamilyId
                            });
                            output.ChangeResult = ChangeResult.Unlock;
                        }
                    }

                    stopwatch.Stop();

                    Console.WriteLine($"解锁新职业花费{stopwatch.Elapsed.TotalMilliseconds}毫秒");
                }
            }
            catch (Exception exp)
            {
                output.ChangeResult = ChangeResult.Failed;
                throw new UserFriendlyException($"转职失败：{exp}");
            }
            return output;
        }

        private const string MY_FAMILY_MESSAGE_FORMAT = "孩子的{0}花费了¥{1}{2}解锁了{3}职业";
        private const string SYSTEM_MESSAGE_FORMAT = "{0}的{1}花费了¥{2}{3}解锁了{4}职业";

        /// <summary>
        /// 查询转职订单结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<QueryChangeResultOutput> QueryChangePorfessionResult(QueryChangeResultInput input)
        {
            var output = new QueryChangeResultOutput();
            Logger.Warn(input.IsTest + "主动查询订单了");
            try
            {
                var family = await _familyRepository
                    .GetAllIncluding(f => f.Father, m => m.Mother, m => m.Babies).FirstOrDefaultAsync(f => f.Id == input.FamilyId);

                var result = _weChatPayAppService.QueryWechatPayResult(new QueryOrderStateInput()
                {
                    TenantId = family.Father.TenantId,
                    OutTradNo = input.OutTradeNo
                });

                if (result.IsSuccess())
                {
                    output.IsChangeSuccess = result.IsSuccess() && result.TradeState == "SUCCESS";

                    if (family != null)
                    {
                        await AddPlayerNewProfession(family, new AddPlayerNewProfessionRequest
                        {
                            PayAmount = result.TotalFee,
                            PlayerId = input.PlayerId,
                            ProfessionId = input.ProfessionId
                        });
                    }
                }
                else
                {
                    output.IsChangeSuccess = false;
                }
            }
            catch (Exception exp)
            {
                throw new UserFriendlyException($"获取转职结果异常:{exp}");
            }

            return output;
        }

        private class AddPlayerNewProfessionRequest
        {
            public Guid PlayerId { get; set; }
            public int ProfessionId { get; set; }
            public double PayAmount { get; set; }

            public bool IsVirtual { get; set; }

            public CostType CostType { get; set; }
        }
        private async Task AddPlayerNewProfession(Family family, AddPlayerNewProfessionRequest input)
        {
            var parent = family.FatherId == input.PlayerId ? "爸爸" : "妈妈";
            var receiverId = family.FatherId == input.PlayerId ? family.MotherId : family.FatherId;
            var myOldProfession = await _playerProfessionRepository.GetAll()
                .Include(p => p.Profession)
                .Where(m => m.PlayerId == input.PlayerId && m.FamilyId == family.Id && m.IsCurrent == true)
                .FirstOrDefaultAsync(x => true);

            if (myOldProfession != null)
            {
                myOldProfession.IsCurrent = false;
                await _playerProfessionRepository.UpdateAsync(myOldProfession);
            }

            await _playerProfessionRepository.InsertAsync(new PlayerProfession
            {
                CreationTime = DateTime.UtcNow,
                FamilyId = family.Id,
                IsCurrent = true,
                LastModificationTime = DateTime.UtcNow,
                PlayerId = input.PlayerId,
                ProfessionId = input.ProfessionId,
                IsVirtualRecharge = input.IsVirtual
            });

            Stopwatch watch = new Stopwatch();
            watch.Start();
            //var dict = _playerProfessionRepository.GetAll()
            //    .Include(p => p.Profession)
            //        .ThenInclude(p => p.Reward)
            //    .Where(p => p.FamilyId == family.Id && p.IsCurrent)
            //    .ToDictionary(p => p.PlayerId, p => p.Profession);

            watch.Stop();
            Console.WriteLine($"查询职业记录某一条耗时{watch.Elapsed.TotalMilliseconds}毫秒");

            //更新幸福指數
            var selfCurrentProfession = _repository.Get(input.ProfessionId);

            if (selfCurrentProfession.RewardId.HasValue)
            {
                var reward = await _rewardRepository.GetAsync(selfCurrentProfession.RewardId.Value);

                //var otherPlayerProfession = dict[receiverId];
                //var selfOldProfession = dict[input.PlayerId];
                var baby = family.Babies.FirstOrDefault(b => b.State == BabyState.UnderAge);
                if (baby != null) {
                    baby.Charm += reward.Charm;
                    baby.Energy += reward.Energy;
                    baby.EmotionQuotient += reward.EmotionQuotient;
                    baby.Healthy += reward.Healthy;
                    baby.Imagine += reward.Imagine;
                    baby.Intelligence += reward.Intelligence;
                    baby.Physique += reward.Physique;
                    baby.WillPower += reward.WillPower;
                    await _babyRepository.UpdateAsync(baby);

                    //属性值(健康与精力)触发事件
                    //await _redisMemoryCache.SetStringAsync(HorseRaceLampMessageType.BabyBirth, $"恭喜xxx宝宝的爸爸转职为xxx，获得{reward.CoinCount}金币");
                    //var triggerType = family.FatherId == input.PlayerId ? TriggerType.FatherProfessionAddition : TriggerType.MotherProfessionAddition;
                    //更新宝宝成长记录
                    await _babyGrowUpRecordAppService.AddBabyGrowUpRecord(new BabyGrowUpRecordEditDto()
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
                        PlayerId = input.PlayerId,
                        TriggerType=  TriggerType.ParentsProfessionAddition,
                    });
                }
                //幸福度计算
                //var currentHappiness = CalculateHappinessDegree(family.Happiness,otherPlayerProfession.SatisfactionDegree, myOldProfession.Profession.SatisfactionDegree, selfCurrentProfession.SatisfactionDegree);
                //family.Happiness = currentHappiness/*selfProfession.SatisfactionDegree + otherPlayerProfession.SatisfactionDegree*/;
                //family.Happiness = currentHappiness >= 100 ? 100 : currentHappiness;
                var title = string.Empty;
                if (input.CostType == CostType.Coin)
                {
                    title = "个金币";
                    //添加金币记录
                    await _familyCoinDCRepository.InsertAsync(new FamilyCoinDepositChangeRecord {
                        Amount = Convert.ToDouble(input.PayAmount),
                        CostType = CoinCostType.ChangeProfession,
                        CurrentFamilyCoinDeposit = family.Deposit,
                        FamilyId = family.Id ,
                        StakeholderId = input.PlayerId
                    });
                    family.Deposit -= Convert.ToDouble(input.PayAmount);
                }
                else
                {
                    title = "元";
                    family.ChargeAmount += input.PayAmount;
                }
                //family.Deposit += reward.CoinCount;
                if (input.IsVirtual)
                {
                    family.VirtualRecharge += input.PayAmount;
                }

                await _familyRepository.UpdateAsync(family);

                //发给自己的消息
                await _informationAppService.Add(new InformationEditDto()
                {
                    Content = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, input.PayAmount,title, selfCurrentProfession.Name),
                    FamilyId = family.Id,
                    ReceiverId = input.PlayerId,
                    Type = InformationType.Event,
                    SenderId = input.PlayerId
                });

                //发给对方的消息
                await _informationAppService.Add(new InformationEditDto()
                {
                    Content = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, input.PayAmount,title, selfCurrentProfession.Name),
                    FamilyId = family.Id,
                    ReceiverId = receiverId,
                    Type = InformationType.Event,
                    SenderId = input.PlayerId
                });

                //系统消息
                await _informationAppService.Add(new InformationEditDto()
                {
                    Content = String.Format(SYSTEM_MESSAGE_FORMAT, baby.Name, parent, input.PayAmount,title, selfCurrentProfession.Name),
                    FamilyId = family.Id,
                    Type = InformationType.System,
                    SenderId = input.PlayerId,
                    ReceiverId = receiverId,
                });
            }
        }

        protected double CalculateHappinessDegree(double oldFamilyValue,double otherValue, double selfOldValue, double selfCurrentValue)
        {
            var eventValue = oldFamilyValue - (otherValue + selfOldValue);

            var currentFamilyValue = eventValue + selfCurrentValue + otherValue;

            return currentFamilyValue;
        }

        public async Task<ProfessionListDto> GetProfession(GetProfessionInput input)
        {
            var playerProfessions = await _playerProfessionRepository.GetAll()
                .Include(p => p.Profession).ThenInclude(c => c.Costs)
              .Where(p => p.FamilyId == input.FamilyId && p.PlayerId == input.PlayerId && p.IsCurrent == true)
              .FirstOrDefaultAsync();
            return playerProfessions.Profession.MapTo<ProfessionListDto>();
        }

        /// <summary>
        /// 获取默认职业
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProfessionListDto>> GetDefaultProfession()
        {
            var list = await _repository.GetAllListAsync(s => s.IsDefault);
            var response = list.MapTo<List<ProfessionListDto>>();
            return response;
        }

        public async Task<GetParentProfessionOutput> GetParentProfession(GetParentProfessionInput input)
        {
            var family = _familyRepository.Get(input.FamilyId);

            var fatherProfession = await GetProfession(new GetProfessionInput()
            {
                FamilyId = input.FamilyId,
                PlayerId = family.FatherId
            });

            var motherProfession = await GetProfession(new GetProfessionInput()
            {
                FamilyId = input.FamilyId,
                PlayerId = family.MotherId
            });

            List<ProfessionListDto> professionListDtos = new List<ProfessionListDto>();

            professionListDtos.Add(fatherProfession);

            professionListDtos.Add(motherProfession);

            var output = new GetParentProfessionOutput();

            output.ProfessionListDtos = new List<ProfessionListDto>();

            output.ProfessionListDtos = professionListDtos;

            return output;
        }

        public async Task<MiniProgramPayOutput> VirtualChangeProfession(VirtualChangeProfessionInput input)
        {
            var family = _familyRepository.Get(input.FamilyId);

            var output = await ChangeProfession(new ChangeProfessionInput()
            {
                PlayerId = input.PlayerId,
                FamilyId = input.FamilyId,
                ProfessionId = input.ProfessionId,
                CostType = CostType.Money,
                IsVirtualChange = true,
            });

            return output;
        }
    }
}