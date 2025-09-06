using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    public abstract class ChineseBabyEntityBase<TKey> :
        Entity<TKey>, IHasCreationTime, ISoftDelete
    {
        public ChineseBabyEntityBase()
        {
            CreationTime = DateTime.UtcNow;
        }
        public virtual bool IsDeleted { get ; set; }
        public virtual DateTime CreationTime { get; set; }
    }

    public abstract class ChineseBabyEntityBase : ChineseBabyEntityBase<int> { }
    public abstract class ChineseBabyEntityGuidBase : ChineseBabyEntityBase<Guid> { }
}
