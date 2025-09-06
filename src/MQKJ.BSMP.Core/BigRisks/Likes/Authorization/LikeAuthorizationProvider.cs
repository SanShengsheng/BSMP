

using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Likes.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="LikePermissions" /> for all permission names. Like
    ///</summary>
    public class LikeAuthorizationProvider : AuthorizationProvider
    {
		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了Like 的权限。
            var pages = context.GetPermissionOrNull(LikePermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(LikePermissions.Node, L("Like"));

			var entityPermission = pages.CreateChildPermission(LikePermissions.All_Likes, L("All_Like"));
			entityPermission.CreateChildPermission(LikePermissions.Query, L("QueryLike"));
			entityPermission.CreateChildPermission(LikePermissions.Create, L("CreateLike"));
			entityPermission.CreateChildPermission(LikePermissions.Edit, L("EditLike"));
			entityPermission.CreateChildPermission(LikePermissions.Delete, L("DeleteLike"));
			entityPermission.CreateChildPermission(LikePermissions.BatchDelete, L("BatchDeleteLike"));
			entityPermission.CreateChildPermission(LikePermissions.ExportExcel, L("ExportExcelLike"));
			 
		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}