using AutoMapper;
using MQKJ.BSMP.Answers;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Answers.Dtos
{

	/// <summary>
	/// 配置Answer的AutoMapper
	/// </summary>
	internal static class CustomerAnswerMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Answer, AnswerListDto>();
            configuration.CreateMap <AnswerEditDto, Answer>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}