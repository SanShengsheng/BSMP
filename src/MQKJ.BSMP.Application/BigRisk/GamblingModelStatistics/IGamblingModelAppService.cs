using Abp.Application.Services.Dto;
using MQKJ.BSMP.GamblingModelStatistics.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.GamblingModelStatistics
{
    public interface IGamblingModelAppService
    {
        List<GetGamblingModelOpeningDataOutput> GetOpeningData(GetGamblingModelOpeningDataDto input);

        Task<List<GetGamblingModelAnswerQuestionDataOutput>> GetAnswerQuestionData(GetGamblingModelAnswerQuestionDataDto input);


        PagedResultDto<GetErrQuestionDistributionOutput> GetErrQuestionDistribution(GetErrQuestionDistributionDto input);
    }
}
