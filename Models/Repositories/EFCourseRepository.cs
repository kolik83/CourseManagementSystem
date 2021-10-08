using AutoMapper;
using CourseManagementSystem.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public class EFCourseRepository : GenericRepository<Course>
    {
        public EFCourseRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                var courses =  await FindAll().ToListAsync();
                return (courses != null)?courses:new List<Course>();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Course> GetCourseByNameAsync(string name)
        {
            try
            {
                return await FindByCondition(c => c.Name.Equals(name)).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<List<StudentCourse>> GetStudentCoursesByCourseNameWithDetailsAsync(string courseName)
        {
            try
            {
                List<StudentCourse> studentCourses = await _context.StudentsCourses.
                    Where(sc => sc.Course.Name.Equals(courseName)).Include(sc => sc.Student).ToListAsync();
                return (studentCourses != null) ? studentCourses : new List<StudentCourse>();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<List<Lesson>> SetDaysOfCourseAsync(Course course, List<DayOfWeekAndHoursDto> daysOfWeekAndHours)
        {
            try
            {
                List<Lesson> result = new List<Lesson>();
                if (isDaysOfWeekAndHoursAreInvalid(daysOfWeekAndHours))
                    return null;
                Course newCourse = new Course
                {
                    Name = course.Name,
                    Lessons = new List<Lesson>(),
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    StudentCourses = new List<StudentCourse>()
                };
                foreach (var dayOfWeekAnHours in daysOfWeekAndHours)
                {
                    result.AddRange(addLessonsInThisDay(course, dayOfWeekAnHours));
                }
                newCourse.Lessons = result;
                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {

                return null ;
            }
        }
        private List<Lesson> addLessonsInThisDay(Course course, DayOfWeekAndHoursDto dayOfWeekAnHours)
        {
            List<Lesson> result = new List<Lesson>();
            string dayOfWeek = dayOfWeekAnHours.DayOfWeek;
            int dayOfWeekInt = convertDayOfWeekFromStringToint(dayOfWeek);

            int startHour = dayOfWeekAnHours.StartHour;
            int endHour = dayOfWeekAnHours.EndHour;
            DateTime startDate = course.StartDate;
            DateTime nextLessonDate = startDate;
            DateTime endDate = course.EndDate;

            int daysUntilDayOfWeek = (dayOfWeekInt - (int)nextLessonDate.DayOfWeek + 7) % 7;
            nextLessonDate = nextLessonDate.AddDays(daysUntilDayOfWeek);

            while (nextLessonDate < endDate)
            {
                Lesson newLesson = new Lesson();
                newLesson.CourseId = course.CourseId;
                newLesson.StartHour = startHour;
                newLesson.EndHour = endHour;
                DateTime newDateTime = new DateTime(nextLessonDate.Year, nextLessonDate.Month, nextLessonDate.Day);
                newLesson.Date = newDateTime;
                _context.Lessons.Add(newLesson);
                result.Add(newLesson);
                nextLessonDate = nextLessonDate.AddDays(7);
            }
            return result;
        }

        private bool isDaysOfWeekAndHoursAreInvalid (List<DayOfWeekAndHoursDto> daysOfWeekAndHours)
        {
            List<string> days = new List<string>() { "sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday" };
            foreach (DayOfWeekAndHoursDto dayOfWeekandHours in daysOfWeekAndHours)
            {
    
                if (!days.Contains(dayOfWeekandHours.DayOfWeek))
                    return true;
                int startHour = dayOfWeekandHours.StartHour;
                int endHour = dayOfWeekandHours.EndHour;
                if (startHour >= endHour || startHour > 24 || startHour < 0 || endHour > 24 || endHour < 0)
                    return true;           
            }
            return false;
        }
        private int convertDayOfWeekFromStringToint(string dayOfWeek)
        {
            int dayOfWeekInt;
            switch (dayOfWeek)
            {
                case "sunday":
                    dayOfWeekInt = 0;
                    break;
                case "monday":
                    dayOfWeekInt = 1;
                    break;
                case "tuesday":
                    dayOfWeekInt = 2;
                    break;
                case "wednesday":
                    dayOfWeekInt = 3;
                    break;
                case "thursday":
                    dayOfWeekInt = 4;
                    break;
                case "friday":
                    dayOfWeekInt = 5;
                    break;
                default:
                    dayOfWeekInt = 6;
                    break;
            }
            return dayOfWeekInt;
        }
    }
}
