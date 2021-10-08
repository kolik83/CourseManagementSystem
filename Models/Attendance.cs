
namespace CourseManagementSystem.Models
{
    public class Attendance
    {
        public long AttendanceId { get; set; }
        
        public long StudentId { get; set; }
        public Student Student { get; set; }
        
        public long LessonId { get; set; }
        public Lesson Lesson { get; set; }

        //public DateTime DateTime { get; set; }
        public bool Attended { get; set; }
        public string Reason { get; set; }
    }
}
