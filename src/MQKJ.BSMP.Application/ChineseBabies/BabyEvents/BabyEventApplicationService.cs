using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures;
using MQKJ.BSMP.ChineseBabies.BabyEvents.Dtos;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Players;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyEvent应用层服务的接口实现方法  
    ///</summary>
    public class BabyEventAppService :
        BsmpApplicationServiceBase<BabyEvent, int, BabyEventEditDto, BabyEventEditDto, GetBabyEventsInput, BabyEventListDto>, IBabyEventAppService
    {
        private readonly IRepository<EventGroupBabyEvent> _groupEventsRepository;
        private readonly IRepository<Baby, int> _babyRepository;
        private readonly IRepository<EventGroup, int> _eventGroupRepository;
        private readonly IRepository<BabyEventRecord, Guid> _babyEventRecordRepository;
        private readonly IRepository<BabyGrowUpRecord, Guid> _babyGrowUpRecordRepository;
        private readonly IRepository<Family, int> _familyRepository;
        private readonly IRepository<Information, Guid> _informationRepository;
        private readonly IRepository<BabyEnding, int> _babyEndingRepository;
        private readonly IRepository<Reward, int> _rewardRepository;
        private readonly IRepository<Player, Guid> _playerRepository;
        //private IDistributedCache _memoryCache;
        private readonly IRepository<PlayerProfession, int> _playerProfessionRepository;
        private readonly IRepository<SystemSetting, int> _systemSettingRepository;
        private readonly IRepository<BabyAssetFeature, Guid> _babyAssetFeatureRepository;
        private readonly IBabyAssetFeatureApplicationService _babyAssetFeatureApplicationService;

        public BabyEventAppService(
            IRepository<BabyEvent, int> repository,
            IRepository<Baby, int> babyRepository,
            IRepository<EventGroup, int> eventGroupRepository,
            IRepository<BabyEventRecord, Guid> babyEventRecordRepository,
            IRepository<BabyGrowUpRecord, Guid> babyGrowUpRecordRepository,
             IRepository<Family, int> familyRepository,
             IRepository<Information, Guid> informationRepository,
             IRepository<BabyEnding, int> babyEndingRepository,
             IRepository<Reward, int> rewardRepository,
             IRepository<Player, Guid> playerRepository,
              IRepository<PlayerProfession, int> playerProfessionRepository,
           IRepository<SystemSetting, int> systemSettingRepository,
           IRepository<BabyAssetFeature, Guid> babyAssetFeatureRepository,
           IBabyAssetFeatureApplicationService babyAssetFeatureApplicationService,

        //IDistributedCache memoryCache,
        IRepository<EventGroupBabyEvent> groupEventsRepository) : base(repository)
        {
            _groupEventsRepository = groupEventsRepository;
            _babyRepository = babyRepository;
            _eventGroupRepository = eventGroupRepository;
            _babyEventRecordRepository = babyEventRecordRepository;
            _babyGrowUpRecordRepository = babyGrowUpRecordRepository;
            _familyRepository = familyRepository;
            _informationRepository = informationRepository;
            _babyEndingRepository = babyEndingRepository;
            _rewardRepository = rewardRepository;
            //_memoryCache = memoryCache;
            _playerRepository = playerRepository;
            _playerProfessionRepository = playerProfessionRepository;
            _systemSettingRepository = systemSettingRepository;
            _babyAssetFeatureRepository = babyAssetFeatureRepository;
            _babyAssetFeatureApplicationService = babyAssetFeatureApplicationService;
        }
        /// <summary>
        /// 获取成长事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<GetGrowUpEventsOutput> GetGrowUpEvents(GetGrowUpEventsInput input)
        {
            var response = new GetGrowUpEventsOutput();
            var growUpEvents = new List<GetGrowUpEventsOutputGrowUpEvent>();
            var growUpEmergencies = new List<GetGrowUpEventsOutputGrowUpEvent>();
            //获取宝宝
            var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            //获取当前事件
            var result = _groupEventsRepository.GetAllIncluding(s => s.EventGroup)
                  .Include(s => s.BabyEvent).ThenInclude(s => s.Options).ThenInclude(s => s.Reward)
                  .Include(s => s.BabyEvent).ThenInclude(s => s.Options).ThenInclude(s => s.Consume)
                  .Where(s => s.BabyEvent != null && s.BabyEvent.Type != IncidentType.Study && s.GroupId == baby.GroupId && !s.IsDeleted).OrderBy(s => s.BabyEvent.Code).AsNoTracking();
            var eventRecordsAsync = await _babyEventRecordRepository.GetAllIncluding(s => s.Event).Where(s => s.BabyId == input.BabyId && s.GroupId == baby.GroupId).ToListAsync();

            var assetFeature = _babyAssetFeatureApplicationService.GetAssetFeature(new AssetFeatureWorkInput() { FamilyId = baby.FamilyId, BabyId = input.BabyId, EventType = EventAdditionType.GrowUp });
            await result.ForEachAsync(s =>
            {
                var model = new GetGrowUpEventsOutputGrowUpEvent();
                model.Event = ObjectMapper.Map<GetGrowUpEventsOutputEvent>(s.BabyEvent);
                 //options
                 //判断金币消耗是否需要乘以系数
                 if (baby.BirthOrder > 1)
                {
                    s.BabyEvent.Options.ToAsyncEnumerable().ForEach(d =>
                    {
                        d.Consume.CoinCount = d.Consume.CoinCount * baby.BirthOrder;
                    });
                }
                var options = ObjectMapper.Map<List<GetGrowUpEventsOutputEventOptions>>(s.BabyEvent.Options);
                model.Options = options;
                 //record
                 var eventRecords = eventRecordsAsync;
                var record = eventRecords.LastOrDefault(rec => rec.BabyId == baby.Id && rec.GroupId == baby.GroupId && rec.EventId == s.EventId);
                if (record != null)
                {
                    model.Record = ObjectMapper.Map<GetGrowUpEventsOutputRecord>(record);
                }
                 //item 
                 if (s.BabyEvent.Type == IncidentType.Growup && (s.BabyEvent.ConditionType == ConditionType.Normal))
                {
                    growUpEvents.Add(model);
                }
                else if (s.BabyEvent.ConditionType != ConditionType.Normal)
                {
                     //判断是否触发突发事件
                     //属性触发
                     //var spcialEventRecord = s.BabyEvent.BabyEventRecords.Count(r => r.State != EventRecordState.Handled && r.BabyId == baby.Id);
                     if ((s.BabyEvent.ConditionType == ConditionType.Property &&
                (s.BabyEvent.BabyProperty == BabyProperty.Healthy && (baby.Healthy <= s.BabyEvent.MaxValue && baby.Healthy > s.BabyEvent.MinValue))
                || (s.BabyEvent.BabyProperty == BabyProperty.Happiness && (baby.Family.Happiness <= s.BabyEvent.MaxValue && baby.Family.Happiness > s.BabyEvent.MinValue))))
                    {
                        if (s.BabyEvent.Type == IncidentType.Block)
                        {
                             //var spcialEventRecord = s.BabyEvent.BabyEventRecords.Count(r => r.State != EventRecordState.Handled && r.State != EventRecordState.WaitOther && r.BabyId == baby.Id);
                             var spcialEventRecord = eventRecords.Count(r => r.State != EventRecordState.Handled && r.State != EventRecordState.WaitOther && r.BabyId == baby.Id && r.EventId == s.EventId);
                            if (spcialEventRecord == 0)
                            {
                                model.Event.IsBlock = true;
                                growUpEvents.Add(model);
                            }
                        }
                        else
                        {
                            growUpEmergencies.Add(model);
                        }
                    }
                    else if (s.BabyEvent.ConditionType == ConditionType.Event)
                    {
                        if (IsValidGrowUpSpecialEvent(s, baby, eventRecords))
                        {
                            if (s.BabyEvent.Type == IncidentType.Block)
                            {
                                model.Event.IsBlock = true;
                                growUpEvents.Add(model);
                            }
                            else
                            {
                                growUpEmergencies.Add(model);
                            }
                        }
                    }
                }
            });
            var group = await result.FirstOrDefaultAsync();
            var groupInfo = new GetGrowUpEventsOutputGroupInfo()
            {
                GroupName = group?.EventGroup.Description
            };
            response.GroupInfo = groupInfo;
            // 排序
            response.GrowUpEvents = growUpEvents;
            response.GrowUpEmergencies = growUpEmergencies;
            response.AssetFeature = await assetFeature;
            return response;

        }
        /// <summary>
        /// 校验是否为有效的突发事件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="baby"></param>
        /// <returns></returns>
        private bool IsValidGrowUpSpecialEvent(EventGroupBabyEvent s, Baby baby, List<BabyEventRecord> babyEventRecords)
        {
            //事件触发型，需要前置事件完成时加载，获取（前置）导火索事件
            var blastingFuse = babyEventRecords.FirstOrDefault(e => e.Event.EventId == s.BabyEvent.Id);
            //Console.WriteLine($"===================导火索事件：{blastingFuse?.Event.Name}，状态：{blastingFuse?.State}");
            //var blastingFuseEventRecords = _babyEventRecordRepository.GetAllIncluding().Where(r => r.BabyId == baby.Id && r.EventId == s.BabyEvent.EventId);
            if (blastingFuse?.Event.Type != IncidentType.Growup)
            {
                return false;
            }
            //触发次数为0且前置（导火索事件）状态为已完成
            var spcialEventRecord = babyEventRecords.Count(r => r.State != EventRecordState.Handled && r.EventId == s.BabyEvent.Id && r.BabyId == baby.Id);
            //var blastingFuseEventRecord = babyEventRecords?.FirstOrDefault(d => d.EventId == s.BabyEvent.EventId);
            return blastingFuse != null && spcialEventRecord <= 1 && blastingFuse.State == EventRecordState.Handled;
        }

        //
        private bool IsValidStudySpecialEvent(EventGroupBabyEvent s, Baby baby, List<BabyEventRecord> babyEventRecords)
        {
            //事件触发型，需要前置事件完成时加载，获取（前置）导火索事件
            var blastingFuse = babyEventRecords.FirstOrDefault(e => e.Event.EventId == s.BabyEvent.Id);

            if (blastingFuse != null && blastingFuse?.Event.Type != IncidentType.Study)
            {
                return false;
            }
            //触发次数为0且前置（导火索事件）状态为已完成
            var spcialEventRecord = babyEventRecords.Count(r => r.State != EventRecordState.Handled && r.EventId == s.BabyEvent.Id && r.BabyId == baby.Id);
            //var blastingFuseEventRecord = babyEventRecords?.FirstOrDefault(d => d.EventId == s.BabyEvent.EventId);
            return blastingFuse != null && spcialEventRecord <= 1 && blastingFuse.State == EventRecordState.Handled;
        }
        public async Task<GetStudyEventsOutput> GetStudyEvents(GetStudyEventsInput input)
        {
            var response = new GetStudyEventsOutput();
            var studyEvents = new List<GetStudyEventsOutputStudyEvent>();
            var studyEmergencies = new List<GetStudyEventsOutputStudyEvent>();
            try
            {
                //Stopwatch stopwatch = new Stopwatch();
                //Stopwatch stopwatch1 = new Stopwatch();

                //stopwatch.Start();
                //获取宝宝
                var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
                response.Setting.Coefficient = baby.BirthOrder;
                //获取当前事件
                var result = _groupEventsRepository.GetAllIncluding(s => s.EventGroup)
                  .Include(s => s.BabyEvent).ThenInclude(s => s.Options).ThenInclude(s => s.Reward)
                  .Include(s => s.BabyEvent).ThenInclude(s => s.Options).ThenInclude(s => s.Consume)
                  .Where(s => s.BabyEvent != null && s.BabyEvent.Type != IncidentType.Growup && s.GroupId == baby.GroupId).OrderBy(s => s.BabyEvent.Code).AsNoTracking();
                var eventRecordsAsync = _babyEventRecordRepository.GetAllIncluding(s => s.Event).Where(s => s.BabyId == input.BabyId && s.GroupId == baby.GroupId).ToListAsync();

                //Logger.Debug($"获取学习事件1，共耗时：{stopwatch.ElapsedMilliseconds}毫秒");
                //stopwatch.Restart();
                var assetFeature = await _babyAssetFeatureApplicationService.GetAssetFeature(new AssetFeatureWorkInput() { FamilyId = baby.FamilyId, BabyId = input.BabyId, EventType = EventAdditionType.Study });
                response.AssetFeature = assetFeature;
                await result.ForEachAsync(async s =>
                {
                    //stopwatch1.Start();
                    var model = new GetStudyEventsOutputStudyEvent();
                    model.Event = ObjectMapper.Map<StudyEventBabyEvent>(s.BabyEvent);
                    //options
                    //判断金币消耗是否需要乘以系数
                    if (baby.BirthOrder > 1)
                    {
                        await s.BabyEvent.Options.ToAsyncEnumerable().ForEachAsync(d =>
                        {
                            d.Consume.CoinCount = d.Consume.CoinCount * baby.BirthOrder;
                        });
                    }
                    var options = ObjectMapper.Map<List<StudyEventOptions>>(s.BabyEvent.Options);
                    model.Options = options;
                    var eventRecords = await eventRecordsAsync;

                    if (model.Event != null && s.BabyEvent != null)
                    {
                        //model.Event.Time = s.BabyEvent.BabyEventRecords.Count(sr => sr.BabyId == input.BabyId);
                        model.Event.Time = eventRecords.Count(sr => sr.BabyId == input.BabyId && sr.EventId == s.EventId);
                    }
                    //record
                    //var record = s.BabyEvent?.BabyEventRecords.LastOrDefault(rec => rec.PlayerId != input.PlayerGuid && rec.BabyId == input.BabyId);

                    var record = eventRecords.LastOrDefault(rec => rec.PlayerId != input.PlayerGuid && rec.BabyId == input.BabyId && rec.EventId == s.EventId);
                    if (record != null)
                    {
                        model.Record = ObjectMapper.Map<GetStudyEventsOutputRecord>(record);
                        var currentTimeStamp = new DateTimeOffset(DateTime.UtcNow);
                        model.Record.State = record.EndTimeStamp < currentTimeStamp.ToUnixTimeSeconds() ? EventRecordState.UnHandle : EventRecordState.Handling;
                    }
                    //item 
                    if (s.BabyEvent.Type == IncidentType.Study && (s.BabyEvent.ConditionType == ConditionType.Normal))
                    {
                        studyEvents.Add(model);
                    }
                    else if (s.BabyEvent.ConditionType != ConditionType.Normal && s.BabyEvent.Type != IncidentType.Growup)
                    {
                        //判断是否触发突发事件
                        if ((s.BabyEvent.ConditionType == ConditionType.Property &&
                        (s.BabyEvent.BabyProperty == BabyProperty.Healthy && (baby.Healthy <= s.BabyEvent.MaxValue && baby.Healthy > s.BabyEvent.MinValue))
                        || (s.BabyEvent.BabyProperty == BabyProperty.Happiness && (baby.Family.Happiness <= s.BabyEvent.MaxValue && baby.Family.Happiness > s.BabyEvent.MinValue))))
                        {
                            //属性触发
                            if (s.BabyEvent.Type == IncidentType.Block)
                            {
                                //var spcialEventRecord = s.BabyEvent.BabyEventRecords.Count(r => r.State != EventRecordState.Handled && r.State != EventRecordState.WaitOther && r.BabyId == baby.Id);
                                var spcialEventRecord = eventRecords.Count(r => r.State != EventRecordState.Handled && r.State != EventRecordState.WaitOther && r.BabyId == baby.Id && r.EventId == s.EventId);
                                if (spcialEventRecord == 0)
                                {
                                    model.Event.IsBlock = true;
                                    studyEvents.Add(model);
                                }
                            }
                            else
                            {
                                studyEmergencies.Add(model);
                            }
                        }
                        else if (s.BabyEvent.ConditionType == ConditionType.Event)
                        {
                            if (IsValidStudySpecialEvent(s, baby, eventRecords))
                            {
                                if (s.BabyEvent.Type == IncidentType.Block)
                                {
                                    model.Event.IsBlock = true;
                                    studyEvents.Add(model);
                                }
                                else
                                {
                                    studyEmergencies.Add(model);
                                }
                            }
                        }
                    }
                });
                //Logger.Debug($"获取学习事件2，共耗时：{stopwatch.ElapsedMilliseconds}毫秒");

                var group = await result.FirstOrDefaultAsync();
                var groupInfo = new GetStudyEventsOutputGroupInfo()
                {
                    GroupName = group?.EventGroup.Description
                };
                response.GroupInfo = groupInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            response.StudyEmergencies = studyEmergencies;
            response.StudyEvents = studyEvents;
            return response;
        }

        public async Task<HandleEventOutput> HandleEvent(HandleEventInput input)
        {
            var response = new HandleEventOutput();
            // 校验提交申请
            var eventItem = await ValidEventSubmit(input);
            // 定时更新相关数据
            response = await AddOrUpdateBabyEventRecord(input, eventItem);

            return response;
        }


        /// <summary>
        /// 验证提交事件是否有效
        /// </summary>
        /// <returns></returns>
        public async Task<BabyEvent> ValidEventSubmit(HandleEventInput input)
        {
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            var babyEvent = await _repository.GetAllIncluding(s => s.Reward).Include(s => s.Consume).Include(s => s.BabyEventRecords)
                .Include(s => s.Options).ThenInclude(s => s.Reward)
                .Include(s => s.Options).ThenInclude(s => s.Consume)
                .FirstOrDefaultAsync(s => s.Id == input.EventId);
            var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            //stopWatch.Stop();
            //Console.WriteLine("=================================");
            //Console.WriteLine($"{stopWatch.ElapsedMilliseconds}");
            //Console.WriteLine("=================================");
            //stopWatch.Start();
            //必要的判断
            //准备
            var eventRecords = babyEvent.BabyEventRecords.Where(s => s.BabyId == input.BabyId);
            var currentTime = DateTime.UtcNow.AddSeconds(babyEvent.CountDown);
            var currentTimeStamp = new DateTimeOffset(currentTime);
            var lastEventRecord = eventRecords.LastOrDefault(s => s.GroupId == baby.GroupId);
            var reward = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Reward;
            var consume = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Consume;
            //CD判断，排除有过期时间的事件，过期的判断在下边进行
            if (lastEventRecord?.EndTimeStamp > currentTimeStamp.ToUnixTimeSeconds() && lastEventRecord.Event.ExpirationGroupId == null)
            {
                throw new AbpException("事件已提交，处于CD期");
            }
            //判断消耗是否够用，只判断健康和精力
            if (baby.Family.Deposit + consume?.CoinCount < 0)
            {
                throw new AbpException("宝宝金钱不够消耗");
            }
            if (baby.Energy + consume?.Energy < 0)
            {
                throw new AbpException("宝宝精力不够消耗");
            }
            //是否存在阻断事件检查
            //- 获取宝宝当前组内阻断事件（因为只可能同时存在一个，所以取第一个）
            var babyBlockEvent = await _groupEventsRepository.GetAllIncluding(s => s.BabyEvent).FirstOrDefaultAsync(s => s.GroupId == baby.GroupId && s.BabyEvent.Type == IncidentType.Block);
            if (babyBlockEvent != null)
            {
                //- 判断阻断事件是否已处理（获取未结束的阻断事件）
                var blockEventRecord = await _babyEventRecordRepository.CountAsync(s => s.BabyId == input.BabyId && s.EventId == babyBlockEvent.EventId && s.GroupId == baby.GroupId && s.EndTimeStamp > currentTimeStamp.ToUnixTimeSeconds());
                if (blockEventRecord > 0)
                {
                    throw new AbpException("存在阻断性事件");
                }
            }

            if (babyEvent.Type == IncidentType.Special)
            {
                //判断事件是否过期
                if (babyEvent.ExpirationGroupId != null && babyEvent.ExpirationGroupId != 0 && babyEvent.ExpirationGroupId < baby.GroupId)
                {
                    throw new AbpException("事件已过期");
                }
            }
            if (babyEvent.OperationType == OperationType.Double)
            {
                var isFather = input.PlayerGuid == baby.Family.FatherId;
                if (lastEventRecord != null && ((lastEventRecord.MotherOptionId == null && isFather)
                    || (lastEventRecord.FatherOptionId == null && !isFather)))
                {
                    //爸爸修改自己的答案（妈妈还没有提交）
                    //妈妈修改自己的答案（爸爸还没有提交）
                    throw new AbpException("对方还没有提交，不允许修改");
                }
            }
            else
            {
                //单人事件
                //事件已完成，因为学习事件可以多次进行，排除学习事件
                if (babyEvent.Type != IncidentType.Study)
                {
                    //非学习事件只能执行一次
                    if (eventRecords.LastOrDefault() != null)
                    {
                        throw new AbpException("事件已处理");
                    }
                }
                if (babyEvent.Type == IncidentType.Growup)
                {

                }
                else if (babyEvent.Type == IncidentType.Study)
                {
                    //判断是否超过最大学习次数
                    if (eventRecords.Count() >= babyEvent.StudyAllowMaxTime)
                    {
                        throw new AbpException("超过最大可学习次数");
                    }
                }
                else if (babyEvent.Type == IncidentType.Special)
                {

                }
                else if (babyEvent.Type == IncidentType.Block)
                {

                }
            }
            //Console.WriteLine("=================================");
            //Console.WriteLine($"{stopWatch.ElapsedMilliseconds}");
            //Console.WriteLine("=================================");
            return babyEvent;
        }
        Func<IncidentType, string> ToIncidentTypeString = (IncidentType t) =>
        {
            switch (t)
            {
                case IncidentType.Growup:
                    return "成长事件";
                case IncidentType.Study:
                    return "学习事件";
                case IncidentType.Special:
                    return "突发事件";
                default:
                    return "事件";
            }

        };
        /// <summary>
        /// 增加或更新事件记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="babyEvent"></param>
        /// <returns></returns>
        private async Task<HandleEventOutput> AddOrUpdateBabyEventRecord(HandleEventInput input, BabyEvent babyEvent)
        {
            var res = new HandleEventOutput();
            var record = new BabyEventRecord();
            //判断是否为双人
            //双人的话是否是第一个人第一次提交
            var eventRecord = babyEvent.BabyEventRecords.Where(s => s.BabyId == input.BabyId && (s.PlayerId == input.PlayerGuid || s.PlayerId == input.TheOtherGuid) && s.EndTimeStamp == null).LastOrDefault();
            //双人事件
            var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            var isFather = input.PlayerGuid == baby.Family.FatherId;
            var title = isFather ? "爸爸" : "妈妈";
            var isGrowUpEvent = babyEvent.Type == IncidentType.Growup;
            var eventType = isGrowUpEvent ? EventAdditionType.GrowUp : EventAdditionType.Study;//TODO: 此处还需进一步完善
            var assetFeature = await _babyAssetFeatureApplicationService.GetAssetFeature(new AssetFeatureWorkInput() { FamilyId = baby.FamilyId, BabyId = input.BabyId, EventType = eventType });
            var cd = babyEvent.CountDown - Convert.ToInt32((assetFeature?.CD * babyEvent.CountDown));
            if (babyEvent.OperationType == OperationType.Double)
            {
                //一方已提交，
                if (eventRecord != null)
                {
                    //并且自己提交的选项编号与对方提交的一致
                    //更新事件为处理中，设置结束时间
                    //新增
                    if ((!isFather && input.OptionId == eventRecord.FatherOptionId) || (isFather && input.OptionId == eventRecord.MotherOptionId))
                    {
                        //妈妈或爸爸提交答案，并且双方答案一致
                        var endTimeStamp = new DateTimeOffset(DateTime.UtcNow.AddSeconds(cd));
                        eventRecord.EndTimeStamp = endTimeStamp.ToUnixTimeSeconds();
                        eventRecord.State = EventRecordState.Handling;
                    }
                    else
                    {
                        eventRecord.State = EventRecordState.UnHandle;
                    }
                    if (isFather)
                    {
                        eventRecord.FatherOptionId = input.OptionId;
                    }
                    else if (!isFather)
                    {
                        //母亲
                        eventRecord.MotherOptionId = input.OptionId;
                    }

                    record = await _babyEventRecordRepository.UpdateAsync(eventRecord);
                    //发送消息
                    if (eventRecord.FatherOptionId != eventRecord.MotherOptionId)
                    {
                        var isConsistent = "不一致";
                        var footer = "请您再次做出选择。";
                        await AddEventMsg(input, $"孩子的{title}就{ToIncidentTypeString(babyEvent.Type)}{babyEvent.Name}做出了选择，你们的选择{isConsistent}。{footer}", InformationType.Event);
                    }
                    res.State = eventRecord.State;
                }
                else if (eventRecord == null)
                {
                    record = await AddBabyEventRecord(input, babyEvent, false, cd);
                    //发送消息
                    await AddEventMsg(input, $"孩子的{title}就{ToIncidentTypeString(babyEvent.Type)}{babyEvent.Name}做出了选择，请您做出选择。", InformationType.Event);
                    res.State = EventRecordState.WaitOther;
                }
            }
            else
            {
                //单人事件
                record = await AddBabyEventRecord(input, babyEvent, true, cd);
                //发送消息
                res.State = EventRecordState.Handling;
            }
            //是否结束
            res.EndTimeStamp = record.EndTimeStamp;
            //var reward = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Reward;

            var inputJson = JsonConvert.SerializeObject(input);
            AddUpdateEventRecordJob(record, inputJson);
            // 处理消耗的金币、精力
            var consume = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Consume;
            await UpdateConsume(consume, baby, baby.Family, assetFeature);

            //是否触发其他事件
            return res;
        }
        /// <summary>
        /// 更新消耗
        /// </summary>
        /// <returns></returns>
        private async Task<bool> UpdateConsume(Reward consume, Baby baby, Family family, AssetFeatureDto assetFeature)
        {
            var res = false;
            // 消耗的金币
            var consumeCoin = consume.CoinCount * baby.BirthOrder;
            if (assetFeature?.Coin != null && assetFeature?.Coin != 0)
            {
                consumeCoin = consumeCoin - Convert.ToInt32((assetFeature?.Coin * consumeCoin));
            }
            family.Deposit += consumeCoin;
            await _familyRepository.UpdateAsync(family);
            // 消耗的精力
            // 消耗的精力=消耗精力-装备对精力精力消耗的加成
            var energyConsume = consume.Energy;
            if (assetFeature?.Energy != null && assetFeature?.Energy != 0)
            {
                energyConsume = Convert.ToInt32(consume.Energy - assetFeature?.Energy * consume.Energy);
            }
            baby.Energy += energyConsume;
            await _babyRepository.UpdateAsync(baby);
            res = true;
            return res;
        }
        /// <summary>
        /// 增加定时任务更新状态
        /// </summary>
        /// <param name="record"></param>
        private void AddUpdateEventRecordJob(BabyEventRecord record, string inputJson)
        {
            if (record.State == EventRecordState.Handling && record.EndTime != null)
            {
                var timeSpan = new DateTimeOffset(Convert.ToDateTime(record.EndTime));
                BackgroundJob.Schedule<BabyEventAppService>((s) => s.HandleBabyEventJob(record.Id, EventRecordState.Handled, inputJson), timeSpan);
            }
        }
        /// <summary>
        /// 定时更新事件记录状态
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="state"></param>
        /// <param name="eventName"></param>
        /// <param name="rewardId"></param>
        /// <param name="image"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task UpdateEventRecordState(Guid recordId, EventRecordState state, string eventName, int? rewardId, string image, string title, string content)
        {
            var record = await _babyEventRecordRepository.GetAsync(recordId);
            if (record != null)
            {
                record.State = state;
                await _babyEventRecordRepository.UpdateAsync(record);
                var remark = "";
                if (rewardId != null)
                {
                    var reward = await _rewardRepository.GetAsync(Convert.ToInt32(rewardId));
                    //发送事件完成消息
                    if (reward != null)
                    {
                        remark = JsonConvert.SerializeObject(reward);
                    }
                }
                //await UpdateBabyAndFamily(input, eventItem);
                //await AddBabyPopertyRecord(input, eventItem);

                await _informationRepository.InsertAsync(new Information()
                {
                    Title = title,
                    Content = content,
                    FamilyId = record.FamilyId,
                    State = InformationState.Create,
                    Type = InformationType.Event,
                    ReceiverId = record.PlayerId,
                    SenderId = record.PlayerId,
                    SystemInformationType = SystemInformationType.EventCompleted,
                    NoticeType = NoticeType.Popout,
                    BabyEventId = record.EventId,
                    Remark = remark,
                    Image = image
                });
            }
        }
        /// <summary>
        /// 提交宝宝成长/学习事件后，定时更新该事件状态、更新家庭和宝宝信息、增加相应记录及发送系统信息
        /// </summary>
        [UnitOfWork]
        public virtual async Task HandleBabyEventJob(Guid recordId, EventRecordState state, string inputJson)
        {
            var record = await _babyEventRecordRepository.GetAsync(recordId);
            var input = JsonConvert.DeserializeObject<HandleEventInput>(inputJson);
            if (record != null)
            {
                record.State = state;
                await _babyEventRecordRepository.UpdateAsync(record);
                var remark = "";
                var babyEvent = _repository.GetAllIncluding()
                    .Include(s => s.Options).ThenInclude(s => s.Reward)
                   .Include(s => s.Options).ThenInclude(s => s.Consume)
                    .FirstOrDefault(s => s.Id == input.EventId);
                //var babyEvent =await _babyEventRepository.GetAsync(input.EventId);
                var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
                var isFather = input.PlayerGuid == baby.Family.FatherId;
                var title = isFather ? "爸爸" : "妈妈";
                if (babyEvent != null)
                {
                    var reward = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Reward;
                    var consume = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Consume;
                    reward = await UpdateBabyAndFamily(input, babyEvent, reward, consume);
                    await AddBabyPopertyRecord(reward, consume, record.BabyId);
                    //发送事件完成消息
                    if (reward != null)
                    {
                        remark = JsonConvert.SerializeObject(reward);
                    }
                    if (babyEvent.OperationType == OperationType.Single)
                    {
                        await AddEventMsg(input, $"{title}带孩子完成了{babyEvent.Name}事件，孩子获得属性成长。", InformationType.Event);
                    }
                    else
                    {
                        var isConsistent = "一致";
                        var footer = "孩子获得属性增长。";
                        await AddEventMsg(input, $"孩子的{title}就{ToIncidentTypeString(babyEvent.Type)}{babyEvent.Name}做出了选择，你们的选择{isConsistent}。{footer}", InformationType.Event);
                    }
                }
                var systemSetting = await _systemSettingRepository.FirstOrDefaultAsync(s => s.Code == 5 && s.Name == "EVENTTITLE");
                var content = systemSetting?.Value == "2" ? $"“{babyEvent.Name}“事件，孩子获得属性增长。" : $"{baby.Name}“{babyEvent.Name}“完成";

                await _informationRepository.InsertAsync(new Information()
                {
                    Title = $"{title}带孩子完成",
                    Content = content,
                    FamilyId = record.FamilyId,
                    State = InformationState.Create,
                    Type = InformationType.Event,
                    ReceiverId = record.PlayerId,
                    SenderId = record.PlayerId,
                    SystemInformationType = SystemInformationType.EventCompleted,
                    NoticeType = NoticeType.Popout,
                    BabyEventId = record.EventId,
                    Remark = remark,
                    Image = babyEvent.ImagePath
                });
            }
        }
        /// <summary>
        /// 增加宝宝事件记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cd"></param>
        /// <param name="isSubmit">是否提交，不提交则不设置结束时间</param>
        private async Task<BabyEventRecord> AddBabyEventRecord(HandleEventInput input, BabyEvent babyEvent, bool isSubmit, int cd)
        {
            //新增
            var endTimeStamp = new DateTimeOffset(DateTime.UtcNow.AddSeconds(cd));
            var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            var isFather = input.PlayerGuid == baby.Family.FatherId;
            var record = await _babyEventRecordRepository.InsertAsync(new BabyEventRecord
            {
                BabyId = input.BabyId,
                FamilyId = input.FamilyId,
                GroupId = Convert.ToInt32(baby.GroupId),
                PlayerId = input.PlayerGuid,
                EventId = input.EventId,
                State = babyEvent.OperationType == OperationType.Double ? EventRecordState.WaitOther : EventRecordState.Handling,
                EndTimeStamp = isSubmit ? (long?)endTimeStamp.ToUnixTimeSeconds() : null,
                EndTime = DateTime.Now.AddSeconds(cd),
                OptionId = input.OptionId,
                FatherOptionId = isFather ? (int?)input.OptionId : null,
                MotherOptionId = !isFather ? (int?)input.OptionId : null
            });
            return record;
        }
        /// <summary>
        /// 增加宝宝属性变更记录
        /// </summary>
        private async Task AddBabyPopertyRecord(Reward reward, Reward consume, int babyId)
        {
            //var reward = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Reward;
            //var consume = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Consume;
            if (reward != null && consume != null)
            {
                await _babyGrowUpRecordRepository.InsertAsync(new BabyGrowUpRecord()
                {
                    BabyId = babyId,
                    Charm = reward.Charm + consume.Charm,
                    Energy = reward.Energy + consume.Energy,
                    EmotionQuotient = reward.EmotionQuotient + consume.EmotionQuotient,
                    Healthy = reward.Healthy + consume.Healthy,
                    Imagine = reward.Imagine + consume.Imagine,
                    Intelligence = reward.Intelligence + consume.Intelligence,
                    Physique = reward.Physique + consume.Physique,
                    WillPower = reward.WillPower + consume.WillPower,
                    TriggerType = TriggerType.BabyEvent
                });
            }

        }
        /// <summary>
        /// 修改宝宝和家庭相关属性
        /// </summary>
        private async Task<Reward> UpdateBabyAndFamily(HandleEventInput input, BabyEvent babyEvent, Reward reward, Reward consume)
        {
            //判断如果是双人，需要第二个人提交时更新
            if (babyEvent.OperationType == OperationType.Double)
            {
                // var eventRecord = await _babyEventRecordRepository.GetAllIncluding().LastOrDefaultAsync(s => s.BabyId == input.BabyId && (s.PlayerId == input.PlayerGuid || s.PlayerId == input.TheOtherGuid) && s.EventId == babyEvent.Id);
                //if (eventRecord.State != EventRecordState.Handling)
                //{
                //    return null;
                //}
            }
            var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            var babyAge = baby.AgeDouble;
            // 装备特性
            var isGrowUpEvent = babyEvent.Type == IncidentType.Growup;
            var eventType = isGrowUpEvent ? EventAdditionType.GrowUp : EventAdditionType.Study;//TODO: 此处还需进一步完善
            var assetFeature = await _babyAssetFeatureApplicationService.GetAssetFeature(new AssetFeatureWorkInput() { FamilyId = baby.FamilyId, BabyId = input.BabyId, EventType = eventType });

            #region 修改宝宝属性及事件组，
            if (reward != null && consume != null)
            {
                // 消耗的精力=消耗精力-装备对精力精力消耗的加成
                var energyConsume = Convert.ToInt32(consume.Energy - assetFeature?.Energy * consume.Energy);
                // 奖励属性加成
                var propertyAddtion = assetFeature == null ? 0 : assetFeature.PropertyAddtion;
                // 计算各个属性加成
                //Console.WriteLine("=================================");
                //Console.WriteLine($"reward.Charm:{reward.Charm},Convert.ToInt32(reward.Charm * propertyAddtion):{Convert.ToInt32(reward.Charm * propertyAddtion)};");
                //Console.WriteLine("=================================");
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
                baby.Charm = baby.Charm + rewardLast.Charm + consume.Charm;
                baby.Energy = baby.Energy + rewardLast.Energy;// + energyConsume;
                baby.EmotionQuotient = baby.EmotionQuotient + rewardLast.EmotionQuotient + consume.EmotionQuotient;
                baby.Healthy = baby.Healthy + rewardLast.Healthy + consume.Healthy;
                baby.Imagine = baby.Imagine + rewardLast.Imagine + consume.Imagine;
                baby.Intelligence = baby.Intelligence + rewardLast.Intelligence + consume.Intelligence;
                baby.Physique = baby.Physique + rewardLast.Physique + consume.Physique;
                baby.WillPower = baby.WillPower + rewardLast.WillPower + consume.WillPower;
                //Console.WriteLine("=================================");
                //Console.WriteLine($"家庭装备属性加成为：{propertyAddtion}，宝宝baby.Charm：{baby.Charm}，reward.Charm:{reward.Charm},consume.Charm:{consume.Charm}");
                //Console.WriteLine("=================================");
                if (babyEvent.Age != null && babyEvent.Age != 0)
                {
                    baby.AgeDouble = Convert.ToDouble(babyEvent.Age);
                }
                if (!string.IsNullOrEmpty(babyEvent.AgeString))
                {
                    baby.AgeString = babyEvent.AgeString;
                }
                await _babyRepository.UpdateAsync(baby);
            }

            #endregion

            #region 修改家庭幸福度&存款&档次（二期）
            var family = baby.Family;
            var salaryTotal = 0.0D;
            double? momSalary = 0.0D;
            double? dadSalary = 0.0D;
            //TODO:判断是否该发工资了
            if (babyEvent.Wage != null && babyEvent.Wage != 0)
            {
                var playerProfessions = _playerProfessionRepository.GetAllIncluding(s => s.Profession).Where(p => p.FamilyId == family.Id);
                var currentPlayerProfession = playerProfessions.Where(p => p.IsCurrent);
                if (currentPlayerProfession.Count() == 0)
                {
                    currentPlayerProfession = playerProfessions;
                }
                if (currentPlayerProfession.Count() > 0)
                {
                    momSalary = currentPlayerProfession.FirstOrDefault(s => s.PlayerId == family.MotherId)?.Profession.Salary;
                    dadSalary = currentPlayerProfession.FirstOrDefault(s => s.PlayerId == family.FatherId)?.Profession.Salary;
                    salaryTotal = Convert.ToDouble(momSalary + dadSalary);
                    if (salaryTotal != 0)
                    {
                        family.Deposit += Convert.ToDouble(salaryTotal) * Convert.ToInt32(babyEvent.Wage);
                    }
                }
            }
            //修改家庭信息
            if (reward != null && consume != null)
            {
                family.Happiness += reward.Happiness + consume.Happiness;
                // x胎系数
                family.Deposit += reward.CoinCount;// + coin;
                await _familyRepository.UpdateAsync(family);
                if (salaryTotal != 0)
                {
                    var monthes = Convert.ToInt32(babyEvent.Wage);
                    //发送系统消息
                    //发送人
                    await _informationRepository.InsertAsync(new Information()
                    {
                        Content = $"孩子的爸爸收到{babyEvent.Wage}个月工资收入" + dadSalary * monthes + "金币",
                        FamilyId = input.FamilyId,
                        State = InformationState.Create,
                        Type = InformationType.Event,
                        ReceiverId = input.PlayerGuid,
                        SenderId = input.PlayerGuid,
                        SystemInformationType = SystemInformationType.PayOff,
                        NoticeType = NoticeType.Popout,
                        BabyEventId = babyEvent.Id,
                        BabyId = input.BabyId,
                        Remark = salaryTotal.ToString()
                    });
                    await _informationRepository.InsertAsync(new Information()
                    {
                        Content = $"孩子的妈妈收到{babyEvent.Wage}个月工资收入" + momSalary * monthes + "金币",
                        FamilyId = input.FamilyId,
                        State = InformationState.Create,
                        Type = InformationType.Event,
                        ReceiverId = input.PlayerGuid,
                        SenderId = input.PlayerGuid,
                        SystemInformationType = SystemInformationType.PayOff,
                        BabyEventId = babyEvent.Id,
                        Remark = salaryTotal.ToString()
                    });
                    //发送系统消息
                    //对方
                    await _informationRepository.InsertAsync(new Information()
                    {
                        Content = $"孩子的爸爸收到{babyEvent.Wage}个月工资收入" + dadSalary * monthes + "金币",
                        FamilyId = input.FamilyId,
                        State = InformationState.Create,
                        Type = InformationType.Event,
                        ReceiverId = input.TheOtherGuid,
                        SenderId = input.PlayerGuid,
                        SystemInformationType = SystemInformationType.PayOff,
                        BabyEventId = babyEvent.Id,
                        Remark = salaryTotal.ToString()
                    });
                    await _informationRepository.InsertAsync(new Information()
                    {
                        Content = $"孩子的妈妈收到{babyEvent.Wage}个月工资收入" + momSalary * monthes + "金币",
                        FamilyId = input.FamilyId,
                        State = InformationState.Create,
                        Type = InformationType.Event,
                        ReceiverId = input.TheOtherGuid,
                        SenderId = input.PlayerGuid,
                        SystemInformationType = SystemInformationType.PayOff,
                        BabyEventId = babyEvent.Id,
                        Remark = salaryTotal.ToString()
                    });
                }

            }
            //是否长大，发送长大消息
            if (babyAge < babyEvent.Age)
            {
                var remark = JsonConvert.SerializeObject(new { ageText = baby.AgeString, gender = baby.Gender, age = Convert.ToInt32(baby.AgeDouble.ToString().Split('.')[0]), ageDouble = baby.AgeDouble });
                //发送系统消息
                //发送人
                await _informationRepository.InsertAsync(new Information()
                {
                    Content = $"时间好快啊，孩子{baby.AgeString}了",
                    FamilyId = input.FamilyId,
                    State = InformationState.Create,
                    Type = InformationType.Event,
                    ReceiverId = input.PlayerGuid,
                    SenderId = input.PlayerGuid,
                    NoticeType = NoticeType.Popout,
                    SystemInformationType = SystemInformationType.BirthDay,
                    BabyEventId = babyEvent.Id,
                    Remark = remark
                });
                await _informationRepository.InsertAsync(new Information()
                {
                    Content = $"时间好快啊，孩子{baby.AgeString}了",
                    FamilyId = input.FamilyId,
                    State = InformationState.Create,
                    Type = InformationType.Event,
                    NoticeType = NoticeType.Popout,
                    ReceiverId = input.TheOtherGuid,
                    SenderId = input.PlayerGuid,
                    SystemInformationType = SystemInformationType.BirthDay,
                    BabyEventId = babyEvent.Id,
                    Remark = remark
                });
            }
            #endregion
            return reward;
        }
        /// <summary>
        /// 发送事件消息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="msgContent"></param>
        private async Task AddEventMsg(HandleEventInput input, string msgContent, InformationType informationType)
        {
            await _informationRepository.InsertAsync(new Information()
            {
                //Title=title,
                Content = msgContent,
                FamilyId = input.FamilyId,
                State = InformationState.Create,
                Type = informationType,
                SenderId = input.PlayerGuid,
                ReceiverId = input.PlayerGuid,
                BabyEventId = input.EventId
            });
            await _informationRepository.InsertAsync(new Information()
            {
                //Title = title,
                Content = msgContent,
                FamilyId = input.FamilyId,
                State = InformationState.Create,
                Type = informationType,
                SenderId = input.PlayerGuid,
                ReceiverId = input.TheOtherGuid,// 注意
                BabyEventId = input.EventId
            });
        }

        internal override IQueryable<BabyEvent> GetQuery(GetBabyEventsInput model)
        {
            var result = _repository.GetAll();
            if (model.BabyId.HasValue)
            {
                var baby = _babyRepository.Get(model.BabyId.Value);
                if (baby != null && baby.GroupId.HasValue)
                {
                    var ids = _groupEventsRepository.GetAll().Where(g => g.GroupId == baby.GroupId.Value).Select(i => i.EventId);
                    if (ids.Any())
                    {
                        result = result.Where(r => ids.Contains(r.Id));
                    }
                }
            }

            return result.WhereIf(model.IncidentType.HasValue, e => e.Type == model.IncidentType.Value);
        }

        public async Task<BabyGrowUpRecordOutput> BabyGrowUpRecord(BabyGrowUpRecordInput input)
        {
            var response = new BabyGrowUpRecordOutput() { HasChange = false };
            //var  lastLogin= await _memoryCache.GetStringAsync("playerLastLoginTime"+input.PlayerGuid);
            var player = await _playerRepository.FirstOrDefaultAsync(s => s.Id == input.PlayerGuid);
            //var lastLoginDateTime = Convert.ToDateTime(lastLogin);
            var records = await _babyGrowUpRecordRepository.GetAllListAsync(r => r.CreationTime > player.LastLoginTime && r.BabyId == input.BabyId);
            //if (records.Count > 0)
            //{
            //    response.HasChange = true;
            //    response.BabyProperty = new BabyGrowUpRecordOutputBabyProperty()
            //    {
            //        Charm = records.Sum(s => s.Charm),
            //        EmotionQuotient = records.Sum(s => s.EmotionQuotient),
            //        Energy = records.Sum(s => s.Energy),
            //        Healthy = records.Sum(s => s.Healthy),
            //        Imagine = records.Sum(s => s.Imagine),
            //        Intelligence = records.Sum(s => s.Intelligence),
            //        Physique = records.Sum(s => s.Physique),
            //        WillPower = records.Sum(s => s.WillPower)
            //    };
            //}
            response.HasChange = true;
            return response;
        }
        /// <summary>
        /// 继续成长
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BabyGoOnGrowUpOutput> BabyGoOnGrowUp(BabyGoOnGrowUpInput input)
        {
            var response = new BabyGoOnGrowUpOutput();
            try
            {
                //修改宝宝组
                var baby = await _babyRepository.FirstOrDefaultAsync(s => s.Id == input.BabyId);
                if (baby.State == BabyState.Adult)
                {
                    throw new AbpException("宝宝已经成人");
                }
                var group = await _eventGroupRepository.FirstOrDefaultAsync(s => s.PrevGroupId == baby.GroupId);
                //判断是否完成了当前组的成长、阻断事件

                var babyEventRecords = _babyEventRecordRepository.GetAllIncluding(s => s.Event).Where(s => s.BabyId == input.BabyId && s.GroupId == baby.GroupId && (s.Event.Type == IncidentType.Growup || s.Event.Type == IncidentType.Block)).OrderByDescending(s => s.EndTimeStamp).DistinctBy(s => s.EventId).ToList();
                var currentGroupEvent = babyEventRecords.Count(s => ((s.EndTimeStamp != null && s.EndTimeStamp.Value > new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()) || (s.EndTimeStamp == null && s.State != EventRecordState.Handled)));
                var currgroup = _groupEventsRepository.GetAll().Where(g => g.EventGroup.Id == baby.GroupId && g.BabyEvent.Type == IncidentType.Growup).Count();
                if (currentGroupEvent > 0 || babyEventRecords.Count < currgroup)
                {
                    throw new AbpException("当前组事件还没有处理完或者重复提交");
                }
                else if (babyEventRecords.Count == 0)
                {
                    throw new AbpException("没有记录的事件操作");
                }
                if (group != null)
                {
                    baby.GroupId = group.Id;
                    response.NextGroupId = group.Id;
                    await _babyRepository.UpdateAsync(baby);
                }
                else
                {
                    var theEnding = await GetEnding(baby);
                    if (theEnding != null)
                    {
                        var story = ObjectMapper.Map<BabyGoOnGrowUpBabyStory>(theEnding);
                        response.StroyEnding = story;
                    }
                    else
                    {
                        response.StroyEnding = new BabyGoOnGrowUpBabyStory()
                        {
                            Description = "",
                            Name = "暂未找到合适的职业",
                            Image = "0.jpg"
                        };
                    }
                    //修改宝宝状态
                    baby.State = BabyState.Adult;
                    baby.BabyEndingId = theEnding?.Id;
                    await _babyRepository.UpdateAsync(baby);
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            //新增加结局，性别判断
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
            var babyEnding = babyEndings.FirstOrDefault(s => s.StudyType == studyGroup?.StudyType && s.StudyMax >= studyGroup?.Count && s.StudyMin < studyGroup?.Count);
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
        public async Task<GetCoupeEventAndRechargeMessageOutput> GetCoupeEventAndRechargeMessage(GetCoupeEventAndRechargeMessageInput input)
        {
            var events = await _babyEventRecordRepository.GetAllIncluding(e => e.Event, e => e.Option)
                .Where(e => e.FamilyId == input.FamilyId && e.Event.OperationType == OperationType.Double && e.State == EventRecordState.WaitOther)
                .ToListAsync();

            var rechargeMessages = await _informationRepository.GetAll()
                .Where(n => n.FamilyId == input.FamilyId && n.ReceiverId == input.PlayerId
                && n.Content.Contains("花费了") && n.Type == InformationType.Event
                && n.State == InformationState.Create)
                .ToListAsync();

            return new GetCoupeEventAndRechargeMessageOutput()
            {
                BabyEventRecords = events,
                Informations = rechargeMessages
            };
        }

        public async Task<DeleteBabyEventRecordOutput> DeleteBabyEventRecord(DeleteBabyEventRecordInput input)
        {
            var response = new DeleteBabyEventRecordOutput();
            var baby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
            if (baby != null && (baby.Family.MotherId == input.PlayerGuid || baby.Family.FatherId == input.PlayerGuid))
            {
                await _babyEventRecordRepository.DeleteAsync(s => s.BabyId == input.BabyId && s.IsDeleted == false);

                baby.GroupId = _eventGroupRepository.FirstOrDefaultAsync(s => !s.PrevGroupId.HasValue)?.Id;
                baby.State = BabyState.UnderAge;
            }
            else
            {
                throw new AbpException("宝宝不存在或玩家编号错误");
            }
            return response;
        }
    }
}


