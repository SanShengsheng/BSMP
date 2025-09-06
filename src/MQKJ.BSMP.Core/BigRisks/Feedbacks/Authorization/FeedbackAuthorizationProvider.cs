

using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Feedbacks.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="FeedbackPermissions" /> for all permission names. Feedback
    ///</summary>
    public class FeedbackAuthorizationProvider : AuthorizationProvider
    {
		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了Feedback 的权限。
            var pages = context.GetPermissionOrNull(FeedbackPermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(FeedbackPermissions.Node, L("Feedback"));

			var entityPermission = pages.CreateChildPermission(FeedbackPermissions.All_Feedbacks , L("All_Feedbacks"));
			entityPermission.CreateChildPermission(FeedbackPermissions.Query, L("QueryFeedback"));
			entityPermission.CreateChildPermission(FeedbackPermissions.Create, L("CreateFeedback"));
			entityPermission.CreateChildPermission(FeedbackPermissions.Edit, L("EditFeedback"));
			entityPermission.CreateChildPermission(FeedbackPermissions.Delete, L("DeleteFeedback"));
			entityPermission.CreateChildPermission(FeedbackPermissions.BatchDelete, L("BatchDeleteFeedback"));
			entityPermission.CreateChildPermission(FeedbackPermissions.ExportExcel, L("ExportExcelFeedback"));
			 
		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}