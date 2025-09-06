

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.StaminaRecords.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="StaminaRecordPermissions" /> for all permission names. StaminaRecord
    ///</summary>
    public class StaminaRecordAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public StaminaRecordAuthorizationProvider()
		{

		}

        public StaminaRecordAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public StaminaRecordAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了StaminaRecord 的权限。
            var pages = context.GetPermissionOrNull(StaminaRecordPermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(StaminaRecordPermissions.Node, L("StaminaRecord"));

            var entityPermission = pages.CreateChildPermission(StaminaRecordPermissions.All_StaminaRecord, L("All_StaminaRecord"));
            entityPermission.CreateChildPermission(StaminaRecordPermissions.Query, L("QueryStaminaRecord"));
			entityPermission.CreateChildPermission(StaminaRecordPermissions.Create, L("CreateStaminaRecord"));
			entityPermission.CreateChildPermission(StaminaRecordPermissions.Edit, L("EditStaminaRecord"));
			entityPermission.CreateChildPermission(StaminaRecordPermissions.Delete, L("DeleteStaminaRecord"));
			entityPermission.CreateChildPermission(StaminaRecordPermissions.BatchDelete, L("BatchDeleteStaminaRecord"));
			entityPermission.CreateChildPermission(StaminaRecordPermissions.ExportExcel, L("ExportExcelStaminaRecord"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}