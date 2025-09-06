
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置Profession的AutoMapper
    /// </summary>
	internal static class ProfessionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Profession,ProfessionListDto>().ForMember(p => p.IsUnLock,option => option.Ignore());
            configuration.CreateMap<Profession, ProfessionListDto>().ForMember(p => p.IsCurrent, option => option.Ignore());
            configuration.CreateMap <ProfessionListDto,Profession>();

            configuration.CreateMap <ProfessionEditDto,Profession>();
            configuration.CreateMap <Profession,ProfessionEditDto>();

        }
	}
}
