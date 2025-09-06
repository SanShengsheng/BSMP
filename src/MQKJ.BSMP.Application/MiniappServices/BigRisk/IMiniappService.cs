using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQKJ.BSMP.MiniappServices.BigRisk.Models;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChat.Dtos;

namespace MQKJ.BSMP.MiniappServices
{
    public interface IMiniappService
    {
        GetOpenIdOutput GetOpenId(GetOpenIdInput input);


        //void CreateTempalateInfo(CreateTemplateInfoInputDto input);

         List<DeveloperPlayersOutput> GetWaitersAsync(Guid? playerId);

        //获取公众号openId
        GetOfficeAccountOpenIdOutput GetOfficeAccountOpenId(GetOpenIdInput input);

        /// <summary>
        ///获取公众号的unionID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetOfficeAccountUnionIdOutput GetOfficeAccountUnionId(GetOfficeAccountUnionIdInput input);

        Tenant GetTenant(int id);

        /// <summary>
        /// 获取微信网站应用的accesstoken
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAccessTokenWithCodeOutput GetWebAccessTokenWithCode(GetAccessTokenWithCodeInput input);


        /// <summary>
        /// 刷新微信网站应用的accesstoken
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        RefreshAccessTokenOutput RefreshAccessToken(RefreshAccessTokenInput input);


        /// <summary>
        /// 检测微信网站应用的accesstoken
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CheckAccessTokenOutput CheckAccessToken(CheckAccessTokenInput input);

        /// <summary>
        /// 获取微信网站应用用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetWechatWebUserInfoOutput GetWechatUserInfo(GetWechatWebUserInfoInput input);

        /// <summary>
        /// 获取所有的租户
        /// </summary>
        /// <returns></returns>
        List<BSMPTenantDto> GetTenants();
    }
}
