using AutoMapper;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.GameTasks.Dtos
{

	/// <summary>
	/// 配置GameTask的AutoMapper
	/// </summary>
	internal static class CustomerGameTaskMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <GameTask, GameTaskListDto>();
            configuration.CreateMap <GameTaskEditDto, GameTask>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}