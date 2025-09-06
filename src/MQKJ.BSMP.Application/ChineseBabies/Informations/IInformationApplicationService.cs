
using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Informations.Dtos;
using MQKJ.BSMP.ChineseBabies.Message.Dtos;
using System;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Information应用层服务的接口方法
    ///</summary>
    public interface IInformationAppService :
        BsmpApplicationService<Information, Guid, InformationEditDto, InformationEditDto, GetInformationsInput, InformationListDto>
    {
        Task< HasNewInformationResponse> HasNewInformation(HasNewInformationRequest request);

        Task ModifyInforationState(ModifyInforationStateInput input);
        Task<PagedResultDto<InformationListDto>> GetInformations(GetInformationsInput input);

        /// <summary>
        /// 获取跑马灯消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<RunHorseInformationListDto>> GetRunHorseInformations(GetRunHorseInformationsInput input);
    }
}
