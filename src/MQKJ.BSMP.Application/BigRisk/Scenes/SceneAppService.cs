using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Roles.Dto;
using MQKJ.BSMP.Scenes;
using MQKJ.BSMP.SceneTypes;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.Scenes.Dto;
using MQKJ.BSMP.Sessions;
using MQKJ.BSMP.Sessions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.Scenes.Authorization;

namespace MQKJ.BSMP.Scenes
{
    [AbpAuthorize(SceneAppPermissions.Scene)]
    public class SceneAppService : BSMP.BSMPAppServiceBase, ISceneAppService
    {
        private readonly IRepository<Scene> _sceneRepository;

        //private readonly IRepository<User, long> _userRepository;

        private readonly IRepository<SceneType> _sceneTypeRepository;

        private readonly IRepository<SceneFile, Guid> _sceneFileRepository;

        private readonly IRepository<BSMPFile, Guid> _BSMPFileRepository;

        public SceneAppService(
            IRepository<Scene> sceneRepository,
            IRepository<SceneType> sceneTypeRepository,
            IRepository<SceneFile, Guid> sceneFileRepository,
            IRepository<BSMPFile, Guid> BSMPFileRepository
            )
        {
            _sceneRepository = sceneRepository;
            _sceneTypeRepository = sceneTypeRepository;
            _sceneFileRepository = sceneFileRepository;
            _BSMPFileRepository = BSMPFileRepository;
        }

        /// <summary>
        /// 获取场景集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetScenesListDto>> GetAllScenesAsync(GetSceneInput input)
        {
            var Scenes = await _sceneRepository.GetAll().OrderBy(input.Sorting).ToListAsync();
            var dtos = Scenes.MapTo<List<GetScenesListDto>>();
            return dtos;
        }

        /// <summary>
        /// 创建或编辑场景
        /// </summary>
        /// <returns></returns>
        public async Task CreateOrUpdateSceneAsync(CreateOrUpdateSceneInput input)
        {
            var entity = _sceneRepository.FirstOrDefault(x => x.Id == input.SceneEditDto.Id);

            var scenetype = _sceneTypeRepository.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName);

            input.SceneEditDto.SceneTypeId = scenetype.Id;

            if (entity != null)
            {
                await UpdateSceneAsync(input.SceneEditDto);
            }
            else
            {
                await CreateSceneAsync(input.SceneEditDto);
            }
        }

        /// <summary>
        /// 删除场景
        /// </summary>
        /// <param name="input"></param>
        //public void DeleteScene(EntityDto input)
        //{
        //    throw new NotImplementedException();
        //}

        [AbpAuthorize(SceneAppPermissions.Scene_DeleteScene)]
        public async Task DeleteSceneAsync(EntityDto input)
        {
            var entity = await _sceneRepository.GetAllIncluding(f => f.SceneFile).FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("该场景不存在");
            }
            else
            {

                if (entity.SceneFile == null)
                {
                    await _sceneRepository.DeleteAsync(input.Id);
                }else
                {
                    throw new UserFriendlyException("该场景已经有图片不能删除");
                }
            }
        }

        /// <summary>
        /// 通过Id获取场景
        /// </summary>
        /// <param name="SceneId"></param>
        //public void GetSceneById(NullableIdDto input)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<GetScenesListDto> GetSceneByIdAsync(NullableIdDto input)
        {
            var entity = await _sceneRepository.GetAsync(input.Id.Value);

            return entity.MapTo<GetScenesListDto>();
        }

        /// <summary>
        /// 获取场景列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public GetScenesListDto GetPagedScenes(GetSceneInput input)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<PagedResultDto<GetScenesListDto>> GetPagedScenesAsync(GetSceneInput input)
        {
            var query = _sceneRepository.GetAll();

            var scnesCount = await query.CountAsync();

            var list = new List<GetScenesListDto>();

            var scenes = (await query.Include(x => x.SceneFile)
                .ThenInclude(f => f.File).OrderBy(input.Sorting)
                //.Where(d => d.SceneFile.IsDefault || d.SceneFile == null)
                .PageBy(input).AsNoTracking().ToListAsync()).Distinct();

            var SceneListDtos = scenes.MapTo<List<GetScenesListDto>>();

            return new PagedResultDto<GetScenesListDto>(scnesCount, SceneListDtos);

        }

        [AbpAuthorize(SceneAppPermissions.Scene_EditScene)]
        public async Task UpdateSceneAsync(SceneEditDto input)
        {

            var entity = await _sceneRepository.GetAsync(input.Id.Value);

            await _sceneRepository.UpdateAsync(input.MapTo(entity));
        }


        [AbpAuthorize(SceneAppPermissions.Scene_CreateScene)]
        protected async Task CreateSceneAsync(SceneEditDto input)
        {
            //var sceneFileEntity = await _sceneFileRepository.FirstOrDefaultAsync(x => x.SceneId == input.Id.Value);
            //if (sceneFileEntity == null)
            //{
            //    await _sceneFileRepository.InsertAsync(new SceneFile()
            //    {
            //        SceneId = input.Id.Value,
            //        FileId = _BSMPFileRepository.FirstOrDefaultAsync(x => x.)
            //    });
            //}

            await _sceneRepository.InsertAsync(input.MapTo<Scene>());
        }

        public async Task<GetSceneForEditOutput> GetSceneForEditAsync(NullableIdDto input)
        {
            var output = new GetSceneForEditOutput();
            SceneEditDto sceneEditDto;
            if (input.Id.HasValue)
            {
                var entity = await _sceneRepository.GetAsync(input.Id.Value);

                sceneEditDto = entity.MapTo<SceneEditDto>();
            }
            else
            {
                sceneEditDto = new SceneEditDto();
            }
            output.SceneEditDto = sceneEditDto;
            return output;
        }
    }
}
