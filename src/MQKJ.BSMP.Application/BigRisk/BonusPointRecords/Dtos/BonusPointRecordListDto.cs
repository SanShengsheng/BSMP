using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.BonusPointRecords.Dtos
{ 
    public class BonusPointRecordListDto : FullAuditedEntityDto<Guid>
    {
    


/// <summary>
/// GatherCount
/// </summary>
public int GatherCount { get; set; }


/// <summary>
/// PlayerId
/// </summary>
public Guid PlayerId { get; set; }


/// <summary>
/// Player
/// </summary>
public Player Player { get; set; }


/// <summary>
/// BonusPointId
/// </summary>
public int BonusPointId { get; set; }


/// <summary>
/// BonusPoint
/// </summary>
public BonusPoint BonusPoint { get; set; }



		
		
		
		//// custom codes 
		
        //// custom codes end
    }
}