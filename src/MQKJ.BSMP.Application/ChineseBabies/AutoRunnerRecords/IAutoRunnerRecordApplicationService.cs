using Abp.Application.Services;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// AutoRunnerRecord应用层服务的接口方法
    ///</summary>
    public interface IAutoRunnerRecordAppService : 
        BsmpApplicationService<AutoRunnerRecord, Guid, AutoRunnerRecordEditDto, AutoRunnerRecordEditDto, GetAutoRunnerRecordsInput, AutoRunnerRecordListDto>
    {
       
    }
}
