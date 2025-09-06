using Abp.Application.Services;
using MQKJ.BSMP.Reports.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Reports
{
    public interface IReportService : IApplicationService
    {
        Task<GetHomeReportResponse> GetHomeReport(GetHomeReportRequest request);
    }
}
