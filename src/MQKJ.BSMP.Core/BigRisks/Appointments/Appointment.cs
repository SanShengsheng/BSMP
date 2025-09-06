using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Appointments
{
    public class Appointment
    {
        /// <summary>
        /// 简单的
        /// </summary>
        public List<AppointmentContent> Easys { get; set; }

        public List<AppointmentContent> Normals { get; set; }

        public List<AppointmentContent> Purgatorys { get; set; }

    }

    public class AppointmentContent
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
