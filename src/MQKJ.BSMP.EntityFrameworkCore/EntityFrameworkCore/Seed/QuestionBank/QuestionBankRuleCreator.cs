using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Dramas;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.QuestionBankRules;
using MQKJ.BSMP.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.QuestionBank
{
    public class QuestionBankRuleCreator
    {
        private readonly BSMPDbContext _context;

        public QuestionBankRuleCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
           // CreateQuestionBankRule();
        }

        private void CreateQuestionBankRule()
        {
            var QuestionBankRules = _context.QuestionBankRules.IgnoreQueryFilters();
            var dramaQuestionLibrarys = _context.DramaQuestionLibrarys.Include(d => d.Drama).OrderBy(s => s.DramaId).ThenBy(s => s.Id).ToList();//   88个题库
            var topics = (_context.Tags.Include(t => t.TagType)).Where(a => a.TagType.Code == "TopicTag").ToList();//   话题
            var scenesOriginal = (_context.Scenes.IgnoreQueryFilters().ToListAsync()).Result;// 场景
            var secrets = (_context.Tags.Include(t => t.TagType)).Where(a => a.TagType.Code == "Privacy").ToList();//  私密度
            var secretDefault = secrets.FirstOrDefault(s => s.TagName.Trim() == "普通");
            var secretPrivate = secrets.FirstOrDefault(s => s.TagName.Trim() == "私密");
            var questionQualitys = (_context.Tags.Include(t => t.TagType)).Where(a => a.TagType.Code == "QuestionQuality").ToList();//  问题质量
            var questionQuality_A = questionQualitys.FirstOrDefault(s => s.TagName.Trim() == "A 级");
            var questionQuality_C = questionQualitys.FirstOrDefault(s => s.TagName.Trim() == "C 级");
            var questionQuality_S = questionQualitys.FirstOrDefault(s => s.TagName.Trim() == "S 级");


            #region 规则
            var scenes = scenesOriginal;
            //  循环题库
            foreach (var item in dramaQuestionLibrarys)
            {
                var dramaIndex = dramaQuestionLibrarys.IndexOf(item);
                var rules = new List<QuestionBankRule>();
                var sceneIndex = 0;
                //  环境变量-场景（鲶鱼效应）
                if (dramaIndex != 0 && dramaQuestionLibrarys.ElementAtOrDefault(dramaIndex - 1).DramaId == item.DramaId)
                {
                    scenes = scenesOriginal.OrderBy(s => Guid.NewGuid()).ToList();
                }
              
                //  插入规则
                foreach (var topic in topics)
                {
                    var index = topics.IndexOf(topic);
                    var rule = new QuestionBankRule();
                    var secret = new Tags.Tag();
                    var questionQuality = new Tags.Tag();
                    rule.TopicId = topic.Id;
                    rule.Sort = index;
                    rule.DramaQuestionLibraryId = item.Id;
                    //  私密度 |    场景 |  话题  |  问题难度    
                    //  循环场景
                    if (sceneIndex >= scenes.Count - 1)
                    {
                        sceneIndex = 0;
                    }
                    else
                    {
                        sceneIndex++;
                    }
                    var scene = scenes.ElementAtOrDefault(sceneIndex);
                    rule.SceneId = scene.Id;

                    //  私密问题频率
                    if (index != 0)
                    {
                        if (item.Drama.RelationDegree == RelationDegree.Ordinary && index % 5 == 0)
                        {
                            secret = secretPrivate;
                            rule.SecretId = secretPrivate.Id;
                        }
                        else if (item.Drama.RelationDegree == RelationDegree.Ambiguous && index % 3 == 0)
                        {
                            secret = secretPrivate;
                            rule.SecretId = secretPrivate.Id;
                        }
                        else if (item.Drama.RelationDegree == RelationDegree.Lovers && index % 4 == 0)
                        {
                            secret = secretPrivate;
                            rule.SecretId = secretPrivate.Id;
                        }
                        else
                        {
                            rule.SecretId = secretDefault.Id;
                            secret = secretDefault;
                        }
                    }
                    else
                    {
                        rule.SecretId = secretDefault.Id;
                        secret = secretDefault;
                    }
                    //  难易度 相连三道题目不出现两个或以上A级别的难度
                    if (item.Drama.DramaType != DramaTypeEnum.新手)
                    {
                        if (index % 3 == 0)
                        {
                            rule.ComplexityId = questionQuality_A.Id;
                            questionQuality = questionQuality_A;
                        }
                        else
                        {
                            rule.ComplexityId = questionQuality_C.Id;
                            questionQuality = questionQuality_C;
                        }
                    }
                    else
                    {
                        rule.ComplexityId = questionQuality_S.Id;
                        questionQuality = questionQuality_S;
                    }

                    rule.Code = "S" + secret.TagName + "_SC" + scene.SceneName + "_T" + topic.TagName + "_P" + questionQuality.TagName + "_" + item.Code;//   规则为："S"+secret（私密度）+"C"+scene（场景编号）+"H"+topic（话题编号）+"Q"+complexity（问题质量）+“_”+Code(dramaQuestionLibrary表下的code字段)
                    var questionBankRule = QuestionBankRules.FirstOrDefault(r => r.Code == rule.Code);
                    if (questionBankRule == null)
                    {
                        rules.Add(rule);
                    }
                }
                _context.QuestionBankRules.AddRangeAsync(rules);
                #endregion
            }

        }
    }
}
