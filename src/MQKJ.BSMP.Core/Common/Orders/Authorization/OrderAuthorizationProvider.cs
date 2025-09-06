

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Orders.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="OrderPermissions" /> for all permission names. Order
    ///</summary>
    public class OrderAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public OrderAuthorizationProvider()
		{

		}

        public OrderAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public OrderAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了Order 的权限。
            var pages = context.GetPermissionOrNull(OrderPermissions.Node);
            if (pages == null)
                pages = context.CreatePermission(OrderPermissions.Node, L("Order"));

            var entityPermission = pages.CreateChildPermission(OrderPermissions.QueryOrders, L("QueryOrders"));
			entityPermission.CreateChildPermission(OrderPermissions.QueryOrderState, L("QueryOrderState"));
			entityPermission.CreateChildPermission(OrderPermissions.ExportToExcel, L("ExportToExcel"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}