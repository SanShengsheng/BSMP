

namespace MQKJ.BSMP.UnLocks.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="UnlockAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class UnlockPermissions
	{
		/// <summary>
		/// Unlock权限节点
		///</summary>
		public const string Node = "Pages.Unlock";

		/// <summary>
		/// Unlock查询授权
		///</summary>
		public const string Query = "Pages.Unlock.Query";

		/// <summary>
		/// Unlock创建权限
		///</summary>
		public const string Create = "Pages.Unlock.Create";

		/// <summary>
		/// Unlock修改权限
		///</summary>
		public const string Edit = "Pages.Unlock.Edit";

		/// <summary>
		/// Unlock删除权限
		///</summary>
		public const string Delete = "Pages.Unlock.Delete";

        /// <summary>
		/// Unlock批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.Unlock.BatchDelete";

		/// <summary>
		/// Unlock导出Excel
		///</summary>
		public const string ExportExcel="Pages.Unlock.ExportExcel";

        public const string AllUnLocks = "Pages.Unlock.AllUnLocks";
		 
         
    }

}

