namespace MQKJ.BSMP.Questions.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
	 /// <see cref="QuestionAppAuthorizationProvider"/>中对权限的定义.
	 /// </summary>
	public static  class QuestionAppPermissions
	{
		/// <summary>
		/// Question管理权限_自带查询授权
		/// </summary>
		public const string Question = "Pages.Question";

		/// <summary>
		/// Question创建权限
		/// </summary>
		public const string Question_CreateQuestion = "Pages.Question.CreateQuestion";

		/// <summary>
		/// Question修改权限
		/// </summary>
		public const string Question_EditQuestion = "Pages.Question.EditQuestion";

		/// <summary>
		/// Question删除权限
		/// </summary>
		public const string Question_DeleteQuestion = "Pages.Question.DeleteQuestion";

        /// <summary>
        /// Question批量删除权限
        /// </summary>
		public const string Question_BatchDeleteQuestions = "Pages.Question.BatchDeleteQuestions";



        //// custom codes 
	  
	    /// <summary>
	    /// 查看所有
	    /// </summary>
	    public const string Question_AllQuestions = "Pages.Question.AllQuestions";
	    /// <summary>
	    /// 审核（冻结与查看）
	    /// </summary>
	    public const string Question_AuditQuestion = "Pages.Question.AuditQuestion";
	  
        //// custom codes end
    }
	
}

