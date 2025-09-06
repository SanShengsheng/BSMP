namespace MQKJ.BSMP.BonusPointRecords.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="BonusPointRecordAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class BonusPointRecordAppPermissions
	{
		/// <summary>
		/// BonusPointRecord管理权限_自带查询授权
		/// </summary>
		public const string BonusPointRecord = "Pages.BonusPointRecord";

		/// <summary>
		/// BonusPointRecord创建权限
		/// </summary>
		public const string BonusPointRecord_CreateBonusPointRecord = "Pages.BonusPointRecord.CreateBonusPointRecord";

		/// <summary>
		/// BonusPointRecord修改权限
		/// </summary>
		public const string BonusPointRecord_EditBonusPointRecord = "Pages.BonusPointRecord.EditBonusPointRecord";

		/// <summary>
		/// BonusPointRecord删除权限
		/// </summary>
		public const string BonusPointRecord_DeleteBonusPointRecord = "Pages.BonusPointRecord.DeleteBonusPointRecord";

        /// <summary>
        /// BonusPointRecord批量删除权限
        /// </summary>
		public const string BonusPointRecord_BatchDeleteBonusPointRecords = "Pages.BonusPointRecord.BatchDeleteBonusPointRecords";

        /// <summary>
	    /// 查看所有
	    /// </summary>
	    public const string BonusPointRecord_AllBonusPoints = "Pages.BonusPointRecord.AllBonusPointsRecord";

        //// custom codes 

        //// custom codes end
    }
	
}

