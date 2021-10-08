using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    public class StudentCourse
    {
        [Key]
        public long StudentCourseId { get; set;}
        public long CourseId { get; set; }
        public Course Course { get; set; }
        public long StudentId { get; set;}
        public Student Student { get; set; }

    }
}
