

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Common.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="EnterpirsePaymentRecordPermissions" /> for all permission names. EnterpirsePaymentRecord
    ///</summary>
    public class EnterpirsePaymentRecordAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public EnterpirsePaymentRecordAuthorizationProvider()
		{

		}

        public EnterpirsePaymentRecordAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public EnterpirsePaymentRecordAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			// 在这里配置了EnterpirsePaymentRecord 的权限。
			var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

			var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

			var entityPermission = administration.CreateChildPermission(EnterpirsePaymentRecordPermissions.Node , L("EnterpirsePaymentRecord"));
			entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.GetEnterpirsePaymentRecordPags, L("GetEnterpirsePaymentRecordPags"));
			entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.RefuseAuditeWithDrawMoney, L("RefuseAuditeWithDrawMoney"));
			entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent, L("WithDrawMoneyForAgent"));
			entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.ExportExcel, L("ExportExcel"));
            entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.ManualPass, L("ManualPass"));
            //entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.ExportExcel, L("ExportExcelEnterpirsePaymentRecord"));
            //entityPermission.CreateChildPermission(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent, L("WithDrawMoneyForAgent"));

        }

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}