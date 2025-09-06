
using AutoMapper;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCards.Dtos;

namespace MQKJ.BSMP.LoveCardFiles.Mapper
{

	/// <summary>
    /// 配置LoveCardFile的AutoMapper
    /// </summary>
	internal static class LoveCardFileMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LoveCardFile,LoveCardFileListDto>();
            configuration.CreateMap <LoveCardFileListDto,LoveCardFile>();

            configuration.CreateMap <LoveCardFileEditDto,LoveCardFile>();
            configuration.CreateMap <LoveCardFile,LoveCardFileEditDto>();

            configuration.CreateMap<BSMPFile, BSMPFileDto>()
                .ForMember(d => d.FileType,options => options.MapFrom(input => input.type));

        }
	}
}
