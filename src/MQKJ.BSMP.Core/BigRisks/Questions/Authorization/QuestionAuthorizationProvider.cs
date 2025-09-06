using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Questions.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="QuestionAppPermissions"/> for all permission names.
    /// </summary>
    public class QuestionAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了Question 的权限。
            var pages = context.GetPermissionOrNull(QuestionAppPermissions.Question);
            if (pages == null)
                pages = context.CreatePermission(QuestionAppPermissions.Question, L("Question"));

            //Tasks
            var question = pages.CreateChildPermission(QuestionAppPermissions.Question_AllQuestions, L("AllQuestions"));
            question.CreateChildPermission(QuestionAppPermissions.Question_AuditQuestion, L("QAuditQuestion"));
           
            question.CreateChildPermission(QuestionAppPermissions.Question_CreateQuestion, L("CreateQuestion"));
            question.CreateChildPermission(QuestionAppPermissions.Question_EditQuestion, L("EditQuestion"));           
            question.CreateChildPermission(QuestionAppPermissions.Question_DeleteQuestion, L("DeleteQuestion"));
			question.CreateChildPermission(QuestionAppPermissions.Question_BatchDeleteQuestions , L("BatchDeleteQuestions"));


			
			//// custom codes 
			
			//// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}