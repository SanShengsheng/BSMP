using AutoMapper;
using MQKJ.BSMP.PlayerExtensions;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.PlayerExtensions.Dtos
{

	/// <summary>
	/// 配置PlayerExtension的AutoMapper
	/// </summary>
	internal static class CustomerPlayerExtensionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <PlayerExtension, PlayerExtensionListDto>();
            configuration.CreateMap <PlayerExtensionEditDto, PlayerExtension>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}