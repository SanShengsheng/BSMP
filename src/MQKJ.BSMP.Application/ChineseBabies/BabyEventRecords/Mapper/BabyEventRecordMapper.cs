
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置BabyEventRecord的AutoMapper
    /// </summary>
	internal static class BabyEventRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BabyEventRecord,BabyEventRecordListDto>();
            configuration.CreateMap <BabyEventRecordListDto,BabyEventRecord>();

            configuration.CreateMap <BabyEventRecordEditDto,BabyEventRecord>();
            configuration.CreateMap <BabyEventRecord,BabyEventRecordEditDto>();

            //configuration.CreateMap<GetGrowUpEventsOutputRecord, BabyEventRecord>().ForMember(s => s.OptionId, o => o.MapFrom(x => x.TheOtherSelectOptionId)).ForMember(s => s.EndTime, o => o.MapFrom(x => x.EndDateTime));
            configuration.CreateMap<BabyEventRecord,GetGrowUpEventsOutputRecord>().ForMember(s => s.TheOtherSelectOptionId, o => o.MapFrom(x => x.OptionId)).ForMember(s => s.EndDateTime, o => o.MapFrom(x => x.EndTimeStamp));
            configuration.CreateMap<BabyEventRecord, GetStudyEventsOutputRecord>().ForMember(s => s.TheOtherSelectOptionId, o => o.MapFrom(x => x.OptionId)).ForMember(s => s.EndDateTime, o => o.MapFrom(x => x.EndTimeStamp));
        }
	}
}
