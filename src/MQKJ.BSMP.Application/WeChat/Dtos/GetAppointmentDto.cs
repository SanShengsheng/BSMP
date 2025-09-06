using MQKJ.BSMP.Appointments;
using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetAppointmentDto
    {
        public AppointmentType AppointmentType { get; set; }


        public TaskType GameTaskType { get; set; }
    }
}
