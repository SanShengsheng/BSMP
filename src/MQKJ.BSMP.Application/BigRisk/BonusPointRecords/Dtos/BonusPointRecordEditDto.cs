using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.BonusPointRecords.Dtos
{
    public class BonusPointRecordEditDto
    {
		/// <summary>
/// Id
/// </summary>
public Guid? Id { get; set; }


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