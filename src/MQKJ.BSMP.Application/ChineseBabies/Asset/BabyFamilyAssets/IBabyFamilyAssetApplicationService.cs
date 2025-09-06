
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


using MQKJ.BSMP.ChineseBabies.Backpack.Dtos;
using MQKJ.BSMP.ChineseBabies.Backpack;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    /// <summary>
    /// BabyFamilyAsset应用层服务的接口方法
    ///</summary>
    public interface IBabyFamilyAssetAppService   : BsmpApplicationService<BabyFamilyAsset, Guid, BabyFamilyAssetEditDto, BabyFamilyAssetEditDto, GetBabyFamilyAssetsInput, BabyFamilyAssetListDto>
    {
        Task<GetBabyFamilyAssetByPageOutput> GetPage(GetBabyFamilyAssetsInput input);

        Task<PostChangeAssetOutput> PostChangeAsset(PostChangeAssetInput input);
        /// <summary>
        /// 重新计算家庭特性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> ReCalculateAssetFeatureAddition(ReCalculateAssetFeatureAdditionInput input);
        /// <summary>
        /// 继承道具属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task InheritFamilyAssetPropertyAddion(InheritFamilyAssetPropertyAddionInput input);
    }
}
