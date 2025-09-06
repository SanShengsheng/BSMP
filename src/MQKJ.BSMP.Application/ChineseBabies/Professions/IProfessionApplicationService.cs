
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Professions.Dtos;
using MQKJ.BSMP.WeChatPay.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Profession应用层服务的接口方法
    ///</summary>
    public interface IProfessionAppService : BsmpApplicationService<Profession,int,ProfessionEditDto,ProfessionEditDto,GetProfessionsInput,ProfessionListDto>
    {

        /// <summary>
        /// 获取职业列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ProfessionListDto>> GetProfessions(GetProfessionsInput input);

        /// <summary>
        /// 转职
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MiniProgramPayOutput> ChangeProfession(ChangeProfessionInput input);

        /// <summary>
        /// 获取职业信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ProfessionListDto> GetProfession(GetProfessionInput input);

        /// <summary>
        /// 获取默认职业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ProfessionListDto>> GetDefaultProfession();

        /// <summary>
        /// 获取转职结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QueryChangeResultOutput> QueryChangePorfessionResult(QueryChangeResultInput input);

        /// <summary>
        /// 获取父母双方的职业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetParentProfessionOutput> GetParentProfession(GetParentProfessionInput input);

        /// <summary>
        /// 虚拟转职
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MiniProgramPayOutput> VirtualChangeProfession(VirtualChangeProfessionInput input);

    }
}
