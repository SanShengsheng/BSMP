using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.TagTypes.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="TagTypeAppPermissions"/> for all permission names.
    /// </summary>
    public class TagTypeAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了TagType 的权限。
            //var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

            var tagtype = context.GetPermissionOrNull( TagTypeAppPermissions.TagType) ?? context.CreatePermission(TagTypeAppPermissions.TagType, L("TagType"));

            //var tagtype = administration.CreateChildPermission(TagTypeAppPermissions.TagType , L("TagType"));
            tagtype.CreateChildPermission(TagTypeAppPermissions.TagType_CreateTagType, L("CreateTagType"));
            tagtype.CreateChildPermission(TagTypeAppPermissions.TagType_EditTagType, L("EditTagType"));           
            tagtype.CreateChildPermission(TagTypeAppPermissions.TagType_DeleteTagType, L("DeleteTagType"));
			tagtype.CreateChildPermission(TagTypeAppPermissions.TagType_BatchDeleteTagTypes , L("BatchDeleteTagTypes"));


			
			//// custom codes 
			
			//// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}