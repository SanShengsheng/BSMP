
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.Common.OperationActivities;
using MQKJ.BSMP.Common.OperationActivities.Dtos;
using MQKJ.BSMP.Common.OperationActivities.DomainService;
using MQKJ.BSMP.Common.OperationActivities.Authorization;


namespace MQKJ.BSMP.Common.OperationActivities
{
    /// <summary>
    /// OperationActivity应用层服务的接口实现方法  
    ///</summary>
    public class OperationActivityAppService : BsmpApplicationServiceBase<OperationActivity, int, OperationActivityEditDto, OperationActivityEditDto, GetOperationActivitiesInput, OperationActivityListDto>,IOperationActivityAppService
    {
        private readonly IRepository<OperationActivity, int> _entityRepository;

        private readonly IOperationActivityManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public OperationActivityAppService(
        IRepository<OperationActivity, int> entityRepository
        ,IOperationActivityManager entityManager
        ):base(entityRepository)
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }

        internal override IQueryable<OperationActivity> GetQuery(GetOperationActivitiesInput model)
        {
            var query = _entityRepository.GetAll()
                  .WhereIf(model.ActivityType.HasValue, r => r.ActivityType == model.ActivityType)
                  .OrderBy(model.Sorting);
            return query;
        }
    }
}


