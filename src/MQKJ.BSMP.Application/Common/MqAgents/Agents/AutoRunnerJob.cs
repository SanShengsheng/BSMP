using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Logging;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Babies;
using MQKJ.BSMP.ChineseBabies.EnergyRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using MQKJ.BSMP.Common.MqAgents.Agents.Config;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner;
using Newtonsoft.Json;

namespace MQKJ.BSMP.Common.MqAgents.Agents
{
    [System.Runtime.InteropServices.Guid("A6B552BC-7AB9-4C6B-B62C-2FFDAB8FB2E7")]
    public class AutoRunnerJob : ApplicationService, IAutoRunnerJob
    {
        private readonly IRepository<Family> _familyRepository;
        private readonly IRepository<AutoRunnerRecord, Guid> _recordRepository;
        private readonly IRepository<AutoRunnerConfig> _configRepository;
        private readonly IRepository<BabyEventRecord, Guid> _babyEventRecordRepository;
        private readonly IRepository<BabyEvent> _eventRepository;
        private readonly IEventGroupAppService _eventGroupAppService;
        //private readonly IBackgroundJobClient _backgroundJob;
        private readonly IEnergyRechargeAppService _energyService;
        private readonly IBabyEventAppService _eventService;
        private readonly IRepository<Baby> _babyRepository;

        private const int DELAY_TIME = 5;
        private static Random _random = new Random();
        private const int RETRY_START_MINUTES = 10;

        public AutoRunnerJob(IRepository<Family> familyRepository,
            IRepository<AutoRunnerRecord, Guid> recordRepository,
            IRepository<BabyEventRecord, Guid> babyEventRecordRepository,
            IRepository<BabyEvent> eventRepository,
            IEventGroupAppService eventGroupAppService,
            IRepository<AutoRunnerConfig> configRepository,
            IEnergyRechargeAppService energyService,
            IBabyEventAppService eventService,
            IRepository<Baby> babyRepository)
        {
            _familyRepository = familyRepository;
            _recordRepository = recordRepository;
            _eventGroupAppService = eventGroupAppService;
            _eventRepository = eventRepository;
            _babyEventRecordRepository = babyEventRecordRepository;
            //_backgroundJob = backgroundJob;
            _configRepository = configRepository;
            _energyService = energyService;
            _eventService = eventService;
            _babyRepository = babyRepository;
        }

        private static Dictionary<string, double> s_levelMinDeposit = new Dictionary<string, double>()
        {

        };

        private static Dictionary<string, int> s_consumeLevelIndex = new Dictionary<string, int>();

        static AutoRunnerJob()
        {
            s_levelMinDeposit.Add(FamilyLevel.Poor.ToString(), 0);
            s_levelMinDeposit.Add(FamilyLevel.WellOff.ToString(), 300000);
            s_levelMinDeposit.Add(FamilyLevel.Rich.ToString(), 1500000);
            s_consumeLevelIndex.Add(ConsumeLevel.Hight.ToString(), 0);
            s_consumeLevelIndex.Add(ConsumeLevel.Random.ToString(), 0);
            s_consumeLevelIndex.Add(ConsumeLevel.Middle.ToString(), 1);
            s_consumeLevelIndex.Add(ConsumeLevel.Low.ToString(), 2);
        }

        public async Task ExecuteEvent(ExecuteEventRequest request)
        {
            
            try
            {
                var findAutoConfig = await GetAutoConfig(request);
                var family = await GetFamily(request.FamilyId);
                var baby = family.Baby;
                var babyEvent = GetBabyevent(request);

                CheckFamilyState(family);   //检查家庭状态
                CheckBabyEvent(babyEvent);  //检查事件
                CheckFamilyMoneyState(family, findAutoConfig, request);

                await HandlerEvent(family, babyEvent, findAutoConfig, request);
            }
            catch (Exception ex)
            {
                Logger.Warn($"执行事件错误,家庭:{request.FamilyId}, 事件:{request.EventId}", ex);
                await StopRunnerJob(new StopRunnerRequest()
                {
                    FamilyId = request.FamilyId,
                    PlayerId = request.PlayerId,
                    Reason = $"机器人已暂停,停止原因:{ex.Message}"
                });
            }
        }

