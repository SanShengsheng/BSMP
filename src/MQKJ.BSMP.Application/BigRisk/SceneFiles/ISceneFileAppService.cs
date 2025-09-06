using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.SceneFiles.Dto;
using MQKJ.BSMP.SceneFiles.SceneFiles.Dto;
using MQKJ.BSMP.Scenes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.SceneManage.SceneFiles
{
    public interface ISceneFileAppService : IApplicationService
    {
        Task UploaSceneFileAsync(UploadSceneFileDto input);

        Task DeleteSceneFile(SceneFileDto input);

        List<GetSceneFileOutput> GetSceneFile(EntityDto<int> input);
        IEnumerable<GetSceneFileOutput> GetAllAsync();

        Task UpdateSceneFile(UpdateSceneFileInput input); 
    }
}
