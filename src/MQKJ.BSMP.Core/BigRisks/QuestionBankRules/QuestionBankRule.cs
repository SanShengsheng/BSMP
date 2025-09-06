using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace MQKJ.BSMP.QuestionBankRules
{
    public class QuestionBankRule : FullAuditedEntity
    {
        /// <summary>
        /// 私密度（标签）
        /// </summary>
        public int SecretId { get; set; }
        /// <summary>
        /// 场景编号
        /// </summary>
        public int SceneId { get; set; }
        /// <summary>
        /// 话题编号（标签）
        /// </summary>
        public int TopicId { get; set; }
        /// <summary>
        /// 难易程度（标签）
        /// </summary>
        public int ComplexityId { get; set; }
        /// <summary>
        /// 剧情题库编号（88个题库）
        /// </summary>
        public int DramaQuestionLibraryId { get; set; }
        /// <summary>
        /// 编码，规则为："S"+secretId（私密度）+"C"+sceneId（场景编号）+"T"+topicId（话题编号）+"P"+complexityId（难易程度）+“_”+Code(dramaQuestionLibrary表下的code字段)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

    }
}
