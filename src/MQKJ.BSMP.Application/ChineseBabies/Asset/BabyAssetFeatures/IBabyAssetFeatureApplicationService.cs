using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures
{
    public interface IBabyAssetFeatureApplicationService:IApplicationService
    {
        /// <summary>
        /// 获取道具特性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AssetFeatureDto> GetAssetFeature(AssetFeatureWorkInput  input);
    }
}
