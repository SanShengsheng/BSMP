
using Moq;
using Xunit;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using TestStack.BDDfy;
using MQKJ.BSMP.Common.MqAgents.Agents;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner;
using Abp;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Hangfire;
using System.Linq.Expressions;
using Hangfire.Common;
using Hangfire.States;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Abp.Extensions;

namespace MQKJ.BSMP.Tests.Agents
{
    public class AutoRunnerJobTests
    {
        private readonly Mock<IRepository<ChineseBabies.Family>> _mockFamilyRepository;
        private readonly Mock<IRepository<AutoRunnerRecord, Guid>> _mockAutoRunnerRecordRepository;
        private readonly IList<AutoRunnerRecord> _records;
        private readonly Mock<IRepository<BabyEvent>> _mockEventRepository;
        private readonly Mock<IRepository<BabyEventRecord, Guid>> _mockBabyEventRecordRepository;
        private readonly Mock<IEventGroupAppService> _mockEventGroupAppService;
        private readonly Mock<IBackgroundJobClient> _mockBackgroundJobClient;
        private readonly Mock<IRepository<AutoRunnerConfig>> _mockConfigRepository;
        private readonly Mock<IEnergyRechargeAppService> _mockEnergyService;
        private readonly Mock<IBabyEventAppService> _mockEventService;
        private readonly Mock<IRepository<ChineseBabies.Baby>> _mockBabyRepository;
        //IRepository<BabyEventRecord, Guid> babyEventRecordRepository,
        //   IRepository<BabyEvent> eventRepository,
        //    IEventGroupAppService eventGroupAppService

        private IAutoRunnerJob _job;
        private int executeEventId = 0;
        private string executeName = "";

        private const string ExecuteStudyEventMethodName = "ExecuteEvent";
        private const string ExecuteGrowupEventMethodName = "ExecuteEvent";
        public AutoRunnerJobTests()
        {
            _mockFamilyRepository = new Mock<IRepository<ChineseBabies.Family>>();
            _mockAutoRunnerRecordRepository = new Mock<IRepository<AutoRunnerRecord, Guid>>();
            _mockEventRepository = new Mock<IRepository<BabyEvent>>();
            _mockEventGroupAppService = new Mock<IEventGroupAppService>();
            _mockBabyEventRecordRepository = new Mock<IRepository<BabyEventRecord, Guid>>();
            _mockBackgroundJobClient = new Mock<IBackgroundJobClient>();
            _mockEnergyService = new Mock<IEnergyRechargeAppService>();
            _mockConfigRepository = new Mock<IRepository<AutoRunnerConfig>>();
            _mockEventService = new Mock<IBabyEventAppService>();
            _mockBabyRepository = new Mock<IRepository<ChineseBabies.Baby>>();

            _records = new List<AutoRunnerRecord>();
            _mockAutoRunnerRecordRepository.Setup(a => a.InsertAsync(It.IsAny<AutoRunnerRecord>()))
               .Returns(Task.FromResult(_records.FirstOrDefault()))
               .Callback((AutoRunnerRecord r) => _records.Add(r));
        }

        [Fact]
        public void StartAutoRunnerNoFamilyTests()
        {
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenNoFamily())
                .Then(x => x.ThenNoFamilyThrow())
                .BDDfy();
        }

