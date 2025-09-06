using MQKJ.BSMP.BSMPFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCard.LoveCardFiles.Dtos
{
    public class GetLoveCardFileByCardIdInput
    {
        /// <summary>
        /// 卡片Id
        /// </summary>
        public Guid LoveCardId { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
    }
}
