using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.TagTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.TagType
{
    public class TagTypeCreator
    {
        private readonly BSMPDbContext _context;

        public TagTypeCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateTagType();
        }

        private void CreateTagType()
        {
            // 年龄段
            var age = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code== "AgeRange");
            if (age == null)
            {
                age = _context.TagType.Add(new TagTypes.TagType(StaticTagTypeNames.Age,"AgeRange") ).Entity;
                _context.SaveChanges();
            }
            // 关系程度
            var relationshipDegree = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code== "RelationshipDegree");
            if (relationshipDegree == null)
            {
                relationshipDegree = _context.TagType.Add(new TagTypes.TagType(StaticTagTypeNames.RelationshipDegree, "RelationshipDegree")).Entity;
                _context.SaveChanges();
            }
            // 话题名称
            var topicName = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code== "TopicTag");
            if (topicName == null)
            {
                topicName = _context.TagType.Add(new TagTypes.TagType(StaticTagTypeNames.TopicName, "TopicTag")).Entity;
                _context.SaveChanges();
            }

            // 私密度
            var privacy = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code =="Privacy");
            if (privacy == null)
            {
                privacy = _context.TagType.Add(new TagTypes.TagType(StaticTagTypeNames.Privacy, "Privacy")).Entity;
                _context.SaveChanges();
            }

            // 问题质量
            var questionQuality = _context.TagType.IgnoreQueryFilters().FirstOrDefault(r => r.Code== "QuestionQuality");
            if (questionQuality == null)
            {
                questionQuality = _context.TagType.Add(new TagTypes.TagType(StaticTagTypeNames.QuestionQuality, "QuestionQuality")).Entity;
                _context.SaveChanges();
            }
        }
        
    }
}
