

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Common.OperationActivities.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="OperationActivityPermissions" /> for all permission names. OperationActivity
    ///</summary>
    public class OperationActivityAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public OperationActivityAuthorizationProvider()
		{

		}

        public OperationActivityAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public OperationActivityAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			// 在这里配置了OperationActivity 的权限。
			var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

			var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

			var entityPermission = administration.CreateChildPermission(OperationActivityPermissions.Node , L("OperationActivity"));
			entityPermission.CreateChildPermission(OperationActivityPermissions.Query, L("QueryOperationActivity"));
			entityPermission.CreateChildPermission(OperationActivityPermissions.Create, L("CreateOperationActivity"));
			entityPermission.CreateChildPermission(OperationActivityPermissions.Edit, L("EditOperationActivity"));
			entityPermission.CreateChildPermission(OperationActivityPermissions.Delete, L("DeleteOperationActivity"));
			entityPermission.CreateChildPermission(OperationActivityPermissions.BatchDelete, L("BatchDeleteOperationActivity"));
			entityPermission.CreateChildPermission(OperationActivityPermissions.ExportExcel, L("ExportExcelOperationActivity"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}