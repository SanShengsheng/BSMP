using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.BonusPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.BonusPoints
{
    public class BonusPointCreator
    {
        private readonly BSMPDbContext _dbContext;

        public BonusPointCreator(BSMPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create()
        {
            CreateBonusPoinEvent();
        }

        public void CreateBonusPoinEvent()
        {
            //授权登录
            var entity = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.authorizationLogin);

            if (entity == null)
            {
                entity = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.authorizationLogin, EventDescription = StaticEventDescription.authorizationLoginDescription, PointsCount = StaticBonusPointsCount.AuthLoginCount }).Entity;
                _dbContext.SaveChanges();
            }

            //普通登录
            var entity1 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.Login);

            if (entity1 == null)
            {
                entity1 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.Login, EventDescription = StaticEventDescription.LoginDescription, PointsCount = StaticBonusPointsCount.LoginCount }).Entity;
                _dbContext.SaveChanges();
            }

            //邀请好友并开始任务
            var entity2 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.InviteFriendAndStartTask);

            if (entity2 == null)
            {
                entity2 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.InviteFriendAndStartTask, EventDescription = StaticEventDescription.InviteFriendAndStartTaskDescription, PointsCount = StaticBonusPointsCount.InviteFirendCount }).Entity;
                _dbContext.SaveChanges();
            }

            //被好友邀请后并开始任务
            var entity3 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.InviteeJoinTask);

            if (entity3 == null)
            {
                entity3 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.InviteeJoinTask, EventDescription = StaticEventDescription.InviteeJoinTaskDescription, PointsCount = StaticBonusPointsCount.InviteeCount }).Entity;
                _dbContext.SaveChanges();
            }
            //主客场双方答案一致
            var entity4 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.AnswerAgreement);

            if (entity4 == null)
            {
                entity4 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.AnswerAgreement, EventDescription = StaticEventDescription.AnswerAgreementDescription, PointsCount = StaticBonusPointsCount.AnswerAgreementCount }).Entity;
                _dbContext.SaveChanges();
            }

            //客场没有答题
            var entity5 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.InviteeNoAnswer);

            if (entity5 == null)
            {
                entity5 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.InviteeNoAnswer, EventDescription = StaticEventDescription.InviteeNoAnswerDescription, PointsCount = StaticBonusPointsCount.InviteeNoAnswerCount }).Entity;
                _dbContext.SaveChanges();
            }

            //主场没有答题
            var entity6 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.InviterNoAnswer);

            if (entity6 == null)
            {
                entity6 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.InviterNoAnswer, EventDescription = StaticEventDescription.InviterNoAnswerDescription, PointsCount = StaticBonusPointsCount.InviterNoAnswerCount }).Entity;
                _dbContext.SaveChanges();
            }

            //3题关
            var entity7 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.ThreeQuestionsBarrier);

            if (entity7 == null)
            {
                entity7 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.ThreeQuestionsBarrier, EventDescription = StaticEventDescription.ThreeQuestionsBarrierDescription, PointsCount = StaticBonusPointsCount.ThreeQuestionCount }).Entity;
                _dbContext.SaveChanges();
            }

            //5题关
            var entity8 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.FiveQuestionsBarrier);

            if (entity8 == null)
            {
                entity8 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.FiveQuestionsBarrier, EventDescription = StaticEventDescription.FiveQuestionsBarrierDescription, PointsCount = StaticBonusPointsCount.FiveQuestionCount }).Entity;
                _dbContext.SaveChanges();
            }

            //10题关
            var entity9 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.TenQuestionsBarrier);

            if (entity9 == null)
            {
                entity9 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.TenQuestionsBarrier, EventDescription = StaticEventDescription.TenQuestionsBarrierDescription, PointsCount = StaticBonusPointsCount.TenQuestionCount }).Entity;
                _dbContext.SaveChanges();
            }

            //使用延时卡
            var entity10 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.UseDelayCard);

            if (entity10 == null)
            {
                entity10 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.UseDelayCard, EventDescription = StaticEventDescription.UseDelayCardDescription, PointsCount = StaticBonusPointsCount.UseDelayCount }).Entity;
                _dbContext.SaveChanges();
            }


            //使用数据卡
            var entity11 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.UseDataCard);

            if (entity11 == null)
            {
                entity11 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.UseDataCard, EventDescription = StaticEventDescription.UseDataCardDescription, PointsCount = StaticBonusPointsCount.UseDataCount }).Entity;
                _dbContext.SaveChanges();
            }

            //分享好友
            var entity12 = _dbContext.BonusPoints.IgnoreQueryFilters().FirstOrDefault(x => x.EventName == StaticEventName.ShareFriend);

            if (entity12 == null)
            {
                entity12 = _dbContext.BonusPoints.Add(new BonusPoint() { EventName = StaticEventName.ShareFriend, EventDescription = StaticEventDescription.ShareFriendsDescription, PointsCount = StaticBonusPointsCount.ShareFriendCount }).Entity;
                _dbContext.SaveChanges();
            }
        }
    }
}
