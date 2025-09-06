
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
using MQKJ.BSMP.ChineseBabies.ChangeProfessionCosts.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// ChangeProfessionCost应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class ChangeProfessionCostAppService : BsmpApplicationServiceBase<ChangeProfessionCost, int, ChangeProfessionCostEditDto, ChangeProfessionCostEditDto, GetChangeProfessionCostsInput, ChangeProfessionCostListDto>, IChangeProfessionCostAppService
    {

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ChangeProfessionCostAppService(IRepository<ChangeProfessionCost, int> entityRepository):base(entityRepository)
        {
            
        }

        internal override IQueryable<ChangeProfessionCost> GetQuery(GetChangeProfessionCostsInput model)
        {
            return _repository.GetAll().Include(p => p.Profession).ThenInclude(r => r.Reward);
        }

        public async Task<ChangeProfessionCostListDto> GetByProfessionIdAndCostType(GetByProfessionIdInput input)
        {
            var entity = await _repository.GetAll().FirstOrDefaultAsync(x => x.ProfessionId == input.ProfessionId && x.CostType == input.CostType);

            if (entity == null)
            {
                return null;
            }
            else
            {
                return entity.MapTo<ChangeProfessionCostListDto>();
            }
        }
    }
}


