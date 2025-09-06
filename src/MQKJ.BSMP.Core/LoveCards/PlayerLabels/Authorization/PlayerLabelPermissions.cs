

namespace MQKJ.BSMP.PlayerLabels.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="PlayerLabelAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class PlayerLabelPermissions
	{
		/// <summary>
		/// PlayerLabel权限节点
		///</summary>
		public const string Node = "Pages.PlayerLabel";

		/// <summary>
		/// PlayerLabel查询授权
		///</summary>
		public const string Query = "Pages.PlayerLabel.Query";

		/// <summary>
		/// PlayerLabel创建权限
		///</summary>
		public const string Create = "Pages.PlayerLabel.Create";

		/// <summary>
		/// PlayerLabel修改权限
		///</summary>
		public const string Edit = "Pages.PlayerLabel.Edit";

		/// <summary>
		/// PlayerLabel删除权限
		///</summary>
		public const string Delete = "Pages.PlayerLabel.Delete";

        /// <summary>
		/// PlayerLabel批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.PlayerLabel.BatchDelete";

		/// <summary>
		/// PlayerLabel导出Excel
		///</summary>
		public const string ExportExcel="Pages.PlayerLabel.ExportExcel";

        /// <summary>
        /// 所有
        /// </summary>
        public const string All_PlayerLabel = "Pages.PlayerLabel.All_PlayerLabel";




    }

}

