

namespace MQKJ.BSMP.Friends.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="FriendAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class FriendPermissions
	{
		/// <summary>
		/// Friend权限节点
		///</summary>
		public const string Node = "Pages.Friend";

		/// <summary>
		/// Friend查询授权
		///</summary>
		public const string Query = "Pages.Friend.Query";

		/// <summary>
		/// Friend创建权限
		///</summary>
		public const string Create = "Pages.Friend.Create";

		/// <summary>
		/// Friend修改权限
		///</summary>
		public const string Edit = "Pages.Friend.Edit";

		/// <summary>
		/// Friend删除权限
		///</summary>
		public const string Delete = "Pages.Friend.Delete";

        /// <summary>
		/// Friend批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.Friend.BatchDelete";

		/// <summary>
		/// Friend导出Excel
		///</summary>
		public const string ExportExcel="Pages.Friend.ExportExcel";

        public const string All_Friends = "Pages.Friend.All_Friend";




    }

}

