using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Dtos
{
    public class LessonDto
    {
        public long LessonId { get; set; }
        public int StartHour { get; set; }
        public string CourseName { get; set; }
        public int EndHour { get; set; }
        public DateTime Date { get; set; }
    }
}
