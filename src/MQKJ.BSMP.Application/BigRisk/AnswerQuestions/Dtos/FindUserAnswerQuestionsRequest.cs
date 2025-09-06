using System;

namespace MQKJ.BSMP.AnswerQuestions.Dtos
{
    public class FindUserAnswerQuestionsRequest
    {
        public FindUserAnswerQuestionsRequest(Guid inviter, Guid invitee)
        {
            InviterPlayId = inviter;
            InviteePlayId = invitee;
        }
        public Guid InviterPlayId { get; set; }
        public Guid InviteePlayId { get; set; }
    }
}