

using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Friends.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="FriendPermissions" /> for all permission names. Friend
    ///</summary>
    public class FriendAuthorizationProvider : AuthorizationProvider
    {
		public override void SetPermissions(IPermissionDefinitionContext context)
		{
            // 在这里配置了Friend 的权限。
            var pages = context.GetPermissionOrNull(FriendPermissions.Node);

            if (pages == null)
                pages = context.CreatePermission(FriendPermissions.Node, L("Friend"));

            var entityPermission = pages.CreateChildPermission(FriendPermissions.All_Friends, L("All_Friend"));
			entityPermission.CreateChildPermission(FriendPermissions.Query, L("QueryFriend"));
			entityPermission.CreateChildPermission(FriendPermissions.Create, L("CreateFriend"));
			entityPermission.CreateChildPermission(FriendPermissions.Edit, L("EditFriend"));
			entityPermission.CreateChildPermission(FriendPermissions.Delete, L("DeleteFriend"));
			entityPermission.CreateChildPermission(FriendPermissions.BatchDelete, L("BatchDeleteFriend"));
			entityPermission.CreateChildPermission(FriendPermissions.ExportExcel, L("ExportExcelFriend"));



			 
			 
			 
		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
		}
    }
}