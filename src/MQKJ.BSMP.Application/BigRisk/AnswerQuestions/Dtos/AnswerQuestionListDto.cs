using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Questions;
using Abp.AutoMapper;

namespace MQKJ.BSMP.AnswerQuestions.Dtos
{
    [AutoMapFrom(typeof(AnswerQuestion))]
    public class AnswerQuestionListDto
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






        //// custom codes 

        //// custom codes end
    }
}