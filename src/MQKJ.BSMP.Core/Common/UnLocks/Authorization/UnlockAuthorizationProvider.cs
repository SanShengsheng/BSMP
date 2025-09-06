

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.UnLocks.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="UnlockPermissions" /> for all permission names. Unlock
    ///</summary>
    public class UnlockAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public UnlockAuthorizationProvider()
		{

		}

        public UnlockAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public UnlockAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了Unlock 的权限。
            var pages = context.GetPermissionOrNull(UnlockPermissions.Node);
            if (pages == null)
                pages = context.CreatePermission(UnlockPermissions.Node, L("Unlock"));

            var entityPermission = pages.CreateChildPermission(UnlockPermissions.AllUnLocks, L("AllUnLocks"));
            entityPermission.CreateChildPermission(UnlockPermissions.Query, L("QueryUnlock"));
			entityPermission.CreateChildPermission(UnlockPermissions.Create, L("CreateUnlock"));
			entityPermission.CreateChildPermission(UnlockPermissions.Edit, L("EditUnlock"));
			entityPermission.CreateChildPermission(UnlockPermissions.Delete, L("DeleteUnlock"));
			entityPermission.CreateChildPermission(UnlockPermissions.BatchDelete, L("BatchDeleteUnlock"));
			entityPermission.CreateChildPermission(UnlockPermissions.ExportExcel, L("ExportExcelUnlock"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}