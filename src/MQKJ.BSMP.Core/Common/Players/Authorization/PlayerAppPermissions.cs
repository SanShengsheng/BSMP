namespace MQKJ.BSMP.Players.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="PlayerAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class PlayerAppPermissions
	{
		/// <summary>
		/// Player管理权限_自带查询授权
		/// </summary>
		public const string Player = "Pages.Player";

		/// <summary>
		/// Player创建权限
		/// </summary>
		public const string Player_CreatePlayer = "Pages.Player.CreatePlayer";

		/// <summary>
		/// Player修改权限
		/// </summary>
		public const string Player_EditPlayer = "Pages.Player.EditPlayer";

		/// <summary>
		/// Player删除权限
		/// </summary>
		public const string Player_DeletePlayer = "Pages.Player.DeletePlayer";

        /// <summary>
        /// Player批量删除权限
        /// </summary>
		public const string Player_BatchDeletePlayers = "Pages.Player.BatchDeletePlayers";

        // <summary>
        /// 查看所有
        /// </summary>
        public const string Player_AllPlayers = "Pages.Player_AllPlayers";



        //// custom codes 

        //// custom codes end
    }
	
}

