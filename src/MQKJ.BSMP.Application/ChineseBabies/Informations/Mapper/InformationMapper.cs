
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Message.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置Information的AutoMapper
    /// </summary>
	internal static class InformationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Information, InformationListDto>();
            configuration.CreateMap<InformationListDto, Information>();
            configuration.CreateMap< Information, HasNewInformationResponseMessage>().ForMember(s=>s.Description,t=>t.Ignore());
            //configuration.CreateMap<InformationEditDto, Information>();
            //configuration.CreateMap<Information, InformationEditDto>();

        }
    }
}
