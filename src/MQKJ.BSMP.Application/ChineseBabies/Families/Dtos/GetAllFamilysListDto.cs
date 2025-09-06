using Abp.AutoMapper;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    [AutoMapFrom(typeof(Family))]
    public class GetAllFamilysListDto
    {
        public int Id { get; set; }
        public ParentPlayerDto Father { get; set; }
        public ParentPlayerDto Mother { get; set; }
        public double Deposit { get; set; }

        public List<Baby> Babies { get; set; }

        public double Happiness { get; set; }
        public FamilyLevel FamilyLevel { get; set; }
        public double ChargeAmount { get; set; }

        public DateTime CreationTime { get; set; }

        public double TotalSubsidyAmount { get; set; }
    }
}
