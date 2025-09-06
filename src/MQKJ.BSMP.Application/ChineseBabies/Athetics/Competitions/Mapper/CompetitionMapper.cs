using AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Mapper
{
    internal static class CompetitionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Competition, CompetitionListDtos>();
            configuration.CreateMap<CompetitionListDtos, Competition>();
            configuration.CreateMap<Competition, PrizeRewardModel>().ForMember(c => c.PrizeCoinCount,option => option.Ignore());
            configuration.CreateMap<Competition, GetRankingListOutput>().ForMember(c => c.SelfNumber, option => option.Ignore());

            //configuration.CreateMap <AutoRunnerConfigEditDto,AutoRunnerConfig>();
            //configuration.CreateMap <AutoRunnerConfig,AutoRunnerConfigEditDto>();

        }
    }
}
