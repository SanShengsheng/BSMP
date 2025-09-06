

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.ChineseBabies.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="VersionManagePermissions" /> for all permission names. VersionManage
    ///</summary>
    public class VersionManageAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public VersionManageAuthorizationProvider()
		{

		}

        public VersionManageAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public VersionManageAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			// 在这里配置了VersionManage 的权限。
			var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

			var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

			var entityPermission = administration.CreateChildPermission(VersionManagePermissions.Node , L("VersionManage"));
			entityPermission.CreateChildPermission(VersionManagePermissions.Query, L("QueryVersionManage"));
			entityPermission.CreateChildPermission(VersionManagePermissions.Create, L("CreateVersionManage"));
			entityPermission.CreateChildPermission(VersionManagePermissions.Edit, L("EditVersionManage"));
			entityPermission.CreateChildPermission(VersionManagePermissions.Delete, L("DeleteVersionManage"));
			entityPermission.CreateChildPermission(VersionManagePermissions.BatchDelete, L("BatchDeleteVersionManage"));
			entityPermission.CreateChildPermission(VersionManagePermissions.ExportExcel, L("ExportExcelVersionManage"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}