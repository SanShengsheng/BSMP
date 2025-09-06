
using AutoMapper;
using MQKJ.BSMP.Feedbacks;
using MQKJ.BSMP.Feedbacks.Dtos;

namespace MQKJ.BSMP.Feedbacks.Mapper
{

	/// <summary>
    /// 配置Feedback的AutoMapper
    /// </summary>
	internal static class FeedbackMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Feedback,FeedbackListDto>();
            configuration.CreateMap <FeedbackListDto,Feedback>();

            configuration.CreateMap <FeedbackEditDto,Feedback>();
            configuration.CreateMap <Feedback,FeedbackEditDto>();

        }
	}
}
