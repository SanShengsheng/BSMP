using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MQKJ.BSMP.DramaQuestionLibraryTypes;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.StoryLines;

namespace MQKJ.BSMP.QuestionBanks
{
    /// <summary>
    /// 题库表
    /// </summary>
    public   class QuestionBank : FullAuditedEntity
    {
        /// <summary>
        /// 剧情题库编号
        /// </summary>
        public  virtual int DramaQuestionLibraryId { get; set; }

        /// <summary>
        /// 问题编号
        /// </summary>
        public  virtual int QuestionId { get; set; }

        /// <summary>
        /// 上一道题编号
        /// </summary>
        public  virtual int LastId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 重复私密度计数（n-x个累加，普通阶段x为5；暧昧阶段x为3；情侣阶段x为4；夫妻阶段不考虑）
        /// </summary>
        public int RepeatSecretCount { get; set; }
        /// <summary>
        /// 重复场景计数（n-2个累加）
        /// </summary>
        public int RepeatSceneCount { get; set; }
        /// <summary>
        /// 重复话题计数（n-2累加，非强制性约束，相连问题允许有1个话题重复）
        /// </summary>
        public int RepeatTopicCount { get; set; }
        /// <summary>
        /// 重复难易程度（只考虑A级别）计数（n-2累加）
        /// </summary>
        public int RepeatComplexityCount { get; set; }

        [ForeignKey("QuestionId")]
        public  virtual  Question Question { get; set; }
        [ForeignKey("DramaQuestionLibraryId")]
        public  virtual DramaQuestionLibrary DramaQuestionLibrary { get; set; }

        public virtual ICollection<StoryLine> StoryLines { get; set; }


    }
}
