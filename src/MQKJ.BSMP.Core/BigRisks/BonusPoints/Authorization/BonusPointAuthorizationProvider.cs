using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.BonusPoints.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="BonusPointAppPermissions"/> for all permission names.
    /// </summary>
    public class BonusPointAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了BonusPoint 的权限。
            var pages = context.GetPermissionOrNull(BonusPointAppPermissions.BonusPoint);
            if (pages == null)
                pages = context.CreatePermission(BonusPointAppPermissions.BonusPoint, L("BonusPoints"));

            //Tasks
            var bonuspoint = pages.CreateChildPermission(BonusPointAppPermissions.BonusPoint_AllBonusPoints, L("AllBonusPoint"));
            bonuspoint.CreateChildPermission(BonusPointAppPermissions.BonusPoint_CreateBonusPoint, L("CreateBonusPoint"));
            bonuspoint.CreateChildPermission(BonusPointAppPermissions.BonusPoint_EditBonusPoint, L("EditBonusPoint"));
            bonuspoint.CreateChildPermission(BonusPointAppPermissions.BonusPoint_DeleteBonusPoint, L("DeleteBonusPoint"));
            bonuspoint.CreateChildPermission(BonusPointAppPermissions.BonusPoint_BatchDeleteBonusPoints, L("BatchDeleteBonusPoints"));



            //// custom codes 

            //// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}