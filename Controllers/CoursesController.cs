using AutoMapper;
using CourseManagementSystem.Models;
using CourseManagementSystem.Models.Dtos;
using CourseManagementSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private EFCourseRepository courseRepository;
        private EFLessonRepository lessonRepository;
        private IMapper _mapper;

        public CoursesController(EFCourseRepository courseRepo, EFLessonRepository lessonRepo, IMapper mapper)
        {
            courseRepository = courseRepo;
            lessonRepository = lessonRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseDto>>> GetAllCourses()
        {
            var courses = await courseRepository.GetAllCoursesAsync();
            if (courses == null)
            {
                return NotFound();
            }
            return _mapper.Map < List<CourseDto>>(courses);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<CourseDto>> GetCourseByName(string name)
        {
            var course = await courseRepository.GetCourseByNameAsync(name);
            if (course == null)
            {
                return NotFound();
            }
            return _mapper.Map<CourseDto>(course);
        }
        [HttpGet("course/{courseName}")]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudentsOfCourse(string courseName)
        {
            var listStudentCourses = await courseRepository.GetStudentCoursesByCourseNameWithDetailsAsync(courseName);
            if (listStudentCourses == null)
            {
                return NotFound();
            }
           
            return _mapper.Map<List<StudentDto>>(listStudentCourses);
        }

        [HttpGet("lessons/all")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllLessons()
        {
            var lessons = await lessonRepository.GetAllLessonsAsync();
            if (lessons == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<LessonDto>>(lessons);
        }
        [HttpGet("lessons/course/{courseName}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsOfCourse(string courseName)
        {
            var lessons = await lessonRepository.GetCourseLessonsAsync(courseName);
            if (lessons == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<LessonDto>>(lessons);
        }       
        [HttpPost]
        [Authorize(Roles = "professor")]
        public async Task<ActionResult<CourseWithDatesDto>> NewCourse(CourseWithDatesDto courseWithDatesDto)
        {
            try
            {
                Course existedCourse = await courseRepository.GetCourseByNameAsync(courseWithDatesDto.Name);
                if (existedCourse == null)
                {
                    Course course = _mapper.Map<Course>(courseWithDatesDto);
                    List<Lesson> lessons = await courseRepository.SetDaysOfCourseAsync(course, courseWithDatesDto.DaysOfWeekAndHours);
                    return Ok();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error:" + ex.Message);
            }
        }
    }
}
