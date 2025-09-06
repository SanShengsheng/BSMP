

using MQKJ.BSMP.Common.SensitiveWords.Dtos;

namespace MQKJ.BSMP.Common.SensitiveWords
{
    /// <summary>
    /// SensitiveWord应用层服务的接口方法
    ///</summary>
    public interface ISensitiveWordAppService : BsmpApplicationService<SensitiveWord, int, SensitiveWordEditDto, SensitiveWordEditDto, GetSensitiveWordsInput, SensitiveWordListDto>
    {

    }
}
