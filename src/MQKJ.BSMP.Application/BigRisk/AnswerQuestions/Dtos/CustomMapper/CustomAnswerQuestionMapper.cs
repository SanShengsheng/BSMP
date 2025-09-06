using AutoMapper;
using MQKJ.BSMP.AnswerQuestions;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.AnswerQuestions.Dtos
{

	/// <summary>
	/// 配置AnswerQuestion的AutoMapper
	/// </summary>
	internal static class CustomerAnswerQuestionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <AnswerQuestion, AnswerQuestionListDto>();
            configuration.CreateMap <AnswerQuestionEditDto, AnswerQuestion>();
		
		    
			
		    //// custom codes 
		     
		    
            
            //// custom codes end

        }
    }
}