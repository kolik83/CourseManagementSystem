using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseManagementSystem.Models;
using CourseManagementSystem.Models.Dtos;

namespace CourseManagementSystem.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<StudentCourse, StudentDto>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Student.Name));
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseWithDatesDto, Course>();
            CreateMap<Course, CourseWithDatesDto>();
            CreateMap<Lesson, LessonDto>().ForMember(dest => dest.CourseName,
                opt => opt.MapFrom(src =>src.Course.Name));
            CreateMap<LessonDto, Lesson>();
            CreateMap<AttendanceDto, Attendance>();
            CreateMap<Attendance, AttendanceDto>().ForMember(dest => dest.StudentName,
                opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.lessonTime, opt => opt.MapFrom(src => src.Lesson.Date));
            CreateMap<StudentCourse, StudentCourseDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest =>dest.Course, opt => opt.MapFrom(src => src.Course.Name)); 
        }
    }
}
