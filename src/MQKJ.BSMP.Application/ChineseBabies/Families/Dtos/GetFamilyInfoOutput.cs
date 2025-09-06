using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    [AutoMapFrom(typeof(Family))]
    public class GetFamilyInfoOutput
    {

        public GetFamilyInfoOutput()
        {
            this.Baby = new BabyDto();

            this.Father = new ParentPlayerDto();
        }

        /// <summary>
        /// Deposit
        /// </summary>
        public double Deposit { get; set; }



        /// <summary>
        /// Happiness
        /// </summary>
        public double Happiness { get; set; }



        /// <summary>
        /// Type
        /// </summary>
        public int Type { get; set; }

        public double VirtualRecharge { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



        /// <summary>
        /// ChargeAmount
        /// </summary>
        public double ChargeAmount { get; set; }

        public ParentPlayerDto Father { get; set; }



        public BabyDto Baby { get; set; }

        //public ParentPlayerDto Mother { get; set; }
    

    }


    [AutoMapFrom(typeof(Player))]
    public class ParentPlayerDto
    {
        public Guid Id { get; set; }

        //public int TenantId { get; set; }

        public string NickName { get; set; }

        public string HeadUrl { get; set; }
    }
}
