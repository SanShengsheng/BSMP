using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public abstract class BsmpApplicationServiceBase<TEntity, TPrimaryKey, AddModel, EditModel, SearchModel, SearchOutModel> : BSMPAppServiceBase,
        BsmpApplicationService<TEntity, TPrimaryKey, AddModel, EditModel, SearchModel, SearchOutModel>
        where TEntity : class, IEntity<TPrimaryKey>
        where AddModel : IAddModel<TEntity, TPrimaryKey>
        where EditModel : IEditModel<TEntity, TPrimaryKey>
        where SearchModel : PagedSortedAndFilteredInputDto, ISearchModel<TEntity, TPrimaryKey>
        where SearchOutModel : ISearchOutModel<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected readonly IRepository<TEntity, TPrimaryKey> _repository;
        public BsmpApplicationServiceBase(IRepository<TEntity, TPrimaryKey> repository)
        {
            _repository = repository;
        }

        public virtual Task Add(AddModel model)
        {
            var entity = GetAddModel(model);
            return _repository.InsertAsync(entity);
        }

        /// <summary>
        /// 获取需要添加的实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual TEntity GetAddModel(AddModel model)
        {
            return ObjectMapper.Map<TEntity>(model);
        }

        public virtual Task Delete(TPrimaryKey key)
        {
            return _repository.DeleteAsync(t => t.Id.Equals(key));
        }

        public virtual async Task Edit(EditModel model)
        {
            var entity = GetEditModel(model);

            await _repository.UpdateAsync(entity);
        }

        private TEntity GetEditModel(EditModel model)
        {
            var entity = _repository.Get(model.Id.Value);
            model.MapTo(entity);

            return entity;
        }

        public virtual async Task<SearchOutModel> Get(TPrimaryKey key)
        {
            var result = await _repository.GetAsync(key);

            return result.MapTo<SearchOutModel>();
        }

        public virtual async Task<PagedResultDto<SearchOutModel>> PageSearch(SearchModel model)
        {
            if (model.PageSize > 0)
            {
                model.MaxResultCount = model.PageSize;
            }
            if (model.PageIndex > 0)
            {
                model.SkipCount = (model.PageIndex - 1) * model.PageSize;
            }
            var query = GetQuery(model);
            var count = query.Count();
            var result = await query
                    .OrderBy(model.Sorting)
                    .PageBy(model)
                   .ToListAsync();

            // var answerListDtos = ObjectMapper.Map<List <AnswerListDto>>(answers);
            var dtos = ToSearchOutModel(result);

            return new PagedResultDto<SearchOutModel>(
                        count,
                        dtos
                );

        }

        internal abstract IQueryable<TEntity> GetQuery(SearchModel model);

        public virtual async Task<IEnumerable<SearchOutModel>> Search(SearchModel model)
        {
            var list = await GetQuery(model).ToListAsync();

            return ToSearchOutModel(list);
        }

        private IReadOnlyList<SearchOutModel> ToSearchOutModel(List<TEntity> list)
        {
            return list.MapTo<List<SearchOutModel>>();
        }
        #region 有返回值
        public virtual Task<TEntity> Create(AddModel model)
        {
            var entity = GetAddModel(model);
            return _repository.InsertAsync(entity);
        }
        public virtual async Task<TEntity> Update(EditModel model)
        {
            var entity = GetEditModel(model);

            return await _repository.UpdateAsync(entity);
        }
        #endregion

    }
}
