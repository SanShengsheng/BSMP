using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP
{
    public interface IAddModel<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

    }

    public interface IEditModel<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        TPrimaryKey? Id { get; set; }
    }
    
    public interface ISearchModel<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
      
    }

    public interface ISearchOutModel<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

    }
}
