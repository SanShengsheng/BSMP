using System;

namespace MQKJ.BSMP.WeChat
{
    /// <summary>
    /// 微信小程序模板消息输入
    /// </summary
    public class WechatTemplateMessageInInput
    {
        /// <summary>
        /// 模板类别名称
        /// </summary>
        public string TemplateTypeName { get; set; }
        /// <summary>
        /// 所需下发的模板消息的id
        /// </summary>
        public string TemplateId
        {
            get
            {
                var str = "";
                switch (this.TemplateTypeName)
                {
                    case "NoticeInviterTheInviteeComeIn"://通知邀请方被邀请人来了
                        str = "Q6n_u8MzCF7Ht8OmCoWlt5nGA8sxxefsNwfj-wEQBKk";
                        break;
                    case "NoticePlayersGameResult"://通知被邀请方游戏结果
                        str = "npmfkT64H_cUDWLjwhwmV6X9Ol0hVARmYUt_kAJJteY";
                        break;
                    case "NoticePlayersGameResultSuccess"://通知邀请方游戏结果——成功
                        str = "npmfkT64H_cUDWLjwhwmVyUD3Oxa5BiyLrLJvu4NRNw";
                        break;
                    case "NoticePlayersGameResultFail"://通知邀请方游戏结果——失败
                        str = "npmfkT64H_cUDWLjwhwmV4RiXo8s4iARnSZGB6yLcUQ";
                        break;
                }
                return str;
            }
            //set => TemplateId = value;
        }
        ///// <summary>
        ///// 小程序编号，默认为关系进化
        ///// </summary>
        //[Required]
        //public EWechatProgrammEnum WechatId { get; set; }
        /// <summary>
        /// 模板需要放大的关键词，不填则默认无放大
        /// </summary>
        public string EmphasisKeyword { get; set; }
        /// <summary>
        /// 邀请方编号
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// 点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。
        /// </summary>
        public string Page { get; set; }
        /// <summary>
        /// 表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id
        /// </summary>
        public string FormId { get; set; }
        /// <summary>
        /// 模板内容，不填则下发空模板
        /// </summary>
        public object Data { get; set; }
    }

}
