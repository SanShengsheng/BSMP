namespace MQKJ.BSMP.ChineseBabies
{
    public class CrontabUpdateFamilyLevelInput
    {
        /// <summary>
        /// CronExpression
        /// https://abunchofutils.com/u/computing/cron-format-helper/%EF%BC%88/
        /// </summary>
        public string CronExpression { get; set; }

        public bool IsReset { get; set; } = false;
    }
}