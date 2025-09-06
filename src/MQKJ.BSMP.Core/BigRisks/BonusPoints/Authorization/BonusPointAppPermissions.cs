namespace MQKJ.BSMP.BonusPoints.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="BonusPointAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class BonusPointAppPermissions
	{
		/// <summary>
		/// BonusPoint管理权限_自带查询授权
		/// </summary>
		public const string BonusPoint = "Pages.BonusPoint";

		/// <summary>
		/// BonusPoint创建权限
		/// </summary>
		public const string BonusPoint_CreateBonusPoint = "Pages.BonusPoint.CreateBonusPoint";

		/// <summary>
		/// BonusPoint修改权限
		/// </summary>
		public const string BonusPoint_EditBonusPoint = "Pages.BonusPoint.EditBonusPoint";

		/// <summary>
		/// BonusPoint删除权限
		/// </summary>
		public const string BonusPoint_DeleteBonusPoint = "Pages.BonusPoint.DeleteBonusPoint";

        /// <summary>
        /// BonusPoint批量删除权限
        /// </summary>
		public const string BonusPoint_BatchDeleteBonusPoints = "Pages.BonusPoint.BatchDeleteBonusPoints";

        // <summary>
        /// 查看所有
        /// </summary>
        public const string BonusPoint_AllBonusPoints = "Pages.BonusPoints.AllBonusPoints";

        //// custom codes 

        //// custom codes end
    }
	
}

