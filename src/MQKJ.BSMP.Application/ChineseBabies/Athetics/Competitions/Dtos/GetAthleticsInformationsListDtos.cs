using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class GetAthleticsInformationsListDtos
    {
        public int FamilyId { get; set; }

        public string Content { get; set; }

        public AthleticsInformationType AthleticsInformationType { get; set; }

        public GetAthleticsInformationsPlayer Receiver { get; set; }
    }

    [AutoMapFrom(typeof(Player))]
    public class GetAthleticsInformationsPlayer
    {

        public string NickName { get; set; }

        public string HeadUrl { get; set; }
    }
}
