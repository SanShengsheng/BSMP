namespace MQKJ.BSMP.GameTasks.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="GameTaskAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class GameTaskAppPermissions
	{
		/// <summary>
		/// GameTask管理权限_自带查询授权
		/// </summary>
		public const string GameTask = "Pages.GameTask";

		/// <summary>
		/// GameTask创建权限
		/// </summary>
		public const string GameTask_CreateGameTask = "Pages.GameTask.CreateGameTask";

		/// <summary>
		/// GameTask修改权限
		/// </summary>
		public const string GameTask_EditGameTask = "Pages.GameTask.EditGameTask";

		/// <summary>
		/// GameTask删除权限
		/// </summary>
		public const string GameTask_DeleteGameTask = "Pages.GameTask.DeleteGameTask";

        /// <summary>
        /// GameTask批量删除权限
        /// </summary>
		public const string GameTask_BatchDeleteGameTasks = "Pages.GameTask.BatchDeleteGameTasks";

        // <summary>
        /// 查看所有
        /// </summary>
        public const string GameTasks_AllGameTasks = "Pages.GameTasks_AllGameTasks";
        //// custom codes 

        //// custom codes end
    }
	
}

