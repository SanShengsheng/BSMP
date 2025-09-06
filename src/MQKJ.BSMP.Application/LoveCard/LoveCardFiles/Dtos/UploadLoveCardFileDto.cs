using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.LoveCardFiles.Dtos
{
    public class UploadLoveCardFileDto
    {

        [Required]
        public IFormFile FormFile { get; set; }

        [Required]
        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        public Guid? LoveCardId { get; set; }

        public UploadLoveCardFileDto(IFormFile file, Guid playerId,Guid? loveCardId)
        {
            this.FormFile = file;
            this.PlayerId = playerId;
            this.LoveCardId = loveCardId;
        }
    }
}
