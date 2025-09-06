
using AutoMapper;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.PlayerLabels.Dtos;

namespace MQKJ.BSMP.PlayerLabels.Mapper
{

	/// <summary>
    /// 配置PlayerLabel的AutoMapper
    /// </summary>
	internal static class PlayerLabelMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <PlayerLabel,PlayerLabelListDto>();
            configuration.CreateMap <PlayerLabelListDto,PlayerLabel>();

            configuration.CreateMap <PlayerLabelEditDto,PlayerLabel>();
            configuration.CreateMap <PlayerLabel,PlayerLabelEditDto>();

        }
	}
}
