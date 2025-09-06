using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.BonusPointRecords.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="BonusPointRecordAppPermissions"/> for all permission names.
    /// </summary>
    public class BonusPointRecordAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            ////在这里配置了BonusPointRecord 的权限。
            var pages = context.GetPermissionOrNull(BonusPointRecordAppPermissions.BonusPointRecord);
            if (pages == null)
                pages = context.CreatePermission(BonusPointRecordAppPermissions.BonusPointRecord, L("BonusPointRecord"));

            //Tasks
            var BonusPointRecord = pages.CreateChildPermission(BonusPointRecordAppPermissions.BonusPointRecord_AllBonusPoints, L("AllBonusPointRecord"));
            BonusPointRecord.CreateChildPermission(BonusPointRecordAppPermissions.BonusPointRecord_CreateBonusPointRecord, L("CreateBonusPointRecord"));
            BonusPointRecord.CreateChildPermission(BonusPointRecordAppPermissions.BonusPointRecord_EditBonusPointRecord, L("EditBonusPointRecord"));
            BonusPointRecord.CreateChildPermission(BonusPointRecordAppPermissions.BonusPointRecord_DeleteBonusPointRecord, L("DeleteBonusPointRecord"));
            BonusPointRecord.CreateChildPermission(BonusPointRecordAppPermissions.BonusPointRecord_BatchDeleteBonusPointRecords, L("BatchDeleteBonusPointRecords"));



            //// custom codes 

            //// custom codes end
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}