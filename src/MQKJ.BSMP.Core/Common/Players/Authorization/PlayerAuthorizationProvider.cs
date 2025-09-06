using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Players.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="PlayerAppPermissions"/> for all permission names.
    /// </summary>
    public class PlayerAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PlayerAppPermissions.Player);
            if (pages == null)
                pages = context.CreatePermission(PlayerAppPermissions.Player, L("Player"));

            var player = pages.CreateChildPermission(PlayerAppPermissions.Player_AllPlayers, L("AllPlayers"));
            player.CreateChildPermission(PlayerAppPermissions.Player_CreatePlayer, L("CreatePlayer"));
            player.CreateChildPermission(PlayerAppPermissions.Player_EditPlayer, L("EditPlayer"));           
            player.CreateChildPermission(PlayerAppPermissions.Player_DeletePlayer, L("DeletePlayer"));
			player.CreateChildPermission(PlayerAppPermissions.Player_BatchDeletePlayers , L("BatchDeletePlayers"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}