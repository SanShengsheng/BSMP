using Abp;

namespace MQKJ.BSMP.Questions.Dtos
{
    public class GetQuestionsRequest
    {
        public int?  AnswerCount{ get; set; }
        public QuestionGender? QuestionGender { get; set; }
        public QuestionState? QuestionState { get; set; }
        public int? TagId { get; set; }
        public int Top { get; set; }

        public int[] ExcludeIds { get; set; }
    }
}