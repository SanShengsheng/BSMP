using AutoMapper;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.Tags.Dtos;

namespace MQKJ.BSMP.Tags.Dto
{

	/// <summary>
	/// 配置Tag的AutoMapper
	/// </summary>
	internal static class CustomerTagMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Tag, TagListDto>();
            configuration.CreateMap <TagEditDto, Tag>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}