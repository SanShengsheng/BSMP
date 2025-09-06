
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
    /// Reward应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class RewardAppService : BsmpApplicationServiceBase<Reward, int, RewardEditDto, RewardEditDto, GetRewardsInput, RewardListDto>, IRewardAppService
    {

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public RewardAppService(IRepository<Reward, int> entityRepository):base(entityRepository)
        {
            
            
        }

        internal override IQueryable<Reward> GetQuery(GetRewardsInput model)
        {
            return _repository.GetAll();
        }
    }
}


