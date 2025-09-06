
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置ChangeProfessionCost的AutoMapper
    /// </summary>
	internal static class ChangeProfessionCostMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ChangeProfessionCost,ChangeProfessionCostListDto>();
            configuration.CreateMap <ChangeProfessionCostListDto,ChangeProfessionCost>();

            configuration.CreateMap <ChangeProfessionCostEditDto,ChangeProfessionCost>();
            configuration.CreateMap <ChangeProfessionCost,ChangeProfessionCostEditDto>();

        }
	}
}
