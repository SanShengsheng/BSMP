using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Threading.Extensions;
using Abp.Extensions;
using Hangfire;
using System.Linq.Expressions;
using Hangfire.Storage;
using Abp.Domain.Uow;
using MQKJ.BSMP.ChineseBabies.Babies;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Config
{
    public class AutoStartWorshipJobConfig
    {
        private const string taskPrefix = "task-prefix";
        private static AutoStartWorshipJobConfig _instance = new AutoStartWorshipJobConfig();
        private static string _execTimesSeparatedByCommas = "09:00,13:00,17:00,21:00";
        private static int _robotFamilyIdentity = -1, _prestigesAddedEachTime=1;     
        private static List<WorkScheduleItem> _sheduletables = new List<WorkScheduleItem>();
        private ClearType _clearType = ClearType.FromMemory;
        public int FromFamilyIdentity
        {
            get { return _robotFamilyIdentity; }
        }
        private AutoStartWorshipJobConfig()
        {
        }
        public static AutoStartWorshipJobConfig Instance
        {
            get => _instance;
        }
        private IDictionary<string, Time> GetExecTimes()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(_execTimesSeparatedByCommas, "^[0-9,:]+$"))
                throw new Abp.UI.UserFriendlyException("_execTimesSeparatedByCommas格式错误");
            if (!_execTimesSeparatedByCommas.Contains(',')) _execTimesSeparatedByCommas = _execTimesSeparatedByCommas + ",";
            return _execTimesSeparatedByCommas
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .DistinctBy(s=>s)
                .Select(s =>
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(s, @"([01]\d|2[0-3]):([0-5]\d)"))
                        throw new Abp.UI.UserFriendlyException("_execTimesSeparatedByCommas格式错误");
                    var _time = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    return new { Hour = int.Parse(_time[0]), Minute = int.Parse(_time[1]) };
                })
                .OrderBy(s => s.Hour)
                .ThenBy(s => s.Minute)
                .Select(s => s).ToDictionary(s => $"{taskPrefix}-{s.Hour}-{s.Minute}", a => new Time { Hour = a.Hour, Minute = a.Minute });
        }
        public static int GetPrestigesByRank(int rank)
        {
            var _current = _sheduletables.Where(s => rank >= s.PrestigesLowerLimit && rank <= s.PrestigesUpperLimit).FirstOrDefault();
            if (_current == null) return -1;
            return new Random(Guid.NewGuid().GetHashCode()).Next(_current.TimesLowerLimit, _current.TimesUpperLimit);
        }
        private int ClearRecurringJobs() {
            var _result = 0;
            List<RecurringJobDtoOutput> recurrings =GetAllRecurringsJobs()?.ToList();
            recurrings?.ForEach(job => RecurringJob.RemoveIfExists(job.Id));
            if (recurrings != null) _result = recurrings.Count;
            return _result;
        }
        public List<RecurringJobDtoOutput> GetAllRecurringsJobs() {
            List<RecurringJobDtoOutput> recurrings=null;
            using (var connection = JobStorage.Current.GetConnection())
                if (connection != null)
                    recurrings = connection.GetRecurringJobs()
                        .Where(job => job.Id.Contains(taskPrefix))
                        .Select(r =>new RecurringJobDtoOutput {
                             CreatedAt = r.CreatedAt,
                             Cron = r.Cron,
                             LastExecution = r.LastExecution,
                             LastJobState = r.LastJobState,
                             NextExecution = r.NextExecution,
                             Queue = r.Queue,
                             Id =r.Id,
                             TimeZoneId = r.TimeZoneId
                        })
                        .ToList();
            return recurrings;
        }
        public string BuildHangFireRunners(Func<int, Expression<Action>> action)
        {
            foreach (var item in this.GetExecTimes())
                RecurringJob.AddOrUpdate(item.Key, action(_robotFamilyIdentity), $"{item.Value.Minute} {item.Value.Hour} * * *",  TimeZoneInfo.Local );
            return _execTimesSeparatedByCommas;
        }
        public struct Time
        {
            public int Hour { get; set; }
            public int Minute { get; set; }
        }
        public int ClearHangFireRunners(params string[] tasks)
        {
            int _result = 0;
            if (tasks == null || tasks.Length == 0) return 0;
            tasks.ToList().ForEach((item) => {
                if (item == null) return;
                RecurringJob.RemoveIfExists(item);
                _result++;
            });
           
            return _result;
        }
        public int ClearAllHangfireRunners(ClearType type= ClearType.None) {          
            int _result = 0;
            if (type != ClearType.None)_instance.Locking(()=> _clearType =type);
            if (_clearType == ClearType.FromMemory)
            {
                foreach (var job in this.GetExecTimes())
                {
                    RecurringJob.RemoveIfExists(job.Key);
                    _result++;
                }
            }
            else _result =ClearRecurringJobs();
            return _result;
        }
        public AutoStartWorshipJobConfig UseTest() {
            this.ClearAllHangfireRunners();
            _sheduletables.Locking((_source) =>
            {             
                _execTimesSeparatedByCommas = $"{DateTime.Now.AddMinutes(3).ToString("HH:mm")},{DateTime.Now.AddMinutes(4).ToString("HH:mm")}";
                _source.Clear();
                _source.AddRange(
                    new WorkScheduleItem[4] {
                        new WorkScheduleItem
                        {
                             PrestigesLowerLimit = 1,
                             PrestigesUpperLimit = 10,
                             TimesLowerLimit =11,
                             TimesUpperLimit = 20
                        },
                        new WorkScheduleItem
                        {
                            PrestigesLowerLimit = 11,
                            PrestigesUpperLimit = 30,
                            TimesLowerLimit = 5,
                            TimesUpperLimit = 15
                        },
                        new WorkScheduleItem
                        {
                            PrestigesLowerLimit = 31,
                            PrestigesUpperLimit = 60,
                            TimesLowerLimit = 0,
                            TimesUpperLimit = 10
                        },
                        new WorkScheduleItem
                        {
                            PrestigesLowerLimit = 61,
                            PrestigesUpperLimit = 100,
                            TimesLowerLimit = 0,
                            TimesUpperLimit = 5
                        }
                });
            });
            return _instance;
        }
        public AutoStartWorshipJobConfig UseDefault()
        {
            this.ClearAllHangfireRunners();
            _sheduletables.Locking((_source) =>
            {
                _source.Clear();
                _source.AddRange(
                    new WorkScheduleItem[4] {
                        new WorkScheduleItem
                        {
                             PrestigesLowerLimit = 1,
                             PrestigesUpperLimit = 10,
                             TimesLowerLimit =11,
                             TimesUpperLimit = 20
                        },
                        new WorkScheduleItem
                        {
                            PrestigesLowerLimit = 11,
                            PrestigesUpperLimit = 30,
                            TimesLowerLimit = 5,
                            TimesUpperLimit = 15
                        },
                        new WorkScheduleItem
                        {
                            PrestigesLowerLimit = 31,
                            PrestigesUpperLimit = 60,
                            TimesLowerLimit = 0,
                            TimesUpperLimit = 10
                        },
                        new WorkScheduleItem
                        {
                            PrestigesLowerLimit = 61,
                            PrestigesUpperLimit = 100,
                            TimesLowerLimit = 0,
                            TimesUpperLimit = 5
                        }
                });
            });
            return _instance;
        }
        public AutoStartWorshipJobConfig Apply(IList<WorkScheduleItem> sheduletables
            , string exectimes
            , int? addedeachtime
            , int? familyIdentity,
            ClearType _type = ClearType.None
            )
        {
            _instance.Locking(() =>
            {
                if (addedeachtime.HasValue)
                {
                    _prestigesAddedEachTime = addedeachtime.Value;
                }
                if (!exectimes.IsNullOrWhiteSpace())
                {
                    _execTimesSeparatedByCommas = exectimes;
                }
                if (sheduletables != null && sheduletables.Count > 0)
                {
                    foreach (var item in sheduletables)
                    {
                        _sheduletables.Add(item);
                    };
                }
                if (familyIdentity.HasValue)
                {
                    _robotFamilyIdentity = familyIdentity.Value;
                }
                if (_type != ClearType.None) {
                    _clearType = _type;
                }
            });
            return _instance;
        }
    }
    public class JobCls
    {
        public void DoWork(int identity)
        {
            var _uowManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
            var _babyPrestigeAppService = IocManager.Instance.Resolve<IBabyPrestigeAppService>();
            var _familyWorshRecordRepo = IocManager.Instance.Resolve<IRepository<FamilyWorshipRecord>>();
            var _familyRepository = IocManager.Instance.Resolve<IRepository<Family>>();
            var _rankResult = Abp.Threading.AsyncHelper.RunSync<RankPrestigesOutput>(() => _babyPrestigeAppService.RankPrestigesByBaby(null));
            try
            {
                _rankResult.PagedResultDto?.Items?.ToList().ForEach(rankitem =>
                {
                    if (rankitem.TimesToday == _rankResult.TimesLimit) return;
                    var getedval = AutoStartWorshipJobConfig.GetPrestigesByRank(rankitem.RankingNumber);
                    if (getedval == -1) return;
                    if (getedval + rankitem.TimesToday > _rankResult.TimesLimit) getedval = _rankResult.TimesLimit - rankitem.TimesToday;
                    using (var _uowHandler = _uowManager.Begin())
                    {
                        _familyRepository.Update(rankitem.FamilyId, f => f.Prestiges = f.Prestiges + getedval);
                        for (int i = 0; i < getedval; i++)
                        {
                            _familyWorshRecordRepo.Insert(new FamilyWorshipRecord
                            {
                                Coins = 0,
                                Prestiges = 1,
                                FromFamilyId = identity,
                                ToFamilyId = rankitem.FamilyId,
                                FromBabyId = null,
                                ToBabyId = rankitem.BabyId
                            });
                        }
                        _uowManager.Current.SaveChanges();
                        _uowHandler.Complete();
                    }
                });
            }
            catch (Exception e)
            {
                e.ReThrow();
            }
            finally
            {
                IocManager.Instance.Release(_familyWorshRecordRepo);
                IocManager.Instance.Release(_babyPrestigeAppService);
            }
        }
    }
    public class WorshipJobConfigInput {
        public IList<WorkScheduleItem> Sheduletables { get; set; }
        public string ExecTime { get; set; }
        public ClearType ClearType { get; set; }

    }
    public class WorkScheduleItem
    {
        public int PrestigesLowerLimit { get; set; }
        public int PrestigesUpperLimit { get; set; }
        public int TimesLowerLimit { get; set; }
        public int TimesUpperLimit { get; set; }
    }
    public class RecurringJobDtoOutput
    {
        public string Id { get; set; }
        public string Cron { get; set; }
        public string Queue { get; set; }
        public DateTime? NextExecution { get; set; }
        public string LastJobState { get; set; }
        public DateTime? LastExecution { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string TimeZoneId { get; set; }
    }
    public enum ClearType {
        None =0,
        FromStorage,
        FromMemory
    }
}
