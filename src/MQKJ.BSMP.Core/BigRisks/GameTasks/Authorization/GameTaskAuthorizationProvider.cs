using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.GameTasks.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="GameTaskAppPermissions"/> for all permission names.
    /// </summary>
    public class GameTaskAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了GameTask 的权限。
            var pages = context.GetPermissionOrNull(GameTaskAppPermissions.GameTask);
            if (pages == null)
                pages = context.CreatePermission(GameTaskAppPermissions.GameTask, L("GameTasks"));

            //Tasks
            var gametask = pages.CreateChildPermission(GameTaskAppPermissions.GameTasks_AllGameTasks , L("AllGameTasks"));
            gametask.CreateChildPermission(GameTaskAppPermissions.GameTask_CreateGameTask, L("CreateGameTask"));
            gametask.CreateChildPermission(GameTaskAppPermissions.GameTask_EditGameTask, L("EditGameTask"));           
            gametask.CreateChildPermission(GameTaskAppPermissions.GameTask_DeleteGameTask, L("DeleteGameTask"));
			gametask.CreateChildPermission(GameTaskAppPermissions.GameTask_BatchDeleteGameTasks , L("BatchDeleteGameTasks"));


			
			//// custom codes 
			
			//// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}