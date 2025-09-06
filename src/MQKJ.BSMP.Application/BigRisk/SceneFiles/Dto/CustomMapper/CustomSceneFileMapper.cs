using MQKJ.BSMP.BSMPFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.SceneManage.SceneFiles.Dto.CustomMapper
{
    internal static class CustomSceneFileMapper
    {
        public static void CreateMappings(AutoMapper.IMapperConfigurationExpression configuration)
        {
            //configuration.AddGlobalIgnore("");
            //var fileMapper = configuration.CreateMap<BSMPFile, UploadSceneFileDto>();
            //fileMapper.ForMember(dto => dto.SceneId,map => map.MapFrom(m => m.))
        }
    }
}
