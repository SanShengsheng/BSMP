using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    /// <typeparam name="AddModel">添加的dto</typeparam>
    /// <typeparam name="EditModel">编辑的dto</typeparam>
    /// <typeparam name="SearchModel">查询的dto</typeparam>
    /// <typeparam name="SearchOutModel">查询的output</typeparam>
    public interface BsmpApplicationService<TEntity, TPrimaryKey, AddModel, EditModel, SearchModel, SearchOutModel> : IApplicationService
        where TEntity : class, IEntity<TPrimaryKey>
        where AddModel : IAddModel<TEntity, TPrimaryKey>
        where EditModel : IEditModel<TEntity, TPrimaryKey>
        where SearchModel : PagedSortedAndFilteredInputDto, ISearchModel<TEntity, TPrimaryKey>
        where SearchOutModel : ISearchOutModel<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
    {
        Task Add(AddModel model);
        Task Edit(EditModel model);
        Task Delete(TPrimaryKey key);
        Task<SearchOutModel> Get(TPrimaryKey key);
        Task<IEnumerable<SearchOutModel>> Search(SearchModel model);
        Task<PagedResultDto<SearchOutModel>> PageSearch(SearchModel model);
        //有返回值
        Task<TEntity> Create(AddModel model);
        Task<TEntity> Update(EditModel model);

    }
}
