using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Professions.Dtos
{
    public class QueryChangeResultInput
    {
        /// <summary>
        ///     微信支付订单号（二选一）
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///     商户系统的订单号，与请求一致（二选一）
        /// </summary>
        public string OutTradeNo { get; set; }

        public int FamilyId { get; set; }

        public int ProfessionId { get; set; }

        public int OtherProfessionId { get; set; }

        public int BabyId { get; set; }

        public Guid PlayerId { get; set; }

        public int ProductId { get; set; }


        public Guid ReceiverId { get; set; }

        public bool IsTest { get; set; }
    }
}
