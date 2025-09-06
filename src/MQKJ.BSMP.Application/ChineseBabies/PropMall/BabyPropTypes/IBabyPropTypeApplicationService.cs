
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


using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyPropType应用层服务的接口方法
    ///</summary>
    public interface IBabyPropTypeAppService :  BsmpApplicationService<BabyPropType, int, BabyPropTypeEditDto, BabyPropTypeEditDto, GetBabyPropTypesInput, BabyPropTypeListDto>
    {
     
		/// <summary>
		/// 通过指定id获取BabyPropTypeListDto信息
		/// </summary>
		Task<List<BabyPropTypeListDto>> GetAll();

        Task<List<BabyPropTypeListDto>> GetAllAssetTypes();


    }
}
