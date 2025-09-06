using System;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameRecords;
using MQKJ.BSMP.GameTasks;

namespace  MQKJ.BSMP.GameRecords.Dtos
{
    public class GameRecordEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


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

    }
}