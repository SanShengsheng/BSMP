namespace MQKJ.BSMP.SceneFiles.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="SceneFileAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class SceneFileAppPermissions
	{
		/// <summary>
		/// SceneFile管理权限_自带查询授权
		/// </summary>
		public const string SceneFile = "Pages.SceneFile";

		/// <summary>
		/// SceneFile创建权限
		/// </summary>
		public const string SceneFile_CreateSceneFile = "Pages.SceneFile.CreateSceneFile";

		/// <summary>
		/// SceneFile修改权限
		/// </summary>
		public const string SceneFile_EditSceneFile = "Pages.SceneFile.EditSceneFile";

		/// <summary>
		/// SceneFile删除权限
		/// </summary>
		public const string SceneFile_DeleteSceneFile = "Pages.SceneFile.DeleteSceneFile";

        /// <summary>
        /// SceneFile批量删除权限
        /// </summary>
		public const string SceneFile_BatchDeleteSceneFiles = "Pages.SceneFile.BatchDeleteSceneFiles";

        /// <summary>
        /// 获取所有
        /// </summary>
		public const string SceneFile_All_SceneFiles = "Pages.SceneFile.SceneFile_All_SceneFiles";



        //// custom codes 

        //// custom codes end
    }
	
}

