using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Scenes.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="SceneAppPermissions"/> for all permission names.
    /// </summary>
    public class SceneAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了Scene 的权限。
            var pages = context.GetPermissionOrNull(SceneAppPermissions.Scene);
            if (pages == null)
                pages = context.CreatePermission(SceneAppPermissions.Scene, L("Scene"));

            var scene = pages.CreateChildPermission(SceneAppPermissions.Scene_AllScenes , L("AllScenes"));
            scene.CreateChildPermission(SceneAppPermissions.Scene_CreateScene, L("CreateScene"));
            scene.CreateChildPermission(SceneAppPermissions.Scene_EditScene, L("EditScene"));           
            scene.CreateChildPermission(SceneAppPermissions.Scene_DeleteScene, L("DeleteScene"));
			scene.CreateChildPermission(SceneAppPermissions.Scene_BatchDeleteScenes , L("BatchDeleteScenes"));


			
			//// custom codes 
			
			//// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}