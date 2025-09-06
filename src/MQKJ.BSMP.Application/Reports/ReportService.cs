using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Migrations;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Players.Dtos;
using MQKJ.BSMP.Reports.Dtos;
using MQKJ.BSMP.Utils.Extensions;
using NPOI.SS.Formula.Functions;
using Remotion.Linq.Clauses;

namespace MQKJ.BSMP.Reports
{
    public class ReportService : BSMPAppServiceBase, IReportService
    {
        private readonly IRepository<Player, Guid> _playerRepository;
        private readonly IRepository<Family> _familyRepository;
        private readonly IRepository<Baby> _babyRepository;
        private readonly IRepository<Order, Guid> _orderRepository;

        private const int REPORT_DAYS = 30;

        public ReportService(IRepository<Player, Guid> playerRepository,
             IRepository<Family> familyRepository,
             IRepository<Baby> babyRepository,
             IRepository<Order, Guid> orderRepository)
        {
            _playerRepository = playerRepository;
            _familyRepository = familyRepository;
            _babyRepository = babyRepository;
            _orderRepository = orderRepository;
        }

        public async Task<GetHomeReportResponse> GetHomeReport(GetHomeReportRequest request)
        {
            var end = DateTime.Today;
            var begin = end.AddDays(-REPORT_DAYS);
            var playerDict =  GetReportStateByEntity(await GetPlayers(request, begin, end));
            var orderDict = await GetOrders(request, begin, end);
            var familyDict = GetReportStateByEntity(await GetFamilies(request, begin, end));
            var babyDict = GetReportStateByEntity(await GetBabies(request, begin, end));
            var result = new GetHomeReportResponse();
            result.Histories = new List<HomeReportHistory>();
            result.Charts = new ChartLine
            {
                Labels = new List<string>(),
                Datasets = new List<ChartDataset>()
            };

            for (int i = 0; i < REPORT_DAYS; i++)
            {
                var day = begin.AddDays(i);

                result.Charts.Labels.Add(day.ToString("M/d"));

                result.Histories.Add(new HomeReportHistory
                {
                    Day = day,
                    BabyTotal = GetDictValue<int>(babyDict, day),
                    FamilyTotal = GetDictValue<int>(familyDict, day),
                    UserTotal = GetDictValue<int>(playerDict, day),
                    RechargeTotal = GetDictValue<double>(orderDict, day).ToFixed(2)
                });

                if (day == end.AddDays(-1))
                {
                    result.YesterDay = new HomeReportData
                    {
                        BabyTotal = GetDictValue<int>(babyDict, day),
                        FamilyTotal = GetDictValue<int>(familyDict, day),
                        UserTotal = GetDictValue<int>(playerDict, day),
                        RechargeTotal = GetDictValue<double>(orderDict, day).ToFixed(2)
                    };
                }
            }

            result.Charts.Datasets.Add(new ChartDataset
            {
                Label = "玩家数",
                Data = result.Histories.Select(s => (double)s.UserTotal).ToList()
            });

            result.Charts.Datasets.Add(new ChartDataset
            {
                Label = "家庭数",
                Data = result.Histories.Select(s => (double)s.FamilyTotal).ToList()
            });

            result.Charts.Datasets.Add(new ChartDataset
            {
                Label = "宝宝数",
                Data = result.Histories.Select(s => (double)s.BabyTotal).ToList()
            });

            result.Charts.Datasets.Add(new ChartDataset
            {
                Label = "订单金额",
                Data = result.Histories.Select(s => s.RechargeTotal).ToList()
            });

            return result;
        }

        private Task<List<Player>> GetPlayers(GetHomeReportRequest request, DateTime begin, DateTime end)
        {
            return _playerRepository.GetAll()
                .Where(p => p.CreationTime >= begin && p.CreationTime < end)
                .WhereIf(request.TenantId.HasValue, p => p.TenantId == request.TenantId)
                .ToListAsync();
        }

        private async Task<Dictionary<DateTime, double>> GetOrders(GetHomeReportRequest request, DateTime begin, DateTime end)
        {
            var list = await _orderRepository.GetAll()
                .Where(p => p.CreationTime >= begin && p.CreationTime < end)
                .Where(p =>p.State == OrderState.Paid)
                .WhereIf(request.TenantId.HasValue, p => p.TenantId == request.TenantId)
                .ToListAsync();

            return list.GroupBy(i => i.CreationTime.ToDay())
                .ToDictionary(i => i.Key, i => i.Sum(o => o.Payment));
        }

        private Task<List<Family>> GetFamilies(GetHomeReportRequest request, DateTime begin, DateTime end)
        {
            return _familyRepository.GetAll()
                .Where(p => p.CreationTime >= begin && p.CreationTime < end)
                .ToListAsync();
        }

        private Task<List<Baby>> GetBabies(GetHomeReportRequest request, DateTime begin, DateTime end)
        {
            return _babyRepository.GetAll()
                .Where(p => p.CreationTime >= begin && p.CreationTime < end)
                .ToListAsync();
        }

        private Dictionary<DateTime, int> GetReportStateByEntity<TEntity>(IEnumerable<TEntity> items)
            where TEntity : IHasCreationTime
        {
            return items.GroupBy(i => i.CreationTime.ToDay())
                .ToDictionary(g => g.Key, g => g.Count());
        }
        
        private TUnit GetDictValue<TUnit>(Dictionary<DateTime, TUnit> dict, DateTime day)
            where TUnit : struct
        {
            if (dict.ContainsKey(day))
            {
                return dict[day];
            }

            return default(TUnit);
        }
    }
}
