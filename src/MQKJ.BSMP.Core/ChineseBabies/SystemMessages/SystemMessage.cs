using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.SystemMessages
{
    [Table("SystemMessages")]
    public class SystemMessage : FullAuditedEntity
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// ֪ͨ���
        /// </summary>
        public NoticeType NoticeType { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public PeriodType PeriodType { get; set; }
        /// <summary>
        /// ���ȼ���1~100,100Ϊ���
        /// </summary>
        public int PriorityLevel { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime ExprieDateTime { get; set; }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int? Code { get; set; }
        /// <summary>
        /// ������Ĭ��Ϊ1
        /// </summary>
        [DefaultValue(1)]
        public int Count { get; set; }
        /// <summary>
        /// ���ڣ����
        /// </summary>
        public int Period { get; set; }

     
    }

    public enum NoticeType
    {
        All = 1,//�����û�
    }

    public enum PeriodType
    {
        NeverStop = 1,//�Ӳ�ֹͣ
        Minute = 2,
    }

}