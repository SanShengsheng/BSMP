namespace MQKJ.BSMP.MiniappServices.Models
{

    public class GetOpenIdInput
    {

        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string Code { get; set; }

        public long UserId { get; set; }
    }
}