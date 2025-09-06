using AutoMapper;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Questions.Dtos
{

	/// <summary>
	/// 配置Question的AutoMapper
	/// </summary>
	internal static class CustomerQuestionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Question, QuestionListDto>();
            configuration.CreateMap <QuestionEditDto, Question>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}