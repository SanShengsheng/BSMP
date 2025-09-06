

namespace MQKJ.BSMP.Orders.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="OrderAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class OrderPermissions
	{
		/// <summary>
		/// Order权限节点
		///</summary>
		public const string Node = "Pages.Order";


        /// <summary>
        /// Order查询授权
        ///</summary>
        public const string QueryOrders = "Pages.Order.QueryOrders";

        /// <summary>
        /// Order查询授权
        ///</summary>
        public const string QueryOrderState = "Pages.Order.QueryOrderState";

        ///// <summary>
        ///// Order创建权限
        /////</summary>
        //public const string Create = "Pages.Order.Create";

        ///// <summary>
        ///// Order修改权限
        /////</summary>
        //public const string Edit = "Pages.Order.Edit";

        ///// <summary>
        ///// Order删除权限
        /////</summary>
        //public const string Delete = "Pages.Order.Delete";

        //      /// <summary>
        ///// Order批量删除权限
        /////</summary>
        //public const string BatchDelete = "Pages.Order.BatchDelete";

        /// <summary>
        /// Order导出Excel
        ///</summary>
        public const string ExportToExcel = "Pages.Order.ExportToExcel";

        //      // <summary>
        //      /// 查看所有
        //      /// </summary>
        //      public const string AllOrders = "Pages.Player_AllOrders";


    }

}

