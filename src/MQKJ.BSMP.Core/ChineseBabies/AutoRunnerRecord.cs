using MQKJ.BSMP.Players;
using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class AutoRunnerRecord : ChineseBabyEntityGuidBase
    {
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
        public int GroupId { get; set; }
        public ActionType ActionType { get; set; }
        public string RelateionId { get; set; }
        public string OriginalData { get; set; }
        public string NewData { get; set; }
        public string Description { get; set; }
        public EventGroup Group { get; set; }
        public Player Player { get; set; }
        public Family Family { get; set; }
        public int? BabyId { get; set; }
    }

    public enum ActionType
    {
        /// <summary>
        /// 转职
        /// </summary>
        ChangeProfression = 1,
        /// <summary>
        /// 成长事件
        /// </summary>
        GrowUp = 2,
        /// <summary>
        /// 学习事件
        /// </summary>
        Study = 3,
        /// <summary>
        /// 开始外挂
        /// </summary>
        StartAuto = 4,
        /// <summary>
        /// 结束外挂
        /// </summary>
        StopAuto = 5,
        /// <summary>
        /// 完成外挂
        /// </summary>
        FinishAuto = 6,
        /// <summary>
        /// 购买精力
        /// </summary>
        BuyEnergy = 7,
        /// <summary>
        /// 成长到下一组
        /// </summary>
        GrowupToNextGroup = 8,
        /// <summary>
        /// 宝宝成人
        /// </summary>
        Ending = 99
    }
}
