using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCardOptions.Dtos
{
    [AutoMapTo(typeof(LoveCardOption))]
    public class OptionCardStateDto
    {
        /// <summary>
        /// 点赞的名片Id
        /// </summary>
        public Guid LoveCardId { get; set; }

        /// <summary>
        /// 点赞人的Id
        /// </summary>
        public Guid OptionPlayerId { get; set; }

        public LoveCardOptionType LoveCardOptionType { get; set; }

        public bool IsLike { get; set; }
    }
}
