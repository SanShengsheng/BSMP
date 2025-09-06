namespace MQKJ.BSMP.Scenes.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="SceneAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class SceneAppPermissions
	{
		/// <summary>
		/// Scene管理权限_自带查询授权
		/// </summary>
		public const string Scene = "Pages.Scene";

		/// <summary>
		/// Scene创建权限
		/// </summary>
		public const string Scene_CreateScene = "Pages.Scene.CreateScene";

		/// <summary>
		/// Scene修改权限
		/// </summary>
		public const string Scene_EditScene = "Pages.Scene.EditScene";

		/// <summary>
		/// Scene删除权限
		/// </summary>
		public const string Scene_DeleteScene = "Pages.Scene.DeleteScene";

        /// <summary>
        /// Scene批量删除权限
        /// </summary>
		public const string Scene_BatchDeleteScenes = "Pages.Scene.BatchDeleteScenes";

        // <summary>
        /// 查看所有
        /// </summary>
        public const string Scene_AllScenes = "Pages.Scene_AllScenes";

        //// custom codes 

        //// custom codes end
    }
	
}

