using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Repositories
{
    public interface IRepositoryGuid<TEntity> : IRepository<TEntity, Guid>, ITransientDependency 
        where TEntity : class, IEntity<Guid>
    {
    }
}
