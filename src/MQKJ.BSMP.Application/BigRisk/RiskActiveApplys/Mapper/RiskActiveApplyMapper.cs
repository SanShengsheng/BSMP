
using AutoMapper;
using MQKJ.BSMP.ActiveApply;
using MQKJ.BSMP.ActiveApply.Dtos;

namespace MQKJ.BSMP.ActiveApply.Mapper
{

	/// <summary>
    /// 配置RiskActiveApply的AutoMapper
    /// </summary>
	internal static class RiskActiveApplyMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <RiskActiveApply,RiskActiveApplyListDto>();
            configuration.CreateMap <RiskActiveApplyListDto,RiskActiveApply>();

            configuration.CreateMap <RiskActiveApplyEditDto,RiskActiveApply>();
            configuration.CreateMap <RiskActiveApply,RiskActiveApplyEditDto>();

        }
	}
}
