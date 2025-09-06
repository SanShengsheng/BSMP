using Abp.AutoMapper;
using Abp.Modules;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.ChineseBabies.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.SceneManage.SceneFiles.Dto.CustomMapper
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class CustomModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                //    config.CreateMap<BSMPFile, UploadSceneFileDto>()
                //    .ForMember(f => f.SceneId, options => options.Ignore())
                //    .ForMember(f => f.SceneFileInput, options => options.Ignore());
                BabyMapper.CreateMappings(config);

            });
        }
    }
}
