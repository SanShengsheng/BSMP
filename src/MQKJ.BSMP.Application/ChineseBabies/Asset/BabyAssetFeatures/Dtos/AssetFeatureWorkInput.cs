namespace MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures
{
    public class AssetFeatureWorkInput
    {
        public int FamilyId { get; set; }
        /// <summary>
        /// 请求操作的事件类型，study、growUp和growUpAndStudy
        /// </summary>
        public EventAdditionType EventType { get; set; }

        public int BabyId { get; set; }
    }
}