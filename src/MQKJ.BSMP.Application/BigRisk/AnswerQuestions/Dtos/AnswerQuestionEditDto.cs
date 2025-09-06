using System;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.AnswerQuestions.Dtos
{
    public class AnswerQuestionEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


        /// <summary>
        /// InviterAnswer
        /// </summary>
        public string InviterAnswer { get; set; }


        /// <summary>
        /// InviteeAnswer
        /// </summary>
        public string InviteeAnswer { get; set; }


        /// <summary>
        /// State
        /// </summary>
        public bool State { get; set; }


        /// <summary>
        /// QuesionId
        /// </summary>
        public int QuesionId { get; set; }


        /// <summary>
        /// Question
        /// </summary>
        public Question Question { get; set; }


        /// <summary>
        /// GameTaskId
        /// </summary>
        public Guid GameTaskId { get; set; }


        /// <summary>
        /// GameTask
        /// </summary>
        public GameTask GameTask { get; set; }


        /// <summary>
        /// Sort
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// ÊÇ·ñ×÷±×
        /// </summary>
        public bool IsCheat { get; set; }

    }
}