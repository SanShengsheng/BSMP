using System;

namespace MQKJ.BSMP.PlayerExtensions.Dtos
{
    public class UpdateUserPointRequest
    {
        public UpdateUserPointRequest(Guid userId, int score)
        {
            UserId = userId;
            Score = score;
        }
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }
}