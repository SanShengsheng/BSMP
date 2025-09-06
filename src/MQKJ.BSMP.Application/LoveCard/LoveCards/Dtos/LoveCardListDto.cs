

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.LoveCards;
using System.Collections.Generic;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.PlayerLabels;
using Abp.AutoMapper;
using MQKJ.BSMP.PlayerLabels.Dtos;
using MQKJ.BSMP.BSMPFiles;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    [AutoMapFrom(typeof(LoveCard))]
    public class LoveCardListDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Player
        /// </summary>
        public PlayerDto Player { get; set; }



        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }



        /// <summary>
        /// StyleCode
        /// </summary>
        public int StyleCode { get; set; }



        /// <summary>
        /// CardCode
        /// </summary>
        public string CardCode { get; set; }



        /// <summary>
        /// LikeCount
        /// </summary>
        public int LikeCount { get; set; }



        /// <summary>
        /// ShareCount
        /// </summary>
        public int ShareCount { get; set; }


        ///// <summary>
        ///// 卡片性别
        ///// </summary>
        //public virtual PlayerGender Gender { get; set; }



        /// <summary>
        /// SaveCount
        /// </summary>
        public int SaveCount { get; set; }


        public LoveCardState State { get; set; }


        /// <summary>
        /// LoveCardOptions
        /// </summary>
        public LoveCardOptionDto LoveCardOption { get; set; }

        /// <summary>
        /// LoveCardFiles
        /// </summary>
        public ICollection<LoveCardFileDto> LoveCardFiles { get; set; }

        public ICollection<PlayerLabelDto> PlayerLabels { get; set; }


    }

    public class LoveCardOptionDto
    {
        public Guid LoveCardId { get; set; }

        /// <summary>
        /// IsLiked
        /// </summary>
        public bool IsLiked { get; set; }

        /// <summary>
        /// OptionPlayerId
        /// </summary>
        public Guid OptionPlayerId { get; set; }
    }

    public class LoveCardFileDto
    {
        public Guid Id { get; set; }

        public Guid LoveCardId { get; set; }

        public BSMPFileDto BSMPFile { get; set; }

    }

    public class BSMPFileDto
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        public FileType FileType { get; set; }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        //public string ThumbnailImagePath { get; set; }
    }

    public class PlayerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }



        /// <summary>
        /// TenantId
        /// </summary>
        public int TenantId { get; set; }



        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }



        /// <summary>
        /// HeadUrl
        /// </summary>
        public string HeadUrl { get; set; }



        /// <summary>
        /// Gender
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
		/// State
		/// </summary>
		public int State { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [Range(1, 10)]
        public string Profession { get; set; }

        /// <summary>
        /// 居住地
        /// </summary>
        public string Domicile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatAccount { get; set; }

        public PlayerExtensionDto PlayerExtension { get; set; }
    }

    public class PlayerExtensionDto
    {
        /// <summary>
        /// 星座
        /// </summary>
        public string Constellation { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        [StringLength(200, ErrorMessage = "最大长度不超过200")]
        public string Introduce { get; set; }

        public bool IsUnLock { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayerLabelDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }



        /// <summary>
        /// LabelContent
        /// </summary>
        public string LabelContent { get; set; }



        /// <summary>
        /// PlayerId
        /// </summary>
        //public Guid PlayerId { get; set; }
    }
}