using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Scenes;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Tag
{
    public class TagCreator
    {
        private readonly BSMPDbContext _context;

        public TagCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateTag();
        }

        private void CreateTag()
        {
            //  私密度
            var privacy = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "Privacy");
            //  私密度 普通
            var secretDefault = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "普通" && t.TagTypeId == privacy.Id);
            if (secretDefault == null)
            {
                secretDefault = _context.Tags.Add(new Tags.Tag() { TagName = "普通", Sort = 1, TagTypeId = privacy.Id }).Entity;
            }
            //  私密度 私密
            var secretPrivate = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "私密" && t.TagTypeId == privacy.Id);
            if (secretPrivate == null)
            {
                secretPrivate = _context.Tags.Add(new Tags.Tag() { TagName = "私密", Sort = 2, TagTypeId = privacy.Id }).Entity;
            }
            //  关系维度
            var relationshipDegreeTagType = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "RelationshipDegree");
            var relationshipDegrees = _context.Tags.IgnoreQueryFilters().Where(t => t.TagTypeId == relationshipDegreeTagType.Id && !t.IsDeleted).ToList();
            #region code
            var relationshipDegreeOrdinary = relationshipDegrees.FirstOrDefault(t => t.TagName == "普通朋友");
            if (relationshipDegreeOrdinary == null)
            {
                var relationshipDegreeFriend = relationshipDegrees.FirstOrDefault(t => t.TagName == "朋友");
                if (relationshipDegreeFriend == null)
                {
                    relationshipDegreeFriend = _context.Tags.Add(new Tags.Tag() { TagName = "朋友", Sort = 1, TagTypeId = relationshipDegreeTagType.Id }).Entity;
                }
            }
            else
            {
                relationshipDegreeOrdinary.TagName = "朋友";
                relationshipDegreeOrdinary = _context.Tags.Update(relationshipDegreeOrdinary).Entity;
            }
            //  暧昧
            var relationshipDegreeAmbiguousBoyAndGirl = relationshipDegrees.FirstOrDefault(t => t.TagName == "暧昧男女");
            if (relationshipDegreeAmbiguousBoyAndGirl == null)
            {
                var relationshipDegreeAmbiguous = relationshipDegrees.FirstOrDefault(t => t.TagName == "暧昧");
                if (relationshipDegreeAmbiguous == null)
                {
                    relationshipDegreeAmbiguous = _context.Tags.Add(new Tags.Tag() { TagName = "暧昧", Sort = 2, TagTypeId = relationshipDegreeTagType.Id }).Entity;
                }
            }
            else
            {
                relationshipDegreeAmbiguousBoyAndGirl.TagName = "暧昧";
                relationshipDegreeAmbiguousBoyAndGirl = _context.Tags.Update(relationshipDegreeAmbiguousBoyAndGirl).Entity;
            }
            //  情侣
            var relationshipDegreeHotLovers = relationshipDegrees.FirstOrDefault(t => t.TagName == "热恋情侣");
            if (relationshipDegreeHotLovers == null)
            {
                var relationshipDegreeLovers = relationshipDegrees.FirstOrDefault(t => t.TagName == "情侣");
                if (relationshipDegreeLovers == null)
                {
                    relationshipDegreeLovers = _context.Tags.Add(new Tags.Tag() { TagName = "情侣", Sort = 3, TagTypeId = relationshipDegreeTagType.Id }).Entity;
                }
            }
            else
            {
                relationshipDegreeHotLovers.TagName = "情侣";
                relationshipDegreeHotLovers = _context.Tags.Update(relationshipDegreeHotLovers).Entity;
            }
            //  夫妻
            var relationshipDegreeCoupleYouth = relationshipDegrees.FirstOrDefault(t => t.TagName == "年轻夫妻" && !t.IsDeleted);
            if (relationshipDegreeCoupleYouth == null)
            {
                var relationshipDegreeCouple = relationshipDegrees.FirstOrDefault(t => t.TagName == "夫妻");
                if (relationshipDegreeCouple == null)
                {
                    relationshipDegreeCouple = _context.Tags.Add(new Tags.Tag() { TagName = "夫妻", Sort = 4, TagTypeId = relationshipDegreeTagType.Id }).Entity;
                }
            }
            else
            {
                relationshipDegreeCoupleYouth.TagName = "夫妻";
                relationshipDegreeCoupleYouth = _context.Tags.Update(relationshipDegreeCoupleYouth).Entity;
            }
            #endregion
            //  问题质量
            var questionQuality = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "QuestionQuality");
            var questionQualityTags = _context.Tags.Where(a => a.TagTypeId == questionQuality.Id && !a.IsDeleted).ToList();//  问题质量
            var questionQuality_A = questionQualityTags.FirstOrDefault(s => s.TagName.Trim() == "A级" && s.TagTypeId == questionQuality.Id);
            if (questionQuality_A == null)
            {
                questionQuality_A = _context.Tags.Add(new Tags.Tag() { TagName = "A级", Sort = 1, TagTypeId = questionQuality.Id }).Entity;
            }
            var questionQuality_C = questionQualityTags.FirstOrDefault(s => s.TagName.Trim() == "C级");
            if (questionQuality_C == null)
            {
                questionQuality_C = _context.Tags.Add(new Tags.Tag() { TagName = "C级", Sort = 2, TagTypeId = questionQuality.Id }).Entity;
            }
            var questionQuality_S = questionQualityTags.FirstOrDefault(s => s.TagName.Trim() == "S级");
            if (questionQuality_S == null)
            {
                questionQuality_S = _context.Tags.Add(new Tags.Tag() { TagName = "S级", Sort = 3, TagTypeId = questionQuality.Id }).Entity;
            }
            //  删掉之前的加上空格的数据
            var questionQuality_A1 = questionQualityTags.FirstOrDefault(s => s.TagName.Trim() == "A 级");
            if (questionQuality_A1 != null)
            {
                questionQuality_A1.IsDeleted = true;
                questionQuality_A1 = _context.Tags.Update(questionQuality_A1).Entity;
            }
            var questionQuality_C1 = questionQualityTags.FirstOrDefault(s => s.TagName.Trim() == "C 级");
            if (questionQuality_C1 != null)
            {
                questionQuality_C1.IsDeleted = true;
                questionQuality_C1 = _context.Tags.Update(questionQuality_C1).Entity;
            }
            var questionQuality_S1 = questionQualityTags.FirstOrDefault(s => s.TagName.Trim() == "S 级");
            if (questionQuality_S1 != null)
            {
                questionQuality_S1.IsDeleted = true;
                questionQuality_S1 = _context.Tags.Update(questionQuality_S1).Entity;
            }
            //  话题
            var topic = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "TopicTag");
            var topic1 = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "合租" && t.TagTypeId == topic.Id);
            if (topic1 == null)
            {
                topic1 = _context.Tags.Add(new Tags.Tag() { TagName = "合租", Sort = 1, TagTypeId = topic.Id }).Entity;
            }
            var topic2 = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "生活习惯" && t.TagTypeId == topic.Id);
            if (topic2 == null)
            {
                topic2 = _context.Tags.Add(new Tags.Tag() { TagName = "生活习惯", Sort = 2, TagTypeId = topic.Id }).Entity;
            }
            var topic3 = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "自拍" && t.TagTypeId == topic.Id);
            if (topic3 == null)
            {
                topic3 = _context.Tags.Add(new Tags.Tag() { TagName = "自拍", Sort = 3, TagTypeId = topic.Id }).Entity;
            }
            var topic4 = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "游戏" && t.TagTypeId == topic.Id);
            if (topic4 == null)
            {
                topic4 = _context.Tags.Add(new Tags.Tag() { TagName = "游戏", Sort = 4, TagTypeId = topic.Id }).Entity;
            }
            var topic5 = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "表白" && t.TagTypeId == topic.Id);
            if (topic5 == null)
            {
                topic5 = _context.Tags.Add(new Tags.Tag() { TagName = "表白", Sort = 5, TagTypeId = topic.Id }).Entity;
            }
            var topic6 = _context.Tags.IgnoreQueryFilters().FirstOrDefault(t => t.TagName == "婚后生活" && t.TagTypeId == topic.Id);
            if (topic6 == null)
            {
                topic6 = _context.Tags.Add(new Tags.Tag() { TagName = "婚后生活", Sort = 6, TagTypeId = topic.Id }).Entity;
            }
        }

    }
}
