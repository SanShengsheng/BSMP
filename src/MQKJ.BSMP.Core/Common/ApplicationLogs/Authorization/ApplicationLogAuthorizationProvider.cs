

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.ApplicationLogs.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="ApplicationLogPermissions" /> for all permission names. ApplicationLog
    ///</summary>
    public class ApplicationLogAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public ApplicationLogAuthorizationProvider()
		{

		}

        public ApplicationLogAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public ApplicationLogAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			// 在这里配置了ApplicationLog 的权限。
			//var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

			//var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

			//var entityPermission = administration.CreateChildPermission(ApplicationLogPermissions.Node , L("ApplicationLog"));
			//entityPermission.CreateChildPermission(ApplicationLogPermissions.Query, L("QueryApplicationLog"));
			//entityPermission.CreateChildPermission(ApplicationLogPermissions.Create, L("CreateApplicationLog"));
			//entityPermission.CreateChildPermission(ApplicationLogPermissions.Edit, L("EditApplicationLog"));
			//entityPermission.CreateChildPermission(ApplicationLogPermissions.Delete, L("DeleteApplicationLog"));
			//entityPermission.CreateChildPermission(ApplicationLogPermissions.BatchDelete, L("BatchDeleteApplicationLog"));
			//entityPermission.CreateChildPermission(ApplicationLogPermissions.ExportExcel, L("ExportExcelApplicationLog"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}