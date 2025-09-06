using System.Collections.Generic;

namespace MQKJ.BSMP.Utils.Message.Wechat.Dtos
{
    public class TemplateList
    {
        public int ErrCode { get; set; }

        public string Errmsg { get; set; }

        public List<Template> List { get; set; }

    }

    public class Template
    {
        public string Template_id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Example { get; set; }

    }
}
