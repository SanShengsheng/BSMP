using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using System;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos
{
    public class GetParentDetailOutput
    {
        /// <summary>
        /// 宝宝基本
        /// </summary>
        public GetParentDetailOutputBasicInfo BasicInfo { get; set; }
        /// <summary>
        /// 职业信息
        /// </summary>
        public GetParentDetailOutputProfessionInfo ProfessionInfo { get; set; }

      
    }
    public class GetParentDetailOutputProfessionInfo
    {
        /// <summary>
        /// 职业
        /// </summary>
        public string ProfessionName { get; set; }

        /// <summary>
        /// 职业Id
        /// </summary>
        public int ProfessionId { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        public double Salary { get; set; }
        /// <summary>
        /// 职业属性加成
        /// </summary>
        public virtual GetParentDetailOutputBasicInfoPropertyAddition ProfessionPropertyAddition { get; set; }
    }
    public class GetParentDetailOutputBasicInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPicture { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

    
    }

    public class GetParentDetailOutputBasicInfoPropertyAddition : BabyPropertyDto
    {

    }
}
