
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


using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;




namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// EnergyRechargeRecord应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class EnergyRechargeRecordAppService : BsmpApplicationServiceBase<EnergyRechargeRecord, Guid, EnergyRechargeRecordEditDto, EnergyRechargeRecordEditDto, GetEnergyRechargeRecordsInput, EnergyRechargeRecordListDto>, IEnergyRechargeRecordAppService
    {
        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EnergyRechargeRecordAppService(
        IRepository<EnergyRechargeRecord, Guid> entityRepository
        
        ):base(entityRepository)
        {            
        }

        internal override IQueryable<EnergyRechargeRecord> GetQuery(GetEnergyRechargeRecordsInput model)
        {
            return _repository.GetAll();
        }
    }
}


