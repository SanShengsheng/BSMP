using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos;
using System;
using static MQKJ.BSMP.ChineseBabies.Athetics.Competitions.CompetitionApplicationService;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetBabyBasicInfoOutput
    {
        /// <summary>
        /// 家庭
        /// </summary>
        public GetBabyBasicInfoOutputFamily Family { get; set; }
        /// <summary>
        /// 宝宝
        /// </summary>
        public GetBabyBasicInfoOutputBaby Baby { get; set; }

        public BabySystemSetting SystemSetting { get; set; }

        public EquipmentPropModel EquipmentProp { get; set; }
        /// <summary>
        /// 大礼包信息，暂时无用
        /// </summary>
        //public GetBabyBasicInfoOutputPropBagInfo BigGiftBag { get; set; } 
    }

    //public class GetBabyBasicInfoOutputPropBagInfo
    //{
    //    public bool IsShow { get; set; }

    //    public string Message { get; set; }
    //}

    public class GetBabyBasicInfoOutputBaby
    {

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        public string AgeString { get; set; }
        /// <summary>
        /// 宝宝属性
        /// </summary>
        public GetBabyBasicInfoOutputBabyProperty BabyProperty { get; set; }
        /// <summary>
        /// 改名所需花费的费用
        /// </summary>
        public double ChangeCost { get; set; } = 10000;

        public string HealthyTitle { get; set; }
        /// <summary>
        /// 出生顺序
        /// </summary>
        public int BirthOrder { get; set; }

        /// <summary>
        /// 段位
        /// </summary>
        public DanGrading DanGrading { get; set; }
        /// <summary>
        /// 宝宝皮肤
        /// </summary>
        public int Skin { get; set; }
    }

    public class GetBabyBasicInfoOutputBabyProperty : BabyPropertyDto
    {

    }

    public class GetBabyBasicInfoOutputFamily
    {
        public string DadName { get; set; }

        public string MomName { get; set; }
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 老爹头像
        /// </summary>
        public string DadHeadPicture { get; set; }
        /// <summary>
        /// 老妈头像
        /// </summary>
        public string MomHeadPicture { get; set; }
        /// <summary>
        /// 家庭存款
        /// </summary>
        public double Deposit { get; set; }
        public int Level { get; set; }
        public string HappinessTitle { get; set; }
        /// <summary>
        /// 爸爸编号
        /// </summary>
        public Guid DadGuid { get; set; }
        /// <summary>
        /// 妈妈编号
        /// </summary>
        public Guid MomGuid { get; set; }
    }

}