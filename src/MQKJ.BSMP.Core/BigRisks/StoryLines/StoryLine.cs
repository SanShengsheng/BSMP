using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MQKJ.BSMP.DramaQuestionLibraryTypes;
using MQKJ.BSMP.Dramas;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.QuestionBanks;

namespace MQKJ.BSMP.StoryLines
{
    /// <summary>
    /// 故事线
    /// </summary>
  public    class StoryLine:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 玩家A编号
        /// </summary>
        public  virtual Guid PlayerAId { get; set; }

        /// <summary>
        /// 玩家Ｂ编号
        /// </summary>
        public  virtual Guid PlayerBId { get; set; }

        /// <summary>
        /// 剧情题库编号
        /// </summary>
        public  virtual int DramaQuestionLibraryId { get; set; }

        /// <summary>
        /// 题库编号
        /// </summary>
        public  virtual  int QuestionBankId { get; set; }

        /// <summary>
        /// 是否通关
        /// </summary>
        public  virtual bool isStageCleared { get; set; }

        [ForeignKey("DramaQuestionLibraryId")]
        public virtual DramaQuestionLibrary DramaQuestionLibrary { get; set; }

        [ForeignKey("QuestionBankId")]
        public virtual QuestionBank QuestionBank { get; set; }

        public virtual Player PlayerA { get; set; }

        public virtual Player PlayerB { get; set; }
    }
}
