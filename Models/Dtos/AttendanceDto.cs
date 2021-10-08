using System;

namespace CourseManagementSystem.Models.Dtos
{
    public class AttendanceDto
    {
        public string StudentName { get; set; }
        
        public long LessonId { get; set; }

        public bool Attended { get; set; }

        public DateTime lessonTime { get; set; }
        public string Reason { get; set; }

    }
}
