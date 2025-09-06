using MQKJ.BSMP.Orders;
using System;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class CalIncomeRequest
    {
        public CalIncomeRequest(Order order, int familyId)
        {
            Order = order;
            FamilyId = familyId;
        }
        public Order Order { get; set; }
        public int FamilyId { get; set; }

    }
}