        private Task CheckGrowupEventState(Family family, BabyEvent babyEvent, AutoRunnerConfig findAutoConfig, ExecuteEventRequest request)
        {
            return Task.CompletedTask;
        }

        private async Task HandlerEvent(Family family, BabyEvent babyEvent, AutoRunnerConfig findAutoConfig, ExecuteEventRequest request)
        {
            var baby = family.Baby;
            var handleEventRequest = new HandleEventInput
            {
                BabyId = family.Baby.Id,
                EventId = request.EventId,
                FamilyId = request.FamilyId,
                OptionId = GetStudyOptionId(babyEvent, findAutoConfig),
                PlayerGuid = request.PlayerId,
                TheOtherGuid = family.FatherId == request.PlayerId ? family.MotherId : family.FatherId
            };

            if (babyEvent.Type == IncidentType.Study)
            {
                await CheckBabyEnergyAndAutoBuy(baby, babyEvent, findAutoConfig, request, handleEventRequest.OptionId);
            }

            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var babyOrginalData = JsonConvert.SerializeObject(baby, jsonSetting);
            var handlerResult = await _eventService.HandleEvent(handleEventRequest);
            //todo 获取最新宝宝数据
            var babyNew = _babyRepository.Get(baby.Id);
            var babyNewData = JsonConvert.SerializeObject(babyNew, jsonSetting);
            await AddActionRecord(new AutoRunnerRecord
            {
                ActionType = babyEvent.Type == IncidentType.Growup ? ActionType.GrowUp : ActionType.Study,
                CreationTime = DateTime.UtcNow,
                Description = babyEvent.Type == IncidentType.Growup ?
                    String.Format(GROWUP_DESCRIPTION_FORMAT, babyEvent.Id, handleEventRequest.OptionId) :
                    String.Format(STUDY_DESCRIPTION_FORMAT, babyEvent.Id, handleEventRequest.OptionId),
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId,
                OriginalData = babyOrginalData,
                NewData = babyNewData,
                RelateionId = babyEvent.Id.ToString(),
                BabyId = baby.Id
            });

            BackgroundJob.Schedule<IAutoRunnerJob>(job => job.StartRunnerJob(new StartRunnerRequest
            {
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId
            }), TimeSpan.FromSeconds(babyEvent.CountDown + DELAY_TIME));
        }

        private const string STUDY_DESCRIPTION_FORMAT = "自动学习了事件,事件ID:{0}, 事件选项:{1}";
        private const string GROWUP_DESCRIPTION_FORMAT = "自动执行了成长事件，事件ID：{0}, 事件选项{1}";

        private int GetStudyOptionId(BabyEvent babyEvent, AutoRunnerConfig findAutoConfig)
        {
            var index = 0;
            if (babyEvent.Type == IncidentType.Study)
            {
                if (findAutoConfig.StudyLevel != ConsumeLevel.Random)
                {
                    index = s_consumeLevelIndex[findAutoConfig.StudyLevel.ToString()];
                }
                else
                {
                    index = _random.Next(0, 3);
                }
            }
            else
            {
                if (findAutoConfig.ConsumeLevel != ConsumeLevel.Random)
                {
                    index = s_consumeLevelIndex[findAutoConfig.StudyLevel.ToString()];
                }
                else
                {
                    index = _random.Next(0, 3);
                }
            }


            return babyEvent.Options.ToArray()[index].Id;
        }

        private const int LIMIT_FAMILY_DEPOSITY = 10000;

