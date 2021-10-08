using AutoMapper;
using CourseManagementSystem.Models.Dtos;
using CourseManagementSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "professor")]
    public class ProfessorsController : ControllerBase
    {
        private EFProfessorRepository professorRepository;
        private IMapper _mapper;
        public ProfessorsController(EFProfessorRepository professorRepo, IMapper mapper)
        {
            professorRepository = professorRepo;
            _mapper = mapper;
        }

        [HttpPost("enrol")]
        public async Task<ActionResult<StudentCourseDto>> EnrolStudentInCourse(StudentCourseDto scDto)
        {
            var result =  _mapper.Map<StudentCourseDto>(await professorRepository.EnrolStudentInCourseAsync(scDto.Student, scDto.Course));
            if (result != null)
                return result;
            return  NotFound();
        }

        [HttpPost("unsubscribe")]
        public async Task<ActionResult<StudentCourseDto>> UnsubscribeStudentFromCourse(StudentCourseDto scDto)
        {
            var result = _mapper.Map<StudentCourseDto>( await professorRepository.UnsubscribeStudentFromCourseAsync(scDto.Student, scDto.Course ));
            if (result != null)
                return result;
            return NotFound();
        }
        [HttpGet("attendance/lesson/{lessonId}")]
        public async Task<ActionResult<List<AttendanceDto>>> GetAttendanceOfLesson(int lessonId)
        {
            var result = await professorRepository.GetAttendanceOfLesson(lessonId);
            if (result != null)
                return _mapper.Map<List<AttendanceDto>>(result);
            return NotFound();
        }
    }
}
