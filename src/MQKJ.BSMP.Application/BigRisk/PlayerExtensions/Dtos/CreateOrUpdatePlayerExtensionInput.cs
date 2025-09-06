using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.PlayerExtensions.Dtos
{
    public class CreateOrUpdatePlayerExtensionInput
    {
        [Required]
        public PlayerExtensionEditDto PlayerExtension { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}