
using AutoMapper;
using MQKJ.BSMP.TextAudios.Dtos;

namespace MQKJ.BSMP.TextAudios.Mapper
{

	/// <summary>
    /// 配置TextAudios的AutoMapper
    /// </summary>
	internal static class TextAudiosMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <TextAudio,TextAudiosListDto>();
            configuration.CreateMap <TextAudiosListDto,TextAudio>();

            configuration.CreateMap <TextAudiosEditDto,TextAudio>();
            configuration.CreateMap <TextAudio,TextAudiosEditDto>();

        }
	}
}
