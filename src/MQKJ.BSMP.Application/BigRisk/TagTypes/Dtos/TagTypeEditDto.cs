using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.TagTypes.Dtos
{
    public class TagTypeEditDto : FullAuditedEntityDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public new int? Id { get; set; }


        /// <summary>
        /// TypeName
        /// </summary>
        [Required(ErrorMessage = "TypeName不能为空")]
        public string TypeName { get; set; }


        /// <summary>
        /// Tags
        /// </summary>
        public ICollection<Tag> Tags { get; set; }


        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }






        //// custom codes 





        //// custom codes end
    }
}