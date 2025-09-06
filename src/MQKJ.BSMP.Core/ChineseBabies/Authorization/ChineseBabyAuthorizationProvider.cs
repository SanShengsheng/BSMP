using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using MQKJ.BSMP.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Authorization
{
    public class ChineseBabyAuthorizationProvider: AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public ChineseBabyAuthorizationProvider()
        {

        }

        public ChineseBabyAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public ChineseBabyAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            // 在这里配置了EnterpirsePaymentRecord 的权限。
            var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

            var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

            var entityPermission = administration.CreateChildPermission(ChinesePermissions.Node, L("Families"));
            entityPermission.CreateChildPermission(ChinesePermissions.GetAllFamilys, L("GetAllFamilys"));
            entityPermission.CreateChildPermission(ChinesePermissions.ExportFamiliesToExcel, L("ExportFamiliesToExcel"));
            entityPermission.CreateChildPermission(ChinesePermissions.SupplementCoinRecharge, L("SupplementCoinRecharge"));
            entityPermission.CreateChildPermission(ChinesePermissions.GetAllRunWaterRecords, L("GetAllRunWaterRecords"));
            entityPermission.CreateChildPermission(ChinesePermissions.ExportRunWaterRecordToExcel, L("ExportRunWaterRecordToExcel"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}
