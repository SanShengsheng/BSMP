using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.Questions.Dtos
{
    public class QuestionStatisicsOutput
    {
        /// <summary>
        /// 问题总数
        /// </summary>
        public double QuestionCount { get; set; }


        /// <summary>
        /// 上线的问题数量
        /// </summary>
        public int OnlineQuestionCount { get; set; }

        /// <summary>
        /// 下线的问题数量
        /// </summary>
        public int OfflineQuesitonCount { get; set; }

        /// <summary>
        /// 男生题占比
        /// </summary>
        public int MaleQuestionCount { get; set; }

        /// <summary>
        /// 女生题占比
        /// </summary>
        public int FemaleQuestionCount { get; set; }

        public List<QuestionRelationDegree> QuestionRelationDegreeList { get; set; }

        public List<QuestionPrivacy> QuestionPrivacieList { get; set; }

        public List<QuestionStatistics> QuestionStatisticList { get; set; }

        public List<QuestionScene>  QuestionScenes { get; set; }

        /// <summary>
        /// 问题私密度占比
        /// </summary>
        public int PrivacyProportion { get; set; }

        /// <summary>
        /// 年龄占比
        /// </summary>
        public int AgeProportion { get; set; }

        /// <summary>
        /// 各场景占比
        /// </summary>
        public int SceneProportion { get; set; }

        public QuestionStatisicsOutput()
        {
            QuestionScenes = new List<QuestionScene>();

            QuestionPrivacieList = new List<QuestionPrivacy>();

            QuestionRelationDegreeList = new List<QuestionRelationDegree>();

            QuestionStatisticList = new List<QuestionStatistics>();
        }

    }

    /// <summary>
    /// 问题关系度
    /// </summary>
    public class QuestionRelationDegree: QuestionStatistics
    {

    }

    /// <summary>
    /// 问题私密度
    /// </summary>
    public class QuestionPrivacy: QuestionStatistics
    {

    }
    /// <summary>
    /// 问题场景
    /// </summary>
    public class QuestionScene
    {
        public string SceneName { get; set; }

        public double QuestionScenePercent { get; set; }
    }
    public class QuestionStatistics
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string QuestionTagName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int QuestionTagCount { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public int QuestionPercent { get; set; }
    }
}
