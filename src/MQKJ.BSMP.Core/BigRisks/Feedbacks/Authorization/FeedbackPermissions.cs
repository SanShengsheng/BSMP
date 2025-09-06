

namespace MQKJ.BSMP.Feedbacks.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="FeedbackAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class FeedbackPermissions
	{
		/// <summary>
		/// Feedback权限节点
		///</summary>
		public const string Node = "Pages.Feedback";

		/// <summary>
		/// Feedback查询授权
		///</summary>
		public const string Query = "Pages.Feedback.Query";

		/// <summary>
		/// Feedback创建权限
		///</summary>
		public const string Create = "Pages.Feedback.Create";

		/// <summary>
		/// Feedback修改权限
		///</summary>
		public const string Edit = "Pages.Feedback.Edit";

		/// <summary>
		/// Feedback删除权限
		///</summary>
		public const string Delete = "Pages.Feedback.Delete";

        /// <summary>
		/// Feedback批量删除权限
		///</summary>
		public const string BatchDelete = "Pages.Feedback.BatchDelete";

		/// <summary>
		/// Feedback导出Excel
		///</summary>
		public const string ExportExcel="Pages.Feedback.ExportExcel";

        /// <summary>
        /// 所有
        ///</summary>
        public const string All_Feedbacks = "Pages.All_Feedback";


    }

}

