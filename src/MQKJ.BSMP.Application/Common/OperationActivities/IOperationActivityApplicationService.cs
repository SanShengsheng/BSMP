
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using MQKJ.BSMP.Common.OperationActivities.Dtos;
using MQKJ.BSMP.Common.OperationActivities;

namespace MQKJ.BSMP.Common.OperationActivities
{
    /// <summary>
    /// OperationActivity应用层服务的接口方法
    ///</summary>
    public interface IOperationActivityAppService : BsmpApplicationService<OperationActivity, int, OperationActivityEditDto, OperationActivityEditDto, GetOperationActivitiesInput, OperationActivityListDto>
    {
           }
}
