

using MQKJ.BSMP.SystemMessages.Dtos;

namespace MQKJ.BSMP.SystemMessages
{
    /// <summary>
    /// SystemMessage应用层服务的接口方法
    ///</summary>
    public interface ISystemMessageAppService : BsmpApplicationService<SystemMessage, int, SystemMessageEditDto, SystemMessageEditDto, GetSystemMessagesInput, SystemMessageListDto>
    {

    }
}
