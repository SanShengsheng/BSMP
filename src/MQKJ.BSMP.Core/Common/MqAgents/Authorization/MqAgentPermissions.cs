

namespace MQKJ.BSMP.Authorization
{
	/// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="MqAgentAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class MqAgentPermissions
	{
		/// <summary>
		/// MqAgent权限节点
		///</summary>
		public const string Node = "Pages.MqAgent";

		/// <summary>
		/// MqAgent查询授权
		///</summary>
		public const string GetAgents = "Pages.MqAgent.GetAgents";

        /// <summary>
        /// 虚拟充值
        /// </summary>
		public const string ToggleVtlRhgPerm = "Pages.MqAgent.ToggleVtlRhgPerm";

		/// <summary>
		/// 修改代理收益比例
		///</summary>
		public const string UpdateAgentRatio = "Pages.MqAgent.UpdateAgentRatio";

        /// <summary>
        /// 修改推广收益比例
        ///</summary>
        public const string UpdatePromoterRatio = "Pages.MqAgent.UpdatePromoterRatio";

		/// <summary>
		/// 修改代理状态
		///</summary>
		public const string UpdateAgentState = "Pages.MqAgent.UpdateAgentState";

        /// <summary>
		/// MqAgent批量删除权限
		///</summary>
		//public const string BatchDelete = "Pages.MqAgent.BatchDelete";

		/// <summary>
		/// 代理业界导出
		///</summary>
		public const string ExportExcel= "Pages.AgentIncome.ExportAgentIncomesToExcel";

        /// <summary>
        /// 获取代理业绩数据
        /// </summary>
        public const string GetAgentIncomes = "Pages.AgentIncome.GetAgentIncomes";

        /// <summary>
        /// 邀请码管理
        /// </summary>
        public const string AgentInviteCodeNode = "Pages.AgentInviteCode";

        /// <summary>
        /// 获取代理邀请码数据
        /// </summary>
        public const string GetAgentInviteCodes = "Pages.AgentIncome.GetAgentInviteCodes";
        
        /// <summary>
        /// 创建代理邀请码
        /// </summary>
        public const string CreateAgentInviteCode = "Pages.AgentIncome.CreateAgentInviteCode";

    }

}

