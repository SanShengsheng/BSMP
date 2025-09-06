using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.PlayerExtensions.Dtos
{ 
    public class PlayerExtensionListDto : FullAuditedEntityDto
    {
    
 


/// <summary>
/// 玩家编号
/// </summary>
public Guid PlayerGuid { get; set; }


/// <summary>
/// 玩家积分（爱豆）
/// </summary>
public int LoveScore { get; set; }


/// <summary>
/// 备用字段
/// </summary>
public int ExtensionFiled1 { get; set; }


/// <summary>
/// 备用字段
/// </summary>
public int ExtensionFiled2 { get; set; }


/// <summary>
/// 备用字段
/// </summary>
public int ExtensionFiled3 { get; set; }


/// <summary>
/// 备用字段
/// </summary>
public string ExtensionFiled4 { get; set; }


/// <summary>
/// 备用字段
/// </summary>
public string ExtensionFiled5 { get; set; }


/// <summary>
/// 备用字段
/// </summary>
public string ExtensionFiled6 { get; set; }



		
		
		
		//// custom codes 
		
        //// custom codes end
    }
}