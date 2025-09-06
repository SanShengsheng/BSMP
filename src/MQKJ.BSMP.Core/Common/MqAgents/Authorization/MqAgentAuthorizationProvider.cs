

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="MqAgentPermissions" /> for all permission names. MqAgent
    ///</summary>
    public class MqAgentAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public MqAgentAuthorizationProvider()
		{

		}

        public MqAgentAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public MqAgentAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			// 在这里配置了MqAgent 的权限。
			var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

			var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

			var entityPermission = administration.CreateChildPermission(MqAgentPermissions.Node , L("MqAgent"));
			entityPermission.CreateChildPermission(MqAgentPermissions.GetAgents, L("GetAgents"));
			entityPermission.CreateChildPermission(MqAgentPermissions.ToggleVtlRhgPerm, L("ToggleVtlRhgPerm"));
			entityPermission.CreateChildPermission(MqAgentPermissions.UpdateAgentRatio, L("UpdateAgentRatio"));
			entityPermission.CreateChildPermission(MqAgentPermissions.UpdatePromoterRatio, L("UpdatePromoterRatio"));
			entityPermission.CreateChildPermission(MqAgentPermissions.UpdateAgentState, L("UpdateAgentState"));
			entityPermission.CreateChildPermission(MqAgentPermissions.ExportExcel, L("ExportAgentIncomesToExcel"));
            entityPermission.CreateChildPermission(MqAgentPermissions.GetAgentIncomes, L("GetAgentIncomes"));



            var entityPermissionInviteCode = administration.CreateChildPermission(MqAgentPermissions.AgentInviteCodeNode, L("AgentInviteCode"));
            entityPermissionInviteCode.CreateChildPermission(MqAgentPermissions.GetAgentInviteCodes, L("GetAgentInviteCodes"));
            entityPermissionInviteCode.CreateChildPermission(MqAgentPermissions.CreateAgentInviteCode, L("CreateAgentInviteCode"));


        }

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}