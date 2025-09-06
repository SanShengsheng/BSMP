namespace MQKJ.BSMP.TagTypes.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="TagTypeAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class TagTypeAppPermissions
	{
		/// <summary>
		/// TagType管理权限_自带查询授权
		/// </summary>
		public const string TagType = "Pages.TagType";

		/// <summary>
		/// TagType创建权限
		/// </summary>
		public const string TagType_CreateTagType = "Pages.TagType.CreateTagType";

		/// <summary>
		/// TagType修改权限
		/// </summary>
		public const string TagType_EditTagType = "Pages.TagType.EditTagType";

		/// <summary>
		/// TagType删除权限
		/// </summary>
		public const string TagType_DeleteTagType = "Pages.TagType.DeleteTagType";

        /// <summary>
        /// TagType批量删除权限
        /// </summary>
		public const string TagType_BatchDeleteTagTypes = "Pages.TagType.BatchDeleteTagTypes";


		
		//// custom codes 
		
        //// custom codes end
    }
	
}

