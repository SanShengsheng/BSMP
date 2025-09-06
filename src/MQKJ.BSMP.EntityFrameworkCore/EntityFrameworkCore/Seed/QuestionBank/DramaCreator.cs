using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Dramas;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.QuestionBank
{
    public class DramaCreator
    {
        private readonly BSMPDbContext _context;

        public DramaCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDrama();
        }

        public void CreateDrama()
        {
            var dramas=_context.Dramas.IgnoreQueryFilters().Count();
            if (dramas>=88)
            {
                return;
            }
            #region  男

            #region 男 
            // 男*普通
            var manNormal = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManNormal");
            if (manNormal == null)
            {
                manNormal = _context.Dramas.Add(new Dramas.Drama { Name = "男*普通", Code = "ManNormal", Gender = QuestionGender.M, RelationDegree = RelationDegree.Ordinary, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }
            // 男*暧昧
            var manAmbiguous = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManAmbiguous");
            if (manAmbiguous == null)
            {
                manAmbiguous = _context.Dramas.Add(new Dramas.Drama { Name = "男*暧昧", Code = "ManAmbiguous", Gender = QuestionGender.M, RelationDegree = RelationDegree.Ambiguous, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }
            // 男*情侣
            var manLovers = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManLovers");
            if (manLovers == null)
            {
                manLovers = _context.Dramas.Add(new Dramas.Drama { Name = "男*情侣", Code = "ManLovers", Gender = QuestionGender.M, RelationDegree = RelationDegree.Lovers, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }
            // 男*夫妻
            var manCouple = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManCouple");
            if (manCouple == null)
            {
                manNormal = _context.Dramas.Add(new Dramas.Drama { Name = "男*夫妻", Code = "ManCouple", Gender = QuestionGender.M, RelationDegree = RelationDegree.Couple, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }


            #endregion

            #region 男*新手

            // 男*普通
            var manNormalNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManNormalNoob");
            if (manNormalNoob == null)
            {
                manNormalNoob = _context.Dramas.Add(new Dramas.Drama { Name = "男*普通*新手", Code = "ManNormalNoob", Gender = QuestionGender.M, RelationDegree = RelationDegree.Ordinary, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }
            // 男*暧昧
            var manAmbiguousNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManAmbiguousNoob");
            if (manAmbiguousNoob == null)
            {
                manAmbiguousNoob = _context.Dramas.Add(new Dramas.Drama { Name = "男*暧昧*新手", Code = "ManAmbiguousNoob", Gender = QuestionGender.M, RelationDegree = RelationDegree.Ambiguous, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }
            // 男*情侣
            var manLoversNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManLoversNoob");
            if (manLoversNoob == null)
            {
                manLoversNoob = _context.Dramas.Add(new Dramas.Drama { Name = "男*情侣*新手", Code = "ManLoversNoob", Gender = QuestionGender.M, RelationDegree = RelationDegree.Lovers, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }
            // 男*夫妻
            var manCoupleNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "ManCoupleNoob");
            if (manCoupleNoob == null)
            {
                manCoupleNoob = _context.Dramas.Add(new Dramas.Drama { Name = "男*夫妻*新手", Code = "ManCoupleNoob", Gender = QuestionGender.M, RelationDegree = RelationDegree.Couple, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }

            #endregion
            #endregion

            #region  女

            #region 女 普通
            // 女*普通
            var womanNormal = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanNormal");
            if (womanNormal == null)
            {
                womanNormal = _context.Dramas.Add(new Dramas.Drama { Name = "女*普通", Code = "WomanNormal", Gender = QuestionGender.F, RelationDegree = RelationDegree.Ordinary, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }
            // 女*暧昧
            var womanAmbiguous = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanAmbiguous");
            if (womanAmbiguous == null)
            {
                womanAmbiguous = _context.Dramas.Add(new Dramas.Drama { Name = "女*暧昧", Code = "WomanAmbiguous", Gender = QuestionGender.F, RelationDegree = RelationDegree.Ambiguous, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }
            // 女*情侣
            var womanLovers = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanLovers");
            if (womanLovers == null)
            {
                womanLovers = _context.Dramas.Add(new Dramas.Drama { Name = "女*情侣", Code = "WomanLovers", Gender = QuestionGender.F, RelationDegree = RelationDegree.Lovers, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }
            // 女*夫妻
            var womanCouple = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanCouple");
            if (womanCouple == null)
            {
                womanCouple = _context.Dramas.Add(new Dramas.Drama { Name = "女*夫妻", Code = "WomanCouple", Gender = QuestionGender.F, RelationDegree = RelationDegree.Couple, DramaType = DramaTypeEnum.正常 }).Entity;
                _context.SaveChanges();
            }


            #endregion

            #region 女*新手

            // 女*普通
            var womanNormalNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanNormalNoob");
            if (womanNormalNoob == null)
            {
                womanNormalNoob = _context.Dramas.Add(new Dramas.Drama { Name = "女*普通*新手", Code = "WomanNormalNoob", Gender = QuestionGender.F, RelationDegree = RelationDegree.Ordinary, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }
            // 女*暧昧
            var womanAmbiguousNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanAmbiguousNoob");
            if (womanAmbiguousNoob == null)
            {
                womanAmbiguousNoob = _context.Dramas.Add(new Dramas.Drama { Name = "女*暧昧*新手", Code = "WomanAmbiguousNoob", Gender = QuestionGender.F, RelationDegree = RelationDegree.Ambiguous, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }
            // 女*情侣
            var womanLoversNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanLoversNoob");
            if (womanLoversNoob == null)
            {
                womanLoversNoob = _context.Dramas.Add(new Dramas.Drama { Name = "女*情侣*新手", Code = "WomanLoversNoob", Gender = QuestionGender.F, RelationDegree = RelationDegree.Lovers, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }
            // 女*夫妻
            var womanCoupleNoob = _context.Dramas.IgnoreQueryFilters().FirstOrDefault(r => r.Code == "WomanCoupleNoob");
            if (womanCoupleNoob == null)
            {
                womanCoupleNoob = _context.Dramas.Add(new Dramas.Drama { Name = "女*夫妻*新手", Code = "WomanCoupleNoob", Gender = QuestionGender.F, RelationDegree = RelationDegree.Couple, DramaType = DramaTypeEnum.新手 }).Entity;
                _context.SaveChanges();
            }

            #endregion
            #endregion
        }
    }
}
