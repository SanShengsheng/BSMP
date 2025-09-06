namespace MQKJ.BSMP
{
    public enum BSMPErrorCodes : int
    {
        #region 养娃  300-600

        #region 宝宝相关 300-350
        /// <summary>
        /// 包含敏感词
        /// </summary>
        ContainSensitiveWord=300,
        /// <summary>
        /// 第一次取名只允许父亲
        /// </summary>
        FirstGiveNameOnlyAllowByFather=301,
        #endregion
        #region 家庭相关 350-400
        /// <summary>
        /// 已经创建过家庭
        /// </summary>
        AlreadyCreateFamily=350,
        /// <summary>
        /// 该家庭不存在
        /// </summary>
        DontExsitFamily = 351,
        /// <summary>
        /// 最后一个宝宝尚未成人
        /// </summary>
        LastBabyNotYetAdult=352,
        #endregion
        #endregion
    }
}
