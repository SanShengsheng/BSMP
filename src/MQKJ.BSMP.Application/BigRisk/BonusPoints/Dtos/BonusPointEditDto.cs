using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPoints.Dtos
{
    [AutoMapTo(typeof(BonusPoint))]
    public class BonusPointEditDto
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
        //[Required(ErrorMessage="EventName不能为空")]
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