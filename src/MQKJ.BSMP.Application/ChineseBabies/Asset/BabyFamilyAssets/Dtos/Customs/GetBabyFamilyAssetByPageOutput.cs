using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    public class GetBabyFamilyAssetByPageOutput
    {
        /// <summary>
        /// 家庭基本信息
        /// </summary>
        public GetBabyFamilyAssetByPageOutput_FamilyBasicInfo FamilyBasicInfo { get; set; }

        public PagedResultDto<GetBabyFamilyAssetByPageOutputAsset> Assets { get; set; }
        /// <summary>
        /// 从事件中获得的道具
        /// </summary>
        public PagedResultDto<GetBabyFamilyAssetByPageOutputEventAsset> EventAssets { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutput_FamilyBasicInfo
    {
        /// <summary>
        /// 存款
        /// </summary>
        public double Deposit { get; set; }
    }
    /// <summary>
    /// 从事件中获得的道具
    /// </summary>
    public class GetBabyFamilyAssetByPageOutputEventAsset
    {
        public string Title { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutputAsset
    {
        public GetBabyFamilyAssetByPageOutputBasicInfo BasicInfo { get; set; }

        public GetBabyFamilyAssetByPageOutputDetail Detail { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutputDetail
    {
        public GetBabyFamilyAssetByPageOutputDetailPropInfo PropInfo { get; set; }
        /// <summary>
        /// 条件集合
        /// </summary>
        public ICollection<GetBabyFamilyAssetByPageOutputDetailTerm> Terms { get; set; }
        /// <summary>
        /// 功能集合
        /// </summary>
        public ICollection<GetBabyFamilyAssetByPageOutputDetailFeature> Features { get; set; }

        public GetBabyFamilyAssetByPageOutputDetailPropertyAddition PropertyAddition { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutputDetailPropertyAddition
    {
        /// <summary>
        /// 事件加成方式
        /// </summary>
        public EventAdditionType EventAdditionType { get; set; }
        /// <summary>
        /// 智力
        /// </summary>
        public virtual int Intelligence { get; set; }
        /// <summary>
        /// 体魄
        /// </summary>
        public virtual int Physique { get; set; }
        /// <summary>
        /// 想象
        /// </summary>
        public virtual int Imagine { get; set; }
        /// <summary>
        /// 意志
        /// </summary>
        public virtual int WillPower { get; set; }
        /// <summary>
        /// 情商
        /// </summary>
        public virtual int EmotionQuotient { get; set; }
        /// <summary>
        /// 魅力
        /// </summary>
        public virtual int Charm { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutputDetailPropInfo
    {
        public int Id { get; set; }

        public int Code { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// CoverImg
        /// </summary>
        public string CoverImg { get; set; }

        /// <summary>
        /// 装备对象
        /// </summary>
        public EquipmentAbleObject EquipmentAbleObject { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutputDetailFeature
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// 特性类别
        /// </summary>
        public FeatureType Type { get; set; }
    }

    public class GetBabyFamilyAssetByPageOutputDetailTerm
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public int Id { get; set; }

      
    }

    public class GetBabyFamilyAssetByPageOutputBasicInfo
    {
        public Guid Id { get; set; }

        public bool IsEquipmenting { get; set; }
     
        public string ExpiredDateTitle { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDateTime { get; set; }
    }

  
}