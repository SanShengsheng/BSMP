

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.LoveCards.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="LoveCardPermissions" /> for all permission names. LoveCard
    ///</summary>
    public class LoveCardAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public LoveCardAuthorizationProvider()
		{

		}

        public LoveCardAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public LoveCardAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了LoveCard 的权限。
            var pages = context.GetPermissionOrNull(LoveCardPermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(LoveCardPermissions.Node, L("LoveCard"));

            var entityPermission = pages.CreateChildPermission(LoveCardPermissions.All_LoveCard, L("All_LoveCard"));
            entityPermission.CreateChildPermission(LoveCardPermissions.Query, L("QueryLoveCard"));
			entityPermission.CreateChildPermission(LoveCardPermissions.Create, L("CreateLoveCard"));
			entityPermission.CreateChildPermission(LoveCardPermissions.Edit, L("EditLoveCard"));
			entityPermission.CreateChildPermission(LoveCardPermissions.Delete, L("DeleteLoveCard"));
			entityPermission.CreateChildPermission(LoveCardPermissions.BatchDelete, L("BatchDeleteLoveCard"));
			entityPermission.CreateChildPermission(LoveCardPermissions.ExportExcel, L("ExportExcelLoveCard"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}