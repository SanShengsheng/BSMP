using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos;
using System;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 获取家庭信息输出
    ///https://lanhuapp.com/web/#/item/board/proto/edit/entry?pid=38cbffb8-30a0-474e-b59c-6b06330d6e0f&imgId=f5e06c23-e232-4bce-ad62-1c7ca9220a9d&from=img
    /// </summary>
    public class GetFamilyOutput2
    {

        /// <summary>
        /// 幸福度
        /// </summary>
        public double Happiness { get; set; }
        /// <summary>
        /// 幸福度名字
        /// </summary>
        public string HappinessTitle { get; set; }
        /// <summary>
        /// 存款
        /// </summary>
        public double Deposit { get; set; }

        public FamilyLevel Level { get; set; }
    
        public BabySystemSetting SystemSetting { get; set; }

        public GetFamilyOutputBabyInfo2 BabyInfo { get; set; }
    }

    public class GetFamilyOutputBabyInfo2
    {
        /// <summary>
        /// 成年宝宝数量
        /// </summary>
        public int AdultBabiesCount { get; set; }
    }
    
}