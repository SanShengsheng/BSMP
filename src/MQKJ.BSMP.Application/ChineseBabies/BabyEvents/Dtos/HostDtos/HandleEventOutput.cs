namespace MQKJ.BSMP.ChineseBabies
{
    public class HandleEventOutput
    {
        /// <summary>
        /// 当前事件处理后的状态
        /// </summary>
        public EventRecordState State { get; set; }
        /// <summary>
        /// 触发事件编号
        /// </summary>
        public int TriggerEventId { get; set; }

        public bool IsEnding { get; set; }
        /// <summary>
        /// 结束时间戳
        /// </summary>
        public long? EndTimeStamp { get; set; }

        public  HandleEventOutputBabyStory StroyEnding{ get; set; }
    }
    public class HandleEventOutputBabyStory{

        public string Name { get; set; }

        public string Description { get; set; }
    }
}