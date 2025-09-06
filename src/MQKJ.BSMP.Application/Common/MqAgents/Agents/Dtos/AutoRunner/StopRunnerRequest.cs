using System;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner
{
    public class StopRunnerRequest
    {
        public int FamilyId { get; set; }
        /// <summary>
        /// 停止运行理由
        /// </summary>
        public string Reason { get; set; }
        public int? BabyId { get; set; }
        public Guid PlayerId { get; set; }

        public bool Retry { get; set; } = true;
    }
}
