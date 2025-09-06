using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Professions.Dtos
{
    public class UpdateChangeProfessionDataInput
    {
        public int FamilyId { get; set; }

        public int ProductId { get; set; }

        public Guid PlayerId { get; set; }
        public double PayAmount { get; set; }
    }
}
