using MQKJ.BSMP.Answers;
using System.Collections.Generic;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    public class StartGameOutput
    {
        public List<QuestionList> InviterQuestions { get; set; }
        public List<QuestionList> InviteeQuestions { get; set; }
        public string ConnectionId { get; set; }
        public string BeConnectionId { get; set; }

        /// <summary>
        /// 邀请方增减积分量
        /// </summary>
        //public int InviterBonusPointCount { get; set; }

        /// <summary>
        /// 被邀请方增减积分量
        /// </summary>
        //public int InviteeBonusPointCount { get; set; }

        public StartGameOutput()
        {
            InviterQuestions = new List<QuestionList>();
            InviteeQuestions = new List<QuestionList>();
        }

    }

    public class QuestionList
    {
        public int QuestionId { get; set; }

        public string Question { get; set; }

        public string BackImageUrl { get; set; }
        /// <summary>
        /// 随略图地址
        /// </summary>
        public string ThumbnailPath { get; set; }
        /// <summary>
        /// 选项列表
        /// </summary>
        public IEnumerable<WechatQuestionAnswer> Answers { get; set; }
        /// <summary>
        /// 对方背景故事文字长度
        /// </summary>
        public int WordLength { get; set; }
        /// <summary>
        /// 场景名
        /// </summary>
        public string SceneName { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 校验人姓名
        /// </summary>
        public string CheckOneName { get; set; }

        /// <summary>
        /// 私密度
        /// </summary>
        public string PrivateDensity { get; set; }
    }

    public class WechatQuestionAnswer
    {
        public WechatQuestionAnswer()
        {

        }

        public WechatQuestionAnswer(Answer answer)
        {
            AnswerId = answer.Id;
            AnswerText = answer.Title;
            Sort = answer.Sort;
        }
        public int AnswerId { get; set; }

        public string AnswerText { get; set; }

        public int Sort { get; set; }
     

    }
}
