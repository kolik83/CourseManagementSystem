using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public class EFLessonRepository:GenericRepository<Lesson>
    {
        public EFLessonRepository(DataContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            try
            {
                var lessons = await FindAll().ToListAsync();
                return (lessons != null) ? lessons : new List<Lesson>();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<List<Lesson>> GetCourseLessonsAsync(string courseName)
        {
            try
            {
                Course course = await _context.Courses.FirstOrDefaultAsync(c => c.Name == courseName);
                if (course == null)
                    return null;
                var lessons =  await FindByCondition(l => l.CourseId == course.CourseId).ToListAsync();
                return (lessons != null) ? lessons : new List<Lesson>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
