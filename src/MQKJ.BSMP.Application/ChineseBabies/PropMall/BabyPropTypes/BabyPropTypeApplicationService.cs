
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
    /// BabyPropType应用层服务的接口实现方法  
    ///</summary>
    public class BabyPropTypeAppService : BsmpApplicationServiceBase<BabyPropType, int, BabyPropTypeEditDto, BabyPropTypeEditDto, GetBabyPropTypesInput, BabyPropTypeListDto>, IBabyPropTypeAppService
    {
        private readonly IRepository<BabyPropType, int> _entityRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public BabyPropTypeAppService(
        IRepository<BabyPropType, int> entityRepository

        ) : base(entityRepository)
        {
            _entityRepository = entityRepository;

        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<BabyPropTypeListDto>> GetAll()
        {
            var response = await _entityRepository.GetAllListAsync();
            return ObjectMapper.Map<List<BabyPropTypeListDto>>(response);
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<BabyPropTypeListDto>> GetAllAssetTypes()
        {
            var result = await _entityRepository.GetAllListAsync();
            var response = ObjectMapper.Map<List<BabyPropTypeListDto>>(result);
            response.Add(new BabyPropTypeListDto()
            {
                Code = 0,
                Id = 0,
                Name = "Else",
                Sort = 99,
                Title = "其他"
            });
            return response;
        }
        internal override IQueryable<BabyPropType> GetQuery(GetBabyPropTypesInput model)
        {
            throw new NotImplementedException();
        }
    }
}


