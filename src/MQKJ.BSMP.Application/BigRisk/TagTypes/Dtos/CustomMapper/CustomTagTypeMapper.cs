using AutoMapper;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.TagTypes.Dtos
{

	/// <summary>
	/// 配置TagType的AutoMapper
	/// </summary>
	internal static class CustomerTagTypeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <TagType, TagTypeListDto>();
            configuration.CreateMap <TagTypeEditDto, TagType>();
		
		    
			
		    //// custom codes 
		     
		     
		    
            
            
            //// custom codes end

        }
    }
}