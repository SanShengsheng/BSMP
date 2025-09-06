
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置PlayerProfession的AutoMapper
    /// </summary>
	internal static class PlayerProfessionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <PlayerProfession,PlayerProfessionListDto>();
            configuration.CreateMap <PlayerProfessionListDto,PlayerProfession>();

            configuration.CreateMap <PlayerProfessionEditDto,PlayerProfession>();
            configuration.CreateMap <PlayerProfession,PlayerProfessionEditDto>();

        }
	}
}
