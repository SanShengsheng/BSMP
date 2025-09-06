using Abp.AutoMapper;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    [AutoMapFrom(typeof(Family))]
    public class GetFamiliesWithPlayerIdOutput
    {
        public int FamilyId { get; set; } 

        public GetFamiliesWithPlayerIdOutput()
        {
            this.Other = new OtherDto();

            this.Baby = new BabyDto();
        }

        public AddOnStatus AddOnStatus { get; set; }

        public OtherDto Other { get; set; }

        /// <summary>
        /// 家庭资产
        /// </summary>
        public double Deposit { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 月收入
        /// </summary>
        public double MonthlyIncome { get; set; }

        public double VirtualRecharge { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 档次
        /// </summary>
        public string Level { get; set; }



        /// <summary>
        /// 充值金额
        /// </summary>
        public double ChargeAmount { get; set; }

        public BabyDto Baby { get; set; }
    }

    [AutoMapFrom(typeof(Baby))]
    public class BabyDto
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public BabyState State { get; set; }
    }

    [AutoMapFrom(typeof(Player))]
    public class OtherDto
    {
        public Guid Id { get; set; }
        
        public string NickName { get; set; }

        public string HeadUrl { get; set; }
    }
}
