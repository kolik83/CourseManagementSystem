using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    public class Lesson
    {
        [Key]
        public long LessonId { get; set; }
        public long CourseId { get; set; }
        public Course Course { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public DateTime Date { get; set; }
    }
}
