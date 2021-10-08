using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    public class Student
    {
        [Key]
        public long StudentId { get; set; }
        public string Name { get; set; }

        public IEnumerable<StudentCourse> StudentCourses { get; set; }

    }
}