        [Fact]
        public void StartAutoRunnerNoBabyTests()
        {
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenNoBaby())
                .Then(x => x.ThenNoBabyStopRunner())
                .BDDfy();
        }

        [Fact]
        public void StartAutoRunnerInitGroupNoEventTests()
        {
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenBabyInitGroup())
                .And(x => x.AndNoEventRecords())
                .Then(x => x.ThenNoBabyStopRunner())
                .BDDfy();
        }

        /// <summary>
        /// 当没有记录的时候,第一次执行学习事件
        /// </summary>
        [Fact]
        public void StudyEventNoRecordsTests()
        {
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenBabyInitGroupStudyEvent())
                .When(x => x.WhenCallStudyBackgroupJob())
                .And(x => x.AndNoEventRecords())
                .And(x => x.AndNotFinishedEventRecords())
                .Then(x => x.ThenNoRecordRunStudyEvent())
                .BDDfy();
        }

        [Fact]
        public void SecondStudyEventTests()
        {
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenBabyInitGroupStudyEvent())
                .When(x => x.WhenCallStudyBackgroupJob())
                .And(x => x.AndNotFinishedEventRecords())
                .And(x => x.AndStudyTwoEventRecords())
                .Then(x => x.ThenRunJob())
                .Then(x => x.ThenShouldEventBe(ExecuteStudyEventMethodName, 2))
                .BDDfy();
        }

        /// <summary>
        /// 执行学习事件 机器人状态已停止
        /// </summary>
        [Fact]
        public void ExecuteStudyEventWhenFamilyStop()
        {
            var request = new ExecuteEventRequest
            {
                FamilyId = 1,
                EventId = 1,
                PlayerId = Guid.NewGuid()
            };

            var action = new Action<Family>(f =>
            {
                f.AddOnStatus = AddOnStatus.NotRunning;
            });
            var configs = new List<AutoRunnerConfig>()
            {
                new AutoRunnerConfig
                {
                    FamilyId = 1
                }
            };
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenFamilyIs(action))
                .When(x => x.WhenInitBabyEvents(_babyEvents.AsQueryable()))
                .When(x => x.WhenInitAutoConfig(configs))
                .Then(x => x.ThenRunExecuteStudyEvent(request))
                .Then(x => x.ThenStopRunnerStatusIs(AddOnStatus.NotRunning, 1, "机器人状态已被更改"))
                .BDDfy();
        }

        /// <summary>
        /// 执行学习事件 没有存款
        /// </summary>
        [Fact]
        public void ExecuteStudyEventNoDeposit()
        {
            var request = new ExecuteEventRequest
            {
                FamilyId = 1,
                EventId = 1,
                PlayerId = Guid.NewGuid()
            };

            var action = new Action<Family>(f =>
            {
                f.Deposit = 0;
            });

            var configs = new List<AutoRunnerConfig>()
            {
                new AutoRunnerConfig
                {
                    FamilyId = 1
                }
            };
            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenFamilyIs(action))
                .When(x => x.WhenInitBabyEvents(_babyEvents.AsQueryable()))
                .When(x => x.WhenInitAutoConfig(configs))
                .Then(x => x.ThenRunExecuteStudyEvent(request))
                .Then(x => x.ThenStopRunnerStatusIs(AddOnStatus.NotRunning, 1, "家庭里没有存款了"))
                .BDDfy();
        }


        [Fact]
        public void ExecuteStudyEventWhenDoubleEvent()
        {
            var request = new ExecuteEventRequest
            {
                FamilyId = 1,
                EventId = 1,
                PlayerId = Guid.NewGuid()
            };

            var action = new Action<Family>(f =>
            {
                f.AddOnStatus = AddOnStatus.Running;
                f.Deposit = 300000;
            });

            var babyEvents = new List<BabyEvent>
            {
                new BabyEvent
                {
                    Id = 1,
                    OperationType = OperationType.Double
                }
            };

            var configs = new List<AutoRunnerConfig>()
            {
                new AutoRunnerConfig
                {
                    FamilyId = 1
                }
            };

            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenFamilyIs(action))
                .When(x => x.WhenInitAutoConfig(configs))
                .When(x => x.WhenInitBabyEvents(babyEvents.AsQueryable()))
                .Then(x => x.ThenRunExecuteStudyEvent(request))
                .Then(x => x.ThenStopRunnerStatusIs(AddOnStatus.NotRunning, 1, "遇到双人事件，机器人自动停止"))
                .BDDfy();
        }

        [Fact]
        public void ExecuteStudyEventWhenNoConfig()
        {
            var request = new ExecuteEventRequest
            {
                FamilyId = 1,
                EventId = 1,
                PlayerId = Guid.NewGuid()
            };

            var action = new Action<Family>(f =>
            {
                f.AddOnStatus = AddOnStatus.Running;
                f.Deposit = 300000;
            });

            var babyAction = new Action<ChineseBabies.Baby>(b =>
            {
                b.Energy = 9;
            });

            var configs = new List<AutoRunnerConfig>();
            var babyEvents = new List<BabyEvent>()
            {
                new BabyEvent
                {
                    Id = 1,
                    Consume = new Reward
                    {
                        Energy = 10
                    },
                    Type = IncidentType.Study
                }
            };

            this.Given(x => x.GivenInitJob())
                .When(x => x.WhenInitBaby())
                .When(x => x.WhenFamilyIs(action))
                .When(x => x.WhenBabyIs(babyAction))
                .When(x => x.WhenInitBabyEvents(babyEvents.AsQueryable()))
                .When(x => x.WhenInitAutoConfig(configs))
                .Then(x => x.ThenRunExecuteStudyEvent(request))
                .Then(x => x.ThenStopRunnerStatusIs(AddOnStatus.NotRunning, 1, "未进行机器人设置，机器人停止!家庭ID"))
                .BDDfy();
        }

        private void WhenFamilyIs(Action<Family> action)
        {
            action(_baseFamily);
        }

        private void WhenBabyIs(Action<ChineseBabies.Baby> action)
        {
            action(_baseBaby);
        }


        private void GivenInitJob()
        {
            _job = new AutoRunnerJob(_mockFamilyRepository.Object,
                _mockAutoRunnerRecordRepository.Object,
                _mockBabyEventRecordRepository.Object,
                _mockEventRepository.Object,
                _mockEventGroupAppService.Object,
                _mockConfigRepository.Object,
                //_mockBackgroundJobClient.Object,
                _mockEnergyService.Object,
                _mockEventService.Object,
                _mockBabyRepository.Object);
        }

        private void WhenNoFamily()
        {
            _mockFamilyRepository.Setup(m => m.GetAll())
                .Returns(new List<ChineseBabies.Family>().AsQueryable());
        }

        private void WhenNoBaby()
        {
            _mockFamilyRepository.Setup(m => m.GetAll())
                .Returns(new List<Family>
                {
                    _baseFamily
                }.AsQueryable());
        }

        private void WhenInitBaby()
        {
            _baseFamily.Babies = new List<ChineseBabies.Baby>
            {
                _baseBaby
            };
            _mockFamilyRepository.Setup(m => m.GetAll())
               .Returns(new List<Family>
               {
                    _baseFamily
               }.AsQueryable());
        }

        private void WhenBabyInitGroup()
        {
            _mockEventGroupAppService.Setup(g => g.GetInitGroup())
                .Returns(_baseNode);
        }

        private void WhenInitBabyEvents(IQueryable<BabyEvent> events)
        {
            _mockEventRepository.Setup(g => g.GetAll())
                .Returns(events);
        }

        private void WhenBabyInitGroupStudyEvent()
        {
            _baseNode.Value.EventGroupBabyEvent = new List<EventGroupBabyEvent>
            {
                new EventGroupBabyEvent
                {
                    BabyEvent = new BabyEvent
                    {
                        Type = IncidentType.Study,
                        Id = 1,
                    },
                },
                new EventGroupBabyEvent
                {
                    BabyEvent = new BabyEvent
                    {
                        Type = IncidentType.Study,
                        Id = 2,
                    },
                },
            };

            _mockEventGroupAppService.Setup(g => g.GetInitGroup())
                .Returns(_baseNode);
        }

        private void WhenInitAutoConfig(IEnumerable<AutoRunnerConfig> configs)
        {
            _mockConfigRepository.Setup(r => r.GetAll())
                .Returns(configs.AsQueryable());
        }

        private void AndNoEventRecords()
        {
            _mockBabyEventRecordRepository.Setup(r => r.GetAll())
                .Returns(new List<BabyEventRecord>().AsQueryable());
        }

        private void AndStudyTwoEventRecords()
        {
            _mockBabyEventRecordRepository.Setup(r => r.GetAll())
                .Returns(new List<BabyEventRecord>()
                {
                    new BabyEventRecord
                    {
                        BabyId = 1,
                        EndTime = DateTime.Now,
                        EventId = 1
                    },
                    new BabyEventRecord
                    {
                        BabyId = 1,
                        EndTime = DateTime.Now,
                        EventId = 1
                    },
                    new BabyEventRecord
                    {
                        BabyId = 1,
                        EndTime = DateTime.Now,
                        EventId = 1
                    },
                    new BabyEventRecord
                    {
                        BabyId = 1,
                        EndTime = DateTime.Now,
                        EventId = 1
                    },
                    new BabyEventRecord
                    {
                        BabyId = 1,
                        EndTime = DateTime.Now,
                        EventId = 1
                    }
                }.AsQueryable());
        }

        private void AndNotFinishedEventRecords()
        {
            BabyEventRecord nullRecord = null;
            _mockBabyEventRecordRepository.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<BabyEventRecord, bool>>>()))
                .ReturnsAsync(nullRecord);
        }

        private void ThenNoBabyStopRunner()
        {
            var task = _job.StartRunnerJob(new StartRunnerRequest
            {
                FamilyId = 1
            });
            task.Wait();

            _baseFamily.AddOnStatus.ShouldBe(AddOnStatus.NotRunning);
            _records.Count.ShouldBe(1);
        }

        private void ThenStopRunnerStatusIs(AddOnStatus status, int recordCount, string errmessage)
        {
            _baseFamily.AddOnStatus.ShouldBe(status);
            _records.Count.ShouldBe(recordCount);
            if (!String.IsNullOrEmpty(errmessage))
            {
                _records[0].Description.ShouldContainWithoutWhitespace(errmessage);
            }
        }

        private void WhenCallStudyBackgroupJob()
        {
            _mockBackgroundJobClient.Setup(x => x.Create(It.IsAny<Job>(), It.IsAny<IState>()))
               .Callback((Job job, IState state) =>
               {
                   executeName = job.Method.Name;
                   executeEventId = ((ExecuteEventRequest)job.Args[0]).EventId;
               });
        }

        private void ThenNoRecordRunStudyEvent()
        {

            var task = _job.StartRunnerJob(new StartRunnerRequest
            {
                FamilyId = 1
            });
            task.Wait();

            executeEventId.ShouldBe(1);
            executeName.ShouldBe(ExecuteStudyEventMethodName);
        }

        private void ThenRunExecuteStudyEvent(ExecuteEventRequest request)
        {
            _job.ExecuteEvent(request).Wait();
        }

        private void ThenRunJob()
        {
            var task = _job.StartRunnerJob(new StartRunnerRequest
            {
                FamilyId = 1
            });
            task.Wait();
        }

        private void ThenShouldEventBe(string name, int eventId)
        {
            executeEventId.ShouldBe(eventId);
            executeName.ShouldBe(name);
        }

        private void ThenNoFamilyThrow()
        {
            var task = _job.StartRunnerJob(new StartRunnerRequest
            {
                FamilyId = 1
            }).ShouldThrowAsync(typeof(AbpException));

            task.Wait();
        }

        private Family _baseFamily = new Family
        {
            Id = 1,
            Babies = new List<ChineseBabies.Baby>(),
            AddOnStatus = AddOnStatus.Running
        };

        private ChineseBabies.Baby _baseBaby = new ChineseBabies.Baby
        {
            FamilyId = 1,
            Age = 0,
            AgeString = "1月",
            Name = "孩之宝",
            Id = 1,
            State = BabyState.UnderAge
        };

        private LinkedListNode<EventGroup> _baseNode = new LinkedListNode<EventGroup>(new EventGroup
        {
            Id = 1,
            EventGroupBabyEvent = new List<EventGroupBabyEvent>
            {

            }
        });

        private List<BabyEvent> _babyEvents = new List<BabyEvent>
        {
            new BabyEvent
            {
                Id = 1,
                Consume = new Reward
                {
                    Energy = 10
                }
            }
        };
    }
}