using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos
{
    public class AgentFamilyOutput
    {
        public string OtherName { get; set; }
        public string OtherHeader { get; set; }
        public string BabyName { get; set; }
        public AddOnStatus AddOnStatus { get; set; }
        public string Status { get; set; }
        public double RealAmount { get; set; }
        public double VirtualAmount { get; set; }
        public string Level { get; set; }
        public string Note { get; set; }
        public int Id { get; set; }
        public double Deposit { get; set; }
        public int? BabyAge { get; set; }
        public int? BabyId { get; set; }
        //TODO: 添加显示描述性年龄
        public string AgeString { get; set; }
    }
}
