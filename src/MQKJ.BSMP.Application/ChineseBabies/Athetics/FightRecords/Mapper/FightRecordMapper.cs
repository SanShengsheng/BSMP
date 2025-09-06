using AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athetics.FightRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.FightRecords.Mapper
{
    internal static class FightRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<FightRecord, FightRecordListDtos>();
            configuration.CreateMap<FightRecordListDtos, FightRecord>();

            //configuration.CreateMap <AutoRunnerConfigEditDto,AutoRunnerConfig>();
            //configuration.CreateMap <AutoRunnerConfig,AutoRunnerConfigEditDto>();

        }
    }
}
