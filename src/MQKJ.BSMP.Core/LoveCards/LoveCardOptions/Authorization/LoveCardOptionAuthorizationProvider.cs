

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.LoveCardOptions.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="LoveCardOptionPermissions" /> for all permission names. LoveCardOption
    ///</summary>
    public class LoveCardOptionAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public LoveCardOptionAuthorizationProvider()
		{

		}

        public LoveCardOptionAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public LoveCardOptionAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了LoveCardOption 的权限。
            var pages = context.GetPermissionOrNull(LoveCardOptionPermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(LoveCardOptionPermissions.Node, L("LoveCardOption"));

            var entityPermission = pages.CreateChildPermission(LoveCardOptionPermissions.All_LoveCardOption, L("All_LoveCardOption"));
            entityPermission.CreateChildPermission(LoveCardOptionPermissions.Query, L("QueryLoveCardOption"));
			entityPermission.CreateChildPermission(LoveCardOptionPermissions.Create, L("CreateLoveCardOption"));
			entityPermission.CreateChildPermission(LoveCardOptionPermissions.Edit, L("EditLoveCardOption"));
			entityPermission.CreateChildPermission(LoveCardOptionPermissions.Delete, L("DeleteLoveCardOption"));
			entityPermission.CreateChildPermission(LoveCardOptionPermissions.BatchDelete, L("BatchDeleteLoveCardOption"));
			entityPermission.CreateChildPermission(LoveCardOptionPermissions.ExportExcel, L("ExportExcelLoveCardOption"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}