using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.TextAudios.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.TextAudios
{
    /// <summary>
    /// TextAudios应用层服务的接口实现方法  
    ///</summary>
    public class TextAudiosAppService : BSMPAppServiceBase, ITextAudiosAppService
    {
        private readonly IRepository<TextAudio, int> _entityRepository;


        /// <summary>
        /// 构造函数 
        ///</summary>
        public TextAudiosAppService(
        IRepository<TextAudio, int> entityRepository

        )
        {
            _entityRepository = entityRepository;
        }


        /// <summary>
        /// 获取TextAudios的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<TextAudiosListDto>> GetPaged(GetTextAudiossInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(s=>s.Sort).AsNoTracking()
                    .WhereIf(input.Scene != null, s => s.Scene == input.Scene)
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<TextAudiosListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<TextAudiosListDto>>();

            return new PagedResultDto<TextAudiosListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取TextAudiosListDto信息
        /// </summary>

        public async Task<TextAudiosListDto> GetById(EntityDto<int> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<TextAudiosListDto>();
        }

        /// <summary>
        /// 获取编辑 TextAudios
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetTextAudiosForEditOutput> GetForEdit(NullableIdDto<int> input)
        {
            var output = new GetTextAudiosForEditOutput();
            TextAudiosEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<TextAudiosEditDto>();

                //textAudiosEditDto = ObjectMapper.Map<List<textAudiosEditDto>>(entity);
            }
            else
            {
                editDto = new TextAudiosEditDto();
            }

            output.TextAudios = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改TextAudios的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateTextAudiosInput input)
        {

            if (input.TextAudios.Id.HasValue)
            {
                await Update(input.TextAudios);
            }
            else
            {
                await Create(input.TextAudios);
            }
        }


        /// <summary>
        /// 新增TextAudios
        /// </summary>

        protected virtual async Task<TextAudiosEditDto> Create(TextAudiosEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <TextAudios>(input);
            var entity = input.MapTo<TextAudio>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<TextAudiosEditDto>();
        }

        /// <summary>
        /// 编辑TextAudios
        /// </summary>

        protected virtual async Task Update(TextAudiosEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除TextAudios信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除TextAudios的方法
        /// </summary>

        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出TextAudios为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


