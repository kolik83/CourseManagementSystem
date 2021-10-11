using AutoMapper;
using CourseManagementSystem.Models;
using CourseManagementSystem.Models.Dtos;
using CourseManagementSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {

        private readonly EFStudentRepository _studentRepository;
        private readonly EFAttendanceRepository _attendanceRepository;
        private readonly EFCourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public StudentsController(EFStudentRepository studentRepo, EFAttendanceRepository attendanceRepo,
            EFCourseRepository courseRepo, IMapper mapper)
        {
            _studentRepository = studentRepo;
            _attendanceRepository = attendanceRepo;
            _courseRepository = courseRepo;
            _mapper = mapper;
        }
       

        [HttpGet("name/{name}")]
        public async Task<ActionResult<StudentDto>> GetStudentByName(string name)
        {
            if (HttpContext.User.Identity.Name == name.Replace(" ", String.Empty).ToLower() || HttpContext.User.IsInRole("professor"))
            {
                var student = await _studentRepository.GetStudentWithDetailsAsync(name);
                if (student == null)
                {
                    return NotFound();
                }
                return _mapper.Map<StudentDto>(student);
            }
            return NotFound();
        }

        [HttpGet("attendance/{studentName}/{courseName}")]
        public async Task<ActionResult<List<AttendanceDto>>> GetAllAttendancesOfStudentForCourse(string studentName, string courseName)
        {
            if (HttpContext.User.Identity.Name == studentName.Replace(" ", String.Empty).ToLower() || HttpContext.User.IsInRole("professor"))
            {
                Student student = await _studentRepository.GetStudentWithDetailsAsync(studentName);
                if (student == null)
                {
                    return NotFound();
                }
                Course course = await _courseRepository.GetCourseWithDetailsAsync(courseName);
                if (course == null)
                    return NotFound();
                List<Attendance> attendances = await _attendanceRepository.GetAllAttendancesOfStudentInCourse(student.StudentId, course);
                return _mapper.Map<List<AttendanceDto>>(attendances);
            }
            return NotFound();
        }
        [HttpGet("name_courses/{name}")]
        public async Task<ActionResult<Student>> GetStudentByNameWithCourses(string name)
        {
            if(HttpContext.User.Identity.Name == name.Replace(" ", String.Empty).ToLower() || HttpContext.User.IsInRole("professor"))
                return await _studentRepository.GetStudentWithDetailsAsync(name);
            return NotFound();
        }

        [HttpPost("attendance")]
        public async Task<ActionResult<AttendanceDto>> AttendLesson(AttendanceDto attendanceDto)
        {
            if(HttpContext.User.Identity.Name == attendanceDto.StudentName.Replace(" ", String.Empty).ToLower())
                return _mapper.Map<AttendanceDto>(await _studentRepository.AttendLessonAsync(attendanceDto));
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "professor")]
        public async Task<ActionResult<StudentDto>> NewStudent(UserDto userDto)
        {

            Student newStudent = await _studentRepository.NewStudent(userDto);
            if (newStudent != null)
            {
                return new StudentDto { Name = newStudent.Name };
            }
            return NotFound();
        }

        [HttpPost("delete-student")]
        [Authorize(Roles = "professor")]
        public async Task<ActionResult<StudentDto>> RemoveStudent(StudentDto studentDto)
        { 
            var result = await _studentRepository.DeleteStudent(studentDto);
            if (result != null)
                return  result;
            return NotFound();
        }
    }
}
