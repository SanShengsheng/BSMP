
using AutoMapper;
using MQKJ.BSMP.Likes;
using MQKJ.BSMP.Likes.Dtos;

namespace MQKJ.BSMP.Likes.Mapper
{

	/// <summary>
    /// 配置Like的AutoMapper
    /// </summary>
	internal static class LikeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Like,LikeListDto>();
            configuration.CreateMap <LikeListDto,Like>();

            configuration.CreateMap <LikeEditDto,Like>();
            configuration.CreateMap <Like,LikeEditDto>();

        }
	}
}
