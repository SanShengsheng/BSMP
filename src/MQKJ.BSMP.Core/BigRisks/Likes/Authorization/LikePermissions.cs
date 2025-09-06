

namespace MQKJ.BSMP.Likes.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="LikeAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class LikePermissions
	{
		/// <summary>
		/// Like权限节点
		///</summary>
		public const string Node = "Pages.Like";

		/// <summary>
		/// Like查询授权
		///</summary>
		public const string Query = "Pages.Like.Query";

		/// <summary>
		/// Like创建权限
		///</summary>
		public const string Create = "Pages.Like.Create";

		/// <summary>
		/// Like修改权限
		///</summary>
		public const string Edit = "Pages.Like.Edit";

		/// <summary>
		/// Like删除权限
		///</summary>
		public const string Delete = "Pages.Like.Delete";

        /// <summary>
		/// Like批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.Like.BatchDelete";

		/// <summary>
		/// Like导出Excel
		///</summary>
		public const string ExportExcel="Pages.Like.ExportExcel";

        /// <summary>
		/// 所有
		///</summary>
		public const string All_Likes = "Pages.All_Like";


    }

}

