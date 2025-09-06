

namespace MQKJ.BSMP.ApplicationLogs.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="ApplicationLogAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class ApplicationLogPermissions
	{
		/// <summary>
		/// ApplicationLog权限节点
		///</summary>
		public const string Node = "Pages.ApplicationLog";

		/// <summary>
		/// ApplicationLog查询授权
		///</summary>
		public const string Query = "Pages.ApplicationLog.Query";

		/// <summary>
		/// ApplicationLog创建权限
		///</summary>
		public const string Create = "Pages.ApplicationLog.Create";

		/// <summary>
		/// ApplicationLog修改权限
		///</summary>
		public const string Edit = "Pages.ApplicationLog.Edit";

		/// <summary>
		/// ApplicationLog删除权限
		///</summary>
		public const string Delete = "Pages.ApplicationLog.Delete";

        /// <summary>
		/// ApplicationLog批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.ApplicationLog.BatchDelete";

		/// <summary>
		/// ApplicationLog导出Excel
		///</summary>
		public const string ExportExcel="Pages.ApplicationLog.ExportExcel";

		 
		 
         
    }

}

