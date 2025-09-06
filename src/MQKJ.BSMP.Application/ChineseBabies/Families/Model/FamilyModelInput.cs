using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Model
{
    public class FamilyModelInput
    {
        public int? Page { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string BabyName { get; set; }
        public FamilyLevel FamilyLevel { get; set; }
        public RechargeRange RechargeLevel { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public FamilyModelOrder[] Orders { get; set; }
        public List<int> TenantIds { get; set; } = new List<int>() { 7, 295 };
    }
    public enum FamilyModelFieldType
    {
        /// <summary>
        /// 充值
        /// </summary>
        Recharge = 2,
        /// <summary>
        /// 资产
        /// </summary>
        Deposit = 1

    }
    public enum OrderType
    {
        DESC = 2,
        ASC = 1
    }
    public class FamilyModelOrder
    {
        public FamilyModelFieldType FieldType { get; set; }
        public OrderType OrderType { get; set; }

    }
}
