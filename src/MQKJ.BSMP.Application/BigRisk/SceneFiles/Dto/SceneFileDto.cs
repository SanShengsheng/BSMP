using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.SceneManage.SceneFiles.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.SceneFiles.Dto
{
    [AutoMapTo(typeof(SceneFile))]
    public class SceneFileDto : FullAuditedEntity<Guid>
    {
        //[Required]
        //public Guid Id { get; set; }
        public int SceneId { get; set; }

        public string SceneFileName { get; set; }

        public string FileId { get; set; }

        public bool IsDefault { get; set; }
    }
}
