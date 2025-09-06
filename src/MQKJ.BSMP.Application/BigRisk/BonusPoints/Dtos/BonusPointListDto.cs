using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.BonusPoints;
using Abp.AutoMapper;

namespace MQKJ.BSMP.BonusPoints.Dtos
{
    [AutoMapFrom(typeof(BonusPoint))]
    public class BonusPointListDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }


        /// <summary>
        /// PointsCount
        /// </summary>
        public int PointsCount { get; set; }


        /// <summary>
        /// EventName
        /// </summary>
        //[Required(ErrorMessage = "EventName不能为空")]
        public string EventName { get; set; }


        /// <summary>
        /// EventDescription
        /// </summary>
        public string EventDescription { get; set; }


        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }






        //// custom codes 

        //// custom codes end
    }
}