
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.Dtos;

namespace MQKJ.BSMP.Common.Mapper
{

    /// <summary>
    /// 配置VersionManage的AutoMapper
    /// </summary>
    internal static class VersionManageMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<VersionManage, VersionManageListDto>();
            configuration.CreateMap<VersionManageListDto, VersionManage>();

            configuration.CreateMap<VersionManageEditDto, VersionManage>();
            configuration.CreateMap<VersionManage, VersionManageEditDto>();

        }
    }
}
