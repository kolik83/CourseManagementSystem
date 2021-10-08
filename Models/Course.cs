using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    

    public class Course
    {

        public long CourseId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }

    }
}