        private void CheckFamilyMoneyState(Family family, AutoRunnerConfig findAutoConfig, ExecuteEventRequest request)
        {
            //var index = s_consumeLevelIndex[findAutoConfig.StudyLevel.ToString()];
            //var option = babyEvent.Options.ToArray()[index];
            if (family.Deposit < LIMIT_FAMILY_DEPOSITY)
            {
                throw new AbpException($"金额不足{LIMIT_FAMILY_DEPOSITY},家庭ID:{family.Id},家庭有:{family.Deposit}");
            }

            if (family.Level != FamilyLevel.Poor && findAutoConfig.LevelAction == LevelAction.Keep)
            {
                var minMoney = s_levelMinDeposit[family.Level.ToString()];
                if ((family.Deposit - LIMIT_FAMILY_DEPOSITY) < minMoney)
                {
                    throw new AbpException($"机器人设置为保持档位,金额不足,家庭ID:{family.Id},家庭有:{family.Deposit}");
                }
            }
        }

        private async Task CheckBabyEnergyAndAutoBuy(Baby baby, BabyEvent babyEvent, AutoRunnerConfig findAutoConfig, ExecuteEventRequest request, int optionId)
        {
            var option = babyEvent.Options.FirstOrDefault(o => o.Id == optionId);

            if(option == null)
            {
                throw new AbpException($"选项为空,选项ID:{optionId}");
            }

            if (option != null && baby.Energy < (-option.Consume?.Energy))
            {
                await AutoBuyEnergy(baby, request);
                await CheckBabyEnergyAndAutoBuy(baby, babyEvent, findAutoConfig, request, optionId);  //充值后再次验证
            }
        }

        /// <summary>
        /// 检查事件
        /// </summary>
        /// <param name="babyEvent"></param>
        /// <param name="family"></param>
        /// <param name="baby"></param>
        private void CheckBabyEvent(BabyEvent babyEvent)
        {
            //双人事件，停止外挂
            if (babyEvent.OperationType == OperationType.Double)
            {
                throw new AbpException($"遇到双人事件，机器人自动停止");
            }

        }

        /// <summary>
        /// 自动购买精力
        /// </summary>
        /// <param name="family"></param>
        /// <param name="baby"></param>
        private async Task AutoBuyEnergy(Baby baby, ExecuteEventRequest request)
        {
            var buyResponse = await _energyService.AutoBuyEnergy(new BuyEnergyInput
            {
                BabyId = baby.Id,
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId
            });

            if (buyResponse.IsSuccess)
            {
                await AddActionRecord(new AutoRunnerRecord
                {
                    ActionType = ActionType.BuyEnergy,
                    CreationTime = DateTime.UtcNow,
                    Description = $"机器人自动购买精力",
                    FamilyId = request.FamilyId,
                    PlayerId = request.PlayerId,
                    BabyId = baby.Id
                });
            }
            else
            {
                throw new AbpException($"购买精力出错,机器人自动停止");
            }
        }

        private void CheckFamilyState(Family family)
        {
            if (family.AddOnStatus != AddOnStatus.Running)
            {
                throw new AbpException($"机器人状态已被更改,目前是:{family.AddOnStatus}");
            }

            if (family.Deposit <= 0)
            {
                throw new AbpException($"家庭里没有存款了,家庭ID:{family.Id}");
            }
        }

        private BabyEvent GetBabyevent(ExecuteEventRequest request)
        {
            return _eventRepository
                .GetAll()
                .Include(f => f.Options)
                    .ThenInclude(o => o.Consume)
                .Include(f =>f.Consume)
                .FirstOrDefault(f => f.Id == request.EventId);
        }

        private Task<AutoRunnerConfig> GetAutoConfig(ExecuteEventRequest request)
        {
            var result = _configRepository.GetAll()
                .Where(f => f.FamilyId == request.FamilyId)
                .FirstOrDefault();

            if (result == null)
            {
                throw new AbpException($"未进行机器人设置，机器人停止!家庭ID:{request.FamilyId}");
            }

            return Task.FromResult(result);
        }

