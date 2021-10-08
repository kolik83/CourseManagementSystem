using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public class EFAttendanceRepository : GenericRepository<Attendance>
    {
        public EFAttendanceRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<Attendance>> GetAllAttendancesOfStudentInCourse(long studentId, Course course)
        {
            try
            {
                List<Lesson> lessons = await _context.Lessons.Where(l => l.CourseId == course.CourseId).ToListAsync();
                List<long> lessonsId = new List<long>();
                foreach (Lesson lesson in lessons)
                {
                    lessonsId.Add(lesson.LessonId);
                }
                var attendances =  await _context.Attendances.Include(a => a.Student)
                    .Include(a => a.Lesson)
                    .Where(a => a.StudentId == studentId && lessonsId.Contains(a.LessonId)).ToListAsync();
                return (attendances != null) ? attendances : new List<Attendance>();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
