using AutoMapper;
using CourseManagementSystem.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public class EFStudentRepository:GenericRepository<Student>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public EFStudentRepository(DataContext context, UserManager<IdentityUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            try
            {
                var students = await FindAll().ToListAsync();
                return (students != null) ? students : new List<Student>();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Student> NewStudent(UserDto userDto)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Student newStudent = new Student { Name = userDto.Name };
                Create(newStudent);
                IdentityUser identityUser = await CreateUserIdentity(userDto.Name, userDto.Password, true);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return newStudent;
            }
            catch (Exception)
            {

                await transaction .RollbackAsync();
                return null;
            }
        }

        public async Task<StudentDto> DeleteStudent(StudentDto studentDto)
        {
            try
            {
                Student student = await FindByCondition(s => s.Name == studentDto.Name).FirstOrDefaultAsync();
                List<StudentCourse> studentCourses = await _context.StudentsCourses.Where(sc => sc.StudentId == student.StudentId).ToListAsync();
                List<Attendance> attendances = await _context.Attendances.Where(a => a.StudentId == student.StudentId).ToListAsync();

                _context.Attendances.RemoveRange(attendances);
                _context.StudentsCourses.RemoveRange(studentCourses);
                _context.Students.Remove(student);

                IdentityUser identityUser = await _userManager.FindByNameAsync(student.Name.Replace(" ", String.Empty).ToLower());
                if(identityUser == null)
                    return null;
                else
                {
                    using var transaction =await _context.Database.BeginTransactionAsync();
                    try
                    {
                        await _userManager.DeleteAsync(identityUser);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return studentDto;
                    }
                    catch (Exception)
                    {
                        await transaction.CommitAsync();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Attendance> AttendLessonAsync(AttendanceDto attendanceDto)
        {
            try
            {
                Student student = await GetStudentWithDetailsAsync(attendanceDto.StudentName);
                if (student == null)
                    return null;
                Lesson lesson = _context.Lessons.Include(l => l.Course).FirstOrDefault(l => l.LessonId == attendanceDto.LessonId);
                if (lesson == null)
                    return null;
                StudentCourse studentCourseOfLesson = null;
                foreach (StudentCourse studentCourse in student.StudentCourses)
                {
                    if (studentCourse.CourseId == lesson.CourseId)
                    {
                        studentCourseOfLesson = studentCourse;
                        break;
                    }
                }
                if (studentCourseOfLesson == null)
                    return null;
                Attendance attendance = new Attendance()
                {
                    StudentId = student.StudentId,
                    LessonId = attendanceDto.LessonId,
                    Attended = (attendanceDto.Reason == null) ? true : false,
                    Reason = attendanceDto.Reason
                };
                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();
                return attendance;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<IdentityUser> CreateUserIdentity(string name, string password, bool isStudent)
        {
            try
            {
                string userName = name.Replace(" ", String.Empty).ToLower();
                string email = userName + "@gmail.com";
                IdentityUser user = new IdentityUser { UserName = userName, Email = email };
                IdentityResult result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    return null;
                }
                string role = isStudent ? "student" : "professor";
                result = await _userManager.AddToRoleAsync(user, "student");
                if (result.Succeeded)
                    return user;
                return null;
            }
            catch (Exception)
            {

                return null; ;
            }
        }
    }
}
