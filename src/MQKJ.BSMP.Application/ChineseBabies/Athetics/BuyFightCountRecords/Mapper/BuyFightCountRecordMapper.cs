using AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Mapper
{
    internal static class BuyFightCountRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BuyFightCountRecord, BuyFightCountRecordListDtos>();
            configuration.CreateMap<BuyFightCountRecordListDtos, BuyFightCountRecord>();

            //configuration.CreateMap <AutoRunnerConfigEditDto,AutoRunnerConfig>();
            //configuration.CreateMap <AutoRunnerConfig,AutoRunnerConfigEditDto>();

        }
    }
}
