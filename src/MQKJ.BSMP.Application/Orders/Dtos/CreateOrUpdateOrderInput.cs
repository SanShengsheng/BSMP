

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Orders;

namespace MQKJ.BSMP.Orders.Dtos
{
    public class CreateOrUpdateOrderInput
    {
        [Required]
        public OrderEditDto Order { get; set; }

    }
}