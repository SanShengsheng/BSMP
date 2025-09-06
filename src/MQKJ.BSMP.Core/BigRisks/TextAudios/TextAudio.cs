using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.CommonEnum;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.TextAudios
{
    [Table("TextAudios")]
    public class TextAudio : FullAuditedEntity
    {
        /// <summary>
        /// 语音内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public EGender Gender { get; set; }

        /// <summary>
        /// 场景（如flag、answer）
        /// </summary>
        public ESceneType Scene { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
