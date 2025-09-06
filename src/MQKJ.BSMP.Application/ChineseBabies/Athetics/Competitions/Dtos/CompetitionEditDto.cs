using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class CompetitionEditDto : IAddModel<Competition, Guid>, IEditModel<Competition, Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }
    }
}
