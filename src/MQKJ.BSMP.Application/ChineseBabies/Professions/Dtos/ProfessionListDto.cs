using Abp.AutoMapper;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapFrom(typeof(Profession))]
    public class ProfessionListDto : ISearchOutModel<Profession, int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



        /// <summary>
        /// Grade
        /// </summary>
        public int Grade { get; set; }



        /// <summary>
        /// Gender
        /// </summary>
        public Gender Gender { get; set; }



        /// <summary>
        /// Salary
        /// </summary>
        public double Salary { get; set; }



        /// <summary>
        /// ImagePath
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 是否解锁
        /// </summary>
        public bool IsUnLock { get; set; }

        public bool IsCurrent { get; set; }


        /// <summary>
        /// RewardId
        /// </summary>
        public int? RewardId { get; set; }

        public RewardListDto Reward { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }



        /// <summary>
        /// Costs
        /// </summary>
        public IEnumerable<ChangeProfessionCostListDto> Costs { get; set; }

        /// <summary>
        /// 幸福指数（家庭幸福度）
        /// </summary>
        public double SatisfactionDegree { get; set; }
        /// <summary>
        /// 是否是默认职业
        /// </summary>
        public bool IsDefault { get; set; }
       

    }
}