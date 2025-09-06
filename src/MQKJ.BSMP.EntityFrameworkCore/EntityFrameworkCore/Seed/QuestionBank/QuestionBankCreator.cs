using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MQKJ.BSMP.DramaQuestionLibraryTypes;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.QuestionBank
{
    public class QuestionBankCreator
    {
        private readonly BSMPDbContext _context;

        public QuestionBankCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // CreateQuestionBank();
        }

        public   void CreateQuestionBank()
        {
            try
            {
                //   初始化
                //   循环题库规则表，种子不考虑排序的问题，当题目过少时排序的意义不大，当审核通过时进行排序
                var questionBanks = _context.QuestionBanks.IgnoreQueryFilters();
                var questionsOrigin = _context.Questions.Include(s => s.Scene).Include(s => s.QuestionTags).ThenInclude(t => t.Tag).ThenInclude(tt => tt.TagType).Include(q => q.QuestionBanks).Where(s => s.State == QuestionState.Online && !s.QuestionBanks.Any(q => q.QuestionId == s.Id)).ToList();//已审核且未入库
                if (questionBanks.Count() > 0 && questionsOrigin.Count > 30)//题库种子只执行一次，且待入库的题目数量不超过30道
                {
                    return;
                }
                var dramaQuestionLibrarys = _context.DramaQuestionLibrarys.Include(d => d.Drama).OrderBy(s => s.DramaId).ThenBy(s => s.Id).ToList();//   88个题库
                var secrets = (_context.Tags.Include(t => t.TagType)).Where(a => a.TagType.Code == "Privacy").ToList();//  私密度
                var secretIds = secrets.Select(s => s.Id).ToList();
                var relationDegreeTags = (_context.Tags.Include(t => t.TagType)).Where(a => a.TagType.Code == "RelationshipDegree").ToList();
                var relationDegreeTagDictionary = new Dictionary<int, int>();
                //  关系维度对照表
                foreach (var item in relationDegreeTags)
                {
                    var relationDegreeId = 0;
                    switch (item.TagName)
                    {
                        case "朋友":
                            relationDegreeId = (int)RelationDegree.Ordinary;
                            break;
                        case "男女":
                            relationDegreeId = (int)RelationDegree.Ambiguous;
                            break;
                        case "情侣":
                            relationDegreeId = (int)RelationDegree.Lovers;
                            break;
                        case "夫妻":
                            relationDegreeId = (int)RelationDegree.Couple;
                            break;
                    }
                    if (!relationDegreeTagDictionary.Keys.Any(k => k == relationDegreeId))
                    {
                        relationDegreeTagDictionary.Add(relationDegreeId, item.Id);
                    }
                }
                //var secretDefault = secrets.FirstOrDefault(s => s.TagName.Trim() == "普通");
                //var secretPrivate = secrets.FirstOrDefault(s => s.TagName.Trim() == "私密");
                var questionQualitys = (_context.Tags.Include(t => t.TagType)).Where(a => a.TagType.Code == "QuestionQuality").ToList();//  问题质量
                var questionQualityIds = questionQualitys.Select(s => s.Id);
                var questionQuality_A = questionQualitys.FirstOrDefault(s => s.TagName.Trim() == "A 级");
                var questionQuality_C = questionQualitys.FirstOrDefault(s => s.TagName.Trim() == "C 级");
                var questionQuality_S = questionQualitys.FirstOrDefault(s => s.TagName.Trim() == "S 级");
                // PS: 一般来说一道题属于一个新手题库+10个常规题库
                //  循环题库 
                foreach (var lib in dramaQuestionLibrarys)
                {
                    var libRelationDegreeTagId = relationDegreeTagDictionary[(int)lib.Drama.RelationDegree];
                    var questionBank = questionBanks.Where(q => q.DramaQuestionLibraryId == lib.Id).ToList();//题库

                    var libQuestions = questionsOrigin.Where(q => q.HomeField == lib.Drama.Gender && q.QuestionTags.Any(qt => qt.TagId == libRelationDegreeTagId) && questionBank.Any(qb => qb.QuestionId != q.Id)); //  限制条件：不能重复
                    var repeatSecretCountPara = 0;//  私密度
                    //var repeatSceneCountPara = 3;
                    //var repeatTopicCountPara = 3;
                    //var repeatComplexityCountPara = 2;
                    switch (lib.Drama.RelationDegree)
                    {
                        case RelationDegree.Ambiguous:
                            repeatSecretCountPara = 3; break;
                        case RelationDegree.Lovers:
                            repeatSecretCountPara = 4; break;
                        case RelationDegree.Ordinary:
                            repeatSecretCountPara = 5; break;
                        default:
                            repeatSecretCountPara = 0;
                            break;
                    }
                    var repeatSecretCountPara1 = repeatSecretCountPara;//新加 为了取消警告
                    //  循环符合题库条件的问题集合
                    foreach (var question in libQuestions)
                    {
                        if (lib.Code.Contains("Noob") && question.QuestionTags.Any(s => s.TagId != questionQuality_S.Id))   //  新手题库，必须为S级
                        {
                            continue;
                        }
                        var questionQuality = question.QuestionTags.FirstOrDefault(s => s.Tag.TagType.Code == "QuestionQuality");
                        var questionTagIds = question.QuestionTags.Select(s => s.TagId);
                        var secret = secrets.FirstOrDefault(f => f.Id == questionTagIds.Intersect(secretIds).FirstOrDefault());
                        //var repeatSecretCount = 0; var repeatSceneCount = 0; var repeatTopicCount = 0; var repeatComplexityCount = 0;//    声明重复变量计数
                        //  计算重复变量
                        //if (libQuestions.)
                        //{

                        //}
                        //var questionBankItem = new QuestionBanks.QuestionBank() { QuestionId = question.Id, DramaQuestionLibraryId = lib.Id, LastId = questionBank.Count > 1 ? questionBank.LastOrDefault().Id : 0 };


                        //_context.QuestionBanks.Add(questionBankItem);
                        //await _context.SaveChangesAsync();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("创建题库出错啦：CreateQuestionBank：错误信息：" + ex.Message);
            }
        }



    }
}
