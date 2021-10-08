using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public class EFProfessorRepository:GenericRepository<Professor>
    {
        public EFProfessorRepository(DataContext context) : base(context)
        {

        }

        public async Task<StudentCourse> EnrolStudentInCourseAsync(string studentName, string courseName)   
        {
            try
            {
                var student = await GetStudentWithDetailsAsync(studentName);
                if (student == null)
                    return null;
                var course = await GetCourseWithDetailsAsync(courseName);
                if (course == null)
                    return null;
                var studentCourses = await GetStudentCourseWithDetailsAsync(studentName, courseName);
                if (studentCourses == null)
                {
                    StudentCourse newStudentCourse = new StudentCourse
                    {
                        CourseId = course.CourseId,
                        StudentId = student.StudentId
                    };
                    await _context.StudentsCourses.AddAsync(newStudentCourse);
                    student.StudentCourses.ToList().Add(newStudentCourse);
                    course.StudentCourses.ToList().Add(newStudentCourse);
                    await _context.SaveChangesAsync();
                    return newStudentCourse;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<StudentCourse> UnsubscribeStudentFromCourseAsync(string studentName, string courseName)
        {
            try
            {
                var student = await GetStudentWithDetailsAsync(studentName);
                if (student == null)
                    return null;
                var course = await GetCourseWithDetailsAsync(courseName);
                if (course == null)
                    return null;
                var studentCourse = await GetStudentCourseWithDetailsAsync(studentName, courseName);
                if (studentCourse == null)
                    return null;
                List<Attendance> attendances = await _context.Attendances
                    .Where(a => a.StudentId == student.StudentId && a.Lesson.CourseId == course.CourseId).ToListAsync();
                if (attendances.Count != 0)
                {

                    _context.StudentsCourses.Remove(studentCourse);
                    _context.Attendances.RemoveRange(attendances);
                    student.StudentCourses.ToList().Remove(studentCourse);
                    course.StudentCourses.ToList().Remove(studentCourse);
                    await _context.SaveChangesAsync();
                    return studentCourse;
                }
                return null;
            }
            catch (Exception)
            {

                return null; ;
            }
        }
        public async Task<List<Attendance>> GetAttendanceOfLesson( int lessonId)
        {
            try
            {
                var attendances = await _context.Attendances.Where(l => l.LessonId == lessonId).Include(l => l.Student).ToListAsync();
                return (attendances != null) ? attendances : new List<Attendance>();
            }
            catch (Exception)
            {

                return null;
            }
        }
       
    }
}
