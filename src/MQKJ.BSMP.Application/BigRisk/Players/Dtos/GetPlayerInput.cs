using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Players;
using System;

namespace MQKJ.BSMP.Players.Dtos
{
    public class GetPlayersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        /// <summary>
        /// 模糊搜索使用的关键字
        /// </summary>
        //public string Filter { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        public int Gender { get; set; }

        /// <summary>
        /// 年龄段
        /// </summary>
        public int AgeRange { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }



        //// custom codes 

        //// custom codes end

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }


    }

    public class GetPlayerInput
    {
        public int? TenantId { get; set; }
        public string OpenId { get; set; }
        public string HeaderUrl { get; set; }
        public string NickName { get; set; }

        public string DeviceModel { get; set; }

        public string DeviceSystem { get; set; }

        public string UnionId { get; set; }

        public Guid? InviterId { get; set; }
    }
}
