using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MQKJ.BSMP.Dramas;
using MQKJ.BSMP.QuestionBanks;
using MQKJ.BSMP.StoryLines;

namespace MQKJ.BSMP.DramaQuestionLibraryTypes
{
    /// <summary>
    /// 剧情题库
    /// </summary>
   public class DramaQuestionLibrary:FullAuditedEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 剧情编号
        /// </summary>
        public virtual int DramaId { get; set; }

        [ForeignKey("DramaId")]
        public virtual Drama Drama { get; set; }

        public  virtual  ICollection<QuestionBank> QuestionBanks { get; set; }

        public virtual ICollection<StoryLine> StroLines { get; set; }
    }
}
