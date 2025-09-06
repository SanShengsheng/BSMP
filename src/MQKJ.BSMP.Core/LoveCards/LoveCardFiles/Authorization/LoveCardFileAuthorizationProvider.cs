

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.LoveCardFiles.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="LoveCardFilePermissions" /> for all permission names. LoveCardFile
    ///</summary>
    public class LoveCardFileAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public LoveCardFileAuthorizationProvider()
		{

		}

        public LoveCardFileAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public LoveCardFileAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了LoveCardFile 的权限。
            var pages = context.GetPermissionOrNull(LoveCardFilePermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(LoveCardFilePermissions.Node, L("LoveCardFile"));

            var entityPermission = pages.CreateChildPermission(LoveCardFilePermissions.All_Card_File, L("All_Card_File"));

			entityPermission.CreateChildPermission(LoveCardFilePermissions.Query, L("QueryLoveCardFile"));
			entityPermission.CreateChildPermission(LoveCardFilePermissions.Create, L("CreateLoveCardFile"));
			entityPermission.CreateChildPermission(LoveCardFilePermissions.Edit, L("EditLoveCardFile"));
			entityPermission.CreateChildPermission(LoveCardFilePermissions.Delete, L("DeleteLoveCardFile"));
			entityPermission.CreateChildPermission(LoveCardFilePermissions.BatchDelete, L("BatchDeleteLoveCardFile"));
			entityPermission.CreateChildPermission(LoveCardFilePermissions.ExportExcel, L("ExportExcelLoveCardFile"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}