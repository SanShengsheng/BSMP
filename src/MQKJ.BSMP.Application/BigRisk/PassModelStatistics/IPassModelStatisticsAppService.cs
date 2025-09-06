using Abp.Application.Services.Dto;
using MQKJ.BSMP.PassModelStatistics.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.PassModelStatistics
{
    public interface IPassModelStatisticsAppService
    {
        List<GetPassModelOpeningDataOutput> GetOpeningData(GetPassModelOpeningDataDto input);

        Task<List<GetPassModelAnswerQuestionDataOutput>> GetAnswerQuestionData(GetPassModelAnswerQuestionDataDto input);

        Task<PagedResultDto<GetLevelDistributionOutput>> GetLevelDistribution(GetPassModelLevelDistributionDto input);
    }
}
