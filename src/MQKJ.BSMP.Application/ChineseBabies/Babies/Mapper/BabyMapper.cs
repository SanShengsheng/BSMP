
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置Baby的AutoMapper
    /// </summary>
	internal static class BabyMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Baby,BabyListDto>();
            configuration.CreateMap <BabyListDto,Baby>();

            configuration.CreateMap <BabyEditDto,Baby>();
            configuration.CreateMap <Baby,BabyEditDto>();
            configuration.CreateMap<Baby, GetBabyBasicInfoOutputBaby>()
                .ForMember(b=>b.BabyProperty,options=>options.Ignore());
            configuration.CreateMap<Baby, GetFamilyElseBabiesByPageOutputBaby>()
            .ForMember(b => b.BabyProperty, options => options.Ignore());
            configuration.CreateMap<Baby, GetFamilyElseBabiesByPageOutputBaby>()
          .ForMember(b => b.StroyEnding, options => options.Ignore());
        }
	}
}
