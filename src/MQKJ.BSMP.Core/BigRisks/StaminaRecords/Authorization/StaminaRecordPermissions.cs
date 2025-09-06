

namespace MQKJ.BSMP.StaminaRecords.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="StaminaRecordAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class StaminaRecordPermissions
	{
		/// <summary>
		/// StaminaRecord权限节点
		///</summary>
		public const string Node = "Pages.StaminaRecord";

		/// <summary>
		/// StaminaRecord查询授权
		///</summary>
		public const string Query = "Pages.StaminaRecord.Query";

		/// <summary>
		/// StaminaRecord创建权限
		///</summary>
		public const string Create = "Pages.StaminaRecord.Create";

		/// <summary>
		/// StaminaRecord修改权限
		///</summary>
		public const string Edit = "Pages.StaminaRecord.Edit";

		/// <summary>
		/// StaminaRecord删除权限
		///</summary>
		public const string Delete = "Pages.StaminaRecord.Delete";

        /// <summary>
		/// StaminaRecord批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.StaminaRecord.BatchDelete";

        public const string All_StaminaRecord = "Pages.All_StaminaRecord";

        /// <summary>
        /// StaminaRecord导出Excel
        ///</summary>
        public const string ExportExcel="Pages.StaminaRecord.ExportExcel";

		 
		 
         
    }

}

