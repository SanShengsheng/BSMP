using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;


using MQKJ.BSMP.Tags.Dto;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.Tags.Dtos;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.Tags
{
    /// <summary>
    /// Tag应用层服务的接口实现方法
    /// </summary>

    public class TagAppService : BSMPAppServiceBase, ITagAppService
    {
        private readonly IRepository<Tag, int> _tagRepository;

        private static Dictionary<int, int> RelationDegreeDic;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TagAppService(
            IRepository<Tag, int> tagRepository

        )
        {
            _tagRepository = tagRepository;


        }


        /// <summary>
        /// 获取Tag的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<TagListDto>> GetPagedTags(GetTagsInput input)
        {

            var query = _tagRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件
            if (input.WithDetail)
            {
                query = query.Include(t => t.TagType);
            }
            var tagCount = await query.CountAsync();

            var tags = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var tagListDtos = ObjectMapper.Map<List <TagListDto>>(tags);
            var tagListDtos = tags.MapTo<List<TagListDto>>();

            return new PagedResultDto<TagListDto>(
                        tagCount,
                        tagListDtos
                );
        }


        /// <summary>
        /// 通过指定id获取TagListDto信息
        /// </summary>
        public async Task<TagListDto> GetTagByIdAsync(EntityDto<int> input)
        {
            var entity = await _tagRepository.GetAsync(input.Id);

            return entity.MapTo<TagListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetTagForEditOutput> GetTagForEdit(NullableIdDto<int> input)
        {
            var output = new GetTagForEditOutput();
            TagEditDto tagEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _tagRepository.GetAsync(input.Id.Value);

                tagEditDto = entity.MapTo<TagEditDto>();

                //tagEditDto = ObjectMapper.Map<List <tagEditDto>>(entity);
            }
            else
            {
                tagEditDto = new TagEditDto();
            }

            output.Tag = tagEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Tag的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateTag(TagEditDto input)
        {

            if (input.Id.HasValue)
            {
                await UpdateTagAsync(input);
            }
            else
            {
                await CreateTagAsync(input);
            }
        }


        /// <summary>
        /// 新增Tag
        /// </summary>

        protected virtual async Task<TagEditDto> CreateTagAsync(TagEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<Tag>(input);

            entity = await _tagRepository.InsertAsync(entity);
            return entity.MapTo<TagEditDto>();
        }

        /// <summary>
        /// 编辑Tag
        /// </summary>

        protected virtual async Task UpdateTagAsync(TagEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _tagRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _tagRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Tag信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteTag(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _tagRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Tag的方法
        /// </summary>

        public async Task BatchDeleteTagsAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _tagRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出Tag为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetTagsToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}



        //// custom codes 

        /// <summary>
        /// 获取关系维度的字典对照
        /// </summary>
        public Dictionary<int, int> GetRelationDegreeTagDic()
        {
            if (RelationDegreeDic != null && RelationDegreeDic.Count == 4)
            {
                return RelationDegreeDic;
            }
            var relationDegreeTags = (_tagRepository.GetAllIncluding(t => t.TagType)).Where(a => a.TagType.Code == "RelationshipDegree").ToList();
            var relationDegreeTagDictionary = new Dictionary<int, int>();
            //  关系维度对照表
            foreach (var item in relationDegreeTags)
            {
                var relationDegreeId = 0;
                switch (item.TagName)
                {
                    case "朋友":
                        relationDegreeId = (int)RelationDegree.Ordinary;
                        break;
                    case "暧昧":
                        relationDegreeId = (int)RelationDegree.Ambiguous;
                        break;
                    case "情侣":
                        relationDegreeId = (int)RelationDegree.Lovers;
                        break;
                    case "夫妻":
                        relationDegreeId = (int)RelationDegree.Couple;
                        break;
                }
                if (!relationDegreeTagDictionary.Keys.Any(k => k == relationDegreeId))
                {
                    relationDegreeTagDictionary.Add(relationDegreeId, item.Id);
                }
            }
            RelationDegreeDic = relationDegreeTagDictionary;
            return RelationDegreeDic;
        }
        //// custom codes end

    }
}


