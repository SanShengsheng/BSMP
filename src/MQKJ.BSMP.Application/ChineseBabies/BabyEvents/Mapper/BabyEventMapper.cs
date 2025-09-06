
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置BabyEvent的AutoMapper
    /// </summary>
	internal static class BabyEventMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BabyEvent,BabyEventListDto>();
            configuration.CreateMap <BabyEventListDto,BabyEvent>();

            //configuration.CreateMap <BabyEventEditDto,BabyEvent>();
            //configuration.CreateMap <BabyEvent,BabyEventEditDto>();
            configuration.CreateMap<BabyEvent, GetGrowUpEventsOutputEvent>().ForMember(dest => dest.Image, o => o.MapFrom(x => x.ImagePath)).ForMember(d=>d.CountDownOrigin,o=>o.Ignore());

            configuration.CreateMap<BabyEvent, StudyEventBabyEvent>().ForMember(dest => dest.Image, o => o.MapFrom(x => x.ImagePath)).ForMember(dest => dest.StudyTypeDescription, o => o.MapFrom(x => x.StudyType!=null? EnumHelper.EnumHelper.GetDescription(x.StudyType):"")); 
            //导入数据
            configuration.CreateMap<ImportEventDto, BabyEvent>().ForMember(dest => dest.ImagePath, o => o.MapFrom(x => x.imagePath));
        }
    }
}
