
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
            
        }
        public IQueryable<T> FindAll()
        {
            return  _context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseWithDetailsAsync(string name)
        {
            return await _context.Courses.Include(c => c.StudentCourses).FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<StudentCourse> GetStudentCourseWithDetailsAsync(string studentName, string courseName)
        {
            return await _context.StudentsCourses.Include(sc => sc.Student).Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.Student.Name == studentName && sc.Course.Name == courseName);
        }
        public async Task<Student> GetStudentWithDetailsAsync(string name)
        {
            return await _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
