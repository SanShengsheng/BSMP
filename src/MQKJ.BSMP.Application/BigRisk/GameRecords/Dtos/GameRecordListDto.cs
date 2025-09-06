using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameRecords;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.GameRecords.Dtos
{ 
    public class GameRecordListDto : FullAuditedEntityDto<Guid>
    {
    



/// <summary>
/// RecordTime
/// </summary>
public DateTime RecordTime { get; set; }


/// <summary>
/// State
/// </summary>
public GameState State { get; set; }


/// <summary>
/// GameTaskId
/// </summary>
public Guid GameTaskId { get; set; }


/// <summary>
/// GameTask
/// </summary>
public GameTask GameTask { get; set; }



		
		
		
		//// custom codes 
		
        //// custom codes end
    }
}