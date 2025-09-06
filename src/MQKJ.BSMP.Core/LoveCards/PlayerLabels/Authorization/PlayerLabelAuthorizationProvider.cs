

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.PlayerLabels.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="PlayerLabelPermissions" /> for all permission names. PlayerLabel
    ///</summary>
    public class PlayerLabelAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public PlayerLabelAuthorizationProvider()
		{

		}

        public PlayerLabelAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public PlayerLabelAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了PlayerLabel 的权限。
            var pages = context.GetPermissionOrNull(PlayerLabelPermissions.Node);
            if (pages == null)
                pages = context.CreatePermission(PlayerLabelPermissions.Node, L("Player"));

            var entityPermission = pages.CreateChildPermission(PlayerLabelPermissions.All_PlayerLabel, L("All_PlayerLabel"));
            entityPermission.CreateChildPermission(PlayerLabelPermissions.Query, L("QueryPlayerLabel"));
			entityPermission.CreateChildPermission(PlayerLabelPermissions.Create, L("CreatePlayerLabel"));
			entityPermission.CreateChildPermission(PlayerLabelPermissions.Edit, L("EditPlayerLabel"));
			entityPermission.CreateChildPermission(PlayerLabelPermissions.Delete, L("DeletePlayerLabel"));
			entityPermission.CreateChildPermission(PlayerLabelPermissions.BatchDelete, L("BatchDeletePlayerLabel"));
			entityPermission.CreateChildPermission(PlayerLabelPermissions.ExportExcel, L("ExportExcelPlayerLabel"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}