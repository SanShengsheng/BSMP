
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置BabyGrowUpRecord的AutoMapper
    /// </summary>
	internal static class BabyGrowUpRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap <BabyGrowUpRecord,BabyGrowUpRecordListDto>();
            //configuration.CreateMap <BabyGrowUpRecordListDto,BabyGrowUpRecord>();

            configuration.CreateMap <BabyGrowUpRecordEditDto,BabyGrowUpRecord>();
            configuration.CreateMap <BabyGrowUpRecord,BabyGrowUpRecordEditDto>();

        }
	}
}
