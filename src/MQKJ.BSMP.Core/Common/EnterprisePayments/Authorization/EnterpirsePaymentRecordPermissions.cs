

namespace MQKJ.BSMP.Common.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="EnterpirsePaymentRecordAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class EnterpirsePaymentRecordPermissions
	{
		/// <summary>
		/// EnterpirsePaymentRecord权限节点
		///</summary>
		public const string Node = "Pages.EnterpirsePaymentRecord";

        /// <summary>
        /// 拒绝提现
        /// </summary>
		public const string RefuseAuditeWithDrawMoney = "Pages.RefuseAuditeWithDrawMoney";

        /// <summary>
        /// 获取所有提现记录数据
        /// </summary>
		public const string GetEnterpirsePaymentRecordPags = "Pages.GetEnterpirsePaymentRecordPags";

		///// <summary>
		///// EnterpirsePaymentRecord查询授权
		/////</summary>
		//public const string Query = "Pages.EnterpirsePaymentRecord.Query";

		///// <summary>
		///// EnterpirsePaymentRecord创建权限
		/////</summary>
		//public const string Create = "Pages.EnterpirsePaymentRecord.Create";

		///// <summary>
		///// EnterpirsePaymentRecord修改权限
		/////</summary>
		//public const string Edit = "Pages.EnterpirsePaymentRecord.Edit";

		///// <summary>
		///// EnterpirsePaymentRecord删除权限
		/////</summary>
		//public const string Delete = "Pages.EnterpirsePaymentRecord.Delete";

  //      /// <summary>
		///// EnterpirsePaymentRecord批量删除权限
		/////</summary>
		//public const string BatchDelete = "Pages.EnterpirsePaymentRecord.BatchDelete";

		/// <summary>
		/// EnterpirsePaymentRecord导出Excel
		///</summary>
		public const string ExportExcel= "Pages.EnterpirsePaymentRecord.ExportExcel";

        public const string WithDrawMoneyForAgent = "WithDrawMoneyForAgent";

        /// <summary>
        /// 手动通过
        /// </summary>
        public const string ManualPass = "ManualPass";



    }

}

