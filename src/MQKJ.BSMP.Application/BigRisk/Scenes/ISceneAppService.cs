using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.Scenes.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace MQKJ.BSMP.Scenes
{
    public interface ISceneAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有场景列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<GetScenesListDto>> GetAllScenesAsync(GetSceneInput input);

        /// <summary>
        /// 获取场景列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetScenesListDto>> GetPagedScenesAsync(GetSceneInput input);
        //GetScenesListDto GetPagedScenes(GetSceneInput input);

        /// <summary>
        /// 根据Id获取场景的信息
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        Task<GetScenesListDto> GetSceneByIdAsync(NullableIdDto Input);
        //void GetSceneById(NullableIdDto Input);

        /// <summary>
        /// 通过Id获取联系人进行编辑的操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetSceneForEditOutput> GetSceneForEditAsync(NullableIdDto input);

        /// <summary>
        /// 添加或者更新场景
        /// </summary>
        /// <returns></returns>
        Task CreateOrUpdateSceneAsync(CreateOrUpdateSceneInput input);
        //int CreateAndUpdateScene();

        //void UpdateScene(UpdateSceneInput input);

        /// <summary>
        /// 删除场景
        /// </summary>
        /// <param name="input"></param>
        Task DeleteSceneAsync(EntityDto input);
        //void DeleteScene(EntityDto input);

    }
}
