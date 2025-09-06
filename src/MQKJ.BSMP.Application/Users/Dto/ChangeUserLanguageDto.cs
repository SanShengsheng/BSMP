using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}