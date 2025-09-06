
using AutoMapper;
using MQKJ.BSMP.UnLocks;
using MQKJ.BSMP.UnLocks.Dtos;

namespace MQKJ.BSMP.UnLocks.Mapper
{

	/// <summary>
    /// 配置Unlock的AutoMapper
    /// </summary>
	internal static class UnlockMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Unlock,UnlockListDto>();
            configuration.CreateMap <UnlockListDto,Unlock>();

            configuration.CreateMap <UnlockEditDto,Unlock>();
            configuration.CreateMap <Unlock,UnlockEditDto>();

        }
	}
}
