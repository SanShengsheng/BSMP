using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.Common.SensitiveWords
{
    [Table("SensitiveWords")]
    public class SensitiveWord: Entity<int>
    {
        public string Content { get; set; }
    }
}
