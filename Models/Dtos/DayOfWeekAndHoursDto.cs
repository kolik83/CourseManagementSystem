using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Dtos
{
    public class DayOfWeekAndHoursDto
    {
        public string DayOfWeek { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}
