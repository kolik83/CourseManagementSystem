using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    public class SeedData
    {
        public static Task SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (context.Professors.Count() == 0)
            {
                context.Professors.AddRange(
                new Professor
                {
                    Name = "Arie"
                },
                 new Professor
                 {
                     Name = "Yakov"
                 });
                context.SaveChanges();
            }


            if (context.Courses.Count() == 0)
            {
                Professor arie = context.Professors.FirstOrDefault();
                Professor yasha = context.Professors.FirstOrDefault(p => p.ProfessorId == 2);
                context.Courses.AddRange(
                new Course
                {
                    Name = "Algebra 1",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {
                    Name = "Algebra 2",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {
                    Name = "Physics 1",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {
                    Name = "Physics 2",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {
                    Name = "Physics 3",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {
                    Name = "Introduction to computer science",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {

                    Name = "Introduction to semiconductors",
                    Lessons = new List<Lesson>(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(150),
                    StudentCourses = new List<StudentCourse>()
                }
                );
                context.SaveChanges();
            }
            if (context.Students.Count() == 0)
            {
                context.Students.AddRange(
                new Student
                {
                    Name = "Nikolai Gukov",
                    StudentCourses = new List<StudentCourse>()
                },
                 new Student
                 {
                     Name = "Alexei Gukov",
                     StudentCourses = new List<StudentCourse>()
                 });
                context.SaveChanges();
            }

            return null;
        }
    }
}
