using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Models
{
    [AutoMapFrom(typeof(Order))]
    public class FamilyOrderModel
    {
        public Guid OrderId { get; set; }

        public DateTime CreationTime { get; set; }

        public OrderState State { get; set; }

        public int? FamilyId { get; set; }

        public FamilyOrderDto Family { get; set; }

        public bool IsDeleted { get; set; }
    }

    [AutoMapFrom(typeof(Family))]
    public class FamilyOrderDto
    {
        public int Id { get; set; }

        public DateTime? CreationTime { get; set; }

        public Guid FatherId { get; set; }

        public Guid MotherId { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AgentIncomeRecordModel
    {
        public FamilyOrderModel Order { get; set; }

        public int MqAgentId { get; set; }

        public Guid? OrderId { get; set; }

        public DateTime CreationTime { get; set; }

        public double Income { get; set; }
    }
}
