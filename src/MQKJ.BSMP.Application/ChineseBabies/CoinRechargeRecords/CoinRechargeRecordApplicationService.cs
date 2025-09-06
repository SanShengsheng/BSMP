
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
using MQKJ.BSMP.ChineseBabies.CoinRechargeRecords.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// CoinRechargeRecord应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class CoinRechargeRecordAppService : BsmpApplicationServiceBase<CoinRechargeRecord, Guid, CoinRechargeRecordEditDto, CoinRechargeRecordEditDto, GetCoinRechargeRecordsInput, CoinRechargeRecordListDto>, ICoinRechargeRecordAppService
    {
        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public CoinRechargeRecordAppService(
        IRepository<CoinRechargeRecord, Guid> entityRepository
        
        ):base(entityRepository)
        {
            
        }

        internal override IQueryable<CoinRechargeRecord> GetQuery(GetCoinRechargeRecordsInput model)
        {
            return _repository.GetAll();
        }

        public async Task<CoinRechargeRecordListDto> GetRechargeRecordByFamilyId(GetRechargeRecordByFamilyIdInput input)
        {
            var rechargeRecord = await _repository.GetAll().FirstOrDefaultAsync(r => r.FamilyId == input.FamilyId && r.RechargeLevel == input.RechargeLevel);

            return rechargeRecord.MapTo<CoinRechargeRecordListDto>();
        }
    }
}


