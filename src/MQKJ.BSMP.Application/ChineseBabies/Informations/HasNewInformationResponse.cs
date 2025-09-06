using System;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies.Message.Dtos
{
    public class HasNewInformationResponse
    {
        public bool HasMessage { get; set; }
        /// <summary>
        /// 弹框消息
        /// </summary>
        public List<HasNewInformationResponseMessage> PopoutMessage { get; set; }

        /// <summary>
        /// 是否解散
        /// </summary>
        public bool IsDismiss { get; set; }

    }
    /// <summary>
    /// 消息个数
    /// </summary>
    public class HasNewInformationResponseMessage
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public SystemInformationType SystemInformationType { get; set; }

        //[NotMapped]
        //public bool IsShow { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        //private string _description { get; set; }
        public string Description
        {
            get
            {
                var result = "";
                switch (SystemInformationType)
                {
                    case SystemInformationType.PayOff:
                        result = "grant-wages";
                        break;
                    case SystemInformationType.EventCompleted:
                        result = "event-end";
                        break;
                    case SystemInformationType.BirthDay:
                        result = "birthday";
                        break;
                    case SystemInformationType.BabyPropertyUpdate:
                        result = "baby-property-update";
                        break;
                    case SystemInformationType.Recharge:
                        result = "recharge";
                        break;
                    case SystemInformationType.DismissFamily:
                        result = "dismiss-init";
                        break;
                    case SystemInformationType.DismissFamilySuccess:
                        result = "dismissed";
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        public string Remark { get; set; }

        public int? BabyEventId { get; set; }
        public string Image { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }

    public class HasNewInformationResponseMessageComparer : IEqualityComparer<Information>
    {
        public bool Equals(Information p1, Information p2)
        {
            if (p1 == null)
                return p2 == null;
            return p1.BabyEventId == p2.BabyEventId && p1.SystemInformationType == p2.SystemInformationType;
        }

        public int GetHashCode(Information p)
        {
            if (p == null)
                return 0;
            return p.BabyEventId.GetHashCode();
        }
    }
}


