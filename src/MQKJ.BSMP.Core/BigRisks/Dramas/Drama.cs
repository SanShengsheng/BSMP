using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.DramaQuestionLibraryTypes;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.PlayerDramas;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.StoryLines;
using System.Collections.Generic;

namespace MQKJ.BSMP.Dramas
{
    /// <summary>
    /// 剧情
    /// </summary>
    public class Drama:FullAuditedEntity
    {
        /// <summary>
        /// 关系程度
        /// </summary>
        public RelationDegree RelationDegree { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public QuestionGender Gender { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类别，1是默认，2是新手
        /// </summary>
        public DramaTypeEnum DramaType { get; set; }

        public virtual  ICollection<DramaQuestionLibrary> DramaQuestionLibrarys { get; set; }

        public virtual  ICollection<PlayerDrama> PlayerDramas { get; set; }


        public  virtual ICollection<StoryLine> StoryLines { get; set; }
    }
}
