using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using MQKJ.BSMP.BSMPFiles;

namespace MQKJ.BSMP.SceneFiles.Dto
{
    [AutoMapFrom(typeof(SceneFile))]
    public class GetSceneFileOutput
    {
        public Guid Id { get; set; }

        //public string SceneFilePath { get; set; }

        /// <summary>
        /// 文件实体
        /// </summary>
        public BSMPFile File { get; set; }

        public string SceneFileName { get; set; }
        public  int SceneId { get; set; }

        public bool IsDefault { get; set; }
    }
}
