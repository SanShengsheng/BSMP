
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置Reward的AutoMapper
    /// </summary>
	internal static class RewardMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Reward,RewardListDto>();
            configuration.CreateMap <RewardListDto,Reward>();

            configuration.CreateMap <RewardEditDto,Reward>();
            configuration.CreateMap <Reward,RewardEditDto>();

        }
	}
}
