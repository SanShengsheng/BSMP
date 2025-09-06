using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.SceneFiles.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="SceneFileAppPermissions"/> for all permission names.
    /// </summary>
    public class SceneFileAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了SceneFile 的权限。
            var pages = context.GetPermissionOrNull(SceneFileAppPermissions.SceneFile);
            if (pages == null)
            {
                pages = context.CreatePermission(SceneFileAppPermissions.SceneFile,L("SceneFile"));
            }
 
			var scenefile = pages.CreateChildPermission(SceneFileAppPermissions.SceneFile_All_SceneFiles , L("All_SceneFiles"));
            scenefile.CreateChildPermission(SceneFileAppPermissions.SceneFile_CreateSceneFile, L("CreateSceneFile"));
            scenefile.CreateChildPermission(SceneFileAppPermissions.SceneFile_EditSceneFile, L("EditSceneFile"));           
            scenefile.CreateChildPermission(SceneFileAppPermissions.SceneFile_DeleteSceneFile, L("DeleteSceneFile"));
			scenefile.CreateChildPermission(SceneFileAppPermissions.SceneFile_BatchDeleteSceneFiles , L("BatchDeleteSceneFiles"));


			
			//// custom codes 
			
			//// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}