        public async Task StartRunnerJob(StartRunnerRequest request)
        {
            try
            {
                var baby = await GetBaby(request.FamilyId);
                if (baby != null)
                {
                    var babyEvent = await GetCurrentEvent(baby, request);
                    if (babyEvent == null)
                    {
                        
                        throw new AbpException($"没有可执行的事件，家庭ID:{request.FamilyId}");
                    }

                    var notFinishedEvent = await _babyEventRecordRepository
                        .FirstOrDefaultAsync(e => e.BabyId == baby.Id && e.EndTime.HasValue && e.EndTime >= DateTime.Now);

                    var eventRequest = new ExecuteEventRequest
                    {
                        EventId = babyEvent.Id,
                        FamilyId = request.FamilyId,
                        PlayerId = request.PlayerId,
                        EndTime = notFinishedEvent == null ? DateTime.Now : notFinishedEvent.EndTime.Value
                    };

                    BackgroundJob.Schedule<IAutoRunnerJob>(
                            job => job.ExecuteEvent(eventRequest),
                            new DateTimeOffset(eventRequest.EndTime));
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("开启机器人出错", ex);
                await StopRunnerJob(new StopRunnerRequest()
                {
                    FamilyId = request.FamilyId,
                    PlayerId = request.PlayerId,
                    Reason = ex.Message,
                    Retry = false,
                });
            }

        }

        /// <summary>
        /// 获取当前要执行的EventId
        /// </summary>
        /// <param name="baby"></param>
        /// <returns></returns>
        private async Task<BabyEvent> GetCurrentEvent(Baby baby, StartRunnerRequest request)
        {
            var node = baby.GroupId.HasValue ?
                _eventGroupAppService.GetCurrentNode(baby.GroupId.Value) :
                _eventGroupAppService.GetInitGroup();

            var group = node.Value;
            

            //todo
            return await GetCurrentEventFromGroup(baby, request, group);
        }

        private async Task<BabyEvent> GetCurrentEventFromGroup(Baby baby, StartRunnerRequest request, EventGroup group)
        {
            var babyEventRecords = _babyEventRecordRepository.GetAll()
                    .Where(b => b.BabyId == baby.Id && b.EndTime.HasValue);

            if (group.Events.Any())
            {
                var lastRecord = babyEventRecords.OrderByDescending(b => b.EndTime);
                //查找这个家庭有没有记录,没有的话,找到初始组的第一个学习任务
                if (lastRecord == null)
                {
                    return group.Events.FirstOrDefault(b => b.Type == IncidentType.Study);
                }

                var studyEvents = group.Events.Where(e => e.Type == IncidentType.Study);
                if (studyEvents.Any())
                {
                    var groupStudyEvents = from study in studyEvents
                                           join record in babyEventRecords on study.Id equals record.EventId into tempRecord
                                           from record in tempRecord.DefaultIfEmpty()
                                           group study by new
                                           {
                                               Id = study.Id,
                                               Max = study.StudyAllowMaxTime
                                           } into g
                                           select new
                                           {
                                               eventId = g.Key.Id,
                                               count = g.Count(),
                                               maxCount = g.Key.Max
                                           };

                    var find = groupStudyEvents.Where(g => g.count < g.maxCount);
                    if (find.Any())
                    {
                        var first = find.FirstOrDefault();
                        return studyEvents.FirstOrDefault(e => e.Id == first.eventId);
                    }
                }

                //成长事件
                var growUpEvents = group.Events.Where(e => e.Type == IncidentType.Growup);
                if (growUpEvents.Any())
                {
                    var eventIds = growUpEvents.Select(e => e.Id);
                    var lastGrowUp = babyEventRecords.Where(l => eventIds.Contains(l.EventId))
                        .Where(l => l.BabyId == baby.Id)
                        .OrderByDescending(e => e.EndTime)
                        .FirstOrDefault();

                    //查找有没有执行过成长事件
                    if (lastGrowUp == null)
                    {
                        var first = growUpEvents
                            .OrderBy(e => e.Code)
                            .FirstOrDefault();

                        if (first != null)
                            return first;
                    }

                    //如果有记录的话,先判断endtime时间
                    if (lastGrowUp.EndTime.HasValue)
                    {
                        //如果小于现在事件,说明事件已经完成,找到下面一个
                        if (lastGrowUp.EndTime <= DateTime.Now)
                        {
                            var lastEvent = growUpEvents.FirstOrDefault(e => e.Id == lastGrowUp.EventId);
                            var findNextEvent = growUpEvents
                                .Where(e => e.Code > lastEvent.Code)
                                .OrderBy(e => e.Code)
                                .FirstOrDefault();

                            if (findNextEvent != null)
                            {
                                return findNextEvent;
                            }
                        }
                    }
                }

                //全部走完了,然后升级组
                var growResult = await _eventService.BabyGoOnGrowUp(new BabyGoOnGrowUpInput
                {
                    BabyId = baby.Id,
                    PlayerGuid = request.PlayerId
                });

                if (growResult.NextGroupId.HasValue)
                {
                    var nextNode = _eventGroupAppService.GetCurrentNode(growResult.NextGroupId.Value);
                    group = nextNode.Value;
                    await AddActionRecord(new AutoRunnerRecord
                    {
                        ActionType = ActionType.GrowupToNextGroup,
                        Description = $"宝宝进入到下一个阶段",
                        CreationTime = DateTime.UtcNow,
                        FamilyId = request.FamilyId,
                        PlayerId = request.PlayerId,
                        BabyId = baby.Id
                    });
                    return await GetCurrentEventFromGroup(baby, request, group);
                }
                else if (growResult.StroyEnding != null)
                {
                    await AddActionRecord(new AutoRunnerRecord
                    {
                        ActionType = ActionType.Ending,
                        Description = $"宝宝已成人,职业:{growResult.StroyEnding?.Name}, {growResult.StroyEnding?.Description}",
                        CreationTime = DateTime.UtcNow,
                        FamilyId = request.FamilyId,
                        PlayerId = request.PlayerId,
                        BabyId = baby.Id
                    });
                }
            }

            return null;
        }

        public async Task StopRunnerJob(StopRunnerRequest request)
        {
            var family = await GetFamily(request.FamilyId);

            if (!family.Father.IsAgenter && !family.Mother.IsAgenter)
            {
                return;
            }
            if (family.AddOnStatus == AddOnStatus.NotRunning) return;
            if (family.AddOnStatus != AddOnStatus.Hide)
            {
                family.AddOnStatus = AddOnStatus.NotRunning;
            }

            await _familyRepository.UpdateAsync(family);

            var record = new AutoRunnerRecord
            {
                ActionType = ActionType.StopAuto,
                CreationTime = DateTime.UtcNow,
                Description = request.Reason,
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId,
                BabyId = family.Baby == null ? 0 : family.Baby.Id
            };

            await AddActionRecord(record);

            //设定时间自动开始
            //if (request.Retry)
            //{
            //    BackgroundJob.Schedule<IAutoRunnerJob>(job => job.AutoStartRunnerJob(new StartAutoRunRequest
            //    {
            //        FamilyId = request.FamilyId,
            //        PlayerId = request.PlayerId
            //    }), TimeSpan.FromMinutes(RETRY_START_MINUTES));
            //}
        }

        private Task AddActionRecord(AutoRunnerRecord record)
        {
            return _recordRepository.InsertAsync(record);
        }

        private async Task<Baby> GetBaby(int familyId)
        {
            var family = await GetFamily(familyId);

            if (family.Baby == null)
            {
                LogHelper.Logger.Warn($"[机器人]：{familyId}家庭没有未成年宝宝，自动暂停");
                throw new AbpException($"{familyId}家庭没有未成年宝宝，自动暂停");
            }

            return family.Baby;
        }

        private Task<Family> GetFamily(int familyId)
        {
            var result = _familyRepository
                .GetAll()
                .Include(f => f.Father)
                .Include(f => f.Mother)
                .Include(f => f.Babies)
                //.GetAllIncluding(f => f.Father, f => f.Mother, f => f.Babies)
                .Where(f => f.Id == familyId)
                .FirstOrDefault();

            if (result == null)
            {
                LogHelper.Logger.Error($"[机器人]运行没有找到家庭,家庭ID：{familyId}");
                throw new AbpException($"没有找到家庭");
            }

            return Task.FromResult(result);
        }

        public async Task CheckCanAutoRunner(CheckCanAutoRunnerRequest request)
        {
            var family = await GetFamily(request.FamilyId);
            if (family.Baby == null)
            {
                //LogHelper.Logger.Warn($"[机器人]：{request.FamilyId}家庭没有未成年宝宝，自动暂停");
                throw new AbpException($"{request.FamilyId}家庭没有未成年宝宝，自动暂停");
            }
            var findAutoConfig = await GetAutoConfig(new ExecuteEventRequest
            {
                FamilyId = request.FamilyId
            });

            CheckFamilyMoneyState(family, findAutoConfig, new ExecuteEventRequest
            {
                
            });
        }

        public async Task AutoStartRunnerJob(StartAutoRunRequest request)
        {
            var family = await GetFamily(request.FamilyId);
            family.AddOnStatus = AddOnStatus.Running;
            await _familyRepository.UpdateAsync(family);
            await _recordRepository.InsertAsync(new AutoRunnerRecord
            {
                ActionType = ActionType.StartAuto,
                CreationTime = DateTime.UtcNow,
                Description = $"机器人在{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}自动启动了",
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId,
                BabyId = family.Baby?.Id
            });
            BackgroundJob.Schedule<IAutoRunnerJob>(job => job.StartRunnerJob(new StartRunnerRequest
            {
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId
            }), TimeSpan.FromSeconds(5));
        }
        /// <inheritdoc/>
        [UnitOfWork(IsDisabled =true)]
        public  string AutoStartWorshipJob() {
           return AutoStartWorshipJobConfig
                .Instance
                .UseDefault()
                .BuildHangFireRunners((identity)=> () => new JobCls().DoWork(identity));
        }
        public int ClearAllWorshipJobs(ClearType type) {
            return AutoStartWorshipJobConfig
                .Instance
                .ClearAllHangfireRunners(type);
        }
        /// <inheritdoc/>
        public int ClearWorshipJob(string job) {
           return AutoStartWorshipJobConfig
                .Instance
                .ClearHangFireRunners(job);
        }
        /// <inheritdoc/>
        public string ApplyWorshipJobConfig(WorshipJobConfigInput input) {
            return AutoStartWorshipJobConfig
                .Instance
                .Apply(input.Sheduletables, input.ExecTime, null, null,input.ClearType)
                .BuildHangFireRunners((identity) => () => new JobCls().DoWork(identity));
        }
        /// <inheritdoc/>
        public string ApplyWorshipJobTestConfig() {
          return  AutoStartWorshipJobConfig
                .Instance
                .UseTest()
                .BuildHangFireRunners((identity) => () => new JobCls().DoWork(identity));
        }
        /// <inheritdoc/>
        public IList<RecurringJobDtoOutput> GetAllWorshipJobs()
        {
            return AutoStartWorshipJobConfig
                .Instance
                .GetAllRecurringsJobs();
        }
        /// <inheritdoc/>
        public string ModifyWorshipSystemConfig(string worshipedTimesMax,string toWorshipTimesMax) {
            var _systemConfigRepo = IocManager.Instance.Resolve<IRepository<SystemSetting>>();
            if (!string.IsNullOrWhiteSpace(worshipedTimesMax) && int.TryParse(worshipedTimesMax,out int _worshipedTimesMax))
                if (_worshipedTimesMax <= 0) throw new Abp.UI.UserFriendlyException("_worshipedTimesMax 格式错误");
                else _systemConfigRepo.GetAll().First(s => s.Name.Equals("WorshipedTimesMax")).Value = worshipedTimesMax;
            if (string.IsNullOrWhiteSpace(toWorshipTimesMax) && int.TryParse(toWorshipTimesMax, out int _toWorshipTimesMax))
                if (_toWorshipTimesMax <= 0) throw new Abp.UI.UserFriendlyException("toWorshipTimesMax 格式错误");
                else _systemConfigRepo.GetAll().First(s => s.Name.Equals("ToWorshipTimesMax")).Value = toWorshipTimesMax;
            return "modify";
        }
    }

   
}
