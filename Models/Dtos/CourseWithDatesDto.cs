using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Dtos
{
    public class CourseWithDatesDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DayOfWeekAndHoursDto> DaysOfWeekAndHours { get; set; }
    }
